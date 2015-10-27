using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Net;
using FISCA.Presentation.Controls;

namespace FISCA.PrivateControls
{
    partial class InitializationManager : BaseForm
    {
        private Form activeForm = null;

        public InitializationManager()
        {
            InitializeComponent();
        }

        private void SmartSchoolLogo_Load(object sender, EventArgs ev)
        {
            this.Location = new Point(50, 50);
        }

        public void SetMessage(string msg)
        {
            try
            {
                this.reflectionLabel1.Text = "<font color=\"MidnightBlue\" size=\"+6\"><i>" + msg + "</i></font>";
            }
            catch { }
        }
        public void SetAssembly(string asm)
        {
            try
            {
                this.lblName.Text = asm;
            }
            catch { }
        }
    }
}
