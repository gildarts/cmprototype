using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;

namespace Manager.TextEditor
{
    public partial class XPathEvaluatorForm : Office2007Form
    {
        public XPathEvaluatorForm(string initXmlData)
        {
            InitializeComponent();

            seSource.Text = initXmlData;
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try
            {
                doc.LoadXml(seSource.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                seResult.Text = string.Empty;

                if (cboEvalMethod.Text == "SelectSingleNode")
                {
                    XmlNode node = doc.DocumentElement.SelectSingleNode(txtXpathExpression.Text);

                    if (node == null)
                        seResult.Text = "<沒有任何結果！>";
                    else
                    {
                        if (cboDisplayMethod.Text == "OuterXml")
                            seResult.Text = node.OuterXml;
                        else if (cboDisplayMethod.Text == "InnerXml")
                            seResult.Text = node.InnerXml;
                        else
                            throw new Exception("沒有此種顯示方式！");
                    }
                }
                else if (cboEvalMethod.Text == "SelectNodes")
                {
                    XmlNodeList nodes = doc.DocumentElement.SelectNodes(txtXpathExpression.Text);

                    if (nodes.Count <= 0)
                        seResult.Text = "<沒有任何結果！>";
                    else
                    {
                        if (cboDisplayMethod.Text == "OuterXml")
                        {
                            foreach (XmlNode each in nodes)
                                seResult.Document.AppendText(each.OuterXml + "\n");
                        }
                        else if (cboDisplayMethod.Text == "InnerXml")
                        {
                            foreach (XmlNode each in nodes)
                                seResult.Document.AppendText(each.InnerXml + "\n");
                        }
                        else
                            throw new Exception("沒有此種顯示方式！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtXpathExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnEvaluate_Click(this, EventArgs.Empty);
        }
    }
}