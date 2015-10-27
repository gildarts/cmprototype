using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using System.Threading;
using System.Threading.Tasks;

namespace Manager
{
    public partial class ServerGroupPanel : UserControl
    {
        private static ServerGroupPanel panel = null;

        public static ServerGroupPanel Instance
        {
            get
            {
                if (panel == null)
                {
                    panel = new ServerGroupPanel();
                    panel.Dock = DockStyle.Fill;
                }
                return panel;
            }
        }

        public ServerGroupPanel()
        {
            InitializeComponent();
        }

        private ServerGroup CurrentGroup { get; set; }

        internal void SetGroupObject(AdvNode<ServerGroup> groupNode)
        {
            CurrentGroup = groupNode.BehindObject;
            txtMemo.Text = CurrentGroup.Memo;

            List<AdvNode<Server>> servers = new List<AdvNode<Server>>();
            bool allconnected = true;

            foreach (Node node in groupNode.Nodes)
            {
                if (node is AdvNode<Server>)
                {
                    Server srv = (node as AdvNode<Server>).BehindObject;
                    servers.Add(node as AdvNode<Server>);

                    if (!srv.IsConnected)
                        allconnected = false;
                }
            }

            //if (!allconnected)
            //{
            //    string msg = "有部份主機並未進行連線，無法取得資訊，是否要進行連線，以取得詳細資訊？";
            //    if (MessageBox.Show(msg, "Prototype", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        MultiTaskingRunner runner = new MultiTaskingRunner();

            //        foreach (AdvNode<Server> each in servers)
            //        {
            //            CancellationTokenSource source = new CancellationTokenSource();

            //            runner.AddTask(string.Format("({0}){1}", each.BehindObject.RegistryData.Memo, each.BehindObject.RegistryData.AccessPointUrl),
            //                state =>
            //                {
            //                    AdvNode<Server> x = state as AdvNode<Server>;
            //                    x.BehindObject.Connect();
            //                }, each, source);
            //        }
            //        runner.ExecuteTasks();

            //        foreach (AdvNode<Server> each in servers)
            //        {
            //            if (each.BehindObject.IsConnected)
            //                each.Image = Images.Normal_Server;
            //        }
            //    }
            //}

            dgvServerList.Rows.Clear();
            foreach (AdvNode<Server> each in servers)
            {
                Server srv = each.BehindObject;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvServerList,
                    srv.AccessPointUrl,
                   DTParser.TryToStandard(srv.ServiceLastUpdate),
                    srv.ServiceVersion,
                    DTParser.TryToStandard(srv.CoreLastUpdate),
                    srv.CoreVersion,
                    srv.DBMSVersionString,
                    srv.Applications.Count() <= 0 ? 0 : srv.Applications.Count() - 1,
                    srv.RegistryData.Memo); //有一個是 shared。
                row.Tag = each;
                dgvServerList.Rows.Add(row);
            }
        }

        private void txtMemo_TextChanged(object sender, EventArgs e)
        {
            CurrentGroup.Memo = txtMemo.Text;
        }

        private void dgvServerList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvServerList.Rows.Count > 0 && e.RowIndex >= 0)
            {
                AdvNode<Server> srv = dgvServerList.SelectedRows[0].Tag as AdvNode<Server>;

                try
                {
                    if (!srv.BehindObject.IsConnected)
                    {
                        AsyncRunner runner = new AsyncRunner();
                        runner.Message = "連線中...";
                        runner.Run(
                            x =>
                            {
                                srv.BehindObject.Connect();
                            },
                            x =>
                            {
                                if (x.IsTaskError)
                                    throw x.TaskError;

                                MainForm.TryConnect(srv);
                            });
                    }

                    MainForm.ViewServer(srv.BehindObject);
                    MainForm.SelectNode(srv);

                }
                catch (Exception ex)
                {
                    ErrorForm.Show(ex.Message, ex);
                }
            }
        }
    }
}
