using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FISCA.DSA;

namespace Manager
{
    public partial class XmlViewer : Office2007Form
    {
        public XmlViewer()
        {
            InitializeComponent();
        }

        public static void View(string xmlContent)
        {
            try
            {
                string content = FISCA.XHelper.Format(xmlContent);

                XmlViewer v = new XmlViewer();
                v.baseSyntaxEditor1.Text = content;

                v.Show();
            }
            catch (Exception ex)
            {
                ErrorForm.Show(ex.Message, ex);
            }
        }
    }
}
