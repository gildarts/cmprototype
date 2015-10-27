using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理功能列包含的按紐
    /// </summary>
   public  class RibbonBarButtonManager
    {
       private RibbonBarItem _Parent;
       internal RibbonBarButtonManager(RibbonBarItem  parent)
       { _Parent = parent; }
       /// <summary>
       /// 建立新的控制項或取得已有的按紐
       /// </summary>
       /// <param name="text">名稱</param>
       /// <returns>按紐</returns>
       public RibbonBarButton this[string text]
       {
           get { return _Parent.GetButton(text); }
       }
       /// <summary>
       /// 建立新的控制項或取得已有的按紐
       /// </summary>
       /// <param name="text">名稱</param>
       /// <returns>按紐</returns>
       public RibbonBarButton GetChild(string text)
       {
           return this[text];
       }
    }
}
