namespace Manager
{
    partial class RegistryForm
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistryForm));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnRegistry = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ErrorNotify = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtAccessPoint = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtMemo = new Manager.TextEditor.BaseSyntaxEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorNotify)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(21, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(86, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Access Point";
            // 
            // btnRegistry
            // 
            this.btnRegistry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistry.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRegistry.Location = new System.Drawing.Point(323, 198);
            this.btnRegistry.Name = "btnRegistry";
            this.btnRegistry.Size = new System.Drawing.Size(75, 23);
            this.btnRegistry.TabIndex = 7;
            this.btnRegistry.Text = "新增";
            this.btnRegistry.Click += new System.EventHandler(this.btnRegistry_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(404, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(21, 48);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(77, 21);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUserName.Border.Class = "TextBoxBorder";
            this.txtUserName.Location = new System.Drawing.Point(118, 46);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(361, 25);
            this.txtUserName.TabIndex = 3;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(21, 83);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(67, 21);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Location = new System.Drawing.Point(118, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(361, 25);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // ErrorNotify
            // 
            this.ErrorNotify.ContainerControl = this;
            // 
            // txtAccessPoint
            // 
            this.txtAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessPoint.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAccessPoint.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            // 
            // 
            // 
            this.txtAccessPoint.Border.Class = "TextBoxBorder";
            this.txtAccessPoint.Location = new System.Drawing.Point(118, 10);
            this.txtAccessPoint.Name = "txtAccessPoint";
            this.txtAccessPoint.Size = new System.Drawing.Size(361, 25);
            this.txtAccessPoint.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtAccessPoint, "Control+J 可列出 DS Name Server 上可能符合的 AccessPoint 清單。");
            this.txtAccessPoint.TextChanged += new System.EventHandler(this.txtAccessPoint_TextChanged);
            // 
            // txtMemo
            // 
            this.txtMemo.AcceptsTab = false;
            document1.LexicalParsingEnabled = false;
            document1.SemanticParsingEnabled = false;
            this.txtMemo.Document = document1;
            this.txtMemo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMemo.IndicatorMarginVisible = false;
            this.txtMemo.Location = new System.Drawing.Point(21, 112);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(458, 80);
            this.txtMemo.TabIndex = 6;
            // 
            // RegistryForm
            // 
            this.AcceptButton = this.btnRegistry;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(501, 233);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegistry);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtAccessPoint);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增 DSA Server";
            this.Load += new System.EventHandler(this.RegistryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorNotify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnRegistry;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private System.Windows.Forms.ErrorProvider ErrorNotify;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAccessPoint;
        private System.Windows.Forms.ToolTip toolTip1;
        private TextEditor.BaseSyntaxEditor txtMemo;
    }
}