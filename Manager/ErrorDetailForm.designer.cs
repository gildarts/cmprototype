namespace Manager
{
    partial class ErrorDetailForm
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.tvStruct = new System.Windows.Forms.TreeView();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panBottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboViewType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.richContent = new Manager.TextEditor.BaseSyntaxEditor();
            this.baseXmlSyntaxLanguage1 = new Manager.TextEditor.BaseXmlSyntaxLanguage();
            this.MainLayout.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvStruct
            // 
            this.tvStruct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStruct.Location = new System.Drawing.Point(3, 3);
            this.tvStruct.Name = "tvStruct";
            this.tvStruct.Size = new System.Drawing.Size(215, 491);
            this.tvStruct.TabIndex = 0;
            this.tvStruct.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvStruct_AfterSelect);
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 2;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.95031F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.04969F));
            this.MainLayout.Controls.Add(this.tvStruct, 0, 0);
            this.MainLayout.Controls.Add(this.panBottom, 0, 1);
            this.MainLayout.Controls.Add(this.richContent, 1, 0);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 2;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.89458F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.10541F));
            this.MainLayout.Size = new System.Drawing.Size(792, 573);
            this.MainLayout.TabIndex = 2;
            // 
            // panBottom
            // 
            this.MainLayout.SetColumnSpan(this.panBottom, 2);
            this.panBottom.Controls.Add(this.label1);
            this.panBottom.Controls.Add(this.cboViewType);
            this.panBottom.Controls.Add(this.btnConfirm);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panBottom.Location = new System.Drawing.Point(3, 500);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(786, 70);
            this.panBottom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "模式";
            this.label1.Visible = false;
            // 
            // cboViewType
            // 
            this.cboViewType.DisplayMember = "Text";
            this.cboViewType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViewType.FormattingEnabled = true;
            this.cboViewType.ItemHeight = 19;
            this.cboViewType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboViewType.Location = new System.Drawing.Point(47, 9);
            this.cboViewType.Name = "cboViewType";
            this.cboViewType.Size = new System.Drawing.Size(121, 25);
            this.cboViewType.TabIndex = 1;
            this.cboViewType.Visible = false;
            this.cboViewType.SelectedIndexChanged += new System.EventHandler(this.cboViewType_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.FontSize = 9F;
            this.comboItem1.Text = "純文字";
            // 
            // comboItem2
            // 
            this.comboItem2.FontSize = 9F;
            this.comboItem2.Text = "Xml 文件";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(704, 40);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 21);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "確定";
            // 
            // richContent
            // 
            this.richContent.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Language = this.baseXmlSyntaxLanguage1;
            document1.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.richContent.Document = document1;
            this.richContent.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richContent.Location = new System.Drawing.Point(224, 3);
            this.richContent.Name = "richContent";
            this.richContent.Size = new System.Drawing.Size(565, 491);
            this.richContent.TabIndex = 3;
            // 
            // ErrorDetailForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnConfirm;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.MainLayout);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ErrorDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "例外細項";
            this.MainLayout.ResumeLayout(false);
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvStruct;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.Panel panBottom;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboViewType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private Manager.TextEditor.BaseSyntaxEditor richContent;
        private Manager.TextEditor.BaseXmlSyntaxLanguage baseXmlSyntaxLanguage1;

    }
}