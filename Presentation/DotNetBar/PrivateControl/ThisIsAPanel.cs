using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    class ThisIsAPanel : Panel
    {
        static ThisIsAPanel()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        public ThisIsAPanel()
        {
            this.Dock = DockStyle.Fill;
        }
        [System.ComponentModel.Browsable(false)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }
        private string _Text = "";
        public new string Text
        {
            get
            {
                if ( DesignMode && _Text == "" )
                    return "";
                else
                    return base.Text;
            }
            set
            {
                _Text = base.Text = ( value == "Drop Controls on this panel to associate them with current selection" ? "" : value );
            }
        }
        private static A _p = null;
        private static A p
        {
            get
            {
                if ( _p == null )
                { DotNetBarReferenceFixer.FixIt(); _p = new Px(); }
                return _p;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if ( DesignMode && _Text == "" )
            {
                base.OnPaint(e);
                p.Size = this.Size;
                p.Paint(e, "Drop Controls on this panel to associate them with current selection");
            }
            else
            {
                base.OnPaint(e);
                p.Size = this.Size;
                p.Paint(e);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            p.Size = this.Size;
            p.PaintBackground(e);
        }
        private interface A
        {
            void PaintBackground(PaintEventArgs e);
            void Paint(PaintEventArgs e);
            void Paint(PaintEventArgs e, string text);
            System.Drawing.Size Size { get; set; }
        }
        private class Px : PanelEx, A
        {
            public Px()
            {
                this.CanvasColor = System.Drawing.SystemColors.Control;
                this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                this.Dock = System.Windows.Forms.DockStyle.Fill;
                this.Location = new System.Drawing.Point(0, 0);
                this.Name = "panelEx1";
                this.Size = new System.Drawing.Size(404, 298);
                this.Style.Alignment = System.Drawing.StringAlignment.Center;
                this.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                this.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                this.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                this.Style.GradientAngle = 90;
                this.Style.WordWrap = true;
                this.TabIndex = 0;
                ( (DevComponents.DotNetBar.Rendering.Office2007Renderer)DevComponents.DotNetBar.Rendering.GlobalManager.Renderer ).ColorTableChanged += delegate
                {
                    this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                    this.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    this.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                    this.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                };
            }

            public void PaintBackground(PaintEventArgs e)
            {
                this.OnPaintBackground(e);
            }

            public new void Paint(PaintEventArgs e)
            {
                base.Text = "";
                this.OnPaint(e);
            }
            public void Paint(PaintEventArgs e, string text)
            {
                base.Text = text;
                this.OnPaint(e);
            }
        }
    }
}
