using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using FISCA.DSA;
using System.IO;

namespace Manager
{
    public partial class TrafficDetail : Office2007Form
    {
        private string logid = string.Empty;

        internal Server Server { get; set; }

        internal TrafficDetail(ServerManagePanel.TrafficData data, Server srv)
        {
            InitializeComponent();

            Server = srv;
            txtDetail.Document.AppendText(FISCA.XHelper.Format(data.RawTraffic.OuterXml));
            txtDetail.Document.AppendText("\n\n");

            logid = data.LogGuid;
        }

        internal TrafficDetail(XmlElement data, Server srv)
        {
            InitializeComponent();

            Server = srv;
            txtDetail.Document.AppendText(FISCA.XHelper.Format(data.OuterXml));
        }

        private void TrafficDetail_Load(object sender, EventArgs e)
        {
            AsyncRunner runner = new AsyncRunner();
            runner.Message = "讀取詳細資料中...";
            runner.Run(
                x =>
                {
                    if (!string.IsNullOrWhiteSpace(logid))
                    {
                        txtDetail.Document.AppendText(FISCA.XHelper.Format(Server.Manager.GetLog(logid).OuterXml));
                    }
                });
        }
    }
}
