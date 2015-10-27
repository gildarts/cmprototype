using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class FileDownloadException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="file"></param>
        /// <param name="innerException"></param>
        public FileDownloadException(string msg, File file, Exception innerException)
            : base(msg, innerException)
        {
            FailureFile = file;
        }

        /// <summary>
        /// 
        /// </summary>
        public File FailureFile { get; private set; }
    }
}
