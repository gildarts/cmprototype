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

namespace UpgradeWebSetting
{
    //public class Upgrader : IAppUpgrader
    //{
    //    #region IAppUpgrader 成員

    //    public string Description
    //    {
    //        get { return "升級 ischool Web 預設的設定。"; }
    //    }

    //    public void DoUpgrade(Connection conn, ISqlCommand cmd, Dictionary<string, string> args)
    //    {
    //        Connection admin = conn.AsContract("admin");

    //        XmlHelper req = new XmlHelper(Spec.PackageContract);

    //        admin.SendRequest("UDSManagerService.ImportContract", new Envelope(req));

    //        FISCA.DSAUtil.DSConnection udtconn = new FISCA.DSAUtil.DSConnection(admin);
    //        AccessHelper helper = new AccessHelper(udtconn);
    //        List<PackageDefinition> packages = helper.Select<PackageDefinition>();
    //        helper.DeletedValues(packages); //先把全部幹掉。

    //        XElement pkgDefinitions = XElement.Parse(Spec.PackageDefinition);

    //        if (!args.ContainsKey("類別"))
    //            throw new ArgumentException("缺少「類別」參數。");

    //        XElement pkgs = pkgDefinitions.XPathSelectElement(string.Format("PackageCatalog[@Type='{0}']", args["類別"]));
    //        if (pkgs != null)
    //        {
    //            List<PackageDefinition> records = new List<PackageDefinition>();
    //            foreach (XElement pkg in pkgs.Elements("Package"))
    //            {
    //                pkg.XPathSelectElement("Params[@application]").Attribute("application").Value = args["accesspoint"];

    //                PackageDefinition pd = new PackageDefinition();
    //                pd.DSConnection = udtconn;
    //                pd.Definition = pkg.ToString();
    //                pd.RefTagID = 0;
    //                pd.TargetType = pkg.Attribute("Type").Value;
    //                records.Add(pd);
    //            }
    //            records.SaveAll();
    //        }
    //        else
    //            throw new ArgumentException(string.Format("找不到指定的類別({0})", args["類別"]));
    //    }

    //    #endregion
    //}
}
