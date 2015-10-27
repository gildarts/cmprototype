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
    public partial class TextViewer : Office2007Form
    {
        public TextViewer()
        {
            InitializeComponent();
        }

        public static void View(string text)
        {
            TextViewer v = new TextViewer();
            v.baseSyntaxEditor1.Text = text;

            v.Show();
        }
    }
}
