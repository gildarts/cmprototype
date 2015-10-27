using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class NavContentDivision : UserControl
    {
        static NavContentDivision()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        public NavContentDivision()
        {
            InitializeComponent();
            ribbonTabItem1.Checked = false;
            ribbonTabItem1.Checked = true;
        }
        private Panel NavP { get; set; }
        private Panel ConP { get; set; }

        public void SetNavP(Panel P)
        {
            if ( NavP != null )
            {
                NavP.LocationChanged -= new EventHandler(SetNavP);
                NavP.SizeChanged -= new EventHandler(SetNavP);
            }
            NavP = P;
            NavP.LocationChanged += new EventHandler(SetNavP);
            NavP.SizeChanged += new EventHandler(SetNavP);
        }

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

        private void SetNavP(object sender, EventArgs e)
        {
            if ( NavP != null )
            {
                if ( NavP.Location.X != navigationPanePanel2.Location.X || NavP.Location.Y != navigationPanePanel2.Location.Y + navigationPane1.Location.Y + panel1.Location.Y )
                    NavP.Location = new Point(navigationPanePanel2.Location.X, navigationPanePanel2.Location.Y + navigationPane1.Location.Y + panel1.Location.Y);
                if ( NavP.Size.Width != navigationPanePanel2.Size.Width || NavP.Size.Height != navigationPanePanel2.Size.Height )
                    NavP.Size = new Size(navigationPanePanel2.Size.Width, navigationPanePanel2.Size.Height);
            }
        }

        private void SetConP(object sender, EventArgs e)
        {
            if ( ConP != null )
            {
                if ( ConP.Location.X != panelContent.Location.X || ConP.Location.Y != panelContent.Location.Y + panel1.Location.Y )
                    ConP.Location = new Point(panelContent.Location.X, panelContent.Location.Y + panel1.Location.Y);
                if ( ConP.Size.Width != panelContent.Size.Width || ConP.Size.Height != panelContent.Size.Height )
                    ConP.Size = new Size(panelContent.Size.Width, panelContent.Size.Height);
            }
        }

        public string Group
        {
            get { return buttonItem1.Text; }
            set { ribbonTabItem1.Text = buttonItem1.Text = value; }
        }
        public Image Pic
        {
            get { return buttonItem1.Image; }
            set
            {

                if ( value == null )
                {
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavContentDivision));
                    value = ( (System.Drawing.Image)( resources.GetObject("buttonItem1.Image") ) );
                }
                buttonItem1.Image = value;
            }
        }
    }
}
