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
    /// 僅包含中央有Content的樣板。
    /// </summary>
    public partial class BlankPanel : UserControl, IBlankPanel
    {
        private FISCA.Presentation.DotNetBar.PrivateControl.BlankDivision blankDivision1;
        /// <summary>
        /// 建構子
        /// </summary>
        public BlankPanel()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sendr, EventArgs e1)
        {
            if (DesignMode && this.blankDivision1 == null)
            {
                this.ResizeRedraw = true;
                this.blankDivision1 = new FISCA.Presentation.DotNetBar.PrivateControl.BlankDivision();
                this.blankDivision1.Cursor = System.Windows.Forms.Cursors.Default;
                this.blankDivision1.Location = new System.Drawing.Point(0, 0);
                this.blankDivision1.TabIndex = 0;
                blankDivision1.SetConP(ContentPanePanel);
            }
        }

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
        /// <param name="e">包含事件的資料。</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DesignMode)
            {
                if (blankDivision1 == null)
                {
                    this.ResizeRedraw = true;
                    this.blankDivision1 = new FISCA.Presentation.DotNetBar.PrivateControl.BlankDivision();
                    this.blankDivision1.Cursor = System.Windows.Forms.Cursors.Default;
                    this.blankDivision1.Location = new System.Drawing.Point(0, 0);
                    this.blankDivision1.TabIndex = 0;
                    blankDivision1.SetConP(ContentPanePanel);
                }
                if (img == null || blankDivision1.Size != this.Size || blankDivision1.Group != this.Group)
                {
                    blankDivision1.Group = _Group;
                    blankDivision1.Size = this.Size;
                    blankDivision1.Invalidate();
                    img = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                    blankDivision1.DrawToBitmap(img, e.ClipRectangle);
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
        /// 取得ContentPane。
        /// </summary>
        public Control ContentPane
        {
            get { return ContentPanePanel; }
        }

        #endregion
    }
}
