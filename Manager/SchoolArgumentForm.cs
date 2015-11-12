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

namespace Manager
{
    public partial class SchoolArgumentForm : Office2007Form
    {
        private Application School { get; set; }

        internal SchoolArgumentForm(Application app)
        {
            InitializeComponent();

            School = app;
        }

        private void SchoolArgumentForm_Load(object sender, EventArgs e)
        {
            txtDMLUserName.Text = School.DMLUserName;
            txtDMLPassword.Text = School.DMLPassword;
            txtDDLUserName.Text = School.DDLUserName;
            txtDDLPassword.Text = School.DDLPassword;
            txtMemo.Text = School.Comment;
            txtDatabaseFullName.Text = School.DatabaseFullName;

            //AsyncRunner<object, List<Database>> runner = new AsyncRunner<object, List<Database>>();
            //runner.Run(
            //    x =>
            //    {
            //        runner.Result = School.Owner.Manager.ListDatabases();
            //    },
            //    x =>
            //    {
            //        if (runner.Result == null) return;

            //        List<string> dbs = new List<string>(from each in runner.Result select each.Name);
            //        txtDatabaseFullName.AutoCompleteCustomSource.AddRange(dbs.ToArray());
            //    });

            if (School.IsShared)
            {
                Text = "預設參數設定";
                txtDatabaseFullName.Text = "jdbc:postgresql://127.0.0.1/new_db";
            }
            else
                Text = string.Format("參數設定 - [{0}]", School.Name);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Application.Argument arg = School.GetArgument();

            arg["db_url"] = txtDatabaseFullName.Text;
            arg["db_user"] = txtDMLUserName.Text;
            arg["db_pwd"] = txtDMLPassword.Text;
            arg["db_udt_user"] = txtDDLUserName.Text;
            arg["db_udt_pwd"] = txtDDLPassword.Text;
            arg["app_comment"] = txtMemo.Text;

            //arg.DMLUserName = txtDMLUserName.Text;
            //arg.DMLPassword = txtDMLPassword.Text;
            //arg.DDLUserName = txtDDLUserName.Text;
            //arg.DDLPassword = txtDDLPassword.Text;
            //arg.Comment = txtMemo.Text;
            //arg.SetDatabaseFullName(txtDatabaseFullName.Text);

            AsyncRunner runner = new AsyncRunner();
            runner.Message = "更新學校設定中...";
            runner.MessageOwner = this;
            runner.Run(
                x =>
                {
                    School.Owner.SetApplicationArgument(arg);
                    School.Owner.Manager.ReloadServer();
                },
                x =>
                {
                    if (x.IsTaskError)
                    {
                        ErrorForm error = new ErrorForm();
                        error.Display(x.TaskError.Message, x.TaskError);
                        DialogResult = System.Windows.Forms.DialogResult.None;
                    }
                    else
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                });
        }

        private void btnSelectDatabase_Click(object sender, EventArgs e)
        {
            new DatabaseListForm(School.Owner).Show();
        }

        private void txtDatabaseName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void txtDatabaseName_DragDrop(object sender, DragEventArgs e)
        {
            Database db = e.Data.GetData(typeof(Database)) as Database;
            if (txtDatabaseFullName.SelectionLength > 0)
            {
                txtDatabaseFullName.SelectedText = db.Name;
            }
            else
            {
                txtDatabaseFullName.AppendText(db.Name);
            }
        }
    }
}