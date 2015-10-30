namespace Manager
{
    partial class MainForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DSATree = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.node2 = new DevComponents.AdvTree.Node();
            this.node3 = new DevComponents.AdvTree.Node();
            this.node4 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuBar1 = new DevComponents.DotNetBar.ContextMenuBar();
            this.TreeContextMenu = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefreshServer = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdateService = new DevComponents.DotNetBar.ButtonItem();
            this.btnRename = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangeRegistry = new DevComponents.DotNetBar.ButtonItem();
            this.mnuChangePassword = new DevComponents.DotNetBar.ButtonItem();
            this.btnSetSuperUser = new DevComponents.DotNetBar.ButtonItem();
            this.btnRemoveServer = new DevComponents.DotNetBar.ButtonItem();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.liMessage = new DevComponents.DotNetBar.LabelItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.progress = new DevComponents.DotNetBar.ProgressBarItem();
            this.FeatureManager = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.bar3 = new DevComponents.DotNetBar.Bar();
            this.btnSecurity = new DevComponents.DotNetBar.ButtonItem();
            this.btnConnectToCenter = new DevComponents.DotNetBar.ButtonItem();
            this.btnChangeSecureCode = new DevComponents.DotNetBar.ButtonItem();
            this.btnClearSetting = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveState = new DevComponents.DotNetBar.ButtonItem();
            this.btnTools = new DevComponents.DotNetBar.ButtonItem();
            this.btnNameService = new DevComponents.DotNetBar.ButtonItem();
            this.btnNameServiceMan = new DevComponents.DotNetBar.ButtonItem();
            this.btnImportMarkList = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            ((System.ComponentModel.ISupportInitialize)(this.DSATree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.dockSite7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.SuspendLayout();
            // 
            // DSATree
            // 
            this.DSATree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.DSATree.AllowDrop = true;
            this.DSATree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.DSATree.BackgroundStyle.Class = "TreeBorderKey";
            this.DSATree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.contextMenuBar1.SetContextMenuEx(this.DSATree, this.TreeContextMenu);
            this.DSATree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DSATree.GridRowLines = true;
            this.DSATree.HotTracking = true;
            this.DSATree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.DSATree.Location = new System.Drawing.Point(3, 3);
            this.DSATree.Name = "DSATree";
            this.DSATree.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1,
            this.node2,
            this.node3,
            this.node4});
            this.DSATree.NodesConnector = this.nodeConnector1;
            this.DSATree.NodeStyle = this.elementStyle1;
            this.DSATree.PathSeparator = ";";
            this.DSATree.Size = new System.Drawing.Size(259, 676);
            this.DSATree.Styles.Add(this.elementStyle1);
            this.DSATree.TabIndex = 0;
            this.DSATree.Text = "advTree1";
            this.DSATree.SelectionChanged += new System.EventHandler(this.DSATree_SelectionChanged);
            this.DSATree.NodeDragFeedback += new DevComponents.AdvTree.TreeDragFeedbackEventHander(this.DSATree_NodeDragFeedback);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "node1";
            // 
            // node2
            // 
            this.node2.Expanded = true;
            this.node2.Name = "node2";
            this.node2.Text = "node2";
            // 
            // node3
            // 
            this.node3.Expanded = true;
            this.node3.Name = "node3";
            this.node3.Text = "node3";
            // 
            // node4
            // 
            this.node4.Expanded = true;
            this.node4.Name = "node4";
            this.node4.Text = "node4";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 26);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DSATree);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.contextMenuBar1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(992, 682);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 2;
            // 
            // contextMenuBar1
            // 
            this.contextMenuBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TreeContextMenu});
            this.contextMenuBar1.Location = new System.Drawing.Point(63, 47);
            this.contextMenuBar1.Name = "contextMenuBar1";
            this.contextMenuBar1.Size = new System.Drawing.Size(236, 26);
            this.contextMenuBar1.Stretch = true;
            this.contextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.contextMenuBar1.TabIndex = 0;
            this.contextMenuBar1.TabStop = false;
            this.contextMenuBar1.Text = "contextMenuBar1";
            // 
            // TreeContextMenu
            // 
            this.TreeContextMenu.AutoExpandOnClick = true;
            this.TreeContextMenu.Name = "TreeContextMenu";
            this.TreeContextMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRefreshServer,
            this.btnUpdateService,
            this.btnRename,
            this.btnChangeRegistry,
            this.mnuChangePassword,
            this.btnSetSuperUser,
            this.btnRemoveServer});
            this.TreeContextMenu.Text = "DSATreeContext";
            this.TreeContextMenu.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.TreeContextMenu_PopupOpen);
            this.TreeContextMenu.PopupShowing += new System.EventHandler(this.TreeContextMenu_PopupShowing);
            // 
            // btnRefreshServer
            // 
            this.btnRefreshServer.Name = "btnRefreshServer";
            this.btnRefreshServer.Text = "Reload Server Configuration";
            this.btnRefreshServer.Click += new System.EventHandler(this.btnRefreshServer_Click);
            // 
            // btnUpdateService
            // 
            this.btnUpdateService.Name = "btnUpdateService";
            this.btnUpdateService.Text = "更新 Service";
            this.btnUpdateService.Click += new System.EventHandler(this.btnUpdateService_Click);
            // 
            // btnRename
            // 
            this.btnRename.Name = "btnRename";
            this.btnRename.Text = "重新命名";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnChangeRegistry
            // 
            this.btnChangeRegistry.Name = "btnChangeRegistry";
            this.btnChangeRegistry.Text = "變更註冊資訊";
            this.btnChangeRegistry.Click += new System.EventHandler(this.btnChangeRegistry_Click);
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Text = "變更密碼";
            this.mnuChangePassword.Click += new System.EventHandler(this.mnuChangePassword_Click);
            // 
            // btnSetSuperUser
            // 
            this.btnSetSuperUser.Name = "btnSetSuperUser";
            this.btnSetSuperUser.Text = "超級使用者設定";
            this.btnSetSuperUser.Click += new System.EventHandler(this.txtSetSuperUser_Click);
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.BeginGroup = true;
            this.btnRemoveServer.Image = global::Manager.Images.Close;
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Text = "移除";
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // bar2
            // 
            this.bar2.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
            this.bar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.liMessage,
            this.labelItem1,
            this.progress});
            this.bar2.Location = new System.Drawing.Point(0, 708);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(992, 22);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar2.TabIndex = 0;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // liMessage
            // 
            this.liMessage.Name = "liMessage";
            this.liMessage.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.liMessage.Text = "就緒";
            this.liMessage.Tooltip = "顯示相關訊息";
            // 
            // labelItem1
            // 
            this.labelItem1.BeginGroup = true;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Stretch = true;
            // 
            // progress
            // 
            // 
            // 
            // 
            this.progress.BackStyle.Class = "";
            this.progress.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progress.ChunkGradientAngle = 0F;
            this.progress.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.progress.Name = "progress";
            this.progress.RecentlyUsed = false;
            this.progress.Width = 150;
            // 
            // FeatureManager
            // 
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.FeatureManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.FeatureManager.BottomDockSite = this.dockSite4;
            this.FeatureManager.EnableFullSizeDock = false;
            this.FeatureManager.LeftDockSite = this.dockSite1;
            this.FeatureManager.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.FeatureManager.ParentForm = this;
            this.FeatureManager.RightDockSite = this.dockSite2;
            this.FeatureManager.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.FeatureManager.ToolbarBottomDockSite = this.dockSite8;
            this.FeatureManager.ToolbarLeftDockSite = this.dockSite5;
            this.FeatureManager.ToolbarRightDockSite = this.dockSite6;
            this.FeatureManager.ToolbarTopDockSite = this.dockSite7;
            this.FeatureManager.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(0, 730);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(992, 0);
            this.dockSite4.TabIndex = 7;
            this.dockSite4.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite1.Location = new System.Drawing.Point(0, 26);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(0, 682);
            this.dockSite1.TabIndex = 4;
            this.dockSite1.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Location = new System.Drawing.Point(992, 26);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 682);
            this.dockSite2.TabIndex = 5;
            this.dockSite2.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 730);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(992, 0);
            this.dockSite8.TabIndex = 11;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 26);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 704);
            this.dockSite5.TabIndex = 8;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(992, 26);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 704);
            this.dockSite6.TabIndex = 9;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Controls.Add(this.bar3);
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(992, 26);
            this.dockSite7.TabIndex = 10;
            this.dockSite7.TabStop = false;
            // 
            // bar3
            // 
            this.bar3.AccessibleDescription = "DotNetBar Bar (bar3)";
            this.bar3.AccessibleName = "DotNetBar Bar";
            this.bar3.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.bar3.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSecurity,
            this.buttonItem5,
            this.btnTools,
            this.buttonItem6});
            this.bar3.Location = new System.Drawing.Point(0, 0);
            this.bar3.MenuBar = true;
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(992, 25);
            this.bar3.Stretch = true;
            this.bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.bar3.TabIndex = 0;
            this.bar3.TabStop = false;
            this.bar3.Text = "bar3";
            // 
            // btnSecurity
            // 
            this.btnSecurity.Name = "btnSecurity";
            this.btnSecurity.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnConnectToCenter,
            this.btnChangeSecureCode,
            this.btnClearSetting});
            this.btnSecurity.Text = "安全";
            this.btnSecurity.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnSecurity_PopupOpen);
            // 
            // btnConnectToCenter
            // 
            this.btnConnectToCenter.Name = "btnConnectToCenter";
            this.btnConnectToCenter.Text = "連線到管理中心";
            this.btnConnectToCenter.Click += new System.EventHandler(this.btnConnectToCenter_Click);
            // 
            // btnChangeSecureCode
            // 
            this.btnChangeSecureCode.Name = "btnChangeSecureCode";
            this.btnChangeSecureCode.Text = "變更加密金鑰";
            this.btnChangeSecureCode.Click += new System.EventHandler(this.btnChangeSecureCode_Click);
            // 
            // btnClearSetting
            // 
            this.btnClearSetting.Name = "btnClearSetting";
            this.btnClearSetting.Text = "清除所有設定";
            this.btnClearSetting.Click += new System.EventHandler(this.btnClearSetting_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem8,
            this.buttonItem7,
            this.btnSaveState});
            this.buttonItem5.Text = "管理";
            // 
            // buttonItem8
            // 
            this.buttonItem8.Image = global::Manager.Images.Add_Server;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Text = "新增 DSA Server";
            this.buttonItem8.Click += new System.EventHandler(this.mnuRegistry_Click);
            // 
            // buttonItem7
            // 
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "新增 Server Group";
            this.buttonItem7.Click += new System.EventHandler(this.mnuAddServerGroup_Click);
            // 
            // btnSaveState
            // 
            this.btnSaveState.Name = "btnSaveState";
            this.btnSaveState.Text = "另存狀態";
            this.btnSaveState.Click += new System.EventHandler(this.btnSaveState_Click);
            // 
            // btnTools
            // 
            this.btnTools.Name = "btnTools";
            this.btnTools.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNameService,
            this.btnNameServiceMan,
            this.btnImportMarkList});
            this.btnTools.Text = "工具";
            // 
            // btnNameService
            // 
            this.btnNameService.Name = "btnNameService";
            this.btnNameService.Text = "Name Service Test";
            this.btnNameService.Click += new System.EventHandler(this.btnNameService_Click);
            // 
            // btnNameServiceMan
            // 
            this.btnNameServiceMan.Name = "btnNameServiceMan";
            this.btnNameServiceMan.Text = "Name Service Manager";
            this.btnNameServiceMan.Click += new System.EventHandler(this.btnNameServiceMan_Click);
            // 
            // btnImportMarkList
            // 
            this.btnImportMarkList.Name = "btnImportMarkList";
            this.btnImportMarkList.Text = "Set Marks";
            this.btnImportMarkList.Click += new System.EventHandler(this.btnImportMarkList_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem9});
            this.buttonItem6.Text = "批次";
            this.buttonItem6.Visible = false;
            // 
            // buttonItem9
            // 
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "更新 Services";
            this.buttonItem9.Click += new System.EventHandler(this.mnuUpdate_Click);
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(0, 26);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(992, 0);
            this.dockSite3.TabIndex = 6;
            this.dockSite3.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(992, 730);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bar2);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DSA Server Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DSATree)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.dockSite7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree DSATree;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.Node node3;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar1;
        private DevComponents.DotNetBar.ButtonItem TreeContextMenu;
        private DevComponents.DotNetBar.ButtonItem btnRefreshServer;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.LabelItem liMessage;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem btnRemoveServer;
        private DevComponents.DotNetBar.DotNetBarManager FeatureManager;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.Bar bar3;
        private DevComponents.DotNetBar.ButtonItem btnSecurity;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem btnRename;
        private DevComponents.DotNetBar.ButtonItem btnUpdateService;
        private DevComponents.DotNetBar.ButtonItem btnChangeRegistry;
        private DevComponents.DotNetBar.ButtonItem btnSetSuperUser;
        private DevComponents.DotNetBar.ButtonItem btnConnectToCenter;
        private DevComponents.DotNetBar.ButtonItem btnChangeSecureCode;
        private DevComponents.DotNetBar.ButtonItem btnTools;
        private DevComponents.DotNetBar.ButtonItem btnNameService;
        private DevComponents.DotNetBar.ButtonItem btnClearSetting;
        private DevComponents.DotNetBar.ProgressBarItem progress;
        private DevComponents.DotNetBar.ButtonItem btnImportMarkList;
        private DevComponents.DotNetBar.ButtonItem btnNameServiceMan;
        private DevComponents.DotNetBar.ButtonItem mnuChangePassword;
        private DevComponents.DotNetBar.ButtonItem btnSaveState;

    }
}

