using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using FISCA.DSA;
using FISCA;

namespace Manager
{
    /// <summary>
    /// DSA 3.0 提供的新工具類型，可建行一般常用的 Xml 操作。
    /// </summary>
    static class XmlUtilities
    {
        /// <summary>
        /// 格式化 Xml 內容。
        /// </summary>
        /// <returns></returns>
        public static string Format(string xmlContent)
        {
            MemoryStream ms = new MemoryStream();

            XmlTextWriter writer = new XmlTextWriter(ms, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 1;
            writer.IndentChar = '\t';

            XmlReader Reader = GetXmlReader(xmlContent);
            writer.WriteNode(Reader, true);
            writer.Flush();
            Reader.Close();

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, System.Text.Encoding.UTF8);

            string Result = sr.ReadToEnd();
            sr.Close();

            writer.Close();
            ms.Close();

            return Result;
        }

        private static XmlReader GetXmlReader(string XmlData)
        {
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreWhitespace = true;

            XmlReader Reader = XmlReader.Create(new StringReader(XmlData), setting);

            return Reader;
        }

        /// <summary>
        /// 複製XmlElement物件，變更其內容不會反應到原來的XmlElement中。
        /// </summary>
        /// <param name="srcElement">要複製的XmlElement物件。</param>
        /// <returns>已複製的XmlElement物件。</returns>
        public static XmlElement CloneElement(XmlElement srcElement)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(srcElement.OuterXml);

            return (XmlElement)xmldoc.DocumentElement;
        }

        /// <summary>
        /// 將Xml字串加入到XmlNode中，可處理不同XmlDocument物件的XmlNode。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static XmlNode AppendChild(XmlNode parent, XmlNode child)
        {
            if (XmlDocument.ReferenceEquals(parent.OwnerDocument, child.OwnerDocument))
                return parent.AppendChild(child);
            else
            {
                XmlNode nChild = parent.OwnerDocument.ImportNode(child, true);
                return parent.AppendChild(nChild);
            }
        }

        /// <summary>
        /// 將Xml字串加入到XmlNode中。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childXmlContent">包含「根」的Xml字串</param>
        public static XmlNode AppendChild(XmlNode parent, string childXmlContent)
        {
            XmlNode nChild = LoadXml(childXmlContent);

            nChild = parent.OwnerDocument.ImportNode(nChild, true);

            return parent.AppendChild(nChild);
        }

        /// <summary>
        /// 載入指定的 Xml 檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXmlFile(string fileName, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.Load(fileName);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// 載入指定的 Xml 檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXmlFile(string fileName)
        {
            return LoadXmlFile(fileName, true);
        }

        /// <summary>
        /// 載入指定的 Xml 資料。
        /// </summary>
        /// <param name="xmlContent">要載入的 Xml 字串資料。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(string xmlString)
        {
            return LoadXml(xmlString, true);
        }

        /// <summary>
        /// 載入指定的 Xml 資料。
        /// </summary>
        /// <param name="xmlString">要載入的 Xml 字串資料。</param>
        /// <param name="preserveWhitespace">是否保留字串中的泛空白字元。</param>
        /// <returns><see cref="XmlElement"/>物件。</returns>
        public static XmlElement LoadXml(string xmlString, bool preserveWhitespace)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = preserveWhitespace;
            xmldoc.LoadXml(xmlString);

            return xmldoc.DocumentElement;
        }

        /// <summary>
        /// 將指定的 Xml 資料以 UTF-8 的編碼方式儲存到檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="elm">要儲存的 Xml 物件。</param>
        public static void SaveXml(string fileName, XmlNode node)
        {
            SaveXml(fileName, node, Encoding.UTF8);
        }

        /// <summary>
        /// 將指定的 Xml 資料儲存到檔案。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <param name="node">要儲存的 Xml 物件。</param>
        /// <param name="enc">儲存的編碼方式。</param>
        public static void SaveXml(string fileName, XmlNode node, Encoding enc)
        {
            File.WriteAllText(fileName, node.OuterXml, enc);
        }

        /// <summary>
        /// 將指定的 Xml 資料以UTF-8的編碼方式寫入到串流中。
        /// </summary>
        /// <param name="outStream">指定的串流。</param>
        /// <param name="node">要輸出的 Xml 物件。</param>
        public static void SaveXml(Stream outStream, XmlNode node)
        {
            SaveXml(outStream, node, Encoding.UTF8);
        }

        /// <summary>
        /// 將指定的 Xml 資料寫入到串流中。
        /// </summary>
        /// <param name="outStream">指定的串流。</param>
        /// <param name="node">要輸出的 Xml 物件。</param>
        /// <param name="enc">輸出的編碼方式。</param>
        public static void SaveXml(Stream outStream, XmlNode node, Encoding enc)
        {
            StreamWriter sw = new StreamWriter(outStream, enc);
            sw.Write(node.OuterXml);
        }

        /// <summary>
        /// 傳送Xml內容到某個網址。
        /// </summary>
        /// <param name="url">目的URL。</param>
        /// <param name="xmlContent">要傳送的Xml內容。</param>
        /// <returns>回傳的Xml資料。</returns>
        public static string HttpSendTo(string url, string xmlContent)
        {
            return HttpSendTo(url, "POST", xmlContent);
        }

        /// <summary>
        /// 傳送Xml內容到某個網址。
        /// </summary>
        /// <param name="url">目的URL。</param>
        /// <param name="method">傳送的方法(POST、GET)</param>
        /// <param name="xmlContent">要傳送的Xml內容。</param>
        /// <returns>回傳的Xml資料。</returns>
        public static string HttpSendTo(string url, string method, string xmlContent)
        {
            //建立Http連線
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);

            //基本設定
            httpReq.Method = method;
            httpReq.ContentType = "text/xml";

            if (method == "POST")
            {
                //寫入Request主體
                StreamWriter reqWriter = new StreamWriter(httpReq.GetRequestStream(), Encoding.UTF8);
                reqWriter.Write(xmlContent);
                reqWriter.Close();
            }
            else if (method == "GET")
            {
                httpReq = (HttpWebRequest)WebRequest.Create(url + "?" + xmlContent);
            }
            else
            {
                throw new InvalidOperationException("不支援指定的 Http Method：" + method);
            }

            //取得Response
            WebResponse httpRsp = null;
            httpRsp = httpReq.GetResponse();

            StreamReader rspStream;

            rspStream = new StreamReader(httpRsp.GetResponseStream(), Encoding.UTF8);

            string result = rspStream.ReadToEnd();
            rspStream.Close(); //這個要記得關閉。

            return result;
        }

        public static XmlElement ResponseBody(this Envelope rsp)
        {
            return XHelper.ParseAsDOM(rsp.BodyContent.XmlString);
        }

        public static XHelper XResponseBody(this Envelope rsp)
        {
            return XHelper.ParseAsHelper(rsp.BodyContent.XmlString);
        }
    }
}
