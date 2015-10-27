using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 代表要傳入 Module 的參數集合。
    /// </summary>
    public class DeployParameters : IEnumerable<string>
    {
        private Dictionary<string, string> Params;

        /// <summary>
        /// DeployParameters 建構式。
        /// </summary>
        public DeployParameters()
        {
        }

        /// <summary>
        /// DeployParameters 建構式。
        /// </summary>
        public DeployParameters(string url)
        {
            Params = new Dictionary<string, string>();

            if (url.IndexOf('#') >= 0)
            {
                string[] urlsplit = url.Split('#');

                if (urlsplit.Length >= 2)
                {
                    string ps = urlsplit[1];
                    foreach (string eachParam in ps.Split('&'))
                    {
                        string[] pair = eachParam.Split('=');
                        Params.Add(pair[0], (pair.Length >= 2) ? pair[1] : "");
                    }
                }
            }
        }

        /// <summary>
        /// 取得參數值，如果 Key 不存在會回傳 string.Empty。
        /// </summary>
        /// <param name="paramName">參數名稱。</param>
        /// <returns></returns>
        public string this[string paramName]
        {
            get
            {
                if (Params.ContainsKey(paramName))
                    return Params[paramName];
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 取得是否包含了指定的參數。
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public bool Contains(string paramName)
        {
            return Params.ContainsKey(paramName);
        }

        /// <summary>
        /// 參數集合的數量。
        /// </summary>
        public int Count { get { return Params.Count; } }

        internal void Add(string paramName, string value)
        {
            Params.Add(paramName, value);
        }

        #region IEnumerable<string> 成員

        public IEnumerator<string> GetEnumerator()
        {
            return Params.Keys.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Params.Keys.GetEnumerator();
        }

        #endregion
    }
}
