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
    internal partial class ModuleDeployProgress : BaseForm
    {
        private List<ModuleDeployment> modules = new List<ModuleDeployment>();
        private ModuleDeployment CurrentModule = null;
        private int Current { get; set; }
        private Timer CompleteTimer = new Timer();

        public ModuleDeployProgress(List<string> modUrls)
        {
            InitializeComponent();

            CompleteTimer.Tick += new EventHandler(CompleteTimer_Tick);
            CompleteTimer.Interval = 200;

            Current = 0;

            foreach (string each in modUrls)
            {
                ModuleUrl url = new ModuleUrl(each);
                DeployFolder folder = new DeployFolder(Paths.Module, FolderType.BaseFolder);
                ModuleDeployment md = new ModuleDeployment(url, folder, Consts.ReleaseBuild);
                modules.Add(md);
            }

            StartDeploy();
        }

        private void CompleteTimer_Tick(object sender, EventArgs e)
        {
            CompleteTimer.Enabled = false;
            Close();
        }

        private void StartDeploy()
        {
            CurrentModule = modules[Current];
            CurrentModule.DeployComplete += new EventHandler<DeployCompleteEventArgs>(Module_DeployComplete);
            DeployProgress.DeployHandler = CurrentModule;
            CurrentModule.Deploy();
        }

        private void Module_DeployComplete(object sender, DeployCompleteEventArgs e)
        {
            CurrentModule.DeployComplete -= Module_DeployComplete;
            Current++;

            if (Current >= modules.Count)
                CompleteTimer.Enabled = true;
            else
                StartDeploy();
        }
    }
}
