using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理Division所對應的功能列
    /// </summary>
    public class DivisionBarManager
    {
        private string _TabName = "";
        internal DivisionBarManager(string tabName)
        {
            _TabName = tabName;
        }
        /// <summary>
        /// 建立新的功能列，或取得已有的功能列
        /// </summary>
        /// <param name="text">功能列的 NavText</param>
        /// <returns>功能列</returns>
        public RibbonBarItem this[ string text]
        {
            get
            {
                return MotherForm.RibbonBarItems[_TabName, text];
            }
        }
        /// <summary>
        /// 建立新的功能列，或取得已有的功能列
        /// </summary>
        /// <param name="text">功能列的 NavText</param>
        /// <returns>功能列</returns>
        public RibbonBarItem GetChild(string text)
        {
            return this[text];
        }
    }
}
