using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSA;
using System.Xml;

namespace Manager
{
    static class DSNameResolver
    {
        public static string NameServer = "http://dsns1.ischool.com.tw/dsns/dsns/setup";

        public static Connection DSNServer { get; set; }

        static DSNameResolver()
        {
            DSNServer = null;
        }

        public static string[] ExportAllName()
        {
            if (DSNServer == null)
            {
                try
                {
                    DSNServer = new Connection();
                    DSNServer.Connect(NameServer, "", "anonymous", "");
                }
                catch (Exception)
                {
                    DSNServer = null;
                    return new string[] { };
                }
            }

            if (DSNServer != null)
            {
                FISCA.XHelper rsp = null;
                try
                {
                    rsp = DSNServer.SendRequest("DS.NameService.Export", new Envelope()).XResponseBody();
                }
                catch
                {
                    return new string[] { };
                }

                List<string> names = new List<string>();
                foreach (XmlElement each in rsp.GetElements("DSNS"))
                    names.Add(each.GetAttribute("Name"));

                return names.ToArray();
            }
            else
                return new string[] { };
        }
    }
}
