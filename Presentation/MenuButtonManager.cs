using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理選單按紐
    /// </summary>
    public class MenuButtonManager:IEnumerable<MenuButton>
    {
        //private BaseItem _Target = null;
        private SubItemsCollection _Collection = null;
        private object _Target = null;
        internal MenuButtonManager(BaseItem target)
        {
            _Target = target;
            _Collection = target.SubItems;
            //if ( target is ButtonItem )
            //{
            //    ( target as ButtonItem ).PopupOpen += delegate { if ( this.PopupOpen != null )this.PopupOpen(this, new EventArgs()); };
            //    ( target as ButtonItem ).PopupClose += delegate { if ( this.PopupClose != null )this.PopupClose(this, new EventArgs()); };
            //}
        }
        internal MenuButtonManager(ButtonX target)
        {
            _Target = target;
            _Collection = target.SubItems;
            //target.PopupOpen += delegate { if ( this.PopupOpen != null )this.PopupOpen(this, new EventArgs()); };
            //target.PopupClose += delegate { if ( this.PopupClose != null )this.PopupClose(this, new EventArgs()); };
        }
        /// <summary>
        /// 建立選單按紐或取得已建立的按紐
        /// </summary>
        /// <param name="text">按紐名稱</param>
        /// <returns>選單按紐</returns>
        public MenuButton this[string text]
        {
            get
            {
                return this[new string[1] { text }];
            }
        }
        /// <summary>
        /// 建立選單按紐或取得已建立的按紐
        /// </summary>
        /// <param name="paths">選單按紐的路徑</param>
        /// <returns>選單按紐</returns>
        public MenuButton this[params string[] paths]
        {
            get
            {
                if ( paths.Length == 0 )
                {
                    //if ( _Target is ButtonItem )
                    //    return new MenuButton(_Target as ButtonItem);
                    //else
                    return null;
                }
                ButtonItem newItem = null;
                foreach ( var item in _Collection )
                {
                    if ( item is ButtonItem && ( (ButtonItem)item ).Text == paths[0] )
                    {
                        newItem = ( (ButtonItem)item );
                        if ( paths.Length == 1 )
                            return new MenuButton(newItem);
                        else
                        {
                            string[] newlist = new string[paths.Length - 1];
                            for ( int i = 0 ; i < newlist.Length ; i++ )
                            {
                                newlist[i] = paths[i + 1];
                            }
                            return new MenuButton(newItem).Items[newlist];
                        }
                    }
                }
                newItem = new ButtonItem();
                newItem.Text = paths[0];
                newItem.AutoExpandOnClick = true;
                newItem.CanCustomize = false;
                if ( _Target is ItemContainer )
                {
                    newItem.ImageFixedSize = new System.Drawing.Size(20, 20);
                    newItem.Image = Properties.Resources.Empty;
                    newItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                }
                else
                {
                    newItem.ImageFixedSize = new System.Drawing.Size(16, 16);
                }
                newItem.SubItemsChanged += new System.ComponentModel.CollectionChangeEventHandler(newItem_SubItemsChanged);
                _Collection.Add(newItem);
                if ( paths.Length == 1 )
                    return new MenuButton(newItem);
                else
                {
                    string[] newlist = new string[paths.Length - 1];
                    for ( int i = 0 ; i < newlist.Length ; i++ )
                    {
                        newlist[i] = paths[i + 1];
                    }
                    return new MenuButton(newItem).Items[newlist];
                }
            }
        }

        void newItem_SubItemsChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            ButtonItem button = (ButtonItem)sender;
            button.AutoExpandOnClick = button.SubItems.Count>0;
        }
        //public event EventHandler PopupOpen;
        //public event EventHandler PopupClose;

        #region IEnumerable<MenuButton> 成員
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<MenuButton> GetEnumerator()
        {
            List<MenuButton> result = new List<MenuButton>();
            foreach ( var item in _Collection )
            {
                if ( item is ButtonItem )
                {
                    var newItem = new MenuButton((ButtonItem)item);
                    result.Add(newItem);
                }
            }
            return result.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
