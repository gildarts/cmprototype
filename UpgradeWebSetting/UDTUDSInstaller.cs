//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Manager.Interfaces;
//using FISCA.DSAClient;
//using System.Xml.Linq;
//using System.IO;
//using System.Windows.Forms;

//namespace UpgradeWebSetting
//{
//    public class UDTUDSInstaller : IAppUpgrader
//    {
//        #region IAppUpgrader 成員

//        public string Description
//        {
//            get { return "UDT、UDS 安裝工具"; }
//        }

//        public void DoUpgrade(Connection adminConn, ISqlCommand cmd, Dictionary<string, string> args)
//        {
//            Connection conn = adminConn.AsContract("admin");

//            string baseFolder = @"D:\TFS2008\Research\Yaoming\CloudManager_Prototype\UpgradeWebSetting\bin\Debug";
//            XElement udtdef = XElement.Load(Path.Combine(baseFolder, "ischool.core.tsml"));
//            XElement udsdef = XElement.Load(Path.Combine(baseFolder, "ischool.core.csml"));

//            Envelope udtrsp = conn.SendRequest("UDTService.DDL.SetTables", new Envelope(new XmlStringHolder(udtdef.ToString())));

//            //MessageBox.Show(XmlHelper.Format(udtrsp.Body.XmlString));

//            Envelope udsrsp = conn.SendRequest("UDSManagerService.ImportContracts", new Envelope(new XmlStringHolder(udsdef.ToString())));

//            //MessageBox.Show(XmlHelper.Format(udsrsp.Body.XmlString));

//            //ischool.core.csml
//            //ischool.core.tsml

//            //UDSManagerService.ImportContracts
//            //UDTService.DDL.SetTables
//        }

//        #endregion
//    }
//}
