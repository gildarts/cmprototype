namespace Manager.TextEditor
{
    partial class XPathEvaluatorForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.baseXmlSyntaxLanguage1 = new Manager.TextEditor.BaseXmlSyntaxLanguage();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.seSource = new Manager.TextEditor.BaseSyntaxEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEvaluate = new DevComponents.DotNetBar.ButtonX();
            this.txtXpathExpression = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboDisplayMethod = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.cboEvalMethod = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.seResult = new Manager.TextEditor.BaseSyntaxEditor();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.seSource);
            this.scMain.Panel1.Controls.Add(this.panel1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.seResult);
            this.scMain.Size = new System.Drawing.Size(984, 664);
            this.scMain.SplitterDistance = 298;
            this.scMain.TabIndex = 0;
            // 
            // seSource
            // 
            this.seSource.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Language = this.baseXmlSyntaxLanguage1;
            document1.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.seSource.Document = document1;
            this.seSource.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.seSource.Location = new System.Drawing.Point(0, 0);
            this.seSource.Name = "seSource";
            this.seSource.Size = new System.Drawing.Size(984, 235);
            this.seSource.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEvaluate);
            this.panel1.Controls.Add(this.txtXpathExpression);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 235);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 63);
            this.panel1.TabIndex = 3;
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEvaluate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEvaluate.Location = new System.Drawing.Point(909, 0);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 28);
            this.btnEvaluate.TabIndex = 2;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // txtXpathExpression
            // 
            // 
            // 
            // 
            this.txtXpathExpression.Border.Class = "TextBoxBorder";
            this.txtXpathExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXpathExpression.Location = new System.Drawing.Point(0, 0);
            this.txtXpathExpression.Name = "txtXpathExpression";
            this.txtXpathExpression.Size = new System.Drawing.Size(984, 28);
            this.txtXpathExpression.TabIndex = 1;
            this.txtXpathExpression.WatermarkText = "輸入 XPath 運算式";
            this.txtXpathExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtXpathExpression_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelX2);
            this.panel2.Controls.Add(this.labelX1);
            this.panel2.Controls.Add(this.cboDisplayMethod);
            this.panel2.Controls.Add(this.cboEvalMethod);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 35);
            this.panel2.TabIndex = 5;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(262, 8);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 19);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "顯示結果";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(3, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 19);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "運算方式";
            // 
            // cboDisplayMethod
            // 
            this.cboDisplayMethod.DisplayMember = "Text";
            this.cboDisplayMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDisplayMethod.FormattingEnabled = true;
            this.cboDisplayMethod.ItemHeight = 18;
            this.cboDisplayMethod.Items.AddRange(new object[] {
            this.comboItem4,
            this.comboItem3});
            this.cboDisplayMethod.Location = new System.Drawing.Point(328, 5);
            this.cboDisplayMethod.Name = "cboDisplayMethod";
            this.cboDisplayMethod.Size = new System.Drawing.Size(121, 24);
            this.cboDisplayMethod.TabIndex = 4;
            this.cboDisplayMethod.Text = "OuterXml";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "OuterXml";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "InnerXml";
            // 
            // cboEvalMethod
            // 
            this.cboEvalMethod.DisplayMember = "Text";
            this.cboEvalMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboEvalMethod.FormattingEnabled = true;
            this.cboEvalMethod.ItemHeight = 18;
            this.cboEvalMethod.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cboEvalMethod.Location = new System.Drawing.Point(69, 5);
            this.cboEvalMethod.Name = "cboEvalMethod";
            this.cboEvalMethod.Size = new System.Drawing.Size(172, 24);
            this.cboEvalMethod.TabIndex = 4;
            this.cboEvalMethod.Text = "SelectSingleNode";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "SelectSingleNode";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "SelectNodes";
            // 
            // seResult
            // 
            this.seResult.Dock = System.Windows.Forms.DockStyle.Fill;
            document2.Language = this.baseXmlSyntaxLanguage1;
            document2.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.seResult.Document = document2;
            this.seResult.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.seResult.Location = new System.Drawing.Point(0, 0);
            this.seResult.Name = "seResult";
            this.seResult.Size = new System.Drawing.Size(984, 362);
            this.seResult.TabIndex = 0;
            // 
            // XPathEvaluatorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(984, 664);
            this.Controls.Add(this.scMain);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "XPathEvaluatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XPath Evaluator";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private BaseSyntaxEditor seSource;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtXpathExpression;
        private DevComponents.DotNetBar.ButtonX btnEvaluate;
        private BaseSyntaxEditor seResult;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboEvalMethod;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDisplayMethod;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private BaseXmlSyntaxLanguage baseXmlSyntaxLanguage1;

    }
}