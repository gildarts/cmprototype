using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Windows.Forms;

namespace FISCA.Presentation
{
    /// <summary>
    /// 功能列上的控制項
    /// </summary>
    public class RibbonBarControlContainer
    {
        private ControlContainerItem _Target;
        internal RibbonBarControlContainer(ControlContainerItem target)
        {
            _Target = target;
            this.Display = true;
        }

        private bool _Display;
        /// <summary>
        /// 取得或設定，指出是否顯示於功能列
        /// </summary>
        public bool Display { get { return _Display; } set { if ( _Display == value )return; _Display = value; if ( DisplayChanged != null )DisplayChanged(this, new EventArgs()); } }
        /// <summary>
        /// 當Display屬性變更時
        /// </summary>
        public event EventHandler DisplayChanged;
        /// <summary>
        /// 取得顯示名稱
        /// </summary>
        public string Text { get { return _Target.Text; } }
        /// <summary>
        /// 取得或設定，控制項的內容
        /// </summary>
        public Control Control { get { return _Target.Control; } set { _Target.Control = value; } }
    }
}
