using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    internal class EmptyProgressReceiver : IProgressReceiver
    {
        #region IProgressReceiver 成員

        public void ProgressStart(int max)
        {
            //throw new NotImplementedException();
        }

        public void ProgressEnd()
        {
            //throw new NotImplementedException();
        }

        public void ProgressStep(int progress)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
