using System;
using System.Collections.Generic;
using System.Text;
using IO = System.IO;
using Path = System.IO.Path;
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace FISCA.Deployment
{
    internal class DownloadQueue
    {
        public DownloadQueue(string baseUrl)
        {
            Files = new FileCollection();
            BaseUrl = baseUrl;

            CompleteTimer = new System.Windows.Forms.Timer();
            CompleteTimer.Tick += new EventHandler(CompleteTimer_Tick);
            CompleteTimer.Interval = 200;
        }

        private string BaseUrl { get; set; }

        private FileCollection Files { get; set; }

        public KeyValuePair<File, Exception>? FailureFile { get; set; }

        private ManualResetEvent WaitHandler { get; set; }

        private int CurrentReadCount { get; set; }

        private HttpWebRequest CurrentRequest { get; set; }

        private IProgressReceiver Progress { get; set; }

        private File CurrentFile { get; set; }

        private System.Windows.Forms.Timer CompleteTimer;
        private bool CompleteFlag = false;
        internal event EventHandler CompleteEvent;

        public void Enqueue(FileCollection files)
        {
            Files.AddRange(files);
        }

        public void Start()
        {
            Start(null);
        }

        public void Start(IProgressReceiver progress)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BackgroundRun), progress);
            CompleteTimer.Enabled = true;
        }

        private void CompleteTimer_Tick(object sender, EventArgs e)
        {
            if (CompleteFlag)
            {
                CompleteFlag = false;
                CompleteTimer.Enabled = false;

                if (CompleteEvent != null)
                    CompleteEvent(this, EventArgs.Empty);
            }
        }

        private void BackgroundRun(object state)
        {
            Progress = state as IProgressReceiver;
            FailureFile = null;

            if (Progress == null)
                Progress = new EmptyProgressReceiver();

            Progress.ProgressStart(Files.TotalSize);

            WaitHandler = new ManualResetEvent(true);
            CurrentReadCount = 0;

            foreach (File each in Files)
            {
                CurrentFile = each;

                WaitHandler.Reset();
                string fileurl = Path.Combine(BaseUrl, CurrentFile.FullName);
                fileurl = fileurl.Replace("\\", "/");
                CurrentRequest = WebRequest.Create(fileurl) as HttpWebRequest;
                CurrentRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                CurrentRequest.AllowWriteStreamBuffering = false;

                CurrentRequest.BeginGetResponse(new AsyncCallback(Callback), null);
                WaitHandler.WaitOne();

                //有檔案下載失敗，就不繼續下載了。
                if (FailureFile != null) break;
            }

            CompleteFlag = true;
            Progress.ProgressEnd();
        }

        private void Callback(IAsyncResult ar)
        {
            try
            {
                HttpWebResponse response = CurrentRequest.EndGetResponse(ar) as HttpWebResponse;

                IO.Stream stream = response.GetResponseStream();
                IO.MemoryStream newstream = new System.IO.MemoryStream();

                int buffersize = 10240, readcount = 0;
                byte[] buffer = new byte[buffersize];

                while ((readcount = stream.Read(buffer, 0, buffersize)) > 0)
                {
                    newstream.Write(buffer, 0, readcount);

                    CurrentReadCount += readcount;
                    Progress.ProgressStep(CurrentReadCount);
                }

                newstream.Seek(0, System.IO.SeekOrigin.Begin);
                CurrentFile.Data = newstream;

                if (!CurrentFile.CheckHash())
                    throw new Exception("已下載的檔案與主機上 manifest 中描述之雜湊不一致。");
            }
            catch (Exception ex)
            {
                FailureFile = new KeyValuePair<File, Exception>(CurrentFile, ex);
            }
            finally
            {
                WaitHandler.Set();
            }
        }
    }
}
