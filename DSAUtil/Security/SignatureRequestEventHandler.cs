using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N��Ʀ�ñ�����ƥ�B�z�{�ǡC
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void SignatureRequestEventHandler(object sender, SignatureRequestEventArgs args);

    /// <summary>
    /// �N��Ʀ�ñ�����n�D�C
    /// </summary>
    /// <param name="rawData">�nñ������l��ơC</param>
    /// <returns>�Ʀ�ñ����ơC</returns>
    public delegate byte[] SignatureRequest(byte[] rawData);

}
