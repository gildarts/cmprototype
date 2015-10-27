using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Manager
{
    public partial class UpgradeResultForm : Form
    {
        public UpgradeResultForm(Dictionary<string, Exception> results)
        {
            InitializeComponent();

            dgvResults.Rows.Clear();
            foreach (KeyValuePair<string, Exception> result in results)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvResults, result.Key, result.Value == null ? "成功" : "失敗");
                row.Tag = result.Value;
                dgvResults.Rows.Add(row);
            }
        }

        private void UpgradeResultForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dgvResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvResults.CurrentRow.Tag != null)
                XmlViewer.View(ErrorReport.Generate(dgvResults.CurrentRow.Tag as Exception));
            else
                MessageBox.Show("沒有錯誤。");
        }
    }
}
