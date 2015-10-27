using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Reflection;

namespace Manager
{
    public partial class ErrorDetailForm : Office2007Form
    {
        public ErrorDetailForm()
        {
            InitializeComponent();
            richContent.Document.ReadOnly = true;
            cboViewType.SelectedIndex = 0;
        }

        public void ShowDetail(Exception ex)
        {
            ConstructTree(ex);
            ShowDialog();
        }

        public void ShowDetail(string message)
        {
            richContent.Text = message;
            ShowDialog();
        }

        private void ConstructTree(Exception ex)
        {
            Exception current = ex;
            while (current != null)
            {
                Type currentT = current.GetType();
                TreeNode tn = new TreeNode(currentT.Name);
                tn.Tag = current;
                tvStruct.Nodes.Add(tn);

                MemberInfo[] mems = currentT.GetProperties();
                foreach (MemberInfo mem in mems)
                {
                    TreeNode pmem = new TreeNode(mem.Name);
                    pmem.Tag = mem;

                    tn.Nodes.Add(pmem);
                }

                tn.Expand();
                current = current.InnerException;
            }
        }

        private void tvStruct_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PropertyInfo pty = e.Node.Tag as PropertyInfo;
            Exception exp = null;

            richContent.Text = "";

            if (e.Node.Tag is Exception)
            {
                richContent.Text = e.Node.Tag.ToString();
                return;
            }

            if (e.Node.Parent != null)
            {
                //Parent 的 Tag 屬性中應該存有特定的 Exception 類別。
                exp = e.Node.Parent.Tag as Exception;

                if (exp == null) return;
            }
            else
                return;

            if (pty != null)
            {
                object result = null;

                try
                {
                    result = pty.GetValue(exp, new object[] { });
                }
                catch (Exception ex)
                {
                    result = "讀取屬性錯誤：" + ex.Message;
                }

                if (result != null)
                {
                    try
                    {
                        //TODO 修改 SyntaxEditor
                        //xmlHelper1.Editor = richContent;
                        richContent.Text = XmlUtilities.Format(result.ToString());
                    }
                    catch
                    {
                        richContent.Text = result.ToString();
                        richContent.Document.ResetLanguage();
                    }
                }
                else
                    richContent.Text = "Null";
            }
        }

        private void cboViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboViewType.Text == "純文字")
            {
                //TODO 修改 SyntaxEditor
                //xmlHelper1.Editor = null;
                richContent.Document.ResetLanguage();
            }
            else
            {
                //TODO 修改 SyntaxEditor
                //xmlHelper1.Editor = richContent;

                try
                {
                    //TODO 修改 SyntaxEditor
                    //xmlHelper1.FormatContent();
                }
                catch { }
            }
        }
    }
}