using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace FISCA.Presentation
{
    /// <summary>
    /// NavContentPresentation中DetailPane的顯示項目
    /// </summary>
    public partial class DetailContent : UserControl
    {
        private bool _SaveButtonVisible = false;
        private bool _CancelButtonVisible = false;
        private bool _Validated = true;
        private bool _Loading = false;
        private string _PrimaryKey = "";
        /// <summary>
        /// 建構子
        /// </summary>
        public DetailContent()
        {
            InitializeComponent();
            Group = this.GetType().Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if ( DesignMode )
                this.ResizeRedraw = true;
        }
        /// <summary>
        /// 取得或設定儲存按鈕顯示或隱藏
        /// </summary>
        public bool SaveButtonVisible
        {
            get
            {
                return _SaveButtonVisible;
            }
            set
            {
                if ( _SaveButtonVisible != value )
                {
                    _SaveButtonVisible = value;
                    OnSaveButtonVisibleChanged(new EventArgs());
                }
            }
        }
        /// <summary>
        /// 取得或設定取消按鈕顯示或隱藏
        /// </summary>
        public bool CancelButtonVisible
        {
            get
            {
                return _CancelButtonVisible;
            }
            set
            {
                if ( _CancelButtonVisible != value )
                {
                    _CancelButtonVisible = value;
                    OnCancelButtonVisibleChanged(new EventArgs());
                }
            }
        }
        /// <summary>
        /// 取得或設定內容驗證結果
        /// </summary>
        public bool ContentValidated
        {
            get { return _Validated; }
            set
            {
                if ( _Validated != value )
                {
                    _Validated = value;
                    OnContentValidatedChanged(new EventArgs());
                }
            }
        }
        /// <summary>
        /// 取得或設訂資料項目所屬類別
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 取得資料擁有者的索引鍵值
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return _PrimaryKey;
            }
            internal set
            {
                if ( _PrimaryKey != value )
                {
                    _PrimaryKey = value;
                    OnPrimaryKeyChanged(new EventArgs());
                }
            }
        }
        /// <summary>
        /// 取得或設定，資料正在讀取中
        /// </summary>
        public bool Loading
        {
            get
            {
                return _Loading;
            }
            set
            {
                if ( _Loading != value )
                {
                    _Loading = value;
                    OnLoadingChanged(new EventArgs());
                }
            }
        }
        /// <summary>
        /// 儲存
        /// </summary>
        internal void Save()
        {
            OnSaveButtonClick(new EventArgs());
        }
        /// <summary>
        /// 取消變更
        /// </summary>
        internal void Undo()
        {
            OnCancelButtonClick(new EventArgs());
        }
        /// <summary>
        /// 當SaveButtonVisible屬性改變時
        /// </summary>
        public event EventHandler SaveButtonVisibleChanged;
        /// <summary>
        /// 當CancelButtonVisible屬性改變時
        /// </summary>
        public event EventHandler CancelButtonVisibleChanged;
        /// <summary>
        /// 當PrimaryKey屬性變更時
        /// </summary>
        public event EventHandler PrimaryKeyChanged;
        /// <summary>
        /// 當Validated屬性變更時
        /// </summary>
        public event EventHandler ContentValidatedChanged;
        /// <summary>
        /// 當Loading屬性變更時
        /// </summary>
        public event EventHandler LoadingChanged;
        /// <summary>
        /// 按下SaveButton時
        /// </summary>
        public event EventHandler SaveButtonClick;
        /// <summary>
        /// 按下CancelButton時
        /// </summary>
        public event EventHandler CancelButtonClick;

        /// <summary>
        /// 引發SaveButtonVisibleChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnSaveButtonVisibleChanged(EventArgs e)
        {
            if ( SaveButtonVisibleChanged != null )
                SaveButtonVisibleChanged(this, e);
        }
        /// <summary>
        /// 引發CancelButtonVisibleChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnCancelButtonVisibleChanged(EventArgs e)
        {
            if ( CancelButtonVisibleChanged != null )
                CancelButtonVisibleChanged(this, e);
        }
        /// <summary>
        /// 引發ContentValidatedChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnContentValidatedChanged(EventArgs e)
        {
            if ( ContentValidatedChanged != null )
                ContentValidatedChanged(this, e);
        }
        /// <summary>
        /// 引發PrimaryKeyChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnPrimaryKeyChanged(EventArgs e)
        {
            if ( PrimaryKeyChanged != null )
                PrimaryKeyChanged(this, e);
        }
        /// <summary>
        /// 引發LoadingChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnLoadingChanged(EventArgs e)
        {
            if ( LoadingChanged != null )
                LoadingChanged(this, e);
        }
        /// <summary>
        /// 引發SaveButtonClick事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnSaveButtonClick(EventArgs e)
        {
            if ( SaveButtonClick != null )
                SaveButtonClick(this, e);
        }
        /// <summary>
        /// 引發CancelButtonClick事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnCancelButtonClick(EventArgs e)
        {
            if ( CancelButtonClick != null )
                CancelButtonClick(this, e);
        }

        private void DetailContent_SizeChanged(object sender, EventArgs e)
        {
            this.Width = 550;
            if ( this.DesignMode )
            {
                this.Height -= this.Height % 5;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if ( DesignMode )
            {
                e.Graphics.DrawRectangle(new Pen(Brushes.Gray, 1.0f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            }
        }
    }
}
