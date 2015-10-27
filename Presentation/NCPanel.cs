using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation
{
    /// <summary>
    /// 畫面左方有Navigation及中央有Content的樣版
    /// </summary>
    public partial class NCPanel : UserControl, INCPanel
    {
        private FISCA.Presentation.DotNetBar.PrivateControl.NavContentDivision navContentDivision1;
        /// <summary>
        /// 建構子
        /// </summary>
        public NCPanel()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sendr, EventArgs e1)
        {
            if (DesignMode && navContentDivision1 == null)
            {
                this.ResizeRedraw = true;
                this.navContentDivision1 = new FISCA.Presentation.DotNetBar.PrivateControl.NavContentDivision();
                this.navContentDivision1.Cursor = System.Windows.Forms.Cursors.Default;
                this.navContentDivision1.Location = new System.Drawing.Point(0, 0);
                this.navContentDivision1.TabIndex = 0;
                navContentDivision1.SetNavP(NavPanePanel);
                navContentDivision1.SetConP(ContentPanePanel);
            }
        }

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    if ( e.Control != NavPanePanel && e.Control != ContentPanePanel )
        //        ContentPanePanel.Controls.Add(e.Control);
        //    else
        //        base.OnControlAdded(e);
        //}

        private DivisionBarManager _RibbonBarItems = null;
        /// <summary>
        /// 取得功能列
        /// </summary>
        [Browsable(false)]
        public DivisionBarManager RibbonBarItems
        {
            get
            {
                if (_RibbonBarItems == null)
                    _RibbonBarItems = new DivisionBarManager(Group);
                return _RibbonBarItems;
            }
        }
        Bitmap img = null;
        /// <summary>
        /// 繪製控制項的背景。
        /// </summary>
        /// <param name="e">包含事件資料。</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DesignMode)
            {
                if (navContentDivision1 == null)
                {
                    this.ResizeRedraw = true;
                    this.navContentDivision1 = new FISCA.Presentation.DotNetBar.PrivateControl.NavContentDivision();
                    this.navContentDivision1.Cursor = System.Windows.Forms.Cursors.Default;
                    this.navContentDivision1.Location = new System.Drawing.Point(0, 0);
                    this.navContentDivision1.TabIndex = 0;
                    navContentDivision1.SetNavP(NavPanePanel);
                    navContentDivision1.SetConP(ContentPanePanel);
                }
                if (img == null || navContentDivision1.Size != this.Size || navContentDivision1.Group != this.Group || navContentDivision1.Pic != this.Picture)
                {
                    navContentDivision1.Group = _Group;
                    navContentDivision1.Pic = _Image;
                    navContentDivision1.Size = this.Size;
                    navContentDivision1.Invalidate();
                    img = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                    navContentDivision1.DrawToBitmap(img, e.ClipRectangle);
                }
                e.Graphics.DrawImage(img, new Point(0, 0));
            }
        }

        #region INavContentDivision 成員
        private string _Group;
        /// <summary>
        /// 取得或設定，Panel對應的標題
        /// </summary>
        [Browsable(true)]
        [Category("INavContentDivision 成員")]
        public string Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// 取得NavigationPane。
        /// </summary>
        public Control NavigationPane
        {
            get { return NavPanePanel; }
        }
        /// <summary>
        /// 取得ContentPane。
        /// </summary>
        public Control ContentPane
        {
            get { return ContentPanePanel; }
        }
        private Image _Image = null;
        /// <summary>
        /// 取得或設定，Panel的圖示。
        /// </summary>
        [Browsable(true)]
        [Category("INavContentDivision 成員")]
        public Image Picture
        {
            get
            {
                if (DesignMode)
                    return _Image;
                else
                {
                    if (_Image != null) return _Image;
                    else
                        return navContentDivision1.Pic;
                }
            }
            set
            {
                if (value != null)
                {
                    Bitmap b = new Bitmap(24, 24);
                    using (Graphics g = Graphics.FromImage(b))
                        g.DrawImage(value, 0, 0, 24, 24);
                    value = b;// newEntity.Picture;
                }
                _Image = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        #endregion
    }
}
