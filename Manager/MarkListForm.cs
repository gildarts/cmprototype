using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Dynamic;

namespace Manager
{
    public partial class MarkListForm : Office2007Form
    {
        public MarkListForm()
        {
            InitializeComponent();
        }

        private void MarkListForm_Load(object sender, EventArgs e)
        {
            dgvMarks.Columns.Clear();

            foreach (Dictionary<string, string> record in MainForm.MarkList.Values)
            {
                if (dgvMarks.Columns.Count <= 0)
                {
                    foreach (string name in record.Keys)
                    {
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.HeaderText = name;
                        dgvMarks.Columns.Add(column);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvMarks, new List<string>(record.Values).ToArray());
                dgvMarks.Rows.Add(row);
            }
        }
    }
}
