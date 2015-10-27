using System.Windows.Forms;
using System.Xml;
using FISCA.Presentation.Controls;

namespace FISCA.PrivateControls
{
    partial class LicenseInfoForm : BaseForm
    {
        internal LicenseInfoForm(LicenseInfo lic)
        {
            InitializeComponent();

            XmlElement token = lic.ApplicationToken.GetTokenContent();

            lblAccessPoint.Text = lic.AccessPoint;
            lblExpiration.Text = token.SelectSingleNode("ApplicationKey/ExpireDate").InnerText;

            foreach (XmlElement each in token.SelectNodes("ApplicationKey/LocationLimit/IP"))
            {
                ListViewItem item = new ListViewItem();
                item.Text = each.GetAttribute("Address");
                lvIPList.Items.Add(item);
            }
        }
    }
}