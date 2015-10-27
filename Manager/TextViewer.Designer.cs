namespace Manager
{
    partial class TextViewer
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
            this.baseSyntaxEditor1 = new Manager.TextEditor.BaseSyntaxEditor();
            this.SuspendLayout();
            // 
            // baseSyntaxEditor1
            // 
            this.baseSyntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.baseSyntaxEditor1.Document = document1;
            this.baseSyntaxEditor1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.baseSyntaxEditor1.Location = new System.Drawing.Point(0, 0);
            this.baseSyntaxEditor1.Name = "baseSyntaxEditor1";
            this.baseSyntaxEditor1.Size = new System.Drawing.Size(784, 562);
            this.baseSyntaxEditor1.TabIndex = 1;
            // 
            // TextViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.baseSyntaxEditor1);
            this.DoubleBuffered = true;
            this.Name = "TextViewer";
            this.Text = "TextViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private TextEditor.BaseSyntaxEditor baseSyntaxEditor1;
    }
}