namespace FISCA.Presentation
{
    partial class NCPanel
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.NavPanePanel = new FISCA.Presentation.DotNetBar.PrivateControl.ThisIsAPanel();
            this.ContentPanePanel = new FISCA.Presentation.DotNetBar.PrivateControl.ThisIsAPanel();
            this.SuspendLayout();
            this.Controls.Add(NavPanePanel);
            this.Controls.Add(ContentPanePanel);
            // 
            // NavPanePanel
            // 
            this.NavPanePanel.Location = new System.Drawing.Point(0, 0);
            this.NavPanePanel.Name = "NavPanePanel";
            this.NavPanePanel.Size = new System.Drawing.Size(870, 662);
            this.NavPanePanel.TabIndex = 1;
            // 
            // ContentPanePanel
            // 
            this.ContentPanePanel.Location = new System.Drawing.Point(0, 0);
            this.ContentPanePanel.Name = "ContentPanePanel";
            this.ContentPanePanel.Size = new System.Drawing.Size(870, 662);
            this.ContentPanePanel.TabIndex = 1;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            //this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(870, 608);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Panel NavPanePanel;
        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Panel ContentPanePanel;
    }
}
