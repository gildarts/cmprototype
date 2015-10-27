using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Manager
{
    public partial class ChangeLogSetting : Office2007Form
    {
        private ServerConfiguration Config { get; set; }

        internal ChangeLogSetting(ServerConfiguration config)
        {
            InitializeComponent();
            Config = config;

            chkEnabledLog.Checked = config.Log.LogEnabled;
            chkLogProcess.Checked = config.Log.LogProcess;
            chkLogUDS.Checked = config.Log.LogUDS;
            cboLogOpt.Text = config.Log.Option.ToString();
            chkCompress.Checked = config.Log.CompressData;
            cboLogDB.Text = config.Log.Target;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Config.Log.LogEnabled = chkEnabledLog.Checked;
                Config.Log.LogProcess = chkLogProcess.Checked;
                Config.Log.LogUDS = chkLogUDS.Checked;
                Config.Log.Option = (LogOpportunity)Enum.Parse(typeof(LogOpportunity), cboLogOpt.Text);
                Config.Log.CompressData = chkCompress.Checked;
                Config.Log.Target = cboLogDB.Text;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
