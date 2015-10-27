using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace FISCA
{
    class AssemblyCenter
    {
        private string[] _base_folders;

        public AssemblyCenter(bool developmentMode)
        {
            DevelopmentMode = developmentMode;
            if (DevelopmentMode)
                _base_folders = new string[] { Paths.Executable };
            else
                _base_folders = new string[] { Paths.Module };

            Assemblies = new Dictionary<string, string>();

            Wait = new ManualResetEvent(true);
        }

        private bool DevelopmentMode
        {
            get;
            set;
        }

        private void LoadAllAssembly()
        {
            Wait.Reset();
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadAssemblies), _base_folders);
        }

        private Dictionary<string, string> Assemblies { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void LoadAssemblies(object state)
        {
            Trace.WriteLine(string.Format("目前工作目錄：{0}", Environment.CurrentDirectory));
            foreach (string path in (string[])state)
                Trace.WriteLine(string.Format("搜尋組件：{0}", path));

            foreach (string path in (string[])state)
            {
                foreach (string each in AllAssemblyFile(path))
                {
                    try
                    {
                        if (Assemblies.ContainsValue(each)) continue;

                        //讀取組件名稱。 
                        string asmname = AssemblyName.GetAssemblyName(each).FullName;

                        if (!Assemblies.ContainsKey(asmname))
                            Assemblies.Add(asmname, each);

                        Trace.WriteLine(string.Format("Found Assembly：{0} ({1})", each, asmname));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            AllAssemblies = new List<string>(Assemblies.Keys);

            Wait.Set();
        }

        private string[] AllAssemblyFile(string path)
        {
            List<string> files = new List<string>();
            if (Directory.Exists(path))
            {
                if (DevelopmentMode)
                {
                    files.AddRange(Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories));
                    files.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories));
                }
                else
                    GetValidAssemblies(path, files);
            }

            return files.ToArray();
        }

        private void GetValidAssemblies(string path, List<string> files)
        {
            if (Directory.Exists(path))
            {
                if (InstalledModules.Contains(GetDeploySourceUrl(path)))
                {
                    files.AddRange(Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly));
                    files.AddRange(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly));
                }

                foreach (string each in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
                    GetValidAssemblies(each, files);
            }
        }

        private string GetDeploySourceUrl(string path)
        {
            string deploysource = string.Empty;

            try
            {
                deploysource = File.ReadAllText(Path.Combine(path, "deploy.source"));
            }
            catch { }

            return deploysource;
        }

        internal List<string> InstalledModules { get; set; }
        private void GetInstalledModules()
        {
            InstalledModules = new List<string>();
            FISCA.DSAUtil.DSXmlHelper request = new FISCA.DSAUtil.DSXmlHelper("Request");
            request.AddElement("Attributes");
            request.AddElement("Condition");

            FISCA.DSAUtil.DSResponse response =
                FISCA.Authentication.DSAServices.CallService("SmartSchool.Module.GetModule",
                new FISCA.DSAUtil.DSRequest(request));

            //下載模組清單。
            FISCA.DSAUtil.DSXmlHelper rsp = response.GetContent();
            foreach (System.Xml.XmlElement each in rsp.GetElements("Module"))
            {
                string moduleUrl = each.SelectSingleNode("ModuleUrl").InnerText;
                InstalledModules.Add(moduleUrl);
            }
        }

        public Assembly Get(string name)
        {
            Wait.WaitOne();

            AssemblyName rName = new AssemblyName(name);
            Assembly result = null;

            //先查詢記憶體中的組件。
            result = SearchAssembly(rName, AppDomain.CurrentDomain.GetAssemblies());

            if (result != null) return result;

            //再到目錄中找。
            if (Assemblies.ContainsKey(name))
                return Assembly.LoadFrom(Assemblies[name]);

            return SearchAssembly(rName, Assemblies);
        }

        private Assembly SearchAssembly(AssemblyName rName, IEnumerable<Assembly> searchBase)
        {
            Assembly result = null;

            foreach (Assembly each in searchBase)
            {
                AssemblyName dName = each.GetName();

                if (dName.Name == rName.Name)
                    result = each;

                if (result != null)
                {
                    if (result.GetName().FullName == rName.FullName)
                        return result;
                }
            }
            return result;
        }

        private Assembly SearchAssembly(AssemblyName rName, Dictionary<string, string> searchBase)
        {
            KeyValuePair<string, string>? result = null;
            foreach (KeyValuePair<string, string> each in searchBase)
            {
                AssemblyName dName = new AssemblyName(each.Key);

                if (dName.Name == rName.Name)
                    result = each;

                if (result != null)
                {
                    if (result.Value.Key == rName.FullName)
                        break;
                }
            }

            if (result != null)
                return Assembly.LoadFrom(result.Value.Value);
            else
                return null;
        }

        public List<string> AllAssemblies { get; private set; }

        private ManualResetEvent Wait { get; set; }

        /// <summary>
        /// 此函數會搜尋所有模組，並載入記憶體中。
        /// </summary>
        public void CacheAssembly()
        {
            GetInstalledModules();
            LoadAllAssembly();
            Wait.WaitOne();
        }
    }
}
