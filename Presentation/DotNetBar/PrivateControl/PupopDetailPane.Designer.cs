namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class PupopDetailPane
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
            if ( disposing && ( components != null ) )
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
            FISCA.Presentation.DefaultPreferenceProvider defaultPreferenceProvider1 = new FISCA.Presentation.DefaultPreferenceProvider();
            this.detailPane1 = new FISCA.Presentation.DotNetBar.PrivateControl.DetailPane();
            this.SuspendLayout();
            // 
            // detailPane1
            // 
            this.detailPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailPane1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.detailPane1.Location = new System.Drawing.Point(0, 0);
            this.detailPane1.Name = "detailPane1";
            this.detailPane1.PreferenceProvider = defaultPreferenceProvider1;
            this.detailPane1.PrimaryKey = "";
            this.detailPane1.Size = new System.Drawing.Size(584, 624);
            this.detailPane1.TabIndex = 0;
            // 
            // PupopDetailPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 624);
            this.Controls.Add(this.detailPane1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ));
            this.Name = "PupopDetailPane";
            this.Text = "PupopDetailPane";
            this.ResumeLayout(false);

        }

        #endregion

        private DetailPane detailPane1;

    }
}