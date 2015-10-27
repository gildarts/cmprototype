using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Deployment;

namespace FISCA.PrivateControls
{
    internal partial class KernelDeployProgress : BaseForm
    {
        public KernelDeployProgress(ModuleDeployment deploy)
        {
            InitializeComponent();
            Deployment = deploy;
            Deployment.DeployComplete += new EventHandler<DeployCompleteEventArgs>(Deployment_DeployComplete);
        }

        private ModuleDeployment Deployment { get; set; }

        private void KernelDeployProgress_Load(object sender, EventArgs e)
        {
            moduleDeployControl1.DeployHandler = Deployment;
            moduleDeployControl1.Deploy();
        }

        private void Deployment_DeployComplete(object sender, DeployCompleteEventArgs e)
        {
            if (e.Success)
            {
                Application.Restart();
            }
        }
    }
}
