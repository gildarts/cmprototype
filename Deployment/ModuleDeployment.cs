using System;
using System.Collections.Generic;
using System.Text;
using IO = System.IO;
using Path = System.IO.Path;
using System.Net;
using System.ComponentModel;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleDeployment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="folder"></param>
        /// <param name="buildName"></param>
        public ModuleDeployment(ModuleUrl url, DeployFolder folder, string buildName)
        {
            ModuleUrl = url;
            DeployFolder = folder;
            DeployBuildName = buildName;

            //Background = new BackgroundWorker();
            //Background.DoWork += new DoWorkEventHandler(Background_DoWork);
            //Background.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Background_RunWorkerCompleted);
        }

        /// <summary>
        /// 取得是否需要更新。
        /// </summary>
        public bool IsUpdateRequired
        {
            get
            {
                Description = ModuleDescription.Load(ModuleUrl); //Web
                Build = Description.GetBuild(DeployBuildName);
                ServerManifest = GetServerManifest(); //Web
                TargetFolder = DecideTargetFolder();

                LocalManifest = GetLocalManifest(); //IO

                //將 Client 的檔案清單建立成 Dictionary。
                LocalFileSet = new FileDictionary(LocalManifest.Files);

                //建立要下載的檔案清單。
                FileCollection updatelist = new FileCollection();
                foreach (File each in ServerManifest.Files)
                {
                    if (!LocalFileSet.ContainsKey(each.FullName)) //Local 沒有指定的檔案，就加入下載清單中。
                        updatelist.Add(each);
                    else
                    {//Loacl 有，但是 Hash 不合的，也加入下載清單中。
                        File localFile = LocalFileSet[each.FullName];
                        if (each.Hash != localFile.Hash)
                            updatelist.Add(each);
                        else
                        {
                            if (!System.IO.File.Exists(Path.Combine(TargetFolder, each.FullName)))
                                updatelist.Add(each);
                        }
                    }
                }

                return updatelist.Count > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Deploy()
        {
            FileCollection updatelist = null;
            DeployCompleteEventArgs eventargs = new DeployCompleteEventArgs();

            try
            {
                Description = ModuleDescription.Load(ModuleUrl); //Web
                Build = Description.GetBuild(DeployBuildName);
                ServerManifest = GetServerManifest(); //Web
                TargetFolder = DecideTargetFolder();

                if (!IO.Directory.Exists(TargetFolder)) //IO
                    IO.Directory.CreateDirectory(TargetFolder);

                LocalManifest = GetLocalManifest(); //IO

                TempFolder = Path.Combine(TargetFolder, "_update_temporal");

                try
                {
                    if (IO.Directory.Exists(TempFolder)) //IO
                        IO.Directory.Delete(TempFolder, true);

                    IO.Directory.CreateDirectory(TempFolder);
                }
                catch (Exception) { }

                //將 Client 的檔案清單建立成 Dictionary。
                LocalFileSet = new FileDictionary(LocalManifest.Files);

                //建立要下載的檔案清單。
                updatelist = new FileCollection();

                foreach (File each in ServerManifest.Files)
                {
                    if (!LocalFileSet.ContainsKey(each.FullName)) //Local 沒有指定的檔案，就加入下載清單中。
                        updatelist.Add(each);
                    else
                    {//Loacl 有，但是 Hash 不合的，也加入下載清單中。
                        File localFile = LocalFileSet[each.FullName];
                        if (each.Hash != localFile.Hash)
                            updatelist.Add(each);
                        else
                        {
                            if (!System.IO.File.Exists(Path.Combine(TargetFolder, each.FullName)))
                                updatelist.Add(each);
                        }
                    }
                }

                DownloadQueue loadqueue = new DownloadQueue(GetDownloadBasePath());
                loadqueue.Enqueue(updatelist);
                loadqueue.Start(DownloadProgress);
                loadqueue.CompleteEvent += delegate
                {
                    if (loadqueue.FailureFile == null)
                    {
                        #region 更新
                        try
                        {
                            eventargs.Files = updatelist; //下載完成的檔案清單。
                            List<string> deletelist = new List<string>();

                            //建立要刪除的檔案清單。
                            foreach (File each in eventargs.Files)
                            {
                                if (LocalFileSet.ContainsKey(each.FullName)) //先把檔案刪除，因為要更新成新的版本。
                                    deletelist.Add(Path.Combine(TargetFolder, each.FullName));
                            }

                            FileDictionary ServerFileSet = new FileDictionary(ServerManifest.Files);

                            foreach (File each in LocalFileSet.Values)
                            {
                                if (!ServerFileSet.ContainsKey(each.FullName)) //Client 前版存在，但是此版不存在的要刪除。
                                    deletelist.Add(Path.Combine(TargetFolder, each.FullName));
                            }

                            //因為檔案可能在執行，所以將「待刪除」檔案移動到暫存資料夾，下次啟動程式時再刪除。
                            foreach (string each in deletelist)
                            {
                                if (IO.File.Exists(each))
                                {
                                    if (!IO.Directory.Exists(TempFolder))
                                        IO.Directory.CreateDirectory(TempFolder);

                                    IO.File.Move(each, Path.Combine(TempFolder, Path.GetRandomFileName() + Environment.TickCount.ToString()));
                                }
                            }

                            //將下載好的新檔案，放到指定位置。
                            foreach (File each in eventargs.Files)
                            {
                                string fullname = Path.Combine(TargetFolder, each.FullName);

                                //if (IO.File.Exists(fullname))
                                //{
                                //    string msg = string.Format("磁碟上包含了 Manifest 沒有描述的檔案，新版本的模組也包含了相同的檔案。({0})", fullname);
                                //    throw new InvalidOperationException(msg);
                                //}

                                if (!IO.Directory.Exists(Path.Combine(TargetFolder, each.Folder)))
                                    IO.Directory.CreateDirectory(Path.Combine(TargetFolder, each.Folder));

                                IO.FileStream diskfile = new IO.FileStream(fullname, IO.FileMode.Create);
                                IO.Stream newfile = each.Data;

                                int buffersize = 1024, readcount = 0;
                                byte[] buffer = new byte[buffersize];
                                while ((readcount = newfile.Read(buffer, 0, buffersize)) > 0)
                                    diskfile.Write(buffer, 0, readcount);

                                newfile.Close();
                                diskfile.Close();
                            }

                            GC.Collect();
                            ServerManifest.Save(Path.Combine(TargetFolder, Consts.ManifestFN));
                            Description.Save(Path.Combine(TargetFolder, Consts.DeployFN));

                            //將 RawUrl 儲存於各模組的目錄中。
                            System.IO.File.WriteAllText(Path.Combine(TargetFolder, Consts.UrlSourceFN), ModuleUrl.RawUrl, Encoding.UTF8);

                            eventargs.ModuleUrl = ModuleUrl.Url;
                            eventargs.InstallFolder = TargetFolder;
                            eventargs.Manifest = ServerManifest;
                            eventargs.Description = Description;

                            eventargs.Success = true;
                        }
                        catch (Exception ex)
                        {
                            eventargs.Success = false;
                            eventargs.Error = ex;
                        }

                        if (DeployComplete != null)
                            DeployComplete(this, eventargs);

                        try
                        {
                            IO.Directory.Delete(TempFolder);
                        }
                        catch { }

                        #endregion
                    }
                    else
                    {
                        eventargs.Error = new FileDownloadException("部份檔案下載失敗，模組更新中止。", loadqueue.FailureFile.Value.Key, loadqueue.FailureFile.Value.Value);
                        eventargs.Success = false;

                        if (DeployComplete != null)
                            DeployComplete(this, eventargs);
                    }
                };
            }
            catch (Exception ex)
            {
                eventargs.Error = ex;
                eventargs.Success = false;

                if (DeployComplete != null)
                    DeployComplete(this, eventargs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeployCompleteEventArgs> DeployComplete;

        private BackgroundWorker Background { get; set; }

        private DeployFolder DeployFolder { get; set; }

        /// <summary>
        /// 真正要存放檔案的路徑。
        /// </summary>
        private string TargetFolder { get; set; }

        /// <summary>
        /// 待刪除檔案的暫存目錄。
        /// </summary>
        private string TempFolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ModuleUrl ModuleUrl { get; set; }

        private ModuleDescription Description { get; set; }

        private string DeployBuildName { get; set; }

        private ModuleBuild Build { get; set; }

        private BuildManifest ServerManifest { get; set; }

        private BuildManifest LocalManifest { get; set; }

        /// <summary>
        /// 描述在 Local  Manifest 中的檔案清單。
        /// </summary>
        private FileDictionary LocalFileSet { get; set; }

        /// <summary>
        /// 取得或設定檔案下載的進度回報介面，注意，回報執行緒將不會是主執行緒，在 UI 顯示上需特別注意。
        /// </summary>
        public IProgressReceiver DownloadProgress { get; set; }

        /// <summary>
        /// 取得是否正在更新中。
        /// </summary>
        public bool IsWorking { get { return Background.IsBusy; } }

        /// <summary>
        /// 取得下載檔案的基礎路徑，通常是 Server Manifeat 載入位置的同一層級。
        /// </summary>
        private string GetDownloadBasePath()
        {
            return ServerManifest.Url.Replace(Consts.ManifestFN, "");
        }

        private string DecideTargetFolder()
        {
            if (DeployFolder.Type == FolderType.TargetFolder)
                return DeployFolder.Location;
            else
            {
                string modfolder;

                if (string.IsNullOrEmpty(Description.DeployFolder))
                    modfolder = Path.Combine(DeployFolder.Location, ModuleUrl.DefaultDeployFolder);
                else
                    modfolder = Path.Combine(DeployFolder.Location, Description.DeployFolder);

                return modfolder;
            }
        }

        private BuildManifest GetLocalManifest()
        {
            BuildManifest localManifest = BuildManifest.Null;
            try
            {
                return BuildManifest.Load(Path.Combine(TargetFolder, Consts.ManifestFN));
            }
            catch (IO.FileNotFoundException)
            {
                return localManifest;
            }
            catch (IO.DirectoryNotFoundException)
            {
                return localManifest;
            }
        }

        private BuildManifest GetServerManifest()
        {
            string manifestUrl = ModuleUrl.GetModuleManifestUrl(Build);

            try
            {
                //ModuleManifest 必須要存在。
                return BuildManifest.Load(manifestUrl);
            }
            catch (WebException ex)
            {
                HttpWebResponse rsp = ex.Response as HttpWebResponse;

                //只有在 Http Status 是 NotFound 時才合理，其他的如主機不對、Prefix 不對，都必需要產生 Exception 告訴使用者。
                //只有 Release 的 Build 才會試著找 Null 的 Build，其他的 Build 失敗就失敗了。
                if (rsp != null &&
                    rsp.StatusCode == HttpStatusCode.NotFound &&
                    Build.Name.ToLower() == Consts.ReleaseBuild)
                {
                    try
                    {
                        //嘗試載入 Null Build 的 ModuleManifest，載入成功回傳結果，失敗就產生 Exception。
                        return BuildManifest.Load(ModuleUrl.GetModuleManifestUrl(ModuleBuild.Null));
                    }
                    catch (Exception)
                    {
                        //Console.Write(ex1.Message);
                    }
                }

                throw ex;
            }
        }
    }
}
