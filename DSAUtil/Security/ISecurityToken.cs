using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N��DSA���w���N��(SecurityToken)�C
    /// </summary>
    public interface ISecurityToken
    {
        /// <summary>
        /// ���oSecurityToken�����e�A���ݽT�O���X��Token�O�X�k���C
        /// </summary>
        XmlElement GetTokenContent();

        /// <summary>
        /// �N��Token�������A�Ҧp�GBasic�BEnhanced�BDSAPassport���C
        /// </summary>
        string TokenType { get;}

        ///// <summary>
        ///// ���ܦ� SecurityToken �O�_�i�H�� DSA Application ���ШϥΡA
        ///// �Ҧp�GPassportToken�A���i���ШϥΦp�GSessionToken�C
        ///// </summary>
        //bool Reuseable { get;}

    }//end ISecurityToken
}