﻿namespace FISCA.PrivateControls
{
    partial class ModuleDeployControl
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
            if (disposing && (components != null))
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
            this.Progress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Progress
            // 
            this.Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Progress.Location = new System.Drawing.Point(3, 53);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(494, 23);
            this.Progress.TabIndex = 3;
            this.Progress.Text = "progressBarX1";
            // 
            // lblMsg
            // 
            this.lblMsg.Location = new System.Drawing.Point(3, 5);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(494, 41);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "lblMsg";
            // 
            // ModuleDeployControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.Progress);
            this.Name = "ModuleDeployControl";
            this.Size = new System.Drawing.Size(500, 81);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ProgressBarX Progress;
        private System.Windows.Forms.Label lblMsg;
    }
}
