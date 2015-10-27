using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FISCA.DSAUtil;
using FISCA.Authentication;

namespace FISCA.UDT
{
    /// <summary>
    /// 同步UDT的Schema
    /// </summary>
    public class SchemaManager
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="conn">同步的對像</param>
        public SchemaManager(DSConnection conn)
        {
            _Conn = conn;
        }

        private DSConnection _Conn;
        private Dictionary<Type, List<FieldInfo>> _Fields = new Dictionary<Type, List<FieldInfo>>();
        private Dictionary<Type, string> _TableName = new Dictionary<Type, string>();
        /// <summary>
        /// 自動同步Schema
        /// </summary>
        /// <param name="sampleRecord"></param>
        public void SyncSchema(ActiveRecord sampleRecord)
        {
            Type type = sampleRecord.GetType();
            lock (type)
            {
                if (!_Fields.ContainsKey(type))
                {
                    _Fields.Add(type, new List<FieldInfo>());
                    #region 檢查欄位宣告錯誤
                    foreach (var field in sampleRecord.fields)
                    {
                        if (field.Name.ToLower() == "uid")
                            throw new Exception("UID被訂定為系統預設的辨識欄位，不允許成為可編輯資料。");
                        foreach (var item in _Fields[type])
                        {
                            if (item.Name.ToLower() == field.Name)
                                throw new Exception(field.Name + "名稱重複，不允許兩個屬性同時代表一個欄位。");
                        }
                        _Fields[type].Add(field);
                    }
                    #endregion
                    var tableName = sampleRecord.TableName();
                    _TableName.Add(type, tableName);
                    XmlElement tableElement = null;
                    while (tableElement == null)
                    {
                        DSRequest req = new DSRequest();
                        DSResponse resp = _Conn.CallService("UDTService.DDL.ListTables", req);
                        tableElement = resp.GetContent().GetElement("Table[@Name='" + tableName.ToLower() + "']");
                        if (tableElement == null)
                        {
                            DSXmlHelper helper = new DSXmlHelper("ImportRequest");
                            helper.AddElement("Table").SetAttribute("Name", tableName);
                            foreach (var field in sampleRecord.fields)
                            {
                                if (field.Type == DataType.Boolean && field.Indexed)
                                    throw new Exception(field.Name + "boolean型別無法建立Index。");
                                XmlElement fieldElement = helper.AddElement("Table", "Field");
                                fieldElement.SetAttribute("Name", field.Name);
                                fieldElement.SetAttribute("DataType", "" + field.Type);
                                fieldElement.SetAttribute("Indexed", "" + field.Indexed);
                            }
                            _Conn.CallService("UDTService.DDL.ImportTables", new DSRequest(helper));
                        }
                    }
                    bool hasNewField = false;
                    DSXmlHelper addFieldHelper = new DSXmlHelper("Request");
                    addFieldHelper.AddElement("TableName").InnerText = tableName;
                    foreach (var field in sampleRecord.fields)
                    {
                        XmlElement fieldElement = tableElement.SelectSingleNode("Field[@Name='" + field.Name.ToLower() + "']") as XmlElement;
                        if (fieldElement == null)//這欄位本來不存在
                        {
                            hasNewField = true;
                            if (field.Type == DataType.Boolean && field.Indexed)
                                throw new Exception(field.Name + "boolean型別無法建立Index。");
                            fieldElement = addFieldHelper.AddElement("Field");
                            fieldElement.SetAttribute("Name", field.Name);
                            fieldElement.SetAttribute("DataType", "" + field.Type);
                            fieldElement.SetAttribute("Indexed", "" + field.Indexed);
                        }
                        else
                        {
                            if ((fieldElement.GetAttribute("DataType") == "Text" ? "string" : fieldElement.GetAttribute("DataType")).ToLower() != ("" + field.Type).ToLower())
                            {
                                throw new Exception("無法自動變更欄位的型別:" + field.Name);
                            }
                            if (bool.Parse(fieldElement.GetAttribute("Indexed")) != field.Indexed)
                            {
                                if (field.Type == DataType.Boolean)
                                    throw new Exception(field.Name + "Boolean型別無法建立Index。");
                                if (field.Type == DataType.String && fieldElement.GetAttribute("DataType") == "Text")
                                    throw new Exception(field.Name + "已被宣告為Text型別無法建立Index。");
                                DSXmlHelper alterFieldIndexHelper = new DSXmlHelper("Request");
                                alterFieldIndexHelper.AddElement("TableName").InnerText = tableName;
                                alterFieldIndexHelper.AddElement("FieldName").InnerText = field.Name;
                                alterFieldIndexHelper.AddElement("Indexed").InnerText = "" + field.Indexed;
                                _Conn.CallService("UDTService.DDL.AlterFieldIndex", new DSRequest(alterFieldIndexHelper));
                            }
                        }
                    }
                    if (hasNewField)
                        _Conn.CallService("UDTService.DDL.AddField", new DSRequest(addFieldHelper));
                }
            }
        }
    }
}
