using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class IgnoreCaseComparer : IEqualityComparer<string>
    {
        private CaseInsensitiveComparer comparer = CaseInsensitiveComparer.DefaultInvariant;

        #region IEqualityComparer<string> 成員

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(string x, string y)
        {
            if (comparer.Compare(x, y) == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(string obj)
        {
            return obj.ToLower().GetHashCode();
        }

        #endregion
    }
}
