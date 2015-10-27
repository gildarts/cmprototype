using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class BlankDivision : UserControl
    {
        static BlankDivision()
        {

            DotNetBarReferenceFixer.FixIt();
        }
        public BlankDivision()
        {
            InitializeComponent();
            ribbonTabItem1.Checked = false;
            ribbonTabItem1.Checked = true;
        }

        private Panel ConP { get; set; }

        public void SetConP(Panel P)
        {
            if ( ConP != null )
            {
                ConP.LocationChanged -= new EventHandler(SetConP);
                ConP.SizeChanged -= new EventHandler(SetConP);
            }
            ConP = P;
            ConP.LocationChanged += new EventHandler(SetConP);
            ConP.SizeChanged += new EventHandler(SetConP);
        }

        private void SetConP(object sender, EventArgs e)
        {
            if ( ConP != null )
            {
                if ( ConP.Location.X != panelContent.Location.X || ConP.Location.Y != panelContent.Location.Y )
                    ConP.Location = new Point(panelContent.Location.X, panelContent.Location.Y);
                if ( ConP.Size.Width != panelContent.Size.Width || ConP.Size.Height != panelContent.Size.Height )
                    ConP.Size = new Size(panelContent.Size.Width, panelContent.Size.Height);
            }
        }

        public string Group
        {
            get { return ribbonTabItem1.Text; }
            set { ribbonTabItem1.Text = value; }
        }
    }
}
