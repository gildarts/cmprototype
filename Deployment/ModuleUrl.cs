using System;
using System.Collections.Generic;
using System.Text;
using Path = System.IO.Path;

namespace FISCA.Deployment
{
    /// <summary>
    /// http://modules.ischool.com.tw/fisca/JHEvaluation
    /// http://modules.ischool.com.tw/fisca/JHEvaluation#County=KaoHsiung
    /// http://modules.ischool.com.tw/fisca/JHEvaluation#County=HsinChu&MyParam=MyValue
    /// </summary>
    public class ModuleUrl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleUrl"></param>
        public ModuleUrl(string moduleUrl)
        {
            if (string.IsNullOrEmpty(moduleUrl))
                throw new ArgumentException("模組的 Url 不可以是空串字。", "moduleUrl");

            Url = moduleUrl;
        }

        /// <summary>
        /// 取得以 ModuleUrl 為基礎的 DeployDescription URL 位置。
        /// </summary>
        public string GetDeployDescriptionUrl()
        {
            return Path.Combine(UrlAppend, Consts.DeployFN);
        }

        /// <summary>
        /// 取得以 ModuleUrl 與 Build 運算後的 ModuleManifest URL 位置，如果 Build 參數為 Null，則不會考慮 Build。
        /// </summary>
        public string GetModuleManifestUrl(ModuleBuild build)
        {
            if (build == ModuleBuild.Null)
                return UrlAppend + Consts.ManifestFN;
            else
                return UrlAppend + build.Name + "/" + Consts.ManifestFN;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultDeployFolder { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RawUrl { get; private set; }

        private string _module_url;
        /// <summary>
        /// 模組的 Url 基礎位置。
        /// </summary>
        public string Url
        {
            get { return _module_url; }
            private set
            {
                RawUrl = value;
                if (value.IndexOf('#') >= 0)
                {
                    string[] splits = value.Split('#');
                    _module_url = splits[0];
                }
                else
                    _module_url = value;

                if (Url.EndsWith("/"))
                {
                    UrlTrim = Url.Remove(Url.Length - 1);
                    UrlAppend = Url;
                }
                else
                {
                    UrlTrim = Url;
                    UrlAppend = Url + "/";
                }

                string[] tokens = UrlTrim.Split('/');
                if (tokens.Length >= 4)
                    DefaultDeployFolder = tokens[tokens.Length - 1];
            }
        }

        /// <summary>
        /// 替 Url 的結尾去除「/」字元。
        /// </summary>
        private string UrlTrim { get; set; }

        /// <summary>
        /// 替 Url 的結尾加上「/」字元。
        /// </summary>
        private string UrlAppend { get; set; }

    }
}
