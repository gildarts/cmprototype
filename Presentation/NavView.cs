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
    /// NavContentPresentation中左方區塊外掛的瀏覽方式
    /// </summary>
    /// <remarks>當Layout時應以傳入的key值做為瀏覽的清單(而非所有可能的key值)，當被選取時觸發ListPaneSourceChanged事件將被選取的key值傳給ListPane</remarks>
    public partial class NavView : UserControl, INavView
    {
        /// <summary>
        /// 建構子。
        /// </summary>
        public NavView()
        {
            InitializeComponent();
            Source = new PrimaryKeysCollection();
            Source.ItemsChanged += delegate
            {
                OnSourceChanged();
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode)
                this.ResizeRedraw = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DesignMode)
            {
                e.Graphics.DrawRectangle(new Pen(Brushes.Gray, 1.0f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnSourceChanged()
        {
            if (SourceChanged != null)
                SourceChanged(this, new EventArgs());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (DesignMode)
            {
                e.Graphics.DrawString(NavText, this.Font, Brushes.LightGray, new PointF(5, 5));
            }
        }
        /// <summary>
        /// 設定此View選取的項目。
        /// </summary>
        /// <param name="source">所有項目得集合</param>
        /// <param name="selectAll">在ListPane中全選。</param>
        /// <param name="addToTemp">直接加入至待處理。</param>
        public void SetListPaneSource(IEnumerable<string> source, bool selectAll, bool addToTemp)
        {
            if (ListPaneSourceChanged != null)
                ListPaneSourceChanged(this, new ListPaneSourceChangedEventArgs(source) { AddToTemp = addToTemp, SelectedAll = selectAll });
        }

        #region INavView 成員

        /// <summary>
        /// 取德或設定，指出是否做用中
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// 取得名稱
        /// </summary>
        [Browsable(true)]
        public string NavText { get; set; }
        /// <summary>
        /// 取得描述
        /// </summary>
        [Browsable(true)]
        public string Description { get; set; }
        /// <summary>
        /// 取得顯示區域內容
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Control DisplayPane { get { return this; } }
        /// <summary>
        /// 取得，所有顯示資料之集合
        /// </summary>
        public PrimaryKeysCollection Source { get; private set; }
        /// <summary>
        /// 要顯示於ListPane的資料變更時
        /// </summary>
        public event EventHandler<ListPaneSourceChangedEventArgs> ListPaneSourceChanged;

        #endregion
        /// <summary>
        /// 選取項目變更。
        /// </summary>
        public event EventHandler SourceChanged;
    }
}
