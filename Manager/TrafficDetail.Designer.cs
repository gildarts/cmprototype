namespace Manager
{
    partial class TrafficDetail
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
            this.baseXmlSyntaxLanguage1 = new Manager.TextEditor.BaseXmlSyntaxLanguage();
            this.txtDetail = new Manager.TextEditor.BaseSyntaxEditor();
            this.SuspendLayout();
            // 
            // txtDetail
            // 
            this.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Language = this.baseXmlSyntaxLanguage1;
            document1.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            document1.ReadOnly = true;
            this.txtDetail.Document = document1;
            this.txtDetail.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDetail.Location = new System.Drawing.Point(0, 0);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(927, 555);
            this.txtDetail.TabIndex = 0;
            // 
            // TrafficDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(927, 555);
            this.Controls.Add(this.txtDetail);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrafficDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "詳細資料";
            this.Load += new System.EventHandler(this.TrafficDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TextEditor.BaseSyntaxEditor txtDetail;
        private TextEditor.BaseXmlSyntaxLanguage baseXmlSyntaxLanguage1;
    }
}