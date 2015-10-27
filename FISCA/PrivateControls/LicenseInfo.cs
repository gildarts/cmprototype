using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Reflection;
using FISCA.DSAUtil;

namespace FISCA.PrivateControls
{
    class LicenseInfo
    {
        private string LicenseFile = Path.Combine(Application.StartupPath, "SmartSchoolLicense.key");

        public bool LicenseExists()
        {
            return File.Exists(LicenseFile);
        }

        public Stream LincenseStream
        {
            get
            {
                try
                {
                    if ( File.Exists(LicenseFile) )
                    {
                        return new FileStream(LicenseFile, FileMode.Open); ;
                    }
                }
                catch { }
                return new MemoryStream();
            }
        }

        public void DecryptLicense()
        {
            try
            {
                FileStream fs = new FileStream(LicenseFile, FileMode.Open);
                byte[] cipher = new byte[fs.Length];
                fs.Read(cipher, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                byte[] plain = ProtectedData.Unprotect(cipher, SetLicense.CryptoKey, DataProtectionScope.LocalMachine);
                string xmlString = Encoding.UTF8.GetString(plain);

                DSXmlHelper hlplicense = new DSXmlHelper(DSXmlHelper.LoadXml(xmlString));
                DSXmlHelper apptoken = new DSXmlHelper("SecurityToken");
                apptoken.SetAttribute(".", "Type", "Application");
                apptoken.AddElement(".", hlplicense.GetElement("ApplicationKey"));

                _access_point = hlplicense.GetText("AccessPoint");
                _app_token = new ApplicationToken(apptoken.BaseElement);
            }
            catch ( Exception ex )
            {
                throw new Exception("解密授權檔失敗", ex);
            }
        }

        private string _access_point;
        public string AccessPoint
        {
            get { return _access_point; }
        }

        private ApplicationToken _app_token;
        public ApplicationToken ApplicationToken
        {
            get { return _app_token; }
        }
    }
}
