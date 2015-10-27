using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FISCA;

namespace Manager.NameService
{
    class AccessPointName
    {
        public AccessPointName()
        {
            Active = true;
            IsPublic = true;
        }

        public AccessPointName(XElement data)
        {
            XElement structMemo = GetStructMemo(data);

            Name = data.AttributeText("Name");
            Url = data.AttributeText("URL");
            SecuredUrl = data.AttributeText("SecuredURL");
            Title = structMemo.ElementText("Title");
            Memo = structMemo.ElementText("Memo");
            Catalog = data.AttributeText("Catalog");
            Active = data.AttributeText("Active") == "1" ? true : false;
            IsPublic = data.AttributeText("IsPublic") == "t" ? true : false;
        }

        private XElement GetStructMemo(XElement data)
        {
            XElement memo = data.Element("StructMemo");
            if (memo == null)
                return new XElement("StructMemo");
            else
                return memo;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string SecuredUrl { get; set; }

        public string Title { get; set; }

        public string Memo { get; set; }

        public string Catalog { get; set; }

        public bool Active { get; set; }

        public void SetActive(string value)
        {
            bool val;

            if (bool.TryParse(value, out val))
                Active = val;
            else
                throw new ArgumentException(string.Format("字串「{0}」無法轉成 Boolean 資料。", value));
        }

        public bool IsPublic { get; set; }

        public void SetIsPublic(string value)
        {
            bool val;

            if (bool.TryParse(value, out val))
                IsPublic = val;
            else
                throw new ArgumentException(string.Format("字串「{0}」無法轉成 Boolean 資料。", value));
        }

        public XElement ToXElement()
        {
            XElement info = new XElement("DSNS");
            info.SetAttributeValue("Active", Active ? "1" : "0");
            info.SetAttributeValue("IsPublic", IsPublic ? "t" : "f");
            info.SetAttributeValue("Catalog", Catalog);
            info.SetAttributeValue("Name", Name);
            info.SetAttributeValue("URL", Url);
            info.SetAttributeValue("SecuredURL", SecuredUrl);

            XElement structMemo = new XElement("StructMemo",
                new XElement("Title", Title),
                new XElement("Memo", Memo));

            info.Add(structMemo);

            return info;
        }
    }
}
