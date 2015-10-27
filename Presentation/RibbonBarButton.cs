using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 功能列上的按紐
    /// </summary>
    public class RibbonBarButton : MenuButton
    {
        internal RibbonBarButton(ButtonItem target)
            : base(target)
        {
            this.Size = MenuButtonSize.Medium;
            this.Display = true;
        }

        private MenuButtonSize _Size;
        /// <summary>
        /// 功能列上按紐的各種大小
        /// </summary>
        public enum MenuButtonSize
        {
            /// <summary>
            /// 非常大，通常是只有大圖而沒有顯示Text。
            /// </summary>
            ExtraLarge,
            /// <summary>
            /// 很大，文字顯示在圖片下方。
            /// </summary>
            Large,
            /// <summary>
            /// 標準，以兩列為一行的排列。
            /// </summary>
            Medium,
            /// <summary>
            /// 小，以三列為一行的排列。
            /// </summary>
            Small
        }
        /// <summary>
        /// 取得或設定，指出按紐的大小
        /// </summary>
        public MenuButtonSize Size { get { return _Size; } set { if (_Size == value)return; _Size = value; if (SizeChanged != null)SizeChanged(this, new EventArgs()); } }
        /// <summary>
        /// 當Size屬性變更時
        /// </summary>
        public event EventHandler SizeChanged;

        private bool _Display;
        /// <summary>
        /// 取得或設定，指出是否顯示於功能列
        /// </summary>
        public bool Display { get { return _Display; } set { if (_Display == value)return; _Display = value; if (DisplayChanged != null)DisplayChanged(this, new EventArgs()); } }
        /// <summary>
        /// 當Display屬性變更時
        /// </summary>
        public event EventHandler DisplayChanged;
    }
}
