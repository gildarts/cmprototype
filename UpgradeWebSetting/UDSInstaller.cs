using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manager.Interfaces;
using FISCA.DSA;
using FISCA;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace UpgradeWebSetting
{
    public class UDSInstaller : IAppUpgrader
    {
        #region IAppUpgrader 成員

        public string Description
        {
            get { return "UDS 安裝工具"; }
        }

        public void DoUpgrade(Connection adminConn, ISqlCommand cmd, Dictionary<string, string> args)
        {
            Connection conn = adminConn.AsContract("admin");

            string path = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "ischool.core.csml");
            XElement udsdef = XElement.Load(path);
            Envelope udsrsp = conn.SendRequest("UDSManagerService.ImportContracts", new Envelope(new XStringHolder(udsdef.ToString())));
        }

        #endregion
    }
}
