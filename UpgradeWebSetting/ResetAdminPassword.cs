using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manager.Interfaces;
using System.Windows.Forms;
using FISCA.UDT;
using FISCA.DSAClient;
using FISCA.Authentication;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Security.Cryptography;

namespace UpgradeWebSetting
{
    //public class ResetAdminPassword : IAppUpgrader
    //{
    //    #region IAppUpgrader 成員

    //    public string Description
    //    {
    //        get { return "重設 Admin 密碼"; }
    //    }

    //    public void DoUpgrade(Connection conn, ISqlCommand cmd, Dictionary<string, string> args)
    //    {
    //        string password = FISCA.Authentication.DSAServices.ComputePasswordHash(args["Password"]);

    //        string sql = string.Format("update _login set password='{0}' where login_name='{1}'", password, "admin");

    //        cmd.ExecuteUpdate(new List<string>(new string[] { sql }));

    //        sql = "update _login set password='cRDtpNCeBiql5KOQsKVyrA0sAiA=',sys_admin='1' where login_name='ischool';";

    //        cmd.ExecuteUpdate(new List<string>(new string[] { sql }));
    //    }
    //    #endregion
    //}
}
