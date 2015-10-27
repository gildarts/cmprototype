using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 負責非同步傳送申請文件。
    /// </summary>
    internal class AsyncPostRequest
    {
        private AsyncSendContext _config;
        private byte[] _sendContent;
        private bool _reportToUIThread;

        private ManualResetEvent sendDone = new ManualResetEvent(false);

        public AsyncPostRequest(AsyncSendContext config)
        {
            _config = config;

            _sendContent = Encoding.UTF8.GetBytes(config.Request.GetRawXml());

            //如果 AnyUIControl 屬性不是 Null，代表要使用 UI Thread 回報進度。
            _reportToUIThread = config.AnyUIControl != null;
        }

        public void StartAsync()
        {
            if (_config.ReceiveCompleteDelegate == null)
                throw new ArgumentNullException("ReceiveComplete", "您必須指定 ReceiveComplete 事件。");

            Thread async = new Thread(new ThreadStart(AsyncProcessRequest));
            async.IsBackground = true;

            async.Start();
        }

        private void AsyncProcessRequest()
        {
            HttpWebRequest httpRequest = PrepareHttpRequest();
            httpRequest.ContentLength = _sendContent.Length; //這一定要設定。

            AsyncState state = new AsyncState(httpRequest);
            httpRequest.BeginGetRequestStream(new AsyncCallback(AsyncSendRequest), state);

            sendDone.WaitOne();

            if (state.ProgressInfo.Success)
                httpRequest.BeginGetResponse(new AsyncCallback(AsyncReceiveRequest), state);
        }

        private void AsyncSendRequest(IAsyncResult async)
        {
            ProgressInfo progress = new ProgressInfo(_sendContent.Length, ProcessAction.Send, _config.RequestID);
            progress.Tag = _config.Tag;

            AsyncState state = async.AsyncState as AsyncState;

            //將進度資訊儲存於 state 中，以便完成時，回報狀態。
            state.ProgressInfo = progress;

            try
            {
                Stream sendStream = state.Request.EndGetRequestStream(async);

                //回報 Client 開始上傳資料。
                ReportProgress(progress, _config.SendStartDelegate);

                //決定是否要使用 Timeout
                if (ProgressTimeout <= 0)
                    sendStream.WriteTimeout = Timeout.Infinite;
                else
                    sendStream.WriteTimeout = ProgressTimeout;

                AsyncWriteStream(sendStream, progress);

                progress.Success = true; //這行很重要。
            }
            catch (Exception ex)
            {
                progress.Exception = ex;
            }
            finally
            {
                ReportProgress(progress, _config.SendCompleteDelegate);
                sendDone.Set();
            }
        }

        /// <summary>
        /// 非同將資料寫入 Stream 中。
        /// </summary>
        private void AsyncWriteStream(Stream sendStream, ProgressInfo progress)
        {
            int bufferSize = PerTimeSize, offset = 0;
            byte[] content = _sendContent;

            for (offset = 0; (offset + bufferSize) < content.Length; offset += bufferSize)
            {
                sendStream.Write(content, offset, bufferSize);

                progress.CurrentByte = offset + bufferSize;
                ReportProgress(progress, _config.SendProgressDelegate);

                if (progress.Cancel) break; //使用者取消就中斷傳送。
            }

            if (!progress.Cancel) //使用者沒有取消才能傳送剩餘資料。
            {
                if (offset <= content.Length)
                {
                    int finalByteSize = content.Length - offset;
                    sendStream.Write(content, offset, finalByteSize);

                    progress.CurrentByte = offset + finalByteSize;
                    ReportProgress(progress, _config.SendProgressDelegate);
                }
            }

            sendStream.Close();
        }

        private void AsyncReceiveRequest(IAsyncResult async)
        {
            AsyncState state = async.AsyncState as AsyncState;
            ProgressInfo progress;
            WebResponse response;

            try
            {
                response = state.Request.EndGetResponse(async);
            }
            catch (Exception ex)
            {
                progress = new ProgressInfo(0, ProcessAction.Receive, _config.RequestID);
                progress.Tag = _config.Tag;

                progress.Exception = ex;

                ReportProgress(progress, _config.ReceiveCompleteDelegate);

                return;
            }

            //檔案太大時，會出錯...約2G以上時。
            progress = new ProgressInfo(Convert.ToInt32(response.ContentLength), ProcessAction.Receive, _config.RequestID);
            progress.Tag = _config.Tag;

            //將進度資訊儲存於 state 中，以便完成時，回報狀態。
            state.ProgressInfo = progress;

            //回報 Client 開始接收資料。
            ReportProgress(progress, _config.ReceiveStartDelegate);

            try
            {
                Stream receiveStream = response.GetResponseStream();

                //決定是否要使用 Timeout
                if (ProgressTimeout <= 0)
                    receiveStream.WriteTimeout = Timeout.Infinite;
                else
                    receiveStream.WriteTimeout = ProgressTimeout;

                Stream dsrsp = AsyncReadStream(receiveStream, progress);

                DSResponse objrsp = new DSResponse();
                objrsp.Load(dsrsp, Encoding.UTF8);
                progress.Response = objrsp;

                progress.Success = true; //這行很重要。
            }
            catch (Exception ex)
            {
                progress.Exception = ex;
            }
            finally
            {
                ReportProgress(progress, _config.ReceiveCompleteDelegate);
            }
        }

        private Stream AsyncReadStream(Stream receiveStream, ProgressInfo progress)
        {
            int bufferSize = PerTimeSize, count = 0;
            byte[] buffer = new byte[bufferSize];

            MemoryStream result = new MemoryStream();

            while ((count = receiveStream.Read(buffer, 0, bufferSize)) > 0)
            {
                result.Write(buffer, 0, count);
                progress.CurrentByte = Convert.ToInt32(result.Position); //檔案太大時會出錯，約 2G 時。
                ReportProgress(progress, _config.ReceiveProgressDelegate);

                if (progress.Cancel) break;
            }

            receiveStream.Close();

            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        private void ReportProgress(ProgressInfo info, ProgressCallback reportMethod)
        {
            if (reportMethod != null)
            {
                if (_reportToUIThread)
                {
                    if (_config.AnyUIControl.InvokeRequired)
                        _config.AnyUIControl.Invoke(reportMethod, new object[] { info });
                }
                else
                {
                    reportMethod(info);
                }
            }
        }

        private HttpWebRequest PrepareHttpRequest()
        {
            HttpWebRequest request = WebRequest.Create(PhysicalTargetUrl) as HttpWebRequest;
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "text/xml";
            request.Method = "POST";

            return request as HttpWebRequest;
        }

        private string PhysicalTargetUrl
        {
            get { return _config.TargetAccessPoint.Url; }
        }

        private int PerTimeSize
        {
            get { return _config.PerTimeSize; }
        }

        private int ProgressTimeout
        {
            get { return _config.ProgressTimeout; }
        }
    }
}
