using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表數位簽章的事件處理程序的所需參數
    /// </summary>
    public class SignatureRequestEventArgs : EventArgs
    {
        private byte[] _signedValue;
        private byte[] _rawValue;

        /// <summary>
        /// 建構式 
        /// </summary>
        /// <param name="rawValue">需要被簽章的原始資料</param>
        public SignatureRequestEventArgs(byte[] rawValue)
        {
            _rawValue = rawValue;
        }

        /// <summary>
        /// 取得或設定已完成的簽章資料
        /// </summary>
        public byte[] SignedValue
        {
            get { return _signedValue; }
            set { _signedValue = value; }
        }

        /// <summary>
        /// 取得需要驗證的原生資料
        /// </summary>
        public byte[] RawValue
        {
            get { return _rawValue; }
        }
    }
}
