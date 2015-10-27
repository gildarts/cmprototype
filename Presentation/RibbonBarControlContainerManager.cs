using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理功能列包含的控制項
    /// </summary>
    public class RibbonBarControlContainerManager
    {
       private RibbonBarItem _Parent;
       internal RibbonBarControlContainerManager(RibbonBarItem parent)
       { _Parent = parent; }
        /// <summary>
        /// 建立新的控制項或取得已有的控制項
        /// </summary>
        /// <param name="text">名稱</param>
        /// <returns>控制項容器</returns>
       public RibbonBarControlContainer this[string text]
       {
           get { return _Parent.GetControl(text); }
       }
        /// <summary>
       /// 建立新的控制項或取得已有的控制項
        /// </summary>
       /// <param name="text">名稱</param>
       /// <returns>控制項容器</returns>
       public RibbonBarControlContainer GetChild(string text)
       {
           return this[text];
       }
    }
}
