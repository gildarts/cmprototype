using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;

namespace Manager
{
    internal class ServerManagePanel_ExportApplication
    {
        private DataGridView Grid { get; set; }

        public ServerManagePanel_ExportApplication(DataGridView grid)
        {
            Grid = grid;
        }

        public void ExportSelected()
        {
            List<Application> apps = new List<Application>();

            foreach (DataGridViewRow row in Grid.SelectedRows)
                apps.Add(row.Tag as Application);

            apps.Reverse();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel 檔案(*.xlsx)|*.xlsx";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Workbook book = new Workbook();

                book.Worksheets.Clear();
                Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

                List<string> columns = GroupParameterNames(apps);
                Dictionary<string, int> map = new Dictionary<string, int>();

                int columnIndex = 0;

                //Name
                sheet.Cells[0, columnIndex].PutValue("name");
                map.Add("name", columnIndex);
                columnIndex++;

                //Enabled
                sheet.Cells[0, columnIndex].PutValue("enabled");
                map.Add("enabled", columnIndex);
                columnIndex++;

                foreach (string column in columns)
                {
                    sheet.Cells[0, columnIndex].PutValue(column);

                    map.Add(column, columnIndex);

                    columnIndex++;
                }

                int row = 1;
                foreach (Application app in apps)
                {
                    sheet.Cells[row, map["name"]].PutValue(app.Name);
                    sheet.Cells[row, map["enabled"]].PutValue(app.Enabled);

                    foreach (KeyValuePair<string, string> pair in app.GetParameters())
                    {
                        sheet.Cells[row, map[pair.Key]].PutValue(pair.Value);
                    }

                    row++;
                }

                book.Save(dialog.FileName, SaveFormat.Xlsx);
                Process.Start(dialog.FileName);
            }
        }

        private List<string> GroupParameterNames(List<Application> apps)
        {
            HashSet<string> columns = new HashSet<string>();

            foreach (Application app in apps)
            {
                foreach (string field in app.GetParameters().Keys)
                {
                    if (!columns.Contains(field))
                        columns.Add(field);
                }
            }

            return new List<string>(columns);
        }
    }
}
