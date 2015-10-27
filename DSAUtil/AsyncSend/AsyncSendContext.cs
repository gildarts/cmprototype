using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表非同步處理的組態資訊。
    /// </summary>
    public class AsyncSendContext
    {
        private int _requestID;
        private int _progressTimeout;
        private int _perTimeSize;
        private string _serviceName;
        private DSRequest _request;
        private ProgressCallback _sendStart, _sendProgress, _sendComplete;
        private ProgressCallback _receiveStart, _receiveProgress, _receiveComplete;
        private Control _anyUIControl;
        private AccessPoint _targetAccessPoint;
        private ISecurityToken _securityToken;
        private object _tag;

        /// <summary>
        /// 建立物件的執行個體。
        /// </summary>
        /// <param name="serviceName">服務名稱。</param>
        /// <param name="request">申請文件。</param>
        public AsyncSendContext(string serviceName, DSRequest request)
        {
            _requestID = GetHashCode();
            _progressTimeout = 30000;
            _perTimeSize = 1024 * 10;
            _serviceName = serviceName;
            _request = request;

            //_anyUIControl = new Control();
            //IntPtr xx = _anyUIControl.Handle;

            //設定服務名稱。
            request.TargetService = serviceName;
        }

        /// <summary>
        /// 多久未回應，就產生逾時例外的時間(單位：千分之一秒)。
        /// </summary>
        public int ProgressTimeout
        {
            get { return _progressTimeout; }
            set { _progressTimeout = value; }
        }

        /// <summary>
        /// 最大一次處理的大小，以 Byte 為單位。
        /// 預設為 2048 Byte。
        /// </summary>
        public int PerTimeSize
        {
            get { return _perTimeSize; }
            set { _perTimeSize = value; }
        }

        /// <summary>
        /// 取得或設定此非同步傳送的編號。
        /// </summary>
        public int RequestID
        {
            get { return _requestID; }
            set { _requestID = value; }
        }

        /// <summary>
        /// 取得服務名稱。
        /// </summary>
        public string ServiceName
        {
            get { return _serviceName; }
        }

        /// <summary>
        /// 取得申請文件。
        /// </summary>
        public DSRequest Request
        {
            get { return _request; }
            internal set { _request = value; }
        }

        /// <summary>
        /// 開始「傳送」資料時的回報事件。
        /// </summary>
        public event ProgressCallback SendStart
        {
            add { _sendStart = value; }
            remove { _sendStart = null; }
        }

        internal ProgressCallback SendStartDelegate
        {
            get { return _sendStart; }
        }

        /// <summary>
        /// 傳送的進度回報事件。
        /// </summary>
        public event ProgressCallback SendProgress
        {
            add { _sendProgress = value; }
            remove { _sendProgress = null; }
        }

        internal ProgressCallback SendProgressDelegate
        {
            get { return _sendProgress; }
        }

        /// <summary>
        /// 完成傳送資料時回報事件。
        /// </summary>
        public event ProgressCallback SendComplete
        {
            add { _sendComplete = value; }
            remove { _sendComplete = null; }
        }

        internal ProgressCallback SendCompleteDelegate
        {
            get { return _sendComplete; }
        }

        /// <summary>
        /// 開始「接收」資料時的回報事件。
        /// </summary>
        public event ProgressCallback ReceiveStart
        {
            add { _receiveStart = value; }
            remove { _receiveStart = null; }
        }

        internal ProgressCallback ReceiveStartDelegate
        {
            get { return _receiveStart; }
        }

        /// <summary>
        /// 接收的進度回報事件。
        /// </summary>
        public event ProgressCallback ReceiveProgress
        {
            add { _receiveProgress = value; }
            remove { _receiveProgress = null; }
        }

        internal ProgressCallback ReceiveProgressDelegate
        {
            get { return _receiveProgress; }
        }

        /// <summary>
        /// 完成接收資料時回報事件。
        /// </summary>
        public event ProgressCallback ReceiveComplete
        {
            add { _receiveComplete = value; }
            remove { _receiveComplete = null; }
        }

        internal ProgressCallback ReceiveCompleteDelegate
        {
            get { return _receiveComplete; }
        }
        /// <summary>
        /// 在進度回報時，如果需要處理 UI，則此屬性必須要指定，如果未指定，回報進度的函數將會在非 UI 的執行緒。
        /// </summary>
        public Control AnyUIControl
        {
            get { return _anyUIControl; }
            set { _anyUIControl = value; }
        }

        internal AccessPoint TargetAccessPoint
        {
            get { return _targetAccessPoint; }
            set { _targetAccessPoint = value; }
        }

        internal ISecurityToken SecurityToken
        {
            get { return _securityToken; }
            set
            {
                _securityToken = value;
                Request.SecurityToken = _securityToken.GetTokenContent();
            }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}
