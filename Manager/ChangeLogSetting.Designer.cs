namespace Manager
{
    partial class ChangeLogSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.chkEnabledLog = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkLogProcess = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkLogUDS = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cboLogOpt = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.chkCompress = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboLogDB = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取消";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(116, 246);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 30;
            this.btnConfirm.Text = "確認";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkEnabledLog
            // 
            this.chkEnabledLog.AutoSize = true;
            this.chkEnabledLog.Location = new System.Drawing.Point(24, 21);
            this.chkEnabledLog.Name = "chkEnabledLog";
            this.chkEnabledLog.Size = new System.Drawing.Size(80, 21);
            this.chkEnabledLog.TabIndex = 32;
            this.chkEnabledLog.Text = "啟用日誌";
            // 
            // chkLogProcess
            // 
            this.chkLogProcess.AutoSize = true;
            this.chkLogProcess.Location = new System.Drawing.Point(45, 48);
            this.chkLogProcess.Name = "chkLogProcess";
            this.chkLogProcess.Size = new System.Drawing.Size(107, 21);
            this.chkLogProcess.TabIndex = 32;
            this.chkLogProcess.Text = "記錄處理細節";
            // 
            // chkLogUDS
            // 
            this.chkLogUDS.AutoSize = true;
            this.chkLogUDS.Location = new System.Drawing.Point(45, 75);
            this.chkLogUDS.Name = "chkLogUDS";
            this.chkLogUDS.Size = new System.Drawing.Size(85, 21);
            this.chkLogUDS.TabIndex = 32;
            this.chkLogUDS.Text = "記錄 UDS";
            // 
            // cboLogOpt
            // 
            this.cboLogOpt.DisplayMember = "Text";
            this.cboLogOpt.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLogOpt.FormattingEnabled = true;
            this.cboLogOpt.ItemHeight = 19;
            this.cboLogOpt.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cboLogOpt.Location = new System.Drawing.Point(105, 140);
            this.cboLogOpt.Name = "cboLogOpt";
            this.cboLogOpt.Size = new System.Drawing.Size(166, 25);
            this.cboLogOpt.TabIndex = 33;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "Always";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Never";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "OccuredException";
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(24, 141);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 34;
            this.labelX1.Text = "記錄時機";
            // 
            // chkCompress
            // 
            this.chkCompress.AutoSize = true;
            this.chkCompress.Location = new System.Drawing.Point(45, 102);
            this.chkCompress.Name = "chkCompress";
            this.chkCompress.Size = new System.Drawing.Size(107, 21);
            this.chkCompress.TabIndex = 32;
            this.chkCompress.Text = "壓縮日誌資料";
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(24, 186);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 34;
            this.labelX2.Text = "日誌資料庫";
            // 
            // cboLogDB
            // 
            this.cboLogDB.DisplayMember = "Text";
            this.cboLogDB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLogDB.FormattingEnabled = true;
            this.cboLogDB.ItemHeight = 19;
            this.cboLogDB.Location = new System.Drawing.Point(105, 185);
            this.cboLogDB.Name = "cboLogDB";
            this.cboLogDB.Size = new System.Drawing.Size(166, 25);
            this.cboLogDB.TabIndex = 33;
            // 
            // ChangeLogSetting
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(285, 285);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboLogDB);
            this.Controls.Add(this.cboLogOpt);
            this.Controls.Add(this.chkCompress);
            this.Controls.Add(this.chkLogUDS);
            this.Controls.Add(this.chkLogProcess);
            this.Controls.Add(this.chkEnabledLog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ChangeLogSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "變更 Log 設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkEnabledLog;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkLogProcess;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkLogUDS;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLogOpt;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCompress;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLogDB;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
    }
}