namespace FISCA.PrivateControls
{
    partial class KernelDeployProgress
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
            this.moduleDeployControl1 = new FISCA.PrivateControls.ModuleDeployControl();
            this.SuspendLayout();
            // 
            // moduleDeployControl1
            // 
            this.moduleDeployControl1.BackColor = System.Drawing.Color.Transparent;
            this.moduleDeployControl1.DeployHandler = null;
            this.moduleDeployControl1.Location = new System.Drawing.Point(12, 12);
            this.moduleDeployControl1.Name = "moduleDeployControl1";
            this.moduleDeployControl1.Size = new System.Drawing.Size(500, 79);
            this.moduleDeployControl1.TabIndex = 0;
            // 
            // KernelDeployProgress
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(525, 93);
            this.Controls.Add(this.moduleDeployControl1);
            this.Name = "KernelDeployProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "核心模組更新";
            this.Load += new System.EventHandler(this.KernelDeployProgress_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ModuleDeployControl moduleDeployControl1;
    }
}