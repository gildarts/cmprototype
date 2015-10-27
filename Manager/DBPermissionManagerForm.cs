using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using System.IO;

namespace Manager
{
    public partial class DBPermissionManagerForm : Office2007Form
    {
        public DBPermissionManagerForm()
        {
            InitializeComponent();
        }

        private void ResetDBPermissionForm_Load(object sender, EventArgs e)
        {
            advTree1.Load(new StringReader(XmlFiles.PermissionTree));
            LockNodes(advTree1.Nodes);
        }

        private void LockNodes(NodeCollection nodes)
        {
            foreach (DevComponents.AdvTree.Node each in nodes)
            {
                each.Editable = false;

                if (each.HasChildNodes)
                    LockNodes(each.Nodes);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未開發完成!");
        }

        private void advTree1_BeforeCollapse(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
