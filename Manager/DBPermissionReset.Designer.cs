namespace Manager
{
    partial class DBPermissionReset
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
            this.components = new System.ComponentModel.Container();
            ActiproSoftware.SyntaxEditor.Document document10 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBPermissionReset));
            ActiproSoftware.SyntaxEditor.Document document11 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document12 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document16 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document14 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document13 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document15 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document17 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document18 = new ActiproSoftware.SyntaxEditor.Document();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtOwner = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.imgRoleType = new System.Windows.Forms.ImageList(this.components);
            this.txtCrud = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.txtSchema = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel4 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem10 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel6 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem4 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel5 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem5 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.btnFuncs = new DevComponents.DotNetBar.ButtonX();
            this.btnLoad = new DevComponents.DotNetBar.ButtonItem();
            this.btnSave = new DevComponents.DotNetBar.ButtonItem();
            this.sqlTrigger = new Manager.TextEditor.BaseSyntaxEditor();
            this.sqlDatabase = new Manager.TextEditor.BaseSyntaxEditor();
            this.sqlSequence = new Manager.TextEditor.BaseSyntaxEditor();
            this.sqlView = new Manager.TextEditor.BaseSyntaxEditor();
            this.sqlTable = new Manager.TextEditor.BaseSyntaxEditor();
            this.sqlSchema = new Manager.TextEditor.BaseSyntaxEditor();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel4.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel6.SuspendLayout();
            this.tabControlPanel5.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(781, 517);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(699, 517);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "重設權限";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(19, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(45, 20);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Owner";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(19, 42);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(41, 20);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "CRUD";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(19, 74);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(52, 20);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "Schema";
            // 
            // txtOwner
            // 
            this.txtOwner.AcceptsTab = false;
            this.txtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document10.Multiline = false;
            document10.Text = "pgsql";
            this.txtOwner.Document = document10;
            this.txtOwner.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwner.IndentationGuidesVisible = false;
            this.txtOwner.IndicatorMarginVisible = false;
            this.txtOwner.IntelliPrompt.DropShadowEnabled = true;
            this.txtOwner.Location = new System.Drawing.Point(77, 11);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.SelectionMarginWidth = 3;
            this.txtOwner.Size = new System.Drawing.Size(779, 24);
            this.txtOwner.TabIndex = 1;
            this.txtOwner.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // imgRoleType
            // 
            this.imgRoleType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgRoleType.ImageStream")));
            this.imgRoleType.TransparentColor = System.Drawing.Color.Transparent;
            this.imgRoleType.Images.SetKeyName(0, "user_16.png");
            this.imgRoleType.Images.SetKeyName(1, "group_16.png");
            // 
            // txtCrud
            // 
            this.txtCrud.AcceptsTab = false;
            this.txtCrud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document11.Multiline = false;
            document11.Text = "SSchool_User";
            this.txtCrud.Document = document11;
            this.txtCrud.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCrud.IndentationGuidesVisible = false;
            this.txtCrud.IndicatorMarginVisible = false;
            this.txtCrud.IntelliPrompt.DropShadowEnabled = true;
            this.txtCrud.Location = new System.Drawing.Point(77, 41);
            this.txtCrud.Name = "txtCrud";
            this.txtCrud.SelectionMarginWidth = 3;
            this.txtCrud.Size = new System.Drawing.Size(779, 24);
            this.txtCrud.TabIndex = 3;
            this.txtCrud.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // txtSchema
            // 
            this.txtSchema.AcceptsTab = false;
            this.txtSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document12.Multiline = false;
            document12.Text = "SSchool_Admin";
            this.txtSchema.Document = document12;
            this.txtSchema.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchema.IndentationGuidesVisible = false;
            this.txtSchema.IndicatorMarginVisible = false;
            this.txtSchema.IntelliPrompt.DropShadowEnabled = true;
            this.txtSchema.Location = new System.Drawing.Point(77, 73);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.SelectionMarginWidth = 3;
            this.txtSchema.Size = new System.Drawing.Size(779, 24);
            this.txtSchema.TabIndex = 5;
            this.txtSchema.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = false;
            this.tabControl1.CloseButtonOnTabsAlwaysDisplayed = false;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel5);
            this.tabControl1.Controls.Add(this.tabControlPanel4);
            this.tabControl1.Controls.Add(this.tabControlPanel6);
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Location = new System.Drawing.Point(19, 110);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(837, 397);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 8;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.Tabs.Add(this.tabItem4);
            this.tabControl1.Tabs.Add(this.tabItem10);
            this.tabControl1.Tabs.Add(this.tabItem5);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel4
            // 
            this.tabControlPanel4.Controls.Add(this.sqlSequence);
            this.tabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel4.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel4.Name = "tabControlPanel4";
            this.tabControlPanel4.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel4.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel4.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel4.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel4.Style.GradientAngle = 90;
            this.tabControlPanel4.TabIndex = 4;
            this.tabControlPanel4.TabItem = this.tabItem10;
            // 
            // tabItem10
            // 
            this.tabItem10.AttachedControl = this.tabControlPanel4;
            this.tabItem10.Name = "tabItem10";
            this.tabItem10.Text = "Sequence";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tabControlPanel1.Controls.Add(this.sqlDatabase);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Database";
            // 
            // tabControlPanel6
            // 
            this.tabControlPanel6.Controls.Add(this.sqlView);
            this.tabControlPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel6.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel6.Name = "tabControlPanel6";
            this.tabControlPanel6.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel6.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel6.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel6.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel6.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel6.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel6.Style.GradientAngle = 90;
            this.tabControlPanel6.TabIndex = 6;
            this.tabControlPanel6.TabItem = this.tabItem4;
            // 
            // tabItem4
            // 
            this.tabItem4.AttachedControl = this.tabControlPanel6;
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.Text = "View";
            // 
            // tabControlPanel5
            // 
            this.tabControlPanel5.Controls.Add(this.sqlTrigger);
            this.tabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel5.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel5.Name = "tabControlPanel5";
            this.tabControlPanel5.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel5.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel5.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel5.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel5.Style.GradientAngle = 90;
            this.tabControlPanel5.TabIndex = 5;
            this.tabControlPanel5.TabItem = this.tabItem5;
            // 
            // tabItem5
            // 
            this.tabItem5.AttachedControl = this.tabControlPanel5;
            this.tabItem5.Name = "tabItem5";
            this.tabItem5.Text = "Trigger";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.sqlTable);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.tabItem3;
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel3;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "Table";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.sqlSchema);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 27);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(837, 370);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "Schema";
            // 
            // btnFuncs
            // 
            this.btnFuncs.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFuncs.AutoExpandOnClick = true;
            this.btnFuncs.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFuncs.Location = new System.Drawing.Point(19, 517);
            this.btnFuncs.Name = "btnFuncs";
            this.btnFuncs.Size = new System.Drawing.Size(75, 23);
            this.btnFuncs.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFuncs.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnLoad,
            this.btnSave});
            this.btnFuncs.TabIndex = 9;
            this.btnFuncs.Text = "功能";
            // 
            // btnLoad
            // 
            this.btnLoad.GlobalItem = false;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Text = "載入 SQL 設定";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.GlobalItem = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "儲存 SQL 設定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // sqlTrigger
            // 
            this.sqlTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            document16.Text = "-- 變數名稱「有」分大小寫\r\n-- @Trigger\r\n--例：GRANT EXECUTE ON FUNCTION @Trigger TO \"SSchool_U" +
                "ser\";";
            this.sqlTrigger.Document = document16;
            this.sqlTrigger.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlTrigger.Location = new System.Drawing.Point(1, 1);
            this.sqlTrigger.Name = "sqlTrigger";
            this.sqlTrigger.Size = new System.Drawing.Size(835, 368);
            this.sqlTrigger.TabIndex = 1;
            this.sqlTrigger.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // sqlDatabase
            // 
            this.sqlDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            document14.Text = "-- 變數名稱「有」分大小寫\r\n-- @Database\r\n--例：GRANT CONNECT ON DATABASE @Database TO \"SSchool" +
                "_User\";";
            this.sqlDatabase.Document = document14;
            this.sqlDatabase.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlDatabase.Location = new System.Drawing.Point(1, 1);
            this.sqlDatabase.Name = "sqlDatabase";
            this.sqlDatabase.Size = new System.Drawing.Size(835, 368);
            this.sqlDatabase.TabIndex = 0;
            this.sqlDatabase.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // sqlSequence
            // 
            this.sqlSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            document13.Text = "-- 變數名稱「有」分大小寫\r\n-- @Sequence\r\n--例：GRANT ALL ON TABLE @Sequence TO \"SSchool_User\";" +
                "";
            this.sqlSequence.Document = document13;
            this.sqlSequence.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlSequence.Location = new System.Drawing.Point(1, 1);
            this.sqlSequence.Name = "sqlSequence";
            this.sqlSequence.Size = new System.Drawing.Size(835, 368);
            this.sqlSequence.TabIndex = 1;
            this.sqlSequence.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // sqlView
            // 
            this.sqlView.Dock = System.Windows.Forms.DockStyle.Fill;
            document15.Text = "-- 變數名稱「有」分大小寫\r\n-- @View\r\n--例：GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE @View" +
                " TO \"SSchool_User\";";
            this.sqlView.Document = document15;
            this.sqlView.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlView.Location = new System.Drawing.Point(1, 1);
            this.sqlView.Name = "sqlView";
            this.sqlView.Size = new System.Drawing.Size(835, 368);
            this.sqlView.TabIndex = 2;
            this.sqlView.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // sqlTable
            // 
            this.sqlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            document17.Text = "-- 變數名稱「有」分大小寫\r\n-- @Table\r\n--例：GRANT SELECT, UPDATE, INSERT, DELETE ON TABLE @Tab" +
                "le TO \"SSchool_User\";";
            this.sqlTable.Document = document17;
            this.sqlTable.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlTable.Location = new System.Drawing.Point(1, 1);
            this.sqlTable.Name = "sqlTable";
            this.sqlTable.Size = new System.Drawing.Size(835, 368);
            this.sqlTable.TabIndex = 1;
            this.sqlTable.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // sqlSchema
            // 
            this.sqlSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            document18.Text = "-- 變數名稱「有」分大小寫\r\n-- @Schema\r\n--例：GRANT ALL ON SCHEMA @Schema TO \"SSchool_User\";";
            this.sqlSchema.Document = document18;
            this.sqlSchema.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqlSchema.Location = new System.Drawing.Point(1, 1);
            this.sqlSchema.Name = "sqlSchema";
            this.sqlSchema.Size = new System.Drawing.Size(835, 368);
            this.sqlSchema.TabIndex = 1;
            this.sqlSchema.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.TextBox_KeyTyped);
            // 
            // DBPermissionReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 552);
            this.ControlBox = false;
            this.Controls.Add(this.btnFuncs);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtSchema);
            this.Controls.Add(this.txtCrud);
            this.Controls.Add(this.txtOwner);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DBPermissionReset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "資料庫權限重設 (Control + J 可列出可用角色)";
            this.Load += new System.EventHandler(this.DBPermissionReset_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel4.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel6.ResumeLayout(false);
            this.tabControlPanel5.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtOwner;
        private System.Windows.Forms.ImageList imgRoleType;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtCrud;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtSchema;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel5;
        private DevComponents.DotNetBar.TabItem tabItem5;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel4;
        private DevComponents.DotNetBar.TabItem tabItem10;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.ButtonX btnFuncs;
        private DevComponents.DotNetBar.ButtonItem btnLoad;
        private DevComponents.DotNetBar.ButtonItem btnSave;
        private TextEditor.BaseSyntaxEditor sqlDatabase;
        private TextEditor.BaseSyntaxEditor sqlTrigger;
        private TextEditor.BaseSyntaxEditor sqlSequence;
        private TextEditor.BaseSyntaxEditor sqlTable;
        private TextEditor.BaseSyntaxEditor sqlSchema;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel6;
        private TextEditor.BaseSyntaxEditor sqlView;
        private DevComponents.DotNetBar.TabItem tabItem4;
    }
}