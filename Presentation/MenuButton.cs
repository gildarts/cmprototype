using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 選單按紐
    /// </summary>
    public class MenuButton
    {
        private BaseItem _Target = null;
        private static Dictionary<BaseItem, string> _LockItems = new Dictionary<BaseItem, string>();
        private ButtonItem _Child = new ButtonItem() { Text = "_________!@#" };
        private bool _SupposeHasChildern = false;

        internal MenuButton(BaseItem target)
        {
            _Target = target;
            _Target.Click += delegate { if ( this.Click != null )this.Click(this, new EventArgs()); };
            List<BaseItem> virtualButtons = new List<BaseItem>();
            if ( target is ButtonItem )
            {
                ( _Target as ButtonItem ).CheckedChanged += delegate { if ( this.CheckedChanged != null )this.CheckedChanged(this, new EventArgs()); };
                ( _Target as ButtonItem ).PopupOpen += delegate(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
                {
                    PopupOpenEventArgs args = new PopupOpenEventArgs();
                    if ( this.PopupOpen != null ) this.PopupOpen(this, args);
                    if ( args.Cancel ) { e.Cancel = true; return; }
                    virtualButtons.Clear();
                    foreach ( BaseItem item in args.Container.SubItems )
                    {
                        virtualButtons.Add(item);
                    }
                    foreach ( var item in virtualButtons )
                    {
                        _Target.SubItems.Add(item);
                    }
                    bool hasChild = false;
                    foreach ( BaseItem item in target.SubItems )
                    {
                        if ( item == _Child )
                            continue;
                        if ( item.Visible )
                        {
                            hasChild = true;
                            break;
                        }
                    }
                    if ( !hasChild )
                    {
                        e.Cancel = true;
                        foreach ( BaseItem item in virtualButtons )
                        {
                            _Target.SubItems.Remove(item);
                        }
                        virtualButtons.Clear();
                        return;
                    }
                    _Child.Visible = false;
                };
                ( _Target as ButtonItem ).PopupClose += delegate
                {
                    if ( this.PopupClose != null ) this.PopupClose(this, new EventArgs());
                };
                ( _Target as ButtonItem ).PopupFinalized += delegate
                {
                    _Child.Visible = _SupposeHasChildern;
                    foreach ( BaseItem item in virtualButtons )
                    {
                        _Target.SubItems.Remove(item);
                    }
                    virtualButtons.Clear();
                };
            }
            Items = new MenuButtonManager(target);
        }
        /// <summary>
        /// 取得或設定，指出是否在點擊時自動改變Checked屬性
        /// </summary>
        public bool AutoCheckOnClick
        {
            get
            {
                return ( _Target is ButtonItem ) ? ( _Target as ButtonItem ).AutoCheckOnClick : false;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); if ( _Target is ButtonItem ) ( _Target as ButtonItem ).AutoCheckOnClick = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否在點擊時自動收合選單
        /// </summary>
        public bool AutoCollapseOnClick
        {
            get
            {
                return _Target != null ? ( ( _Target is ButtonItem ) ? ( _Target as ButtonItem ).AutoCollapseOnClick : false ) : false;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); if ( _Target is ButtonItem ) ( _Target as ButtonItem ).AutoCollapseOnClick = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否在選單按紐上方增加一條分格線
        /// </summary>
        public bool BeginGroup
        {
            get
            {
                return _Target != null ? ( ( _Target is ButtonItem ) ? ( _Target as ButtonItem ).BeginGroup : false ) : false;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); if ( _Target is ButtonItem ) ( _Target as ButtonItem ).BeginGroup = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否已選取
        /// </summary>
        public bool Checked
        {
            get
            {
                return ( _Target is ButtonItem ) ? ( _Target as ButtonItem ).Checked : false;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); if ( _Target is ButtonItem ) ( _Target as ButtonItem ).Checked = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否啟用按紐
        /// </summary>
        public bool Enable
        {
            get
            {
                return _Target.Enabled;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target.Enabled = value;
            }
        }
        /// <summary>
        /// 取得或設定，鈕控上顯示的影像
        /// </summary>
        public Image Image// { get { return _Target.Image; } set { if ( _LockItems.ContainsKey(_Target) )throw new Exception("無法對已上鎖物件進行此動作。"); _Target.Image = value; } }
        {
            get
            {
                return GetImage();
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); SetImage(value);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal protected virtual Image GetImage()
        {
            return ( _Target is ButtonItem ) ? ( ( _Target as ButtonItem ).Image == Properties.Resources.Empty ? null : ( _Target as ButtonItem ).Image ) : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        internal protected virtual void SetImage(Image image)
        {
            if ( _Target is ButtonItem ) ( _Target as ButtonItem ).Image = ( image == null && _Target.Parent is ItemContainer ) ? Properties.Resources.Empty : image;
        }
        /// <summary>
        /// 鎖定此按紐，除非呼叫UnLock並傳入相同Password否則無法編輯此按紐
        /// </summary>
        /// <param name="password">密碼</param>
        public void Lock(string password)
        {
            if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對Locked物件進行此動作。"); _LockItems.Add(_Target, password);
        }
        /// <summary>
        /// 取得或設定，指出按紐顯示的文字
        /// </summary>
        public string Text
        {
            get
            {
                return _Target.Text;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target.Text = value;
            }
        }
        /// <summary>
        /// 解除鎖定此按紐
        /// </summary>
        /// <param name="password">密碼</param>
        public void UnLock(string password)
        {
            if ( _LockItems.ContainsKey(_Target) )
            {
                if ( _LockItems[_Target] != password ) throw new Exception("解除鎖定密碼錯誤。");
                else _LockItems.Remove(_Target);
            }
        }
        /// <summary>
        /// 取得或設定，指出是否顯示按紐
        /// </summary>
        public bool Visible
        {
            get
            {
                return _Target.Visible;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target.Visible = value;

            }
        }
        /// <summary>
        /// 取得或設定，其包含控制項相關資料
        /// </summary>
        public object Tag
        {
            get
            {
                return _Target.Tag;
            }
            set
            {
                if ( _LockItems.ContainsKey(_Target) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target.Tag = value;
            }
        }
        /// <summary>
        /// 取得或設定，永遠以包含子項目的方式顯示
        /// </summary>
        public bool SupposeHasChildern
        {
            get
            {
                return _SupposeHasChildern;
            }
            set
            {
                _SupposeHasChildern = value;
                if ( _Target is ButtonItem )
                {
                    if ( _SupposeHasChildern && !( _Target as ButtonItem ).SubItems.Contains(_Child) )
                        ( _Target as ButtonItem ).SubItems.Add(_Child);
                    if ( !_SupposeHasChildern && ( _Target as ButtonItem ).SubItems.Contains(_Child) )
                        ( _Target as ButtonItem ).SubItems.Remove(_Child);
                }
            }
        }
        /// <summary>
        /// 當按下按紐時。
        /// </summary>
        public event EventHandler Click;
        /// <summary>
        /// 當Checked暑性變更時
        /// </summary>
        public event EventHandler CheckedChanged;
        /// <summary>
        /// 當子選單開啟時
        /// </summary>
        public event EventHandler<PopupOpenEventArgs> PopupOpen;
        /// <summary>
        /// 當子選單關閉時
        /// </summary>
        public event EventHandler PopupClose;
        /// <summary>
        /// 取得子選單按紐集合
        /// </summary>
        public MenuButtonManager Items { get; private set; }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="text">子選單按紐名稱</param>
        /// <returns>子選單按紐</returns>
        public MenuButton this[string text] { get { return Items[text]; } }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="text">子選單按紐名稱</param>
        /// <returns>子選單按紐</returns>
        public MenuButton GetChild(string text)
        {
            return Items[text];
        }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="paths">子選單按紐路徑</param>
        /// <returns>子選單按紐</returns>
        public MenuButton GetChild(params string[] paths)
        {
            return Items[paths];
        }
    }
    /// <summary>
    /// 包含PopupOpen事件的資料
    /// </summary>
    public class PopupOpenEventArgs : EventArgs
    {
        internal ButtonItem Container { get; private set; }
        /// <summary>
        /// 建構子
        /// </summary>
        public PopupOpenEventArgs()
        {
            Cancel = false;
            Container = new ButtonItem();
            VirtualButtons = new MenuButton(Container);
        }
        /// <summary>
        /// 取消Popup
        /// </summary>
        public bool Cancel { get; set; }
        /// <summary>
        /// 虛擬按鈕
        /// </summary>
        /// <remarks>
        /// 對VirtualButtons加入子項目，新加入的子項目會加入在展開後的子項目的最末，並於收合時移除。
        /// 可以應用於在選單開啟時動態決定項目的情境。
        /// </remarks>
        public MenuButton VirtualButtons { get; private set; }
    }
}
