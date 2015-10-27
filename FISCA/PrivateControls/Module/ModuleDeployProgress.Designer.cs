namespace FISCA.PrivateControls
{
    partial class ModuleDeployProgress
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
            this.DeployProgress = new FISCA.PrivateControls.ModuleDeployControl();
            this.SuspendLayout();
            // 
            // DeployProgress
            // 
            this.DeployProgress.BackColor = System.Drawing.Color.Transparent;
            this.DeployProgress.DeployHandler = null;
            this.DeployProgress.Location = new System.Drawing.Point(12, 12);
            this.DeployProgress.Name = "DeployProgress";
            this.DeployProgress.Size = new System.Drawing.Size(500, 80);
            this.DeployProgress.TabIndex = 0;
            // 
            // ModuleDeployProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 93);
            this.Controls.Add(this.DeployProgress);
            this.Name = "ModuleDeployProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模組更新";
            this.ResumeLayout(false);

        }

        #endregion

        private ModuleDeployControl DeployProgress;

    }
}