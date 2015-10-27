using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class PupopDetailPane : Office2007Form
    {
        static PupopDetailPane()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        public PupopDetailPane()
        {
            InitializeComponent();
        }
        public string PrimaryKey
        {
            get { return this.detailPane1.PrimaryKey; }
            set
            {
                detailPane1.PrimaryKey = value;
            }
        }
        public void SetDescriptionPane(FISCA.Presentation.DescriptionPane pane)
        {
            detailPane1.SetDescriptionPane(pane);
        }

        public void SetDescription(string description)
        {
            detailPane1.SetDescription( description);
            this.Text = description;
        }

        public IPreferenceProvider PreferenceProvider
        {
            get{ return detailPane1.PreferenceProvider; }
            set { detailPane1.PreferenceProvider = value; }
        }
        public void AddDetailItem(Presentation.DetailContent content)
        {
            detailPane1.AddDetailItem(content);
        }
    }
}
