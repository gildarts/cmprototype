using System;
using System.Collections.Generic;
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

                int column = 0;
                sheet.Cells[0, column++].PutValue("name");
                sheet.Cells[0, column++].PutValue("db_url");
                sheet.Cells[0, column++].PutValue("db_user");
                sheet.Cells[0, column++].PutValue("db_pwd");
                sheet.Cells[0, column++].PutValue("db_udt_user");
                sheet.Cells[0, column++].PutValue("db_udt_pwd");
                sheet.Cells[0, column++].PutValue("school_code");
                sheet.Cells[0, column++].PutValue("app_comment");
                sheet.Cells[0, column++].PutValue("enabled");

                int row = 1;
                foreach (Application app in apps)
                {
                    column = 0;
                    sheet.Cells[row, column++].PutValue(app.Name);
                    sheet.Cells[row, column++].PutValue(app.DatabaseFullName);
                    sheet.Cells[row, column++].PutValue(app.DMLUserName);
                    sheet.Cells[row, column++].PutValue(app.DMLPassword);
                    sheet.Cells[row, column++].PutValue(app.DDLUserName);
                    sheet.Cells[row, column++].PutValue(app.DDLPassword);
                    sheet.Cells[row, column++].PutValue(app.SchoolCode);
                    sheet.Cells[row, column++].PutValue(app.Comment);
                    sheet.Cells[row, column++].PutValue(app.Enabled);
                    row++;
                }

                book.Save(dialog.FileName, SaveFormat.Xlsx);
            }
        }
    }
}
