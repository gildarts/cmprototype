using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 非同步處理時的狀態資訊。
    /// </summary>
    internal class AsyncState
    {
        private HttpWebRequest _request;
        private ProgressInfo _progress;

        public AsyncState(HttpWebRequest request)
        {
            _request = request;
        }

        public HttpWebRequest Request
        {
            get { return _request; }
        }

        public ProgressInfo ProgressInfo
        {
            get { return _progress; }
            set { _progress = value; }
        }
    }
}
