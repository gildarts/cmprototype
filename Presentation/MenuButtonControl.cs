using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;

namespace FISCA.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuButtonControl
    {
        private ButtonX _Target2 = null;
        private static Dictionary<ButtonX, string> _LockItems2 = new Dictionary<ButtonX, string>();
        private ButtonItem _Child = new ButtonItem() { Text = "_________!@#" };
        private bool _SupposeHasChildern = false;

        internal MenuButtonControl(ButtonX target)
        {
            _Target2 = target;
            _Target2.Click += delegate { if ( this.Click != null )this.Click(this, new EventArgs()); };
            _Target2.CheckedChanged += delegate { if ( this.CheckedChanged != null )this.CheckedChanged(this, new EventArgs()); };
            List<BaseItem> virtualButtons = new List<BaseItem>();
            _Target2.PopupOpen += delegate
            {
                PopupOpenEventArgs args = new PopupOpenEventArgs();
                if ( this.PopupOpen != null ) this.PopupOpen(this, args);
                virtualButtons.Clear();
                foreach ( BaseItem item in args.Container.SubItems )
                {
                    virtualButtons.Add(item);
                }
                foreach ( var item in virtualButtons )
                {
                    _Target2.SubItems.Add(item);
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
                    foreach ( BaseItem item in virtualButtons )
                    {
                        _Target2.SubItems.Remove(item);
                    }
                    virtualButtons.Clear();
                    return;
                }
                _Child.Visible = false;
            };
            _Target2.PopupClose += delegate
            {
                _Child.Visible = _SupposeHasChildern;
                foreach ( BaseItem item in virtualButtons )
                {
                    _Target2.SubItems.Remove(item);
                }
                virtualButtons.Clear();

                if ( this.PopupClose != null ) this.PopupClose(this, new EventArgs());
            };
            SubItems = new MenuButtonManager(target);
        }
        /// <summary>
        /// 取得或設定，指出是否在點擊時自動改變Checked屬性
        /// </summary>
        public bool AutoCheckOnClick
        {
            get
            {
                return _Target2.AutoCheckOnClick;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.AutoCheckOnClick = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否已選取
        /// </summary>
        public bool Checked
        {
            get
            {
                return _Target2.Checked;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Checked = value;
            }
        }
        /// <summary>
        /// 取得或設定，指出是否啟用按紐
        /// </summary>
        public bool Enable
        {
            get
            {
                return _Target2.Enabled;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Enabled = value;
            }
        }
        /// <summary>
        /// 取得或設定，鈕控上顯示的影像
        /// </summary>
        public Image Image
        {
            get
            {
                return _Target2.Image;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Image = value;
            }
        }
        /// <summary>
        /// 鎖定此按紐，除非呼叫UnLock並傳入相同Password否則無法編輯此按紐
        /// </summary>
        /// <param name="password">密碼</param>
        public void Lock(string password)
        {
            if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對Locked物件進行此動作。"); _LockItems2.Add(_Target2, password);
        }
        /// <summary>
        /// 取得或設定，指出按紐顯示的文字
        /// </summary>
        public string Text
        {
            get
            {
                return _Target2.Text;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Text = value;

            }
        }
        /// <summary>
        /// 解除鎖定此按紐
        /// </summary>
        /// <param name="password">密碼</param>
        public void UnLock(string password)
        {
            if ( _LockItems2.ContainsKey(_Target2) )
            {
                if ( _LockItems2[_Target2] != password ) throw new Exception("解除鎖定密碼錯誤。");
                else _LockItems2.Remove(_Target2);
            }
        }
        /// <summary>
        /// 取得或設定，指出是否顯示按紐
        /// </summary>
        public bool Visible
        {
            get
            {
                return _Target2.Visible;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Visible = value;
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
                if ( _SupposeHasChildern && !_Target2.SubItems.Contains(_Child) )
                    _Target2.SubItems.Add(_Child);
                if ( !_SupposeHasChildern && _Target2.SubItems.Contains(_Child) )
                    _Target2.SubItems.Remove(_Child);
            }
        }
        /// <summary>
        /// 取得或設定，其包含控制項相關資料
        /// </summary>
        public object Tag
        {
            get
            {
                return _Target2.Tag;
            }
            set
            {
                if ( _LockItems2.ContainsKey(_Target2) ) throw new Exception("無法對已上鎖物件進行此動作。"); _Target2.Tag = value;
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
        //public event EventHandler PopupOpen;
        public event EventHandler<PopupOpenEventArgs> PopupOpen;
        /// <summary>
        /// 當子選單關閉時
        /// </summary>
        public event EventHandler PopupClose;
        /// <summary>
        /// 取得子選單按紐集合
        /// </summary>
        public MenuButtonManager SubItems { get; private set; }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="text">子選單按紐名稱</param>
        /// <returns>子選單按紐</returns>
        public MenuButton this[string text] { get { return SubItems[text]; } }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="text">子選單按紐名稱</param>
        /// <returns>子選單按紐</returns>
        public MenuButton GetChild(string text)
        {
            return SubItems[text];
        }
        /// <summary>
        /// 建立子選單按紐或取得已存在的按紐
        /// </summary>
        /// <param name="paths">子選單按紐路徑</param>
        /// <returns>子選單按紐</returns>
        public MenuButton GetChild(params string[] paths)
        {
            return SubItems[paths];
        }
    }
}
