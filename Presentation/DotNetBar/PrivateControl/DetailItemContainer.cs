using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;
using System.Windows.Forms;
using FISCA.Presentation;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    class DetailItemContainer : ExpandablePanel
    {
        static DetailItemContainer()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        private LinkLabel savebutton;
        private LinkLabel undobutton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        protected System.Windows.Forms.PictureBox pictureBox1;
        //private PictureBox
        private List<Presentation.DetailContent> items = new List<FISCA.Presentation.DetailContent>();
        private string _PrimaryKey = "";
        private bool _Loaded = false;

        public DetailItemContainer()
        {
            Style.BackColor1.Color = Color.Red;
            InitializeComponent();
            this.flowLayoutPanel1.SizeChanged += delegate
            {
                this.Height = this.TitleHeight + flowLayoutPanel1.Height;
                int x = ( this.Width - pictureBox1.Width ) / 2;
                int y = ( this.Height - this.TitleHeight - pictureBox1.Height ) / 2 + this.TitleHeight;
                if ( x < 0 ) x = 0;
                if ( y < 0 ) y = 0;
                pictureBox1.Location = new Point(x, y);
            };
            this.ExpandedChanged += delegate
            {
                if ( this.Expanded )
                {
                    item_LoadingChanged(null, null);
                }
                else
                {
                    pictureBox1.Visible = false;
                }
            };
            this.VisibleChanged += delegate
            {
                if ( !_Loaded ) Load();
            };
        }

        public void SetPrimaryKey(string key)
        {
            if ( key != _PrimaryKey )
            {
                _PrimaryKey = key;
                Load();
            }
        }

        public void Reload()
        {
            Load();
        }

        private void Load()
        {
            _Loaded = false;
            if ( Visible == false )
                return;
            foreach ( var item in items )
            {
                item.PrimaryKey = _PrimaryKey;
            }
            _Loaded = true;
        }

        public void AddContent(Presentation.DetailContent item)
        {
            if ( item == null ) return;

            this.TitleText = item.Group;

            item.Margin = new System.Windows.Forms.Padding(0);
            item.ContentValidatedChanged += new EventHandler(item_SaveButtonVisibleChanged);
            item.SaveButtonVisibleChanged += new EventHandler(item_SaveButtonVisibleChanged);
            item.CancelButtonVisibleChanged += new EventHandler(item_CancelButtonVisibleChanged);
            item.LoadingChanged += new EventHandler(item_LoadingChanged);
            item.LoadingChanged += new EventHandler(item_SaveButtonVisibleChanged);
            item.LoadingChanged += new EventHandler(item_CancelButtonVisibleChanged);
            item.BackColor = Color.Transparent;
            items.Add(item);

            flowLayoutPanel1.Controls.Add(item);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            this.savebutton = new LinkLabel();
            this.undobutton = new LinkLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ));
            this.ButtonImageCollapse = Properties.Resources.expandablebuttonimagecollapse;
            this.ButtonImageExpand = Properties.Resources.expandablebuttonimageexpand;
            this.AutoSize = true;
            this.ExpandOnTitleClick = true;
            this.CanvasColor = System.Drawing.SystemColors.Control;
            this.ColorScheme.DockSiteBackColorGradientAngle = 0;
            this.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "expandablePanel1";
            this.Size = new System.Drawing.Size(554, 54);
            this.TabIndex = 0;
            this.TitleText = "Title Bar";
            this.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.Style.BackColor1.Color = System.Drawing.Color.White;
            this.Style.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            this.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.Style.BorderColor.Color = System.Drawing.Color.White;
            this.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.Style.GradientAngle = 90;
            this.Style.WordWrap = true;
            this.TabIndex = 0;
            this.TitleHeight = 30;
            this.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.TitleStyle.BackColor1.Color = System.Drawing.Color.Transparent;
            this.TitleStyle.BackColor2.Color = System.Drawing.Color.Transparent;
            this.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.TitleStyle.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.TitleStyle.ForeColor.Color = System.Drawing.Color.DimGray;
            this.TitleStyle.GradientAngle = 90;
            this.TitleText = "Title Bar";
            this.AutoSize = true;

            this.savebutton.AutoSize = true;
            this.savebutton.LinkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 240 ) ) ) ), ( (int)( ( (byte)( 133 ) ) ) ), ( (int)( ( (byte)( 2 ) ) ) ));
            this.savebutton.Location = new System.Drawing.Point(2, 6);
            this.savebutton.Name = "linkLabel1";
            this.savebutton.Size = new System.Drawing.Size(31, 13);
            this.savebutton.TabIndex = 19;
            this.savebutton.TabStop = true;
            this.savebutton.Text = "";//"儲存";
            this.savebutton.Visible = false;
            this.savebutton.Click += new System.EventHandler(savebutton_Click);


            this.undobutton.AutoSize = true;
            this.undobutton.LinkColor = System.Drawing.Color.FromArgb(( (int)( ( (byte)( 240 ) ) ) ), ( (int)( ( (byte)( 133 ) ) ) ), ( (int)( ( (byte)( 2 ) ) ) ));
            this.undobutton.Location = new System.Drawing.Point(35, 6);
            this.undobutton.Name = "linkLabel2";
            this.undobutton.Size = new System.Drawing.Size(31, 13);
            this.undobutton.TabIndex = 20;
            this.undobutton.TabStop = true;
            this.undobutton.Text = "";//"儲存";
            this.undobutton.Visible = false;
            this.undobutton.Click += new System.EventHandler(undobutton_Click);

            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(554, 10);
            this.flowLayoutPanel1.TabIndex = 1;

            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = Properties.Resources.Loading;
            this.pictureBox1.Location = new System.Drawing.Point(77, 208);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;

            this.Controls.Add(pictureBox1);
            this.Controls.Add(savebutton);
            this.Controls.Add(undobutton);
            this.Controls.Add(flowLayoutPanel1);
            this.ResumeLayout(false);
        }
        private void item_LoadingChanged(object sender, EventArgs e)
        {
            bool loading = false;
            foreach ( var item in items )
            {
                if ( item.Loading )
                {
                    loading = true;
                    break;
                }
            }
            if ( loading )
            {
                //this.flowLayoutPanel1.Enabled = false;
                //this.flowLayoutPanel1.Visible=false;
                this.pictureBox1.Visible = this.Expanded;
            }
            else
            {
                //this.flowLayoutPanel1.Enabled = true;
                //this.flowLayoutPanel1.Visible = true;
                this.pictureBox1.Visible = false;
            }
        }
        private void item_SaveButtonVisibleChanged(object sender, EventArgs e)
        {
            bool visible = false;
            foreach ( var item in items )
            {
                if ( !item.ContentValidated | item.Loading )
                {
                    visible = false;
                    break;
                }
                if ( item.SaveButtonVisible )
                {
                    visible = true;
                }
            }
            savebutton.Visible = visible;
            if ( visible )
            {
                savebutton.Text = "儲存";
            }
            else
            {
                savebutton.Text = "";
            }
        }
        private void item_CancelButtonVisibleChanged(object sender, EventArgs e)
        {
            bool visible = false;
            foreach ( var item in items )
            {
                if ( item.Loading )
                {
                    visible = false;
                    break;
                }
                if ( item.CancelButtonVisible )
                {
                    visible = true;
                }
            }
            undobutton.Visible = visible;
            if ( visible )
            {
                undobutton.Text = "取消";
            }
            else
            {
                undobutton.Text = "";
            }
        }
        private void savebutton_Click(object sender, System.EventArgs e)
        {
            foreach ( var item in items )
            {
                if ( item.SaveButtonVisible )
                    item.Save();
            }
        }
        private void undobutton_Click(object sender, System.EventArgs e)
        {
            foreach ( var item in items )
            {
                if ( item.CancelButtonVisible )
                    item.Undo();
            }
        }
    }
}
