using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FISCA.Presentation
{
    /// <summary>
    /// 包含畫面左方有Navigation及中央有Content的樣版
    /// </summary>
    public interface INCPanel
    {
        /// <summary>
        /// 取得名稱，將會對應到相同名稱的功能表列
        /// </summary>
        string Group { get; }
        /// <summary>
        /// 取得左方區塊的內容
        /// </summary>
        Control NavigationPane { get; }
        /// <summary>
        /// 取得中央區塊的內容
        /// </summary>
        Control ContentPane { get; }
        /// <summary>
        /// 取得圖片
        /// </summary>
        Image Picture { get; }
    }
}
