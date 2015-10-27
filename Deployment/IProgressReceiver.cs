using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProgressReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        void ProgressStart(int max);

        /// <summary>
        /// 
        /// </summary>
        void ProgressEnd();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        void ProgressStep(int progress);
    }
}
