using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IO = System.IO;
using System.Security.Cryptography;

namespace FISCA.Deployment
{
    /// <summary>
    /// 代表一個檔案的相關資訊。
    /// </summary>
    public class File
    {
        /// <summary>
        /// 
        /// </summary>
        public File()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public File(XmlElement data)
        {
            Name = XmlTools.GetText(data, "@Name");
            Folder = XmlTools.GetText(data, "@Folder");
            Size = XmlTools.GetInteger(data, "@Size");

            string strVersion = XmlTools.GetText(data, "Version");
            if (strVersion != string.Empty)
                Version = new Version(strVersion);
            else
                Version = new Version("1.0.0.0");

            Hash = XmlTools.GetText(data, "@Hash");
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get
            {
                return IO.Path.Combine(Folder, Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 檔案的內容。
        /// </summary>
        public IO.Stream Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckHash()
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hash = sha1.ComputeHash(Data);
            Data.Seek(0, System.IO.SeekOrigin.Begin);

            return Hash == Convert.ToBase64String(hash);
        }

    }
}
