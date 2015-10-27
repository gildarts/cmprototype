using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    /// <summary>
    /// 代表 HTTP 的連線資訊。
    /// </summary>
    struct UrlData
    {
        public UrlData(string userName, string password, string url)
            : this()
        {
            UserName = userName;
            Password = password;
            Url = url;
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Url { get; private set; }
    }
}
