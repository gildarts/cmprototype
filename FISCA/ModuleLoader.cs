using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using FISCA.PrivateControls;
using FISCA.Deployment;

namespace FISCA
{
    /// <summary>
    /// 模組載入器。
    /// </summary>
    public class ModuleLoader
    {
        private static Dictionary<Type, ModuleMetadata> _modules = new Dictionary<Type, ModuleMetadata>();

        /// <summary>
        /// 取得部署參數。
        /// </summary>
        /// <param name="searchType">要取得參數的型別。</param>
        /// <param name="defaultParamValues">預設的參數資訊。</param>
        public static DeployParameters GetDeployParametsers(Type searchType, string defaultParamValues)
        {
            string defParams = "#" + defaultParamValues;
            try
            {
                string file = new FileInfo(searchType.Assembly.CodeBase.Replace("file:///", "")).DirectoryName + "/" + Consts.UrlSourceFN;
                string rawUrl = System.IO.File.ReadAllText(file, Encoding.UTF8);

                if (rawUrl.IndexOf('#') < 0) //找不到#，代表沒有傳參數。
                    return new DeployParameters(defParams);
                else
                    return new DeployParameters(rawUrl);
            }
            catch (Exception)
            {
                return new DeployParameters(defParams);
            }
        }

        /// <summary>
        /// 取得模組的相關資訊。
        /// </summary>
        /// <param name="mainType">包含模組進入點的型別，例：GetModuleMetadata(typeof(Program));。</param>
        /// <returns>模組資訊物件。</returns>
        public static ModuleMetadata GetModuleMetadata(Type mainType)
        {
            if (_modules.ContainsKey(mainType))
                return _modules[mainType];
            else
                return null;
        }

        /// <summary>
        /// 取得已經載入到記憶體中的模組自我描述資訊。
        /// </summary>
        public static ModuleMetadata[] Modules { get { return new List<ModuleMetadata>(_modules.Values).ToArray(); } }

        internal ModuleLoader(AssemblyCenter asmcenter)
        {
            AssemblyCenter = asmcenter;
            ModuleLoadError += new EventHandler<ModuleLoadErrorArgs>(Instance_ModuleLoadError);
        }

        private void Instance_ModuleLoadError(object sender, ModuleLoadErrorArgs e)
        {
            try
            {
                string approot = System.Windows.Forms.Application.StartupPath;
                StreamWriter writer = new StreamWriter(Path.Combine(approot, "模組載入失敗訊息.txt"), true, Encoding.UTF8);

                ExceptionReport report = new ExceptionReport();
                report.AddType(typeof(System.Net.HttpWebRequest));
                report.AddType(typeof(System.Net.HttpWebResponse));

                writer.WriteLine("模組載入失敗：" + e.Module.Assembly.FullName);
                writer.WriteLine(report.Transform(e.Error));

                writer.Close();
            }
            catch { }
        }

        private AssemblyCenter AssemblyCenter { get; set; }

        internal void Load()
        {
            //掃描所有組件，找出所有的模組進入點。
            List<ModuleEntryPoint> points = ModuleEntryPoint.ScanEntryPoint(AssemblyCenter.AllAssemblies, AssemblyCenter);

            ModuleEntryPoint appmain;

            //建立具名進入點的對照表。
            Dictionary<string, ModuleEntryPoint> namedPoints = GetNamedPoints(points, out appmain);

            //啟動第一個進入點。
            if (appmain != null) Invoke(appmain);

            //先 Invoke 有名稱的進入點。
            InvokeModules(new List<ModuleEntryPoint>(namedPoints.Values), namedPoints);

            //再 Invoke 沒有名稱的進入點。
            InvokeModules(points, namedPoints);
        }

        /// <summary>
        /// 當模組載入失敗時引發。
        /// </summary>
        public event EventHandler<ModuleLoadErrorArgs> ModuleLoadError;

        /// <summary>
        /// 當模組要載入時引發。
        /// </summary>
        public event EventHandler<ModuleLoadingArgs> ModuleLoading;

        private void InvokeModules(List<ModuleEntryPoint> points, Dictionary<string, ModuleEntryPoint> lookup)
        {
            foreach (ModuleEntryPoint each in points)
            {
                if (each.InvokeCalled) continue;
                if (each.InvokeFail) continue;

                int recursive = 0;
                try
                {
                    InvokeModule(each, lookup, ref recursive);
                }
                catch (Exception ex)
                {
                    if (ModuleLoadError != null)
                        ModuleLoadError(this, new ModuleLoadErrorArgs(new ModuleMetadata(each), ex));

                    each.InvokeFail = true;
                }
                System.Windows.Forms.Application.DoEvents();
                System.Windows.Forms.Application.RaiseIdle(new EventArgs());
            }
        }

        private void InvokeModule(ModuleEntryPoint point, Dictionary<string, ModuleEntryPoint> lookup, ref int recursive)
        {
            if (point.InvokeCalled) return;

            if (recursive >= 16) throw new ModuleLoadingException("模組有循環參照，無法正確啟動模組。", string.Empty, null);

            foreach (string eachDep in point.Dependencies)
            {
                if (lookup.ContainsKey(eachDep))
                {
                    if (!lookup[eachDep].InvokeCalled)
                    {
                        recursive++;
                        InvokeModule(lookup[eachDep], lookup, ref recursive);
                    }
                }
                else
                    throw new ArgumentException("指定的相依模組不存在。", eachDep);
            }

            if (!point.InvokeCalled) Invoke(point);
        }

        private void Invoke(ModuleEntryPoint each)
        {
            try
            {
                string folder = new System.IO.FileInfo(each.MainMethod.ReflectedType.Assembly.Location).DirectoryName;
                ModuleLoadingArgs args = new ModuleLoadingArgs(folder, new ModuleMetadata(each));

                if (ModuleLoading != null) ModuleLoading(this, args);

                if (!args.Cancel)
                {
                    each.Invoke();
                    _modules.Add(each.MainMethod.ReflectedType, new ModuleMetadata(each));
                    Trace.WriteLine("Invoke:" + each.GlobalIdentify);
                }
            }
            catch (Exception ex)
            {
                if (ModuleLoadError != null)
                    ModuleLoadError(this, new ModuleLoadErrorArgs(new ModuleMetadata(each), ex));
            }
        }

        private Dictionary<string, ModuleEntryPoint> GetNamedPoints(List<ModuleEntryPoint> points, out ModuleEntryPoint appmain)
        {
            Dictionary<string, ModuleEntryPoint> lookup = new Dictionary<string, ModuleEntryPoint>();
            appmain = null;

            foreach (ModuleEntryPoint each in points)
            {
                if (each.MainType is ApplicationMainAttribute)
                {
                    //TODO 整個系統只能有一個主要進入點。
                    //if (appMain != null) throw new ArgumentException("整個系統只能有一個主要進入點。");

                    appmain = each;
                    continue;
                }

                if (!string.IsNullOrEmpty(each.EntryName))
                {
                    if (lookup.ContainsKey(each.EntryName))
                    {
                        ModuleEntryPoint other = lookup[each.EntryName];

                        string msg = string.Format("發現有相同的進入點名稱\n{0}\n{1}", each.GlobalIdentify, other.GlobalIdentify);
                        throw new ModuleLoadingException(msg, null, null);
                    }

                    lookup.Add(each.EntryName, each);
                }
            }

            return lookup;
        }
    }
}
