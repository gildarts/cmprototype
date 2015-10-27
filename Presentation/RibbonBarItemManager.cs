using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理功能列
    /// </summary>
    public class RibbonBarItemManager
    {
        private DotNetBar.PrivateControl.MotherForm _Parent;
        private Dictionary<string, Dictionary<string, RibbonBarItem>> _RibbonBarItemDictionary = new Dictionary<string, Dictionary<string, RibbonBarItem>>();
        private int _XCount = 0;
        internal RibbonBarItemManager(DotNetBar.PrivateControl.MotherForm parent)
        {
            _Parent = parent;
        }
        /// <summary>
        /// 建立新的功能列，或取得已有的功能列
        /// </summary>
        /// <param name="tabName">功能列所屬的Tab</param>
        /// <param name="text">功能列的 NavText</param>
        /// <returns></returns>
        public RibbonBarItem this[string tabName, string text]
        {
            get
            {
                if ( _RibbonBarItemDictionary.ContainsKey(tabName) && _RibbonBarItemDictionary[tabName].ContainsKey(text) )
                {
                    //原本就有就回傳
                    return _RibbonBarItemDictionary[tabName][text];
                }
                else
                {
                    RibbonTabItem targetTab = null;
                    RibbonTabItem targetTabSec = null;
                    _Parent.EnsureTabs(tabName);
                    targetTab = _Parent.GetRibbonTab(tabName);
                    targetTabSec = _Parent.GetRibbonTab(tabName + "(延伸)");
                    RibbonBarItem item = new RibbonBarItem(text);
                    targetTab.Panel.Controls.Add(item.DisplayRibbon);
                    item.Index = _XCount;
                    _XCount += 1;
                    targetTabSec.Panel.Controls.Add(item.HiddenRibbon);
                    if ( !_RibbonBarItemDictionary.ContainsKey(tabName) ) _RibbonBarItemDictionary.Add(tabName, new Dictionary<string, RibbonBarItem>());
                    if ( !_RibbonBarItemDictionary[tabName].ContainsKey(text) ) _RibbonBarItemDictionary[tabName].Add(text, item);
                    return item;
                }
            }
        }
        /// <summary>
        /// 建立新的功能列，或取得已有的功能列
        /// </summary>
        /// <param name="tabName">功能列所屬的Tab</param>
        /// <param name="text">功能列的 NavText</param>
        /// <returns></returns>
        public RibbonBarItem GetChild(string tabName, string text)
        {
            return this[tabName, text];
        }
        internal List<RibbonBarItem> TabItems(string tabName)
        {
            if ( _RibbonBarItemDictionary.ContainsKey(tabName) )
                return new List<RibbonBarItem>(_RibbonBarItemDictionary[tabName].Values);
            else
                return new List<RibbonBarItem>();
        }
    }
}
