using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N��Ʀ�ñ�����ƥ�B�z�{�Ǫ��һݰѼ�
    /// </summary>
    public class SignatureRequestEventArgs : EventArgs
    {
        private byte[] _signedValue;
        private byte[] _rawValue;

        /// <summary>
        /// �غc�� 
        /// </summary>
        /// <param name="rawValue">�ݭn�Qñ������l���</param>
        public SignatureRequestEventArgs(byte[] rawValue)
        {
            _rawValue = rawValue;
        }

        /// <summary>
        /// ���o�γ]�w�w������ñ�����
        /// </summary>
        public byte[] SignedValue
        {
            get { return _signedValue; }
            set { _signedValue = value; }
        }

        /// <summary>
        /// ���o�ݭn���Ҫ���͸��
        /// </summary>
        public byte[] RawValue
        {
            get { return _rawValue; }
        }
    }
}
