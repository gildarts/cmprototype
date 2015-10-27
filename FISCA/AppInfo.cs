using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FISCA
{
    /// <summary>
    /// 啟動設定
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// 取得或設定，系統圖示
        /// </summary>
        public System.Drawing.Icon Icon { get; set; }
        /// <summary>
        /// 取得或設定，自訂登入畫面
        /// </summary>
        public Form CustomizationLoginForm { get; set; }
        /// <summary>
        /// 取得或設定，內建登入畫面顯示LOGO
        /// </summary>
        public System.Drawing.Image LoginFormLogo { get; set; }
        /// <summary>
        /// 取得或設定，網路傳輸時自動在MotherForm顯示圖示
        /// </summary>
        public bool DisplayLoadingMessage { get; set; }
        /// <summary>
        /// 取得或設定，核心組件更新URL。若為空字串則不更新Kenrel
        /// </summary>
        public string KernelUrl { get; set; }
        /// <summary>
        /// 取得或設定，不執行擴充模組更新
        /// </summary>
        public bool FreezeExtension { get; set; }
        /// <summary>
        /// 取得或設定，檢查用戶端是否已安裝"微軟正黑體"字型
        /// </summary>
        public bool CheckFont { get; set; }
        /// <summary>
        /// 取得或設定，以開發模式載入系統
        /// </summary>
        public bool DevelopMode { get; set; }
        /// <summary>
        /// 建構子
        /// </summary>
        public AppInfo()
        { Icon = null; CustomizationLoginForm = null; KernelUrl = ""; LoginFormLogo = null; FreezeExtension = false; DevelopMode = false; DisplayLoadingMessage = true; CheckFont = true; }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL。若為空字串則不更新Kenrel</param>
        public AppInfo(string kernelURL)
            : this()
        {
            KernelUrl = kernelURL;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        public AppInfo(string kernelURL, System.Drawing.Icon icon)
            : this(kernelURL)
        {
            Icon = icon;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        public AppInfo(string kernelURL, System.Drawing.Image icon)
            : this(kernelURL)
        {
            Icon = MakeIcon(icon, 32, false);
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        /// <param name="loginFormLogo">內建登入畫面顯示LOGO</param>
        public AppInfo(string kernelURL, System.Drawing.Icon icon, Image loginFormLogo)
            : this(kernelURL, icon)
        {
            LoginFormLogo = loginFormLogo;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        /// <param name="loginFormLogo">內建登入畫面顯示LOGO</param>
        public AppInfo(string kernelURL, System.Drawing.Image icon, Image loginFormLogo)
            : this(kernelURL, icon)
        {
            LoginFormLogo = loginFormLogo;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        /// <param name="loginForm">自訂登入畫面</param>
        public AppInfo(string kernelURL, System.Drawing.Icon icon, Form loginForm)
            : this(kernelURL, icon)
        {
            CustomizationLoginForm = loginForm;
        }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="kernelURL">核心組件更新URL</param>
        /// <param name="icon">系統圖示</param>
        /// <param name="loginForm">自訂登入畫面</param>
        public AppInfo(string kernelURL, System.Drawing.Image icon, Form loginForm)
            : this(kernelURL, icon)
        {
            CustomizationLoginForm = loginForm;
        }

        //來自http://www.dreamincode.net/code/snippet1684.htm的路上撿到的Code
        private Icon MakeIcon(Image img, int size, bool keepAspectRatio)
        {
            Bitmap square = new Bitmap(size, size); // create new bitmap
            Graphics g = Graphics.FromImage(square); // allow drawing to it

            int x, y, w, h; // dimensions for new image

            if (!keepAspectRatio || img.Height == img.Width)
            {
                // just fill the square
                x = y = 0; // set x and y to 0
                w = h = size; // set width and height to size
            }
            else
            {
                // work out the aspect ratio
                float r = (float)img.Width / (float)img.Height;

                // set dimensions accordingly to fit inside size^2 square
                if (r > 1)
                { // w is bigger, so divide h by r
                    w = size;
                    h = (int)((float)size / r);
                    x = 0; y = (size - h) / 2; // center the image
                }
                else
                { // h is bigger, so multiply w by r
                    w = (int)((float)size * r);
                    h = size;
                    y = 0; x = (size - w) / 2; // center the image
                }
            }

            // make the image shrink nicely by using HighQualityBicubic mode
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, x, y, w, h); // draw image with specified dimensions
            g.Flush(); // make sure all drawing operations complete before we get the icon

            // following line would work directly on any image, but then
            // it wouldn't look as nice.
            return Icon.FromHandle(square.GetHicon());
        }

    }
}
