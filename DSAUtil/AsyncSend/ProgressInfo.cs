using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表非同步傳送 Request 時的進度資訊。
    /// </summary>
    public class ProgressInfo
    {
        private int _requestID;
        private bool _success;
        private bool _cancel;
        private int _totalSize, _currentProgressByte;
        private ProcessAction _action;
        private DSResponse _response;
        private Exception _exception;
        private object _tag;

        internal ProgressInfo(int totalSize, ProcessAction action, int requestID)
        {
            _requestID = requestID;
            _success = false;
            _totalSize = totalSize;
            _currentProgressByte = 0;
            _action = action;
            _response = null;
            _exception = null;
        }

        /// <summary>
        /// 申請文件的傳送編號。
        /// </summary>
        public int RequestID
        {
            get { return _requestID; }
        }

        /// <summary>
        /// 取得目前動作(傳送或接收)。
        /// </summary>
        public ProcessAction CurrentAction
        {
            get { return _action; }
        }

        /// <summary>
        /// 取得是否已完成非同步動作，只有當沒有 Exception 也沒有被 Cancel 又完成動作時，此屬性才會為 True。
        /// </summary>
        public bool Success
        {
            //不可以使用 TotalSize ==CurrentProgressByte 來判斷是否完成。
            //因為有可能在作傳送結尾(Stream.Close())時發生錯誤。
            get { return _success && !HasException && !Cancel; }
            internal set { _success = value; }
        }

        /// <summary>
        /// 取得或是設定是否取消傳送資料。
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; ; }
        }

        /// <summary>
        /// 取得總共要處理的 Byte 數。
        /// </summary>
        public int TotalByte
        {
            get { return _totalSize; }
        }

        /// <summary>
        /// 取得目前處理的進度。
        /// </summary>
        public int CurrentByte
        {
            get { return _currentProgressByte; }
            internal set { _currentProgressByte = value; }
        }

        /// <summary>
        /// 取得目前處理進度，以百分比表示。
        /// </summary>
        public int CurrentPercentage
        {
            get
            {
                return Convert.ToInt32((float)CurrentByte / (float)TotalByte * 100f);
            }
        }

        /// <summary>
        /// 取得回傳的 DSResponse，如果是上傳資料，則永遠是 Null。
        /// </summary>
        public DSResponse Response
        {
            get { return _response; }
            internal set { _response = value; }
        }

        /// <summary>
        /// 取得例外狀況資訊。
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
            internal set { _exception = value; }
        }

        /// <summary>
        /// 取得是否產生例外。
        /// </summary>
        public bool HasException
        {
            get
            {
                return _exception != null;
            }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}