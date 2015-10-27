using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FISCA.Presentation
{
    /// <summary>
    /// 系統主視窗
    /// </summary>
    public class MotherForm
    {
        static MotherForm()
        {
            FISCA.Presentation.DotNetBar.PrivateControl.DotNetBarReferenceFixer.FixIt();
        }
        private static FISCA.Presentation.DotNetBar.PrivateControl.IMotherForm _Instance = null;
        private static FISCA.Presentation.DotNetBar.PrivateControl.IMotherForm Instance
        {
            get
            {
                if (_Instance == null)
                {
                    FISCA.Presentation.DotNetBar.PrivateControl.DotNetBarReferenceFixer.FixIt();
                    _Instance = new FISCA.Presentation.DotNetBar.PrivateControl.MotherForm();
                }
                return _Instance;
            }
        }
        private MotherForm()
        {
        }
        /// <summary>
        /// 取得Form
        /// </summary>
        public static Form Form { get { return (Form)Instance; } }
        /// <summary>
        /// 加入Division
        /// </summary>
        /// <param name="panel"></param>
        public static void AddPanel(INCPanel panel) { Instance.AddEntity(panel); }
        /// <summary>
        /// 加入Division
        /// </summary>
        /// <param name="panel"></param>
        public static void AddPanel(IBlankPanel panel) { Instance.AddEntity(panel); }
        /// <summary>
        /// 取得功能列
        /// </summary>
        public static RibbonBarItemManager RibbonBarItems { get { return Instance.RibbonBarItems; } }
        /// <summary>
        /// 取得開始按紐的選單
        /// </summary>
        public static MenuButton StartMenu { get { return Instance.StartMenu; } }
        /// <summary>
        /// 取得或設定，使用者設定資料的處理器
        /// </summary>
        public static IPreferenceProvider PreferenceProvider { get { return Instance.PreferenceProvider; } set { Instance.PreferenceProvider = value; } }
        /// <summary>
        /// 顯示文字於下方狀態列
        /// </summary>
        /// <param name="text">文字</param>
        public static void SetStatusBarMessage(string text)
        {
            Instance.SetStatusBarMessage(text);
        }
        /// <summary>
        /// 顯示文字及進度條於下方狀態列
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="progress">進度</param>
        public static void SetStatusBarMessage(string text, int progress)
        {
            Instance.SetStatusBarMessage(text, progress);
        }
    }
}
