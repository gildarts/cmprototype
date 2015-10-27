using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ActiproSoftware.SyntaxEditor;
using System.IO;
using System.Reflection;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using System.Xml.Linq;
using FISCA;

namespace Manager
{
    public partial class DBPermissionReset : Office2007Form
    {
        private ServerManager CurrentServerManager { get; set; }

        private DatabaseManager DBManager { get; set; }

        private List<string> TargetDatabases { get; set; }

        internal DBPermissionReset(Server server, string exampleDatabase, List<string> targetDatabases)
        {
            InitializeComponent();
            CurrentServerManager = server.Manager;
            DBManager = new DatabaseManager(server.Manager, exampleDatabase);
            TargetDatabases = targetDatabases;
        }

        private void DBPermissionReset_Load(object sender, EventArgs e)
        {
            try
            {
                List<DatabaseManager.RoleData> roles = DBManager.ListRoles();
                foreach (SyntaxEditor each in new SyntaxEditor[] { txtOwner, txtCrud, txtSchema, sqlDatabase, sqlSchema, sqlTable, sqlView, sqlTrigger })
                {
                    IntelliPrompt intelli = each.IntelliPrompt;

                    intelli.MemberList.Sorted = false;
                    intelli.MemberList.ImageList = imgRoleType;
                    intelli.MemberList.Clear();

                    roles.ForEach(x => intelli.MemberList.Add(new IntelliPromptMemberListItem(x.Name, x.CanLogin ? 0 : 1)));
                }

                using (Stream sqllang = Assembly.GetExecutingAssembly().GetManifestResourceStream("Manager.SyntaxLanguage.ActiproSoftware.SQL.xml"))
                {
                    SyntaxLanguage lang = DynamicSyntaxLanguage.LoadFromXml(sqllang, 0);
                    sqlDatabase.Document.Language = lang;
                    sqlSchema.Document.Language = lang;
                    sqlSequence.Document.Language = lang;
                    sqlTable.Document.Language = lang;
                    sqlView.Document.Language = lang;
                    sqlTrigger.Document.Language = lang;
                }
            }
            catch (Exception ex)
            {
                ErrorForm err = new ErrorForm();
                err.Display(ex.Message, ex);
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox_KeyTyped(object sender, KeyTypedEventArgs e)
        {
            SyntaxEditor editor = sender as SyntaxEditor;

            if (IsMemberListKey(e.KeyData) || IsMemberListChar(e.KeyChar))
            {
                if (editor.IntelliPrompt.MemberList.Visible) return;
                editor.IntelliPrompt.MemberList.Show();
            }
        }

        private bool IsMemberListChar(char c)
        {
            //return c == ',';
            return false;
        }

        private static bool IsMemberListKey(Keys key)
        {
            return ((key & (Keys.Control | Keys.J)) == (Keys.Control | Keys.J));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Enabled = false;
            MultiTaskingRunner runner = new MultiTaskingRunner();

            string owner = txtOwner.Text;
            string crud = txtCrud.Text;
            string schema = txtSchema.Text;
            CommandTemplate template = new CommandTemplate();
            template.ToDatabase = sqlDatabase.Text;
            template.ToSchema = sqlSchema.Text;
            template.ToTable = sqlTable.Text;
            template.ToView = sqlView.Text;
            template.ToSequence = sqlSequence.Text;
            template.ToTrigger = sqlTrigger.Text;

            foreach (string db in TargetDatabases)
            {
                runner.AddTask(db, x =>
                {
                    DatabaseManager dbman = new DatabaseManager(CurrentServerManager, x.ToString());
                    StringBuilder sql = GetResetSqlStatement(dbman, owner, crud, schema, template);
                    dbman.ExecuteUpdate(sql.ToString());
                }, db, new System.Threading.CancellationTokenSource());
            }
            runner.ExecuteTasks();
            Enabled = true;
            Close();
        }

        private StringBuilder GetResetSqlStatement(DatabaseManager dbmanager, string owner, string crud, string schema, CommandTemplate template)
        {
            StringBuilder commands = new StringBuilder();
            List<DatabaseManager.RoleData> roles = dbmanager.ListRoles();
            DatabaseManager.RoleData publicRole = new DatabaseManager.RoleData();
            publicRole.Name = "public";
            publicRole.CanLogin = false;
            roles.Add(publicRole);

            /* Database */
            foreach (DatabaseManager.RoleData role in roles)
            {
                string revokeDB = string.Format("REVOKE ALL ON DATABASE \"{0}\" FROM \"{1}\";", dbmanager.TargetDatabase, role.Name);
                commands.AppendLine(revokeDB);
            }

            string setDBOwner = string.Format("ALTER DATABASE \"{0}\" OWNER TO \"{1}\";", dbmanager.TargetDatabase, owner);
            commands.AppendLine(setDBOwner);

            string grantCrudConnect = string.Format("GRANT CONNECT ON DATABASE \"{0}\" TO \"{1}\";", dbmanager.TargetDatabase, crud);
            commands.AppendLine(grantCrudConnect);

            string grantSchemaConnect = string.Format("GRANT CONNECT ON DATABASE \"{0}\" TO \"{1}\";", dbmanager.TargetDatabase, schema);
            commands.AppendLine(grantSchemaConnect);

            commands.AppendLine(template.ToDatabase.Replace("@Database", "\"" + dbmanager.TargetDatabase + "\""));

            /* Schema */
            foreach (DatabaseManager.RoleData role in roles)
            {
                string revokeDB = string.Format("REVOKE ALL ON SCHEMA {0} FROM \"{1}\";", "public", role.Name);
                commands.AppendLine(revokeDB);
            }

            string grantSchemaAll = string.Format("GRANT USAGE ON SCHEMA \"public\" TO \"{0}\";", crud);
            commands.AppendLine(grantSchemaAll);

            string grantCrudUsage = string.Format("GRANT ALL ON SCHEMA \"public\" TO \"{0}\";", schema);
            commands.AppendLine(grantCrudUsage);

            commands.AppendLine(template.ToSchema.Replace("@Schema", "\"public\""));

            /* Tables */
            foreach (string table in dbmanager.ListTables())
            {
                foreach (DatabaseManager.RoleData role in roles)
                {
                    string cmd = string.Format("REVOKE ALL ON \"{0}\" FROM \"{1}\";", table, role.Name);
                    commands.AppendLine(cmd);
                }

                if (table.StartsWith("_$_"))
                {
                    //設定 UDT
                    string setUDTOwner = string.Format("ALTER TABLE \"{0}\" OWNER TO \"{1}\";", table, schema);
                    commands.AppendLine(setUDTOwner);
                }
                else
                {
                    //設定非 UDT
                    string SetUnUDTOwner = string.Format("ALTER TABLE \"{0}\" OWNER TO \"{1}\";", table, owner);
                    commands.AppendLine(SetUnUDTOwner);
                }

                //設定 CRUD 權限
                string setCrud = string.Format("GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE \"{0}\" TO \"{1}\";", table, crud);
                commands.AppendLine(setCrud);

                //設定 All 權限
                string setAll = string.Format("GRANT ALL ON TABLE \"{0}\" TO \"{1}\";", table, schema);
                commands.AppendLine(setAll);

                commands.AppendLine(template.ToTable.Replace("@Table", "\"" + table + "\""));
            }

            /* Views */
            foreach (string view in dbmanager.ListViews())
            {
                foreach (DatabaseManager.RoleData role in roles)
                {
                    string cmd = string.Format("REVOKE ALL ON \"{0}\" FROM \"{1}\";", view, role.Name);
                    commands.AppendLine(cmd);
                }

                if (view.StartsWith("_$_"))
                {
                    //設定 UDT
                    string setUDTOwner = string.Format("ALTER TABLE \"{0}\" OWNER TO \"{1}\";", view, schema);
                    commands.AppendLine(setUDTOwner);
                }
                else
                {
                    //設定非 UDT
                    string SetUnUDTOwner = string.Format("ALTER TABLE \"{0}\" OWNER TO \"{1}\";", view, owner);
                    commands.AppendLine(SetUnUDTOwner);
                }

                //設定 CRUD 權限
                string setCrud = string.Format("GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE \"{0}\" TO \"{1}\";", view, crud);
                commands.AppendLine(setCrud);

                //設定 All 權限
                string setAll = string.Format("GRANT ALL ON TABLE \"{0}\" TO \"{1}\";", view, schema);
                commands.AppendLine(setAll);

                commands.AppendLine(template.ToView.Replace("@View", "\"" + view + "\""));
            }

            /* Sequence */
            foreach (string sequence in dbmanager.ListSequences())
            {
                foreach (DatabaseManager.RoleData role in roles)
                {
                    string cmd = string.Format("REVOKE ALL ON \"{0}\" FROM \"{1}\";", sequence, role.Name);
                    commands.AppendLine(cmd);
                }

                string setOwner = string.Format("ALTER TABLE \"{0}\" OWNER TO \"{1}\";", sequence, owner);
                commands.AppendLine(setOwner);

                string setUsage1 = string.Format("GRANT ALL ON TABLE \"{0}\" TO \"{1}\";", sequence, crud);
                commands.AppendLine(setUsage1);

                string setUsage2 = string.Format("GRANT ALL ON TABLE \"{0}\" TO \"{1}\";", sequence, schema);
                commands.AppendLine(setUsage2);

                commands.AppendLine(template.ToSequence.Replace("@Sequence", "\"" + sequence + "\""));
            }

            /* Trigger */
            foreach (string trigger in dbmanager.ListTrigger())
            {
                foreach (DatabaseManager.RoleData role in roles)
                {
                    string cmd = string.Format("REVOKE ALL ON FUNCTION {0}() from \"{1}\";", trigger, role.Name);
                    commands.AppendLine(cmd);
                }

                string setOwner = string.Format("ALTER FUNCTION {0}() OWNER TO \"{1}\";", trigger, owner);
                commands.AppendLine(setOwner);

                string setExecute1 = string.Format("GRANT EXECUTE ON FUNCTION {0}() TO \"{1}\";", trigger, crud);
                commands.AppendLine(setExecute1);

                string setExecute2 = string.Format("GRANT EXECUTE ON FUNCTION {0}() TO \"{1}\";", trigger, schema);
                commands.AppendLine(setExecute2);

                commands.AppendLine(template.ToTrigger.Replace("@Trigger", "" + trigger + "()"));
            }

            return commands;

            //try
            //{
            //    AsyncRunner runner = new AsyncRunner();
            //    runner.Message = "重設權限中..." + dbmanager.TargetDatabase;
            //    runner.MessageOwner = this;
            //    runner.Run(
            //        x =>
            //        {
            //            dbmanager.ExecuteUpdate(commands.ToString());
            //        },
            //        x =>
            //        {
            //            if (x.IsTaskError)
            //            {
            //                ErrorForm err = new ErrorForm();
            //                err.Display(x.TaskError.Message, x.TaskError);
            //            }
            //        });
            //}
            //catch (Exception ex)
            //{
            //    ErrorForm err = new ErrorForm();
            //    err.Display(ex.Message, ex);
            //}
        }

        class CommandTemplate
        {
            public CommandTemplate()
            {
            }

            public string ToDatabase { get; set; }

            public string ToSchema { get; set; }

            public string ToTable { get; set; }

            public string ToView { get; set; }

            public string ToSequence { get; set; }

            public string ToTrigger { get; set; }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.xml|*.xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    XElement template = XElement.Load(dialog.FileName);

                    sqlDatabase.Text = template.ElementText("Database");
                    sqlSchema.Text = template.ElementText("Schema");
                    sqlTable.Text = template.ElementText("Table");
                    sqlView.Text = template.ElementText("View");
                    sqlSequence.Text = template.ElementText("Sequence");
                    sqlTrigger.Text = template.ElementText("Trigger");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XElement template = new XElement("CommandTemplate");

            template.Add(new XElement("Database", new XCData(sqlDatabase.Text)));
            template.Add(new XElement("Schema", new XCData(sqlSchema.Text)));
            template.Add(new XElement("Table", new XCData(sqlTable.Text)));
            template.Add(new XElement("View", new XCData(sqlView.Text)));
            template.Add(new XElement("Sequence", new XCData(sqlSequence.Text)));
            template.Add(new XElement("Trigger", new XCData(sqlTrigger.Text)));

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "*.xml|*.xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                template.Save(dialog.FileName);
        }
    }
}
