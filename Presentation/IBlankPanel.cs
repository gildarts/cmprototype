using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation
{
    /// <summary>
    /// 僅包含畫面中央有Content的樣版
    /// </summary>
    public interface IBlankPanel
    {
        /// <summary>
        /// 取得名稱，將會對應到相同名稱的功能表列
        /// </summary>
        string Group { get; }
        /// <summary>
        /// 取得中央區塊的內容
        /// </summary>
        Control ContentPane { get; }
    }
}