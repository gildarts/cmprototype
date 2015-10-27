using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class FileDictionary : Dictionary<string, File>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        public FileDictionary(FileCollection files)
            : base(new IgnoreCaseComparer())
        {
            foreach (File each in files)
                Add(each.FullName, each);
        }
    }
}
