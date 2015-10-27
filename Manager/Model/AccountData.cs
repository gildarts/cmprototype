using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Win = System.Windows.Forms;

namespace Manager
{
    /// <summary>
    /// 代表資料庫帳號。
    /// </summary>
    class AccountData
    {
        /// <summary>
        /// 代表預設的 DBAccount，不特別指定 DB 的帳號資訊。
        /// </summary>
        public static AccountData Default { get; private set; }

        static AccountData()
        {
            Default = new AccountData(string.Empty, string.Empty);
        }

        public AccountData(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("UserName:{0}\nPassword:{1}", UserName, Password);
        }
    }
}
