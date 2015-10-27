using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Manager.Interfaces;

namespace Manager
{
    public partial class UpgraderSelector : Office2007Form
    {
        private List<IAppUpgrader> Items { get; set; }

        public UpgraderSelector(IAppUpgrader[] upgraders)
        {
            InitializeComponent();

            Items = new List<IAppUpgrader>(upgraders);
            Selected = null;
        }

        private void UpgraderSelector_Load(object sender, EventArgs e)
        {
            dgvUpgraderList.DataSource = Items;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Selected = dgvUpgraderList.CurrentRow.DataBoundItem as IAppUpgrader;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public IAppUpgrader Selected { get; private set; }
    }
}
