using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using FISCA.Deployment;
using FISCA.Presentation.Controls;

namespace FISCA.PrivateControls
{
    internal partial class ModuleDeployControl : UserControl, IProgressReceiver
    {
        public ModuleDeployControl()
        {
            InitializeComponent();
        }

        public ModuleDeployControl(ModuleUrl url, DeployFolder folder, string buildName)
            : this()
        {
            DeployHandler = new ModuleDeployment(url, folder, buildName);
            lblMsg.Text = string.Format("更新：{0}", url.Url);
        }

        public ModuleDeployControl(ModuleDeployment deploy)
            : this()
        {
            DeployHandler = deploy;
            lblMsg.Text = string.Format("更新：{0}", deploy.ModuleUrl.Url);
        }

        private void ModuleDeploy_DeployComplete(object sender, DeployCompleteEventArgs e)
        {
            Application.DoEvents();

            if (e.Success)
            {
                //lblMsg.Text = string.Format("模組「{0}」更新完成。", e.Description.DisplayName);
            }
            else
            {
                lblMsg.Text = e.Error.Message;

                ExtraProcess exprocess = new ExtraProcess();
                ExceptionReport report = new ExceptionReport();
                report.AddType(typeof(WebException));
                report.AddType(typeof(HttpWebRequest));
                report.AddType(typeof(HttpWebResponse), exprocess);
                report.AddType(typeof(Uri));
                report.AddType(typeof(File));
                report.AddType(typeof(MethodBase), true);

                error_msg = report.Transform(e.Error);
                TextViewer.ViewXml(error_msg);
            }
        }

        private string error_msg;

        private ModuleDeployment _module_deploy;
        public ModuleDeployment DeployHandler
        {
            get
            {
                return _module_deploy;
            }
            set
            {
                if (DeployHandler != null)
                    DeployHandler.DeployComplete -= new EventHandler<DeployCompleteEventArgs>(ModuleDeploy_DeployComplete);

                _module_deploy = value;

                if (DeployHandler != null)
                {
                    DeployHandler.DownloadProgress = this;
                    DeployHandler.DeployComplete += new EventHandler<DeployCompleteEventArgs>(ModuleDeploy_DeployComplete);
                    lblMsg.Text = string.Format("更新：{0}", DeployHandler.ModuleUrl.Url);
                    Application.DoEvents();
                }
            }
        }

        public void Deploy()
        {
            DeployHandler.Deploy();
        }

        private void lblMsg_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(error_msg))
                TextViewer.ViewXml(error_msg);
        }

        private class ExtraProcess : IExtraProcesser
        {
            #region IExtraProcess 成員

            public ExtraInformation[] Process(object instance)
            {
                HttpWebResponse rsp = instance as HttpWebResponse;
                List<ExtraInformation> infos = new List<ExtraInformation>();

                if (rsp != null)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(rsp.GetResponseStream(), Encoding.GetEncoding("big5"));
                    infos.Add(new ExtraInformation("GetResponseStream", reader.ReadToEnd()));
                }

                return infos.ToArray();
            }

            #endregion
        }

        #region IProgressReceiver 成員

        private delegate void ProgressMethod(int progress);

        public void ProgressStart(int max)
        {
            if (InvokeRequired)
            {
                Invoke(new ProgressMethod(ProgressStart), max);
            }
            else
            {
                Progress.Value = 0;
                Progress.Maximum = max;
                Progress.Minimum = 0;
            }
        }

        public void ProgressStep(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new ProgressMethod(ProgressStep), progress);
            }
            else
            {
                Progress.Value = progress;
                Application.DoEvents();
            }
        }

        public void ProgressEnd()
        {
        }
        #endregion
    }
}
