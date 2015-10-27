using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Manager.TextEditor
{
    public partial class ShortcutHelp : Office2007Form
    {
        public ShortcutHelp()
        {
            InitializeComponent();
        }

        private void ShortcutHelp_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}