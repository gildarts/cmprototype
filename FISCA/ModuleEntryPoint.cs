using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace FISCA
{
    internal class ModuleEntryPoint
    {
        public ModuleEntryPoint()
        {
            Dependencies = new List<string>();
            InvokeFail = false;
            InvokeCalled = false;
            ModuleDescription = null;
            BuildManifest = null;
            _assemlby = null;
        }

        public string EntryName { get; internal set; }

        public XmlElement ModuleDescription { get; internal set; }

        public XmlElement BuildManifest { get; internal set; }

        private Assembly _assemlby;
        internal Assembly Assembly
        {
            get { return _assemlby; }
            set
            {
                _assemlby = value;

                BuildManifest = LoadXmlFile("build.xml");
                ModuleDescription = LoadXmlFile("module.xml");
            }
        }

        private XmlElement LoadXmlFile(string p)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(new FileInfo(Assembly.Location).DirectoryName, p));
                return doc.DocumentElement;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("載入「{0}」檔案失敗。\n{1}\n", p, ex.Message));
                return null;
            }
        }

        public MethodInfo MainMethod { get; internal set; }

        public MainMethodAttribute MainType { get; internal set; }

        public List<string> Dependencies { get; internal set; }

        /// <summary>
        /// Invoke 是否被呼叫過。
        /// </summary>
        public bool InvokeCalled { get; private set; }

        public bool InvokeFail { get; internal set; }

        public string GlobalIdentify
        {
            get
            {
                return string.Format("{0}:{1}", Assembly.GetName().FullName, EntryName);
            }
        }

        public void Invoke()
        {
            InvokeCalled = true;

            try
            {
                MainMethod.Invoke(null, null);

                Trace.WriteLine(string.Format("啟動模組：{0}, 進入點名稱：{1}", Assembly.GetName().FullName, EntryName));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("錯誤，啟動模組：{0}, 進入點名稱：{1}", Assembly.GetName().FullName, EntryName));
                throw new ModuleLoadingException("呼叫模組進入點發生例外狀況，詳細資訊請檢查 InnerException 資訊。", Assembly.GetName().FullName, ex);
            }
        }

        #region Static Methods

        public static List<ModuleEntryPoint> ScanEntryPoint(IEnumerable<string> assemblyStrings, AssemblyCenter resolver)
        {
            List<ModuleEntryPoint> points = new List<ModuleEntryPoint>();
            Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

            foreach (string each in assemblyStrings)
            {
                //如果同樣的組件已經載入，就不載入了。
                if (assemblies.ContainsKey(each))
                    continue;

                Assembly asm = resolver.Get(each);

                //resolver.Get 可能會取得到別版的組件，也可能跟之前的已經入的組件相同。
                if (assemblies.ContainsKey(asm.FullName))
                    continue;

                if (asm == null)
                    throw new ModuleLoadingException(string.Format("找不到模組指定的組件「{0}」。", each), each, null);

                Type[] exptypes = null;

                try
                {
                    exptypes = asm.GetExportedTypes();
                }
                catch (Exception)
                {
                    continue;
                }

                assemblies.Add(each, asm);

                foreach (Type eacht in exptypes)
                {
                    foreach (MethodInfo eachMethod in eacht.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
                    {
                        object[] main = eachMethod.GetCustomAttributes(typeof(MainMethodAttribute), false);
                        object[] dependencies = eachMethod.GetCustomAttributes(typeof(DependencyAttribute), false);

                        if (main.Length > 0)
                        {
                            MainMethodAttribute mma = main[0] as MainMethodAttribute;
                            ModuleEntryPoint mep = new ModuleEntryPoint();

                            mep.Assembly = asm;
                            mep.MainMethod = eachMethod;
                            mep.EntryName = mma.Name;
                            mep.MainType = mma;

                            foreach (DependencyAttribute eachd in dependencies)
                                mep.Dependencies.Add(eachd.MainName);

                            points.Add(mep);
                        }
                    }
                }
            }

            return points;
        }

        private static IEnumerable<string> AllAssemblyFiles(string baseFolder)
        {
            List<string> files = new List<string>();

            files.AddRange(Directory.GetFiles(baseFolder, "*.dll", SearchOption.AllDirectories));
            files.AddRange(Directory.GetFiles(baseFolder, "*.exe", SearchOption.AllDirectories));

            return files.ToArray();
        }

        #endregion

    }
}
