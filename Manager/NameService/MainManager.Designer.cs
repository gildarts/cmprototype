namespace Manager.NameService
{
    partial class MainManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnBackup = new DevComponents.DotNetBar.ButtonX();
            this.btnRestore = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.dgvDSNSList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSecuredUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chCatalog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chActive = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.chIsPublic = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.txtFilter = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtDSNSHost = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.btnDownload = new DevComponents.DotNetBar.ButtonX();
            this.btnBackupExcel = new DevComponents.DotNetBar.ButtonX();
            this.btnRestoreExcel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSNSList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBackup
            // 
            this.btnBackup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBackup.Location = new System.Drawing.Point(12, 527);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "備份 Xml";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRestore.Location = new System.Drawing.Point(94, 527);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "回存 Xml";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(615, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(697, 527);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvDSNSList
            // 
            this.dgvDSNSList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDSNSList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSNSList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chName,
            this.chUrl,
            this.chSecuredUrl,
            this.chTitle,
            this.chMemo,
            this.chCatalog,
            this.chActive,
            this.chIsPublic});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSNSList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSNSList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDSNSList.Location = new System.Drawing.Point(12, 84);
            this.dgvDSNSList.Name = "dgvDSNSList";
            this.dgvDSNSList.RowTemplate.Height = 24;
            this.dgvDSNSList.Size = new System.Drawing.Size(760, 432);
            this.dgvDSNSList.TabIndex = 2;
            this.dgvDSNSList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSNSList_CellClick);
            // 
            // chName
            // 
            this.chName.DataPropertyName = "Name";
            this.chName.HeaderText = "Name";
            this.chName.Name = "chName";
            // 
            // chUrl
            // 
            this.chUrl.DataPropertyName = "Url";
            this.chUrl.HeaderText = "Url";
            this.chUrl.Name = "chUrl";
            // 
            // chSecuredUrl
            // 
            this.chSecuredUrl.DataPropertyName = "SecuredUrl";
            this.chSecuredUrl.HeaderText = "SecuredUrl";
            this.chSecuredUrl.Name = "chSecuredUrl";
            // 
            // chTitle
            // 
            this.chTitle.DataPropertyName = "Title";
            this.chTitle.HeaderText = "Title";
            this.chTitle.Name = "chTitle";
            // 
            // chMemo
            // 
            this.chMemo.DataPropertyName = "Memo";
            this.chMemo.HeaderText = "Memo";
            this.chMemo.Name = "chMemo";
            // 
            // chCatalog
            // 
            this.chCatalog.DataPropertyName = "Catalog";
            this.chCatalog.HeaderText = "Catalog";
            this.chCatalog.Name = "chCatalog";
            // 
            // chActive
            // 
            this.chActive.Checked = true;
            this.chActive.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chActive.CheckValue = null;
            this.chActive.DataPropertyName = "Active";
            this.chActive.HeaderText = "Active";
            this.chActive.Name = "chActive";
            // 
            // chIsPublic
            // 
            this.chIsPublic.Checked = true;
            this.chIsPublic.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chIsPublic.CheckValue = null;
            this.chIsPublic.DataPropertyName = "IsPublic";
            this.chIsPublic.HeaderText = "Public";
            this.chIsPublic.Name = "chIsPublic";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtFilter.Border.Class = "TextBoxBorder";
            this.txtFilter.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFilter.Location = new System.Drawing.Point(56, 48);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(716, 25);
            this.txtFilter.TabIndex = 3;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 50);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(38, 21);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "Filter";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(76, 21);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "DSNS Host";
            // 
            // txtDSNSHost
            // 
            this.txtDSNSHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDSNSHost.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Manager.Properties.Settings.Default, "DSNSHost", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDSNSHost.DisplayMember = "Text";
            this.txtDSNSHost.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtDSNSHost.FormattingEnabled = true;
            this.txtDSNSHost.ItemHeight = 19;
            this.txtDSNSHost.Items.AddRange(new object[] {
            this.comboItem2});
            this.txtDSNSHost.Location = new System.Drawing.Point(94, 10);
            this.txtDSNSHost.Name = "txtDSNSHost";
            this.txtDSNSHost.Size = new System.Drawing.Size(596, 25);
            this.txtDSNSHost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtDSNSHost.TabIndex = 10;
            this.txtDSNSHost.Text = global::Manager.Properties.Settings.Default.DSNSHost;
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "http://dsns.ischool.com.tw/dsns/dsns/setup";
            // 
            // btnDownload
            // 
            this.btnDownload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDownload.Location = new System.Drawing.Point(697, 10);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDownload.TabIndex = 11;
            this.btnDownload.Text = "下載資料";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnBackupExcel
            // 
            this.btnBackupExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackupExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackupExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBackupExcel.Location = new System.Drawing.Point(196, 527);
            this.btnBackupExcel.Name = "btnBackupExcel";
            this.btnBackupExcel.Size = new System.Drawing.Size(75, 23);
            this.btnBackupExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBackupExcel.TabIndex = 0;
            this.btnBackupExcel.Text = "備份 Excel";
            this.btnBackupExcel.Click += new System.EventHandler(this.btnBackupExcel_Click);
            // 
            // btnRestoreExcel
            // 
            this.btnRestoreExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestoreExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRestoreExcel.Location = new System.Drawing.Point(278, 527);
            this.btnRestoreExcel.Name = "btnRestoreExcel";
            this.btnRestoreExcel.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRestoreExcel.TabIndex = 1;
            this.btnRestoreExcel.Text = "回存 Excel";
            this.btnRestoreExcel.Click += new System.EventHandler(this.btnRestoreExcel_Click);
            // 
            // MainManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtDSNSHost);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.dgvDSNSList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRestoreExcel);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBackupExcel);
            this.Controls.Add(this.btnBackup);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "MainManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DS Name Service 管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainManager_FormClosed);
            this.Load += new System.EventHandler(this.MainManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSNSList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnBackup;
        private DevComponents.DotNetBar.ButtonX btnRestore;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDSNSList;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFilter;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSecuredUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn chTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn chMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chCatalog;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn chActive;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn chIsPublic;
        private DevComponents.DotNetBar.Controls.ComboBoxEx txtDSNSHost;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.ButtonX btnDownload;
        private DevComponents.DotNetBar.ButtonX btnBackupExcel;
        private DevComponents.DotNetBar.ButtonX btnRestoreExcel;
    }
}