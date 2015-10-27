using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;

    /// <summary>
    /// �N��@��DSA��Envelope
    /// </summary>
    public class DSEnvelope : XmlBaseObject
    {
        /// <summary>
        /// �N������Xml��ơC
        /// </summary>
        protected DSXmlHelper envelope;

        protected const string HeaderName = "Header";
        protected const string BodyName = "Body";

        /// <summary>
        /// �N��DSA 3.0����Envelope�����C
        /// </summary>
        public DSEnvelope()
        {
            envelope = new DSXmlHelper("Envelope");
            envelope.AddElement(HeaderName);
            envelope.AddElement(BodyName);

            //�⪬�A�]�w���¦Xml��ƪ��󤤡C
            Load(envelope.BaseElement);
        }

        /// <summary>
        /// �]�wBody�������e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <param name="content">Body�����e�C</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public virtual XmlElement SetContent(XmlElement content)
        {
            //�פJ�s�`�I
            XmlNode nContent = envelope.BaseElement.OwnerDocument.ImportNode(content, true);

            //�[�J�`�I��Body
            return envelope.GetElement(BodyName).AppendChild(nContent) as XmlElement;
        }

        /// <summary>
        /// �]�wBody�������e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <param name="content">Body�����e�C</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(XmlDocument content)
        {
            if (content != null)
                return SetContent(content.DocumentElement);
            else
                return null;
        }

        /// <summary>
        /// �]�wBody�������e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <param name="content">Body�����e�C</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(DSXmlHelper content)
        {
            if (content != null)
                return SetContent(content.BaseElement);
            else
                return null;
        }

        /// <summary>
        /// �]�wBody�������e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <param name="content">Body�����e�C</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(DSXmlCreator content)
        {
            return SetContent(content.GetResultAsXmlDocument().DocumentElement);
        }

        /// <summary>
        /// �]�wBody�������e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <param name="content">Body�����e�C</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(string content)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(content);

            return SetContent(xmldoc.DocumentElement);
        }

        /// <summary>
        /// ���oBody�����e�A�ëD���DSEnvelope�����e�C
        /// </summary>
        /// <returns>�^�Ǥ��t�uBody�v��Xml��ơC</returns>
        /// <remarks>�p�G���^���O��ưƥ��A���ƪ��ק藍�|�v�T���Ӫ���ơC</remarks>
        public DSXmlHelper GetContent()
        {
            if (!HasContent)
                return null;

            return new DSXmlHelper(GetContentAsXmlElement());
        }

        /// <summary>
        /// �ΥH�P�_�b�uBody�v���O�_����ơC
        /// </summary>
        /// <value>�p�G�uBody�v������ơA�Y�^��True�C</value>
        public bool HasContent
        {
            get
            {
                //Body �����l�����j��s�N�O�����e�C
                //envelope.BaseElement.HasChildNodes
                foreach (XmlNode node in envelope.GetElement(BodyName).ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// �s�WHeader��Envelope���C
        /// </summary>
        /// <param name="headerName">���Y�W�١C</param>
        /// <param name="value">���Y�ȡC</param>
        public void SetHeader(string name, string value)
        {
            XmlElement head = envelope.GetElement(HeaderName + "/" + name);

            if (head != null)
                head.InnerText = value;
            else
                envelope.AddElement(HeaderName, name, value);
        }

        /// <summary>
        /// �s�WHeader��Envelope���C
        /// </summary>
        /// <param name="header">�n�s�W��XmlElement����C</param>
        public void SetHeader(XmlElement header)
        {
            //�N��󤤤w�s�b�����X
            XmlElement head = envelope.GetElement(HeaderName + "/" + header.Name);

            if (head != null) //�p�G�w�s�b
            {
                XmlNode newHeader = envelope.BaseElement.OwnerDocument.ImportNode(header, true);
                envelope.GetElement(HeaderName).ReplaceChild(newHeader, head);
            }
            else
                envelope.AddElement(HeaderName, header);
        }

        /// <summary>
        /// ���oHeader��ơA�ק��Ʒ|�v�T��Ӫ�Header�C
        /// </summary>
        /// <param name="name">Header�W�١C</param>
        /// <returns>Header����ơA�p�G���s�b�h�^��Null�C</returns>
        public XmlElement GetHeader(string name)
        {
            return envelope.GetElement(HeaderName + "/" + name);
        }

        /// <summary>
        /// �qEnvelope�������@��Header�C
        /// </summary>
        /// <param name="Name">Header���W�١A�p�G���s�b�|����Exception�C</param>
        public void RemoveHeader(string name)
        {
            string removePath = HeaderName + "/" + name;

            if (envelope.PathExist(removePath))
            {
                try
                {
                    envelope.RemoveElement(removePath);
                }
                catch (XmlProcessException ex)
                {
                    throw new XmlProcessException(em.Get("SpecialHeaderNotFound", new Replace("Name", name)), ex);
                }
                catch (Exception ex)
                {
                    throw new XmlProcessException(em.Get("HeaderNameNotValidate", new Replace("Name", name)), ex);
                }
            }
            else
                throw new XmlProcessException(em.Get("SpecialHeaderNotFound", new Replace("Name", name)));
        }

        /// <summary>
        /// �q XmlElement������JEnvelope���e�A���e����Envelop�h�|����Exception�C
        /// </summary>
        /// <param name="xmlContent">�]�t�n���J��Xml��󪺦r��C</param>
        public override void Load(XmlElement xmlContent)
        {
            //if (SimpleXmlValidator.Standard.ValidateDocument(StandardXmlSchema.Envelope, xmlContent.OuterXml))
                envelope = new DSXmlHelper(xmlContent);
            //else
            //    throw new XmlProcessException(em.Get("XmlDocumentNotIsDSDocument",
            //        new Replace("Reason", SimpleXmlValidator.Standard.GetLastError())));

            base.Load(xmlContent);
        }

        /// <summary>
        /// �^��Body���������e�A���tBody�C
        /// </summary>
        /// <returns>�S����ƴN�^��Null�C</returns>
        protected XmlElement GetContentAsXmlElement()
        {
            XmlNode nContent = null;

            //���קKBody����Text���`�I�A�ҥH���o��foreach�C
            foreach (XmlNode n in envelope.GetElement(BodyName).ChildNodes)
            {
                if (n.NodeType == XmlNodeType.Element)
                {
                    nContent = n;
                    break;
                }
            }

            if (nContent != null)
            {
                return nContent as XmlElement;
            }
            else
                return null;
        }

        #region Protected Methods

        #endregion
    }
}