namespace Manager
{
    partial class ServerManagePanel
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.VisualStudio2005SyntaxEditorRenderer visualStudio2005SyntaxEditorRenderer1 = new ActiproSoftware.SyntaxEditor.VisualStudio2005SyntaxEditorRenderer();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel5 = new DevComponents.DotNetBar.TabControlPanel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddSchool = new DevComponents.DotNetBar.ButtonItem();
            this.btnSetupDefaultSchool = new DevComponents.DotNetBar.ButtonItem();
            this.btnListDatabase = new DevComponents.DotNetBar.ButtonItem();
            this.btnDBWindow = new DevComponents.DotNetBar.ButtonItem();
            this.btnImport = new DevComponents.DotNetBar.ButtonItem();
            this.btnExport = new DevComponents.DotNetBar.ButtonItem();
            this.btnSetTemplate = new DevComponents.DotNetBar.ButtonItem();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.schoolManager = new DevComponents.DotNetBar.ButtonItem();
            this.btnTestConn = new DevComponents.DotNetBar.ButtonItem();
            this.mcTestDSNS = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangeAppConfig = new DevComponents.DotNetBar.ButtonItem();
            this.mcChangeArgument = new DevComponents.DotNetBar.ButtonItem();
            this.mcResetDBPermission = new DevComponents.DotNetBar.ButtonItem();
            this.mcChangeEnabledStatus = new DevComponents.DotNetBar.ButtonItem();
            this.mcRename = new DevComponents.DotNetBar.ButtonItem();
            this.btnSpecUpgrade = new DevComponents.DotNetBar.ButtonItem();
            this.btnExportApps = new DevComponents.DotNetBar.ButtonItem();
            this.mcDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btnRiskFeature = new DevComponents.DotNetBar.ButtonItem();
            this.btnDebugDB = new DevComponents.DotNetBar.ButtonItem();
            this.mc1RunSingleSql = new DevComponents.DotNetBar.ButtonItem();
            this.mcResetSecureCode = new DevComponents.DotNetBar.ButtonItem();
            this.dgvSchoolManageList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFilterPattern = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblSelectionCount = new DevComponents.DotNetBar.LabelX();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.SManager = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX22 = new DevComponents.DotNetBar.LabelX();
            this.txtDBMSVersion = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX20 = new DevComponents.DotNetBar.LabelX();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtAccessPointUrl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnChangeLogSetting = new DevComponents.DotNetBar.ButtonX();
            this.btnChangePassword = new DevComponents.DotNetBar.ButtonX();
            this.txtCoreVersion = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtServiceLastUpdate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtServiceVersion = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCompress = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLogUDS = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLogDB = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtLogOpt = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.txtLogProcess = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtServerType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtCoreLastUpate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtLogEnabled = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.synMemo = new Manager.TextEditor.BaseSyntaxEditor();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btnSaveConfig = new DevComponents.DotNetBar.ButtonItem();
            this.btnRawEdit = new DevComponents.DotNetBar.ButtonItem();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.txtSDUpdateUrl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.txtSDUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.txtSIUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSIUpdateUrl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.txtSIPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSDPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvLoadbalance = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SConfig = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtDifferenceTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.txtServerProcessTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX26 = new DevComponents.DotNetBar.LabelX();
            this.txtClientWaitTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX25 = new DevComponents.DotNetBar.LabelX();
            this.txtReceiveSize = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX24 = new DevComponents.DotNetBar.LabelX();
            this.txtSendSize = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX23 = new DevComponents.DotNetBar.LabelX();
            this.btnLoadFile = new DevComponents.DotNetBar.ButtonX();
            this.dgvTraffics = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSendSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chReceiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chClientWait = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chServerProcessTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDifferenceTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX21 = new DevComponents.DotNetBar.LabelX();
            this.txtTrafficFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchoolManageList)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadbalance)).BeginInit();
            this.tabControlPanel1.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraffics)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.CanReorderTabs = false;
            this.tabControl1.Controls.Add(this.tabControlPanel5);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 768);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.SManager);
            this.tabControl1.Tabs.Add(this.SConfig);
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel5
            // 
            this.tabControlPanel5.Controls.Add(this.bar1);
            this.tabControlPanel5.Controls.Add(this.contextMenuBar1);
            this.tabControlPanel5.Controls.Add(this.txtFilterPattern);
            this.tabControlPanel5.Controls.Add(this.lblSelectionCount);
            this.tabControlPanel5.Controls.Add(this.labelX18);
            this.tabControlPanel5.Controls.Add(this.dgvSchoolManageList);
            this.tabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel5.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel5.Name = "tabControlPanel5";
            this.tabControlPanel5.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel5.Size = new System.Drawing.Size(1024, 740);
            this.tabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel5.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel5.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel5.Style.GradientAngle = 90;
            this.tabControlPanel5.TabIndex = 5;
            this.tabControlPanel5.TabItem = this.SManager;
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddSchool,
            this.btnSetupDefaultSchool,
            this.btnListDatabase,
            this.btnDBWindow,
            this.btnImport,
            this.btnExport,
            this.btnSetTemplate});
            this.bar1.Location = new System.Drawing.Point(1, 1);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1022, 35);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar1.TabIndex = 9;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnAddSchool
            // 
            this.btnAddSchool.Image = global::Manager.Images.Add_Database_25;
            this.btnAddSchool.Name = "btnAddSchool";
            this.btnAddSchool.Text = "新增學校";
            this.btnAddSchool.Tooltip = "新增學校";
            this.btnAddSchool.Click += new System.EventHandler(this.btnAddApp_Click);
            // 
            // btnSetupDefaultSchool
            // 
            this.btnSetupDefaultSchool.Image = global::Manager.Images.Settings_25;
            this.btnSetupDefaultSchool.Name = "btnSetupDefaultSchool";
            this.btnSetupDefaultSchool.Text = "變更預設連線設定。";
            this.btnSetupDefaultSchool.Tooltip = "變更預設連線設定，當建立新學校時會使用此預設值。";
            this.btnSetupDefaultSchool.Click += new System.EventHandler(this.btnSetupDefaultSchool_Click);
            // 
            // btnListDatabase
            // 
            this.btnListDatabase.Image = global::Manager.Images.Database_25;
            this.btnListDatabase.Name = "btnListDatabase";
            this.btnListDatabase.Text = "資料庫清單";
            this.btnListDatabase.Tooltip = "資料庫清單";
            this.btnListDatabase.Click += new System.EventHandler(this.btnListDatabase_Click);
            // 
            // btnDBWindow
            // 
            this.btnDBWindow.Image = global::Manager.Images.SQL_25;
            this.btnDBWindow.Name = "btnDBWindow";
            this.btnDBWindow.Text = "DBWindow";
            this.btnDBWindow.Tooltip = "SQL Command Window";
            this.btnDBWindow.Visible = false;
            this.btnDBWindow.Click += new System.EventHandler(this.btnDBWindow_Click);
            // 
            // btnImport
            // 
            this.btnImport.Image = global::Manager.Images.Import_26;
            this.btnImport.Name = "btnImport";
            this.btnImport.Text = "匯入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::Manager.Images.Export_26;
            this.btnExport.Name = "btnExport";
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSetTemplate
            // 
            this.btnSetTemplate.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnSetTemplate.Name = "btnSetTemplate";
            this.btnSetTemplate.Text = "樣版資料庫：";
            this.btnSetTemplate.Click += new System.EventHandler(this.btnSetTemplate_Click);
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.schoolManager});
            this.contextMenuBar1.Location = new System.Drawing.Point(174, 41);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(128, 26);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.contextMenuBar1.TabIndex = 8;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // schoolManager
            // 
            this.schoolManager.AutoExpandOnClick = true;
            this.schoolManager.Name = "schoolManager";
            this.schoolManager.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnTestConn,
            this.mcTestDSNS,
            this.btnChangeAppConfig,
            this.mcRename,
            this.btnSpecUpgrade,
            this.btnExportApps,
            this.mcDelete,
            this.btnRiskFeature});
            this.schoolManager.Text = "SchoolManager";
            this.schoolManager.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.Context_PopupOpen);
            // 
            // btnTestConn
            // 
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Text = "測試連線(資料庫)";
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click_1);
            // 
            // mcTestDSNS
            // 
            this.mcTestDSNS.Name = "mcTestDSNS";
            this.mcTestDSNS.Text = "測試 DSNS";
            this.mcTestDSNS.Click += new System.EventHandler(this.btnTestDSNS_Click);
            // 
            // btnChangeAppConfig
            // 
            this.btnChangeAppConfig.BeginGroup = true;
            this.btnChangeAppConfig.Name = "btnChangeAppConfig";
            this.btnChangeAppConfig.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mcChangeArgument,
            this.mcResetDBPermission,
            this.mcChangeEnabledStatus});
            this.btnChangeAppConfig.Text = "變更設定...";
            // 
            // mcChangeArgument
            // 
            this.mcChangeArgument.BeginGroup = true;
            this.mcChangeArgument.Name = "mcChangeArgument";
            this.mcChangeArgument.Text = "連線設定(資料庫)";
            this.mcChangeArgument.Click += new System.EventHandler(this.btnChangeArgument_Click);
            // 
            // mcResetDBPermission
            // 
            this.mcResetDBPermission.Name = "mcResetDBPermission";
            this.mcResetDBPermission.Text = "重設權限(資料庫)";
            this.mcResetDBPermission.Click += new System.EventHandler(this.btnResetDBPermission_Click);
            // 
            // mcChangeEnabledStatus
            // 
            this.mcChangeEnabledStatus.Name = "mcChangeEnabledStatus";
            this.mcChangeEnabledStatus.Text = "啟用/停用學校";
            this.mcChangeEnabledStatus.Click += new System.EventHandler(this.btnChangeEnableStatus_Click);
            // 
            // mcRename
            // 
            this.mcRename.Name = "mcRename";
            this.mcRename.Text = "重新命名";
            this.mcRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSpecUpgrade
            // 
            this.btnSpecUpgrade.Name = "btnSpecUpgrade";
            this.btnSpecUpgrade.Text = "升級學校設定";
            this.btnSpecUpgrade.Visible = false;
            this.btnSpecUpgrade.Click += new System.EventHandler(this.btnSpecUpgrade_Click);
            // 
            // btnExportApps
            // 
            this.btnExportApps.Name = "btnExportApps";
            this.btnExportApps.Text = "匯出設定";
            this.btnExportApps.Visible = false;
            // 
            // mcDelete
            // 
            this.mcDelete.BeginGroup = true;
            this.mcDelete.Image = global::Manager.Images.Close;
            this.mcDelete.Name = "mcDelete";
            this.mcDelete.Text = "刪除學校";
            this.mcDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRiskFeature
            // 
            this.btnRiskFeature.BeginGroup = true;
            this.btnRiskFeature.ForeColor = System.Drawing.Color.Red;
            this.btnRiskFeature.Name = "btnRiskFeature";
            this.btnRiskFeature.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnDebugDB,
            this.mc1RunSingleSql,
            this.mcResetSecureCode});
            this.btnRiskFeature.Text = "危險功能...";
            this.btnRiskFeature.Visible = false;
            // 
            // btnDebugDB
            // 
            this.btnDebugDB.ForeColor = System.Drawing.Color.Red;
            this.btnDebugDB.Name = "btnDebugDB";
            this.btnDebugDB.Text = "資料庫命令視窗";
            this.btnDebugDB.Visible = false;
            this.btnDebugDB.Click += new System.EventHandler(this.btnDebugDB_Click);
            // 
            // mc1RunSingleSql
            // 
            this.mc1RunSingleSql.ForeColor = System.Drawing.Color.Red;
            this.mc1RunSingleSql.Name = "mc1RunSingleSql";
            this.mc1RunSingleSql.Text = "升級 Schema";
            this.mc1RunSingleSql.Click += new System.EventHandler(this.btnRunSingleSql_Click);
            // 
            // mcResetSecureCode
            // 
            this.mcResetSecureCode.BeginGroup = true;
            this.mcResetSecureCode.ForeColor = System.Drawing.Color.Red;
            this.mcResetSecureCode.Name = "mcResetSecureCode";
            this.mcResetSecureCode.Text = "重設安全代碼";
            this.mcResetSecureCode.Click += new System.EventHandler(this.btnResetSecureCode_Click);
            // 
            // dgvSchoolManageList
            // 
            this.dgvSchoolManageList.AllowUserToAddRows = false;
            this.dgvSchoolManageList.AllowUserToDeleteRows = false;
            this.dgvSchoolManageList.AllowUserToResizeRows = false;
            this.dgvSchoolManageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchoolManageList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvSchoolManageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchoolManageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.chDBName,
            this.Column3,
            this.Column4});
            this.contextMenuBar1.SetContextMenuEx(this.dgvSchoolManageList, this.schoolManager);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchoolManageList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSchoolManageList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvSchoolManageList.HighlightSelectedColumnHeaders = false;
            this.dgvSchoolManageList.Location = new System.Drawing.Point(16, 74);
            this.dgvSchoolManageList.Name = "dgvSchoolManageList";
            this.dgvSchoolManageList.PaintEnhancedSelection = false;
            this.dgvSchoolManageList.ReadOnly = true;
            this.dgvSchoolManageList.RowHeadersVisible = false;
            this.dgvSchoolManageList.RowTemplate.Height = 24;
            this.dgvSchoolManageList.SelectAllSignVisible = false;
            this.dgvSchoolManageList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchoolManageList.Size = new System.Drawing.Size(994, 653);
            this.dgvSchoolManageList.TabIndex = 1;
            this.dgvSchoolManageList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchoolManageList_CellDoubleClick);
            this.dgvSchoolManageList.SelectionChanged += new System.EventHandler(this.dgvSchoolManageList_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "學校名稱";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // chDBName
            // 
            this.chDBName.HeaderText = "資料庫名稱";
            this.chDBName.Name = "chDBName";
            this.chDBName.ReadOnly = true;
            this.chDBName.Width = 160;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "啟用";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "備註";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // txtFilterPattern
            // 
            this.txtFilterPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtFilterPattern.Border.Class = "TextBoxBorder";
            this.txtFilterPattern.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFilterPattern.ButtonCustom.Text = "X";
            this.txtFilterPattern.ButtonCustom.Visible = true;
            this.txtFilterPattern.HideSelection = false;
            this.txtFilterPattern.Location = new System.Drawing.Point(828, 43);
            this.txtFilterPattern.Name = "txtFilterPattern";
            this.txtFilterPattern.Size = new System.Drawing.Size(182, 25);
            this.txtFilterPattern.TabIndex = 5;
            this.txtFilterPattern.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
            this.txtFilterPattern.WatermarkText = "輸入字串過慮學校清單";
            this.txtFilterPattern.ButtonCustomClick += new System.EventHandler(this.txtFilterPattern_ButtonCustomClick);
            this.txtFilterPattern.TextChanged += new System.EventHandler(this.txtFilterPattern_TextChanged);
            // 
            // lblSelectionCount
            // 
            this.lblSelectionCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSelectionCount.BackgroundStyle.Class = "";
            this.lblSelectionCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSelectionCount.Location = new System.Drawing.Point(93, 44);
            this.lblSelectionCount.Name = "lblSelectionCount";
            this.lblSelectionCount.Size = new System.Drawing.Size(75, 23);
            this.lblSelectionCount.TabIndex = 2;
            this.lblSelectionCount.Text = "0";
            // 
            // labelX18
            // 
            this.labelX18.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.Class = "";
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(16, 44);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(75, 23);
            this.labelX18.TabIndex = 2;
            this.labelX18.Text = "學校清單";
            // 
            // SManager
            // 
            this.SManager.AttachedControl = this.tabControlPanel5;
            this.SManager.Name = "SManager";
            this.SManager.Text = "School Manager";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.groupPanel2);
            this.tabControlPanel2.Controls.Add(this.synMemo);
            this.tabControlPanel2.Controls.Add(this.bar2);
            this.tabControlPanel2.Controls.Add(this.groupPanel1);
            this.tabControlPanel2.Controls.Add(this.dgvLoadbalance);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1024, 740);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 0;
            this.tabControlPanel2.TabItem = this.SConfig;
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.labelX22);
            this.groupPanel2.Controls.Add(this.txtDBMSVersion);
            this.groupPanel2.Controls.Add(this.labelX20);
            this.groupPanel2.Controls.Add(this.labelX17);
            this.groupPanel2.Controls.Add(this.labelX1);
            this.groupPanel2.Controls.Add(this.txtAccessPointUrl);
            this.groupPanel2.Controls.Add(this.btnChangeLogSetting);
            this.groupPanel2.Controls.Add(this.btnChangePassword);
            this.groupPanel2.Controls.Add(this.txtCoreVersion);
            this.groupPanel2.Controls.Add(this.txtServiceLastUpdate);
            this.groupPanel2.Controls.Add(this.labelX4);
            this.groupPanel2.Controls.Add(this.txtServiceVersion);
            this.groupPanel2.Controls.Add(this.txtCompress);
            this.groupPanel2.Controls.Add(this.txtLogUDS);
            this.groupPanel2.Controls.Add(this.labelX10);
            this.groupPanel2.Controls.Add(this.labelX7);
            this.groupPanel2.Controls.Add(this.labelX9);
            this.groupPanel2.Controls.Add(this.txtUserName);
            this.groupPanel2.Controls.Add(this.txtLogDB);
            this.groupPanel2.Controls.Add(this.txtLogOpt);
            this.groupPanel2.Controls.Add(this.labelX19);
            this.groupPanel2.Controls.Add(this.txtLogProcess);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Controls.Add(this.labelX8);
            this.groupPanel2.Controls.Add(this.txtServerType);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.txtCoreLastUpate);
            this.groupPanel2.Controls.Add(this.labelX5);
            this.groupPanel2.Controls.Add(this.txtLogEnabled);
            this.groupPanel2.Location = new System.Drawing.Point(12, 45);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(547, 337);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 21;
            this.groupPanel2.Text = "一般資訊";
            // 
            // labelX22
            // 
            this.labelX22.AutoSize = true;
            this.labelX22.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX22.BackgroundStyle.Class = "";
            this.labelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX22.Location = new System.Drawing.Point(13, 154);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new System.Drawing.Size(115, 21);
            this.labelX22.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX22.TabIndex = 15;
            this.labelX22.Text = "Server DBMS版本";
            // 
            // txtDBMSVersion
            // 
            this.txtDBMSVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBMSVersion.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtDBMSVersion.Border.Class = "TextBoxBorder";
            this.txtDBMSVersion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDBMSVersion.Location = new System.Drawing.Point(128, 152);
            this.txtDBMSVersion.Name = "txtDBMSVersion";
            this.txtDBMSVersion.ReadOnly = true;
            this.txtDBMSVersion.Size = new System.Drawing.Size(399, 25);
            this.txtDBMSVersion.TabIndex = 14;
            // 
            // labelX20
            // 
            this.labelX20.AutoSize = true;
            this.labelX20.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX20.BackgroundStyle.Class = "";
            this.labelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX20.Location = new System.Drawing.Point(350, 124);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new System.Drawing.Size(60, 21);
            this.labelX20.TabIndex = 13;
            this.labelX20.Text = "核心版本";
            // 
            // labelX17
            // 
            this.labelX17.AutoSize = true;
            this.labelX17.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.Class = "";
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(350, 93);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(60, 21);
            this.labelX17.TabIndex = 13;
            this.labelX17.Text = "服務版本";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(74, 21);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "存取點位置";
            // 
            // txtAccessPointUrl
            // 
            this.txtAccessPointUrl.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtAccessPointUrl.Border.Class = "TextBoxBorder";
            this.txtAccessPointUrl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAccessPointUrl.Location = new System.Drawing.Point(128, -2);
            this.txtAccessPointUrl.Name = "txtAccessPointUrl";
            this.txtAccessPointUrl.ReadOnly = true;
            this.txtAccessPointUrl.Size = new System.Drawing.Size(399, 25);
            this.txtAccessPointUrl.TabIndex = 0;
            this.txtAccessPointUrl.Text = "http://localhost:8080/is4/mamanger";
            // 
            // btnChangeLogSetting
            // 
            this.btnChangeLogSetting.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangeLogSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeLogSetting.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChangeLogSetting.Enabled = false;
            this.btnChangeLogSetting.Location = new System.Drawing.Point(78, 184);
            this.btnChangeLogSetting.Name = "btnChangeLogSetting";
            this.btnChangeLogSetting.Size = new System.Drawing.Size(31, 23);
            this.btnChangeLogSetting.TabIndex = 12;
            this.btnChangeLogSetting.Text = "...";
            this.btnChangeLogSetting.Click += new System.EventHandler(this.btnChangeLogSetting_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChangePassword.Enabled = false;
            this.btnChangePassword.Location = new System.Drawing.Point(454, 31);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(73, 23);
            this.btnChangePassword.TabIndex = 11;
            this.btnChangePassword.Text = "變更密碼";
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // txtCoreVersion
            // 
            this.txtCoreVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCoreVersion.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtCoreVersion.Border.Class = "TextBoxBorder";
            this.txtCoreVersion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCoreVersion.Location = new System.Drawing.Point(417, 122);
            this.txtCoreVersion.Name = "txtCoreVersion";
            this.txtCoreVersion.ReadOnly = true;
            this.txtCoreVersion.Size = new System.Drawing.Size(110, 25);
            this.txtCoreVersion.TabIndex = 0;
            this.txtCoreVersion.Text = "4.0";
            // 
            // txtServiceLastUpdate
            // 
            this.txtServiceLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceLastUpdate.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtServiceLastUpdate.Border.Class = "TextBoxBorder";
            this.txtServiceLastUpdate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServiceLastUpdate.Location = new System.Drawing.Point(128, 91);
            this.txtServiceLastUpdate.Name = "txtServiceLastUpdate";
            this.txtServiceLastUpdate.ReadOnly = true;
            this.txtServiceLastUpdate.Size = new System.Drawing.Size(215, 25);
            this.txtServiceLastUpdate.TabIndex = 3;
            this.txtServiceLastUpdate.Text = "2010-08-05 11:46:36.229+08";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 93);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(114, 21);
            this.labelX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "組態最後更動日期";
            // 
            // txtServiceVersion
            // 
            this.txtServiceVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServiceVersion.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtServiceVersion.Border.Class = "TextBoxBorder";
            this.txtServiceVersion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServiceVersion.Location = new System.Drawing.Point(417, 91);
            this.txtServiceVersion.Name = "txtServiceVersion";
            this.txtServiceVersion.ReadOnly = true;
            this.txtServiceVersion.Size = new System.Drawing.Size(110, 25);
            this.txtServiceVersion.TabIndex = 0;
            this.txtServiceVersion.Text = "1.0.0.0";
            // 
            // txtCompress
            // 
            this.txtCompress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompress.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtCompress.Border.Class = "TextBoxBorder";
            this.txtCompress.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCompress.Location = new System.Drawing.Point(320, 245);
            this.txtCompress.Name = "txtCompress";
            this.txtCompress.ReadOnly = true;
            this.txtCompress.Size = new System.Drawing.Size(107, 25);
            this.txtCompress.TabIndex = 9;
            this.txtCompress.Text = "否";
            // 
            // txtLogUDS
            // 
            this.txtLogUDS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogUDS.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtLogUDS.Border.Class = "TextBoxBorder";
            this.txtLogUDS.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLogUDS.Location = new System.Drawing.Point(320, 214);
            this.txtLogUDS.Name = "txtLogUDS";
            this.txtLogUDS.ReadOnly = true;
            this.txtLogUDS.Size = new System.Drawing.Size(107, 25);
            this.txtLogUDS.TabIndex = 9;
            this.txtLogUDS.Text = "否";
            // 
            // labelX10
            // 
            this.labelX10.AutoSize = true;
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(13, 31);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(74, 21);
            this.labelX10.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX10.TabIndex = 10;
            this.labelX10.Text = "使用者名稱";
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(249, 247);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(60, 21);
            this.labelX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX7.TabIndex = 9;
            this.labelX7.Text = "壓縮資料";
            // 
            // labelX9
            // 
            this.labelX9.AutoSize = true;
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(249, 216);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(65, 21);
            this.labelX9.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX9.TabIndex = 9;
            this.labelX9.Text = "記錄 UDS";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtUserName.Border.Class = "TextBoxBorder";
            this.txtUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserName.Location = new System.Drawing.Point(128, 29);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(320, 25);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "admin";
            // 
            // txtLogDB
            // 
            this.txtLogDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogDB.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtLogDB.Border.Class = "TextBoxBorder";
            this.txtLogDB.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLogDB.Location = new System.Drawing.Point(128, 276);
            this.txtLogDB.Name = "txtLogDB";
            this.txtLogDB.ReadOnly = true;
            this.txtLogDB.Size = new System.Drawing.Size(299, 25);
            this.txtLogDB.TabIndex = 8;
            // 
            // txtLogOpt
            // 
            this.txtLogOpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogOpt.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtLogOpt.Border.Class = "TextBoxBorder";
            this.txtLogOpt.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLogOpt.Location = new System.Drawing.Point(128, 245);
            this.txtLogOpt.Name = "txtLogOpt";
            this.txtLogOpt.ReadOnly = true;
            this.txtLogOpt.Size = new System.Drawing.Size(107, 25);
            this.txtLogOpt.TabIndex = 8;
            this.txtLogOpt.Text = "Always";
            // 
            // labelX19
            // 
            this.labelX19.AutoSize = true;
            this.labelX19.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX19.BackgroundStyle.Class = "";
            this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX19.Location = new System.Drawing.Point(24, 278);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new System.Drawing.Size(74, 21);
            this.labelX19.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX19.TabIndex = 7;
            this.labelX19.Text = "日誌資料庫";
            // 
            // txtLogProcess
            // 
            this.txtLogProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogProcess.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtLogProcess.Border.Class = "TextBoxBorder";
            this.txtLogProcess.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLogProcess.Location = new System.Drawing.Point(128, 214);
            this.txtLogProcess.Name = "txtLogProcess";
            this.txtLogProcess.ReadOnly = true;
            this.txtLogProcess.Size = new System.Drawing.Size(107, 25);
            this.txtLogProcess.TabIndex = 8;
            this.txtLogProcess.Text = "是";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(24, 247);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 21);
            this.labelX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX6.TabIndex = 7;
            this.labelX6.Text = "記錄時機";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 62);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "運作類型";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(24, 216);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(87, 21);
            this.labelX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX8.TabIndex = 7;
            this.labelX8.Text = "記錄處理細節";
            // 
            // txtServerType
            // 
            this.txtServerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerType.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtServerType.Border.Class = "TextBoxBorder";
            this.txtServerType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServerType.Location = new System.Drawing.Point(128, 60);
            this.txtServerType.Name = "txtServerType";
            this.txtServerType.ReadOnly = true;
            this.txtServerType.Size = new System.Drawing.Size(399, 25);
            this.txtServerType.TabIndex = 2;
            this.txtServerType.Text = "服務分享式(SharedService)";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 124);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(114, 21);
            this.labelX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "核心最後更新日期";
            // 
            // txtCoreLastUpate
            // 
            this.txtCoreLastUpate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCoreLastUpate.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtCoreLastUpate.Border.Class = "TextBoxBorder";
            this.txtCoreLastUpate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCoreLastUpate.Location = new System.Drawing.Point(128, 122);
            this.txtCoreLastUpate.Name = "txtCoreLastUpate";
            this.txtCoreLastUpate.ReadOnly = true;
            this.txtCoreLastUpate.Size = new System.Drawing.Size(215, 25);
            this.txtCoreLastUpate.TabIndex = 4;
            this.txtCoreLastUpate.Text = "2010-08-04T04:54:50.526294Z";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(13, 184);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 21);
            this.labelX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.labelX5.TabIndex = 5;
            this.labelX5.Text = "啟用日誌";
            // 
            // txtLogEnabled
            // 
            this.txtLogEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogEnabled.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.txtLogEnabled.Border.Class = "TextBoxBorder";
            this.txtLogEnabled.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLogEnabled.Location = new System.Drawing.Point(128, 182);
            this.txtLogEnabled.Name = "txtLogEnabled";
            this.txtLogEnabled.ReadOnly = true;
            this.txtLogEnabled.Size = new System.Drawing.Size(107, 25);
            this.txtLogEnabled.TabIndex = 5;
            this.txtLogEnabled.Text = "是";
            // 
            // synMemo
            // 
            this.synMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            document1.LexicalParsingEnabled = false;
            document1.SemanticParsingEnabled = false;
            this.synMemo.Document = document1;
            this.synMemo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.synMemo.IndicatorMarginVisible = false;
            this.synMemo.Location = new System.Drawing.Point(12, 633);
            this.synMemo.Name = "synMemo";
            visualStudio2005SyntaxEditorRenderer1.ResetAllPropertiesOnSystemColorChange = false;
            this.synMemo.Renderer = visualStudio2005SyntaxEditorRenderer1;
            this.synMemo.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.Vertical;
            this.synMemo.Size = new System.Drawing.Size(999, 98);
            this.synMemo.TabIndex = 10;
            this.synMemo.TextChanged += new System.EventHandler(this.synMemo_TextChanged);
            // 
            // bar2
            // 
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveConfig,
            this.btnRawEdit});
            this.bar2.Location = new System.Drawing.Point(1, 1);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(1022, 33);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar2.TabIndex = 20;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Enabled = false;
            this.btnSaveConfig.Image = global::Manager.Images.Save;
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Text = "buttonItem1";
            this.btnSaveConfig.Visible = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnRawEdit
            // 
            this.btnRawEdit.Name = "btnRawEdit";
            this.btnRawEdit.Text = "編輯設定";
            this.btnRawEdit.Tooltip = "直接編輯 Server 的 Xml 設定";
            this.btnRawEdit.Click += new System.EventHandler(this.btnRawEdit_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX11);
            this.groupPanel1.Controls.Add(this.txtSDUpdateUrl);
            this.groupPanel1.Controls.Add(this.labelX13);
            this.groupPanel1.Controls.Add(this.txtSDUserName);
            this.groupPanel1.Controls.Add(this.labelX15);
            this.groupPanel1.Controls.Add(this.txtSIUserName);
            this.groupPanel1.Controls.Add(this.txtSIUpdateUrl);
            this.groupPanel1.Controls.Add(this.labelX14);
            this.groupPanel1.Controls.Add(this.labelX12);
            this.groupPanel1.Controls.Add(this.labelX16);
            this.groupPanel1.Controls.Add(this.txtSIPassword);
            this.groupPanel1.Controls.Add(this.txtSDPassword);
            this.groupPanel1.Location = new System.Drawing.Point(12, 389);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(547, 238);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 19;
            this.groupPanel1.Text = "更新設定";
            // 
            // labelX11
            // 
            this.labelX11.AutoSize = true;
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(17, 9);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(69, 21);
            this.labelX11.TabIndex = 0;
            this.labelX11.Text = "Definition";
            // 
            // txtSDUpdateUrl
            // 
            this.txtSDUpdateUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSDUpdateUrl.Border.Class = "TextBoxBorder";
            this.txtSDUpdateUrl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSDUpdateUrl.Location = new System.Drawing.Point(92, 7);
            this.txtSDUpdateUrl.Name = "txtSDUpdateUrl";
            this.txtSDUpdateUrl.ReadOnly = true;
            this.txtSDUpdateUrl.Size = new System.Drawing.Size(435, 25);
            this.txtSDUpdateUrl.TabIndex = 1;
            // 
            // labelX13
            // 
            this.labelX13.AutoSize = true;
            this.labelX13.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.Class = "";
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(13, 40);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(73, 21);
            this.labelX13.TabIndex = 2;
            this.labelX13.Text = "UserName";
            // 
            // txtSDUserName
            // 
            this.txtSDUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSDUserName.Border.Class = "TextBoxBorder";
            this.txtSDUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSDUserName.Location = new System.Drawing.Point(92, 38);
            this.txtSDUserName.Name = "txtSDUserName";
            this.txtSDUserName.ReadOnly = true;
            this.txtSDUserName.Size = new System.Drawing.Size(435, 25);
            this.txtSDUserName.TabIndex = 3;
            // 
            // labelX15
            // 
            this.labelX15.AutoSize = true;
            this.labelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.Class = "";
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(13, 147);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(73, 21);
            this.labelX15.TabIndex = 8;
            this.labelX15.Text = "UserName";
            // 
            // txtSIUserName
            // 
            this.txtSIUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSIUserName.Border.Class = "TextBoxBorder";
            this.txtSIUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSIUserName.Location = new System.Drawing.Point(92, 145);
            this.txtSIUserName.Name = "txtSIUserName";
            this.txtSIUserName.ReadOnly = true;
            this.txtSIUserName.Size = new System.Drawing.Size(435, 25);
            this.txtSIUserName.TabIndex = 9;
            // 
            // txtSIUpdateUrl
            // 
            this.txtSIUpdateUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSIUpdateUrl.Border.Class = "TextBoxBorder";
            this.txtSIUpdateUrl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSIUpdateUrl.Location = new System.Drawing.Point(92, 112);
            this.txtSIUpdateUrl.Name = "txtSIUpdateUrl";
            this.txtSIUpdateUrl.ReadOnly = true;
            this.txtSIUpdateUrl.Size = new System.Drawing.Size(435, 25);
            this.txtSIUpdateUrl.TabIndex = 7;
            // 
            // labelX14
            // 
            this.labelX14.AutoSize = true;
            this.labelX14.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.Class = "";
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(19, 72);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(67, 21);
            this.labelX14.TabIndex = 4;
            this.labelX14.Text = "Password";
            // 
            // labelX12
            // 
            this.labelX12.AutoSize = true;
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.Class = "";
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(4, 114);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(82, 21);
            this.labelX12.TabIndex = 6;
            this.labelX12.Text = "Component";
            // 
            // labelX16
            // 
            this.labelX16.AutoSize = true;
            this.labelX16.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.Class = "";
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(19, 180);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(67, 21);
            this.labelX16.TabIndex = 10;
            this.labelX16.Text = "Password";
            // 
            // txtSIPassword
            // 
            this.txtSIPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSIPassword.Border.Class = "TextBoxBorder";
            this.txtSIPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSIPassword.Location = new System.Drawing.Point(92, 178);
            this.txtSIPassword.Name = "txtSIPassword";
            this.txtSIPassword.ReadOnly = true;
            this.txtSIPassword.Size = new System.Drawing.Size(435, 25);
            this.txtSIPassword.TabIndex = 11;
            this.txtSIPassword.UseSystemPasswordChar = true;
            // 
            // txtSDPassword
            // 
            this.txtSDPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSDPassword.Border.Class = "TextBoxBorder";
            this.txtSDPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSDPassword.Location = new System.Drawing.Point(92, 70);
            this.txtSDPassword.Name = "txtSDPassword";
            this.txtSDPassword.ReadOnly = true;
            this.txtSDPassword.Size = new System.Drawing.Size(435, 25);
            this.txtSDPassword.TabIndex = 5;
            this.txtSDPassword.UseSystemPasswordChar = true;
            // 
            // dgvLoadbalance
            // 
            this.dgvLoadbalance.AllowUserToAddRows = false;
            this.dgvLoadbalance.AllowUserToDeleteRows = false;
            this.dgvLoadbalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoadbalance.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvLoadbalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadbalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLoadbalance.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoadbalance.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLoadbalance.Location = new System.Drawing.Point(565, 46);
            this.dgvLoadbalance.Name = "dgvLoadbalance";
            this.dgvLoadbalance.ReadOnly = true;
            this.dgvLoadbalance.RowTemplate.Height = 24;
            this.dgvLoadbalance.Size = new System.Drawing.Size(446, 581);
            this.dgvLoadbalance.TabIndex = 17;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "負載平衡設定";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // SConfig
            // 
            this.SConfig.AttachedControl = this.tabControlPanel2;
            this.SConfig.Name = "SConfig";
            this.SConfig.Text = "Server Configuration";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.groupPanel3);
            this.tabControlPanel1.Controls.Add(this.btnLoadFile);
            this.tabControlPanel1.Controls.Add(this.dgvTraffics);
            this.tabControlPanel1.Controls.Add(this.labelX21);
            this.tabControlPanel1.Controls.Add(this.txtTrafficFile);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1024, 740);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 6;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // groupPanel3
            // 
            this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.txtDifferenceTime);
            this.groupPanel3.Controls.Add(this.labelX27);
            this.groupPanel3.Controls.Add(this.txtServerProcessTime);
            this.groupPanel3.Controls.Add(this.labelX26);
            this.groupPanel3.Controls.Add(this.txtClientWaitTime);
            this.groupPanel3.Controls.Add(this.labelX25);
            this.groupPanel3.Controls.Add(this.txtReceiveSize);
            this.groupPanel3.Controls.Add(this.labelX24);
            this.groupPanel3.Controls.Add(this.txtSendSize);
            this.groupPanel3.Controls.Add(this.labelX23);
            this.groupPanel3.Location = new System.Drawing.Point(15, 624);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(994, 103);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 3;
            this.groupPanel3.Text = "總計";
            // 
            // txtDifferenceTime
            // 
            // 
            // 
            // 
            this.txtDifferenceTime.Border.Class = "TextBoxBorder";
            this.txtDifferenceTime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDifferenceTime.Location = new System.Drawing.Point(595, 7);
            this.txtDifferenceTime.Name = "txtDifferenceTime";
            this.txtDifferenceTime.ReadOnly = true;
            this.txtDifferenceTime.Size = new System.Drawing.Size(158, 25);
            this.txtDifferenceTime.TabIndex = 3;
            // 
            // labelX27
            // 
            this.labelX27.AutoSize = true;
            this.labelX27.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.Class = "";
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Location = new System.Drawing.Point(522, 9);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(60, 21);
            this.labelX27.TabIndex = 2;
            this.labelX27.Text = "相差時間";
            // 
            // txtServerProcessTime
            // 
            // 
            // 
            // 
            this.txtServerProcessTime.Border.Class = "TextBoxBorder";
            this.txtServerProcessTime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServerProcessTime.Location = new System.Drawing.Point(352, 41);
            this.txtServerProcessTime.Name = "txtServerProcessTime";
            this.txtServerProcessTime.ReadOnly = true;
            this.txtServerProcessTime.Size = new System.Drawing.Size(158, 25);
            this.txtServerProcessTime.TabIndex = 1;
            // 
            // labelX26
            // 
            this.labelX26.AutoSize = true;
            this.labelX26.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX26.BackgroundStyle.Class = "";
            this.labelX26.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX26.Location = new System.Drawing.Point(264, 43);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new System.Drawing.Size(87, 21);
            this.labelX26.TabIndex = 0;
            this.labelX26.Text = "主機秏用時間";
            // 
            // txtClientWaitTime
            // 
            // 
            // 
            // 
            this.txtClientWaitTime.Border.Class = "TextBoxBorder";
            this.txtClientWaitTime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtClientWaitTime.Location = new System.Drawing.Point(352, 7);
            this.txtClientWaitTime.Name = "txtClientWaitTime";
            this.txtClientWaitTime.ReadOnly = true;
            this.txtClientWaitTime.Size = new System.Drawing.Size(158, 25);
            this.txtClientWaitTime.TabIndex = 9;
            // 
            // labelX25
            // 
            this.labelX25.AutoSize = true;
            this.labelX25.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX25.BackgroundStyle.Class = "";
            this.labelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX25.Location = new System.Drawing.Point(264, 9);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new System.Drawing.Size(87, 21);
            this.labelX25.TabIndex = 8;
            this.labelX25.Text = "用戶等待時間";
            // 
            // txtReceiveSize
            // 
            // 
            // 
            // 
            this.txtReceiveSize.Border.Class = "TextBoxBorder";
            this.txtReceiveSize.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtReceiveSize.Location = new System.Drawing.Point(94, 41);
            this.txtReceiveSize.Name = "txtReceiveSize";
            this.txtReceiveSize.ReadOnly = true;
            this.txtReceiveSize.Size = new System.Drawing.Size(158, 25);
            this.txtReceiveSize.TabIndex = 7;
            // 
            // labelX24
            // 
            this.labelX24.AutoSize = true;
            this.labelX24.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX24.BackgroundStyle.Class = "";
            this.labelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX24.Location = new System.Drawing.Point(21, 43);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new System.Drawing.Size(60, 21);
            this.labelX24.TabIndex = 6;
            this.labelX24.Text = "接收大小";
            // 
            // txtSendSize
            // 
            // 
            // 
            // 
            this.txtSendSize.Border.Class = "TextBoxBorder";
            this.txtSendSize.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSendSize.Location = new System.Drawing.Point(94, 7);
            this.txtSendSize.Name = "txtSendSize";
            this.txtSendSize.ReadOnly = true;
            this.txtSendSize.Size = new System.Drawing.Size(158, 25);
            this.txtSendSize.TabIndex = 5;
            // 
            // labelX23
            // 
            this.labelX23.AutoSize = true;
            this.labelX23.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX23.BackgroundStyle.Class = "";
            this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX23.Location = new System.Drawing.Point(21, 9);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new System.Drawing.Size(60, 21);
            this.labelX23.TabIndex = 4;
            this.labelX23.Text = "傳送大小";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLoadFile.Location = new System.Drawing.Point(936, 15);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "載入";
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // dgvTraffics
            // 
            this.dgvTraffics.AllowUserToAddRows = false;
            this.dgvTraffics.AllowUserToDeleteRows = false;
            this.dgvTraffics.AllowUserToResizeRows = false;
            this.dgvTraffics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTraffics.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTraffics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraffics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.chSendSize,
            this.chReceiveSize,
            this.chClientWait,
            this.chServerProcessTime,
            this.chDifferenceTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTraffics.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTraffics.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvTraffics.HighlightSelectedColumnHeaders = false;
            this.dgvTraffics.Location = new System.Drawing.Point(15, 46);
            this.dgvTraffics.Name = "dgvTraffics";
            this.dgvTraffics.ReadOnly = true;
            this.dgvTraffics.RowHeadersVisible = false;
            this.dgvTraffics.RowTemplate.Height = 24;
            this.dgvTraffics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTraffics.Size = new System.Drawing.Size(996, 568);
            this.dgvTraffics.TabIndex = 2;
            this.dgvTraffics.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTraffics_CellDoubleClick);
            this.dgvTraffics.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvTraffics_SortCompare);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "時間";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "服務名稱";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // chSendSize
            // 
            this.chSendSize.HeaderText = "傳送大小";
            this.chSendSize.Name = "chSendSize";
            this.chSendSize.ReadOnly = true;
            // 
            // chReceiveSize
            // 
            this.chReceiveSize.HeaderText = "接收大小";
            this.chReceiveSize.Name = "chReceiveSize";
            this.chReceiveSize.ReadOnly = true;
            // 
            // chClientWait
            // 
            this.chClientWait.HeaderText = "用戶等待時間";
            this.chClientWait.Name = "chClientWait";
            this.chClientWait.ReadOnly = true;
            this.chClientWait.ToolTipText = "用戶端「程式」等待主機回應的時間，不包含程式處理資料的時間。";
            this.chClientWait.Width = 120;
            // 
            // chServerProcessTime
            // 
            this.chServerProcessTime.HeaderText = "主機耗用時間";
            this.chServerProcessTime.Name = "chServerProcessTime";
            this.chServerProcessTime.ReadOnly = true;
            this.chServerProcessTime.ToolTipText = "主機花費多少時間處理該申請文件(Request) Document。";
            this.chServerProcessTime.Width = 120;
            // 
            // chDifferenceTime
            // 
            this.chDifferenceTime.HeaderText = "相差時間";
            this.chDifferenceTime.Name = "chDifferenceTime";
            this.chDifferenceTime.ReadOnly = true;
            // 
            // labelX21
            // 
            this.labelX21.AutoSize = true;
            this.labelX21.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX21.BackgroundStyle.Class = "";
            this.labelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX21.Location = new System.Drawing.Point(15, 16);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new System.Drawing.Size(74, 21);
            this.labelX21.TabIndex = 1;
            this.labelX21.Text = "流量記錄檔";
            // 
            // txtTrafficFile
            // 
            this.txtTrafficFile.AllowDrop = true;
            this.txtTrafficFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTrafficFile.Border.Class = "TextBoxBorder";
            this.txtTrafficFile.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTrafficFile.Location = new System.Drawing.Point(90, 14);
            this.txtTrafficFile.Name = "txtTrafficFile";
            this.txtTrafficFile.Size = new System.Drawing.Size(840, 25);
            this.txtTrafficFile.TabIndex = 0;
            this.txtTrafficFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtTrafficFile_DragDrop);
            this.txtTrafficFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtTrafficFile_DragEnter);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Traffic Diagnostics";
            this.tabItem1.Visible = false;
            // 
            // ServerManagePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "ServerManagePanel";
            this.Size = new System.Drawing.Size(1024, 768);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchoolManageList)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadbalance)).EndInit();
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraffics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem SConfig;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel5;
        private DevComponents.DotNetBar.TabItem SManager;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSDUpdateUrl;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSIUpdateUrl;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSIPassword;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSDPassword;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSIUserName;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSDUserName;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLoadbalance;
        private DevComponents.DotNetBar.LabelX labelX18;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvSchoolManageList;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFilterPattern;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem schoolManager;
        private DevComponents.DotNetBar.ButtonItem mcRename;
        private DevComponents.DotNetBar.ButtonItem mcChangeEnabledStatus;
        private DevComponents.DotNetBar.ButtonItem mcDelete;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddSchool;
        private DevComponents.DotNetBar.ButtonItem mcChangeArgument;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btnSaveConfig;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonItem btnSetupDefaultSchool;
        private DevComponents.DotNetBar.ButtonItem btnSetTemplate;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAccessPointUrl;
        private DevComponents.DotNetBar.ButtonX btnChangeLogSetting;
        private DevComponents.DotNetBar.ButtonX btnChangePassword;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCoreVersion;
        private DevComponents.DotNetBar.Controls.TextBoxX txtServiceLastUpdate;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtServiceVersion;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLogUDS;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLogProcess;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtServerType;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCoreLastUpate;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLogEnabled;
        private TextEditor.BaseSyntaxEditor synMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private DevComponents.DotNetBar.LabelX labelX20;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvTraffics;
        private DevComponents.DotNetBar.LabelX labelX21;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTrafficFile;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.ButtonX btnLoadFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSendSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn chReceiveSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn chClientWait;
        private System.Windows.Forms.DataGridViewTextBoxColumn chServerProcessTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn chDifferenceTime;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDifferenceTime;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.Controls.TextBoxX txtServerProcessTime;
        private DevComponents.DotNetBar.LabelX labelX26;
        private DevComponents.DotNetBar.Controls.TextBoxX txtClientWaitTime;
        private DevComponents.DotNetBar.LabelX labelX25;
        private DevComponents.DotNetBar.Controls.TextBoxX txtReceiveSize;
        private DevComponents.DotNetBar.LabelX labelX24;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSendSize;
        private DevComponents.DotNetBar.LabelX labelX23;
        private DevComponents.DotNetBar.ButtonItem mcResetDBPermission;
        private DevComponents.DotNetBar.ButtonItem mcResetSecureCode;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCompress;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLogDB;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLogOpt;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonItem btnListDatabase;
        private DevComponents.DotNetBar.ButtonItem mcTestDSNS;
        private DevComponents.DotNetBar.ButtonItem mc1RunSingleSql;
        private DevComponents.DotNetBar.ButtonItem btnDebugDB;
        private DevComponents.DotNetBar.ButtonItem btnDBWindow;
        private DevComponents.DotNetBar.ButtonItem btnChangeAppConfig;
        private DevComponents.DotNetBar.ButtonItem btnRiskFeature;
        private DevComponents.DotNetBar.LabelX labelX22;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDBMSVersion;
        private DevComponents.DotNetBar.LabelX lblSelectionCount;
        private DevComponents.DotNetBar.ButtonItem btnSpecUpgrade;
        private DevComponents.DotNetBar.ButtonItem btnRawEdit;
        private DevComponents.DotNetBar.ButtonItem btnExportApps;
        private DevComponents.DotNetBar.ButtonItem btnImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn chDBName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private DevComponents.DotNetBar.ButtonItem btnTestConn;
        private DevComponents.DotNetBar.ButtonItem btnExport;


    }
}
