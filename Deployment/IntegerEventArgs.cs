using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class IntegerEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public IntegerEventArgs(int value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }
    }
}
