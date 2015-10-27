using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �o�OFault������
    /// </summary>
    public class Fault
    {
        private XmlElement _fault;

        /// <summary>
        /// ��l��<see cref="Fault"/>���s�������C
        /// </summary>
        /// <param name="fault">Fault ��T�C</param>
        public Fault(XmlElement fault)
        {
            _fault = fault;
        }

        /// <summary>
        /// ���Ѫ��ӷ��W�١C
        /// </summary>
        public string Source
        {
            get { return _fault.SelectSingleNode("Source").InnerText; }
        }

        /// <summary>
        /// ���Ѫ��N�X�C
        /// </summary>
        public string Code
        {
            get { return _fault.SelectSingleNode("Code").InnerText; }
        }

        /// <summary>
        /// ���ѻ����C
        /// </summary>
        public string Message
        {
            get { return _fault.SelectSingleNode("Message").InnerText; }
        }

        /// <summary>
        /// ���Ѫ��ԲӸ�T�C
        /// </summary>
        public string Detail
        {
            get { return _fault.SelectSingleNode("Detail").InnerText; }
        }

        /// <summary>
        /// �p�G�٦���L���~�A�h�^�ǡA�_�h�^�� Null �ѦҡC
        /// </summary>
        public Fault InnerFault
        {
            get
            {
                //���_�O�_�٦���L Xml �����C
                if (_fault.NextSibling != null)
                {
                    //���_�O�_�����W�٬O Fault �P�O�_�O�@�ӡu�����v�C
                    if (_fault.NextSibling.LocalName == "Fault" &&
                        _fault.NextSibling.NodeType == XmlNodeType.Element)
                        return new Fault((XmlElement)_fault.NextSibling);
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        internal Exception GetInnerFaultException
        {
            get
            {
                if (InnerFault != null)
                    return new DSAFaultException(InnerFault);
                else
                    return null;
            }
        }
    }
}
