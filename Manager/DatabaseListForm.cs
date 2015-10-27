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

namespace Manager
{
    internal partial class DatabaseListForm : Office2007Form
    {
        /// <summary>
        /// 資料庫清單。
        /// </summary>
        private List<Database> Databases { get; set; }

        private Server CurrentServer { get; set; }

        public DatabaseListForm(Server srv)
        {
            InitializeComponent();

            CurrentServer = srv;

        }

        private void DatabaseListForm_Load(object sender, EventArgs e)
        {
            //School Manager Prepare.
            dgvDatabases.Rows.Clear();
            DataGridViewRow drow = new DataGridViewRow();
            drow.CreateCells(dgvDatabases, "讀取中...", null);
            dgvDatabases.Rows.Add(drow);

            AsyncRunner<object, XmlElement> runner = new AsyncRunner<object, XmlElement>();
            runner.Message = "載入資料庫清單中";
            runner.Run(
                arg =>
                {
                    Databases = CurrentServer.Manager.ListDatabases();
                },
                arg =>
                {
                    if (arg.IsTaskError)
                    {
                        dgvDatabases.Rows.Clear();
                        DataGridViewRow temp = new DataGridViewRow();
                        temp.CreateCells(dgvDatabases, "錯誤:" + arg.TaskError.Message, null);
                        dgvDatabases.Rows.Add(temp);
                    }
                    else
                    {
                        //School Manager Complete
                        dgvDatabases.Rows.Clear();
                        foreach (Database each in Databases)
                        {
                            DataGridViewRow approw = new DataGridViewRow();
                            approw.CreateCells(dgvDatabases, each.Name, each.Description);
                            approw.Tag = each;
                            dgvDatabases.Rows.Add(approw);
                        }
                    }
                });
        }

        private void dgvDatabases_MouseMove(object sender, MouseEventArgs e)
        {
            if (dgvDatabases.SelectedRows.Count <= 0) return;
            if (dgvDatabases.SelectedRows[0].Tag == null) return;

            if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
                dgvDatabases.DoDragDrop(dgvDatabases.SelectedRows[0].Tag, DragDropEffects.Link);
        }
    }
}
