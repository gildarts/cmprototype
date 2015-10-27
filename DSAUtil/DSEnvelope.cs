using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;

    /// <summary>
    /// 代表一個DSA的Envelope
    /// </summary>
    public class DSEnvelope : XmlBaseObject
    {
        /// <summary>
        /// 代表內部的Xml資料。
        /// </summary>
        protected DSXmlHelper envelope;

        protected const string HeaderName = "Header";
        protected const string BodyName = "Body";

        /// <summary>
        /// 代表DSA 3.0的基本Envelope概念。
        /// </summary>
        public DSEnvelope()
        {
            envelope = new DSXmlHelper("Envelope");
            envelope.AddElement(HeaderName);
            envelope.AddElement(BodyName);

            //把狀態設定到基礎Xml資料物件中。
            Load(envelope.BaseElement);
        }

        /// <summary>
        /// 設定Body中的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <param name="content">Body的內容。</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public virtual XmlElement SetContent(XmlElement content)
        {
            //匯入新節點
            XmlNode nContent = envelope.BaseElement.OwnerDocument.ImportNode(content, true);

            //加入節點到Body
            return envelope.GetElement(BodyName).AppendChild(nContent) as XmlElement;
        }

        /// <summary>
        /// 設定Body中的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <param name="content">Body的內容。</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(XmlDocument content)
        {
            if (content != null)
                return SetContent(content.DocumentElement);
            else
                return null;
        }

        /// <summary>
        /// 設定Body中的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <param name="content">Body的內容。</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(DSXmlHelper content)
        {
            if (content != null)
                return SetContent(content.BaseElement);
            else
                return null;
        }

        /// <summary>
        /// 設定Body中的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <param name="content">Body的內容。</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(DSXmlCreator content)
        {
            return SetContent(content.GetResultAsXmlDocument().DocumentElement);
        }

        /// <summary>
        /// 設定Body中的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <param name="content">Body的內容。</param>
        /// <include file='Util30\LibDocument\DSEnvelope.xml' path='Documents/Document[@Name="SetContent"]/*'/>
        public XmlElement SetContent(string content)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(content);

            return SetContent(xmldoc.DocumentElement);
        }

        /// <summary>
        /// 取得Body的內容，並非整個DSEnvelope的內容。
        /// </summary>
        /// <returns>回傳不含「Body」的Xml資料。</returns>
        /// <remarks>如果取回的是資料副本，對資料的修改不會影響到原來的資料。</remarks>
        public DSXmlHelper GetContent()
        {
            if (!HasContent)
                return null;

            return new DSXmlHelper(GetContentAsXmlElement());
        }

        /// <summary>
        /// 用以判斷在「Body」中是否有資料。
        /// </summary>
        /// <value>如果「Body」中有資料，即回傳True。</value>
        public bool HasContent
        {
            get
            {
                //Body 中的子元素大於零就是有內容。
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
        /// 新增Header到Envelope中。
        /// </summary>
        /// <param name="headerName">標頭名稱。</param>
        /// <param name="value">標頭值。</param>
        public void SetHeader(string name, string value)
        {
            XmlElement head = envelope.GetElement(HeaderName + "/" + name);

            if (head != null)
                head.InnerText = value;
            else
                envelope.AddElement(HeaderName, name, value);
        }

        /// <summary>
        /// 新增Header到Envelope中。
        /// </summary>
        /// <param name="header">要新增的XmlElement物件。</param>
        public void SetHeader(XmlElement header)
        {
            //將文件中已存在的取出
            XmlElement head = envelope.GetElement(HeaderName + "/" + header.Name);

            if (head != null) //如果已存在
            {
                XmlNode newHeader = envelope.BaseElement.OwnerDocument.ImportNode(header, true);
                envelope.GetElement(HeaderName).ReplaceChild(newHeader, head);
            }
            else
                envelope.AddElement(HeaderName, header);
        }

        /// <summary>
        /// 取得Header資料，修改資料會影響原來的Header。
        /// </summary>
        /// <param name="name">Header名稱。</param>
        /// <returns>Header的資料，如果不存在則回傳Null。</returns>
        public XmlElement GetHeader(string name)
        {
            return envelope.GetElement(HeaderName + "/" + name);
        }

        /// <summary>
        /// 從Envelope中移除一個Header。
        /// </summary>
        /// <param name="Name">Header的名稱，如果不存在會產生Exception。</param>
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
        /// 從 XmlElement物件載入Envelope內容，內容不符Envelop則會產生Exception。
        /// </summary>
        /// <param name="xmlContent">包含要載入之Xml文件的字串。</param>
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
        /// 回傳Body之中的內容，不含Body。
        /// </summary>
        /// <returns>沒有資料就回傳Null。</returns>
        protected XmlElement GetContentAsXmlElement()
        {
            XmlNode nContent = null;

            //為避免Body中有Text的節點，所以有這個foreach。
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