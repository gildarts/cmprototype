using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSA;

namespace Manager
{
    /// <summary>
    /// 代表連線到特定 DSA Server  所需的資訊。
    /// </summary>
    class ServerRegistryData
    {
        public ServerRegistryData()
        {
            SuperUser = AccountData.Default;
        }

        public string AccessPointUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public AccountData SuperUser { get; set; }

        public string Memo { get; set; }

        public XmlElement ToXml()
        {
            FISCA.XHelper data = new FISCA.XHelper("<Server/>");
            data.SetAttribute(".", "AccessPoint", AccessPointUrl);
            data.SetAttribute(".", "UserName", UserName);
            data.SetAttribute(".", "Password", ProtectedPassword(Password));
            data.SetAttribute(".", "DBSuperUserName", SuperUser.UserName);
            data.SetAttribute(".", "DBSuperUserPassword", ProtectedPassword(SuperUser.Password));
            data.AddElement(".", "Memo");
            //data.AddCDataSection("Memo", Memo);
            data.SetInnerXml("Memo", string.Format("<![CDATA[{0}]]>", Memo));

            return data.Data;
        }

        public void FromXml(XmlElement data)
        {
            FISCA.XHelper dsxml = new FISCA.XHelper(data);
            AccessPointUrl = dsxml.GetText("@AccessPoint");
            UserName = dsxml.GetText("@UserName");
            Password = UnprotectedPassword(dsxml.GetText("@Password"));
            Memo = dsxml.GetText("Memo");

            string superUser = dsxml.GetText("@DBSuperUserName");
            string superUserPassword = UnprotectedPassword(dsxml.GetText("@DBSuperUserPassword"));
            if (string.IsNullOrWhiteSpace(superUser))
                SuperUser = AccountData.Default;
            else
                SuperUser = new AccountData(superUser, superUserPassword);
        }

        private static string UnprotectedPassword(string chiperPassword)
        {
            try
            {
                if (Program.EncryptPassword)
                    return Manager.Password.UnProtected(chiperPassword);
                else
                    return chiperPassword;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ProtectedPassword(string plainPassword)
        {
            if (Program.EncryptPassword)
                return Manager.Password.Protected(plainPassword);
            else
                return plainPassword;
        }
    }
}
