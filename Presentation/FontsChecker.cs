using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Presentation
{
    /// <summary>
    /// 檢查用戶端系統是否安裝系統字型
    /// </summary>
    public static class FontsChecker
    {
        /// <summary>
        /// 檢查系統是否已安裝標準字型
        /// </summary>
        /// <param name="showInstallForm">當發現系統沒有安裝時顯示安裝指引</param>
        /// <returns>是否已安裝標準字型</returns>
        public static bool Check(bool showInstallForm)
        {
            System.Drawing.Text.InstalledFontCollection systemFonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (var item in systemFonts.Families)
            {
                if (item.GetName(1028) == "微軟正黑體") return true;
            }
            if (showInstallForm)
                new FISCA.Presentation.DotNetBar.PrivateControl.InstallFonts().Show();
            return false;
        }
    }
}
