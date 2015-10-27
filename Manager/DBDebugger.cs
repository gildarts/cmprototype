using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO;
using System.Reflection;
using System.Xml;
using ActiproSoftware.SyntaxEditor;
using System.IO.Compression;

namespace Manager
{
    public partial class DBDebugger : Office2007Form
    {
        private DatabaseManager Engine { get; set; }

        internal DBDebugger(DatabaseManager db)
        {
            InitializeComponent();
            Engine = db;
        }

        private void DBDebugger_Load(object sender, EventArgs e)
        {
            Stream sqllang = Assembly.GetExecutingAssembly().GetManifestResourceStream("Manager.SyntaxLanguage.ActiproSoftware.SQL.xml");
            CmdText.Document.LoadLanguageFromXml(sqllang, 0);
            sqllang.Close();

            dgvResult.Dock = DockStyle.Fill;
            txtResult.Dock = DockStyle.Fill;

            try
            {
                Text = string.Format("SQL Command Window - [{0}]", Engine.GetTargetDatabase());

                List<string> tables = Engine.ListTables();
                tables.Sort();

                IntelliPrompt intelli = CmdText.IntelliPrompt;

                intelli.MemberList.Sorted = false;
                intelli.MemberList.Clear();

                tables.ForEach(x => intelli.MemberList.Add(new IntelliPromptMemberListItem(x, 0)));
            }
            catch (Exception ex)
            {
                ErrorForm err = new ErrorForm();
                err.Display(ex.Message, ex);
                Close();
            }
        }

        private void CmdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && e.Modifiers == Keys.Shift)
                ExecuteManualQuery(false);
            else if (e.KeyCode == Keys.F5)
                ExecuteManualQuery(true);
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            ExecuteManualQuery(true);
        }

        private void btnExecuteUpdate_Click(object sender, EventArgs e)
        {
            ExecuteManualQuery(false);
        }

        private void ExecuteManualQuery(bool isQuery)
        {
            try
            {
                ShowResultControl(isQuery);

                string cmd = string.Empty;
                if (CmdText.SelectedView.SelectedText.Length <= 0)
                    cmd = CmdText.Text;
                else
                    cmd = CmdText.SelectedView.SelectedText;

                AsyncRunner runner = new AsyncRunner();
                runner.Message = "執行 SQL 中…";

                int t1 = Environment.TickCount;
                runner.Run(
                    x =>
                    {
                        if (isQuery)
                            x.Result = Engine.ExecuteQuery(cmd);
                        else
                            x.Result = Engine.ExecuteUpdate(cmd).OuterXml;
                    },
                    x =>
                    {
                        if (x.IsTaskError)
                        {
                            ErrorForm err = new ErrorForm();
                            err.Display(x.TaskError.Message, x.TaskError);
                            return;
                        }

                        if (isQuery)
                        {
                            XmlElement result = x.Result as XmlElement;
                            StringReader reader = new StringReader(result.OuterXml);
                            DataSet ds = new DataSet();
                            ds.ReadXml(reader, XmlReadMode.Auto);
                            reader.Close();

                            if (ds.Tables.Count > 0)
                            {
                                dgvResult.AutoGenerateColumns = true;
                                dgvResult.DataSource = ds.Tables[0];
                                dgvResult.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                            }
                            else
                                dgvResult.DataSource = null;
                        }
                        else
                        {
                            txtResult.Text = x.Result.ToString();
                        }
                    });
                lblMsg.Text = string.Format("花費時間：{0} 毫秒", Environment.TickCount - t1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowResultControl(bool isQuery)
        {
            dgvResult.Visible = isQuery;
            txtResult.Visible = !isQuery;
        }

        private static bool IsMemberListKey(Keys key)
        {
            return ((key & (Keys.Control | Keys.J)) == (Keys.Control | Keys.J));
        }

        private void CmdText_KeyTyped(object sender, KeyTypedEventArgs e)
        {
            SyntaxEditor editor = sender as SyntaxEditor;

            if (IsMemberListKey(e.KeyData))
            {
                if (editor.IntelliPrompt.MemberList.Visible) return;
                editor.IntelliPrompt.MemberList.Show();
            }
        }

        private void btnTextView_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedCells.Count > 0)
            {
                TextViewer.View(dgvResult.SelectedCells[0].Value.ToString());
            }
        }

        private void btnXmlView_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedCells.Count > 0)
            {
                XmlViewer.View(dgvResult.SelectedCells[0].Value.ToString());
            }
        }

        private void btnBase64XmlView_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedCells.Count > 0)
            {
                try
                {
                    string compress = dgvResult.SelectedCells[0].Value.ToString();

                    byte[] compressBinary = Convert.FromBase64String(compress);

                    GZipStream de = new GZipStream(new MemoryStream(compressBinary), CompressionMode.Decompress);

                    StreamReader reader = new StreamReader(de, Encoding.UTF8);

                    string uncompress = reader.ReadToEnd();
                    reader.Close();

                    XmlViewer.View(uncompress);
                }
                catch (Exception ex)
                {
                    ErrorForm.Show(ex.Message, ex);
                }
            }
        }
    }
}
