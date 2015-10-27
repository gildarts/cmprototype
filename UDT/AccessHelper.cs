using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FISCA.DSAUtil;
using FISCA.UDT.Condition;
using FISCA.Authentication;

namespace FISCA.UDT
{
    /// <summary>
    /// 存取UDT資料
    /// </summary>
    public class AccessHelper
    {
        //private static Dictionary<Type, List<FieldInfo>> _Fields = new Dictionary<Type, List<FieldInfo>>();
        //private static Dictionary<Type, string> _TableName = new Dictionary<Type, string>();
        private static Dictionary<DSConnection, SchemaManager> _SchemaManagers = new Dictionary<DSConnection, SchemaManager>();
        private static Dictionary<Type, System.Reflection.ConstructorInfo> _Creater = new Dictionary<Type, System.Reflection.ConstructorInfo>();
        //private static Dictionary<Type, QueryCompiler> _Compiler = new Dictionary<Type, QueryCompiler>();
        private static void SyncSchema(DSConnection conn, ActiveRecord sampleRecord)
        {
            lock (_SchemaManagers)
            {
                if (!_SchemaManagers.ContainsKey(conn))
                    _SchemaManagers.Add(conn, new SchemaManager(conn));
                _SchemaManagers[conn].SyncSchema(sampleRecord);
            }
            //Type T = sampleRecord.GetType();
            //if (_Fields.ContainsKey(T)) return;
            //_Fields.Add(T, new List<FieldInfo>());
            //#region 檢查欄位宣告錯誤
            //foreach (var field in sampleRecord.fields)
            //{
            //    if (field.Name.ToLower() == "uid")
            //        throw new Exception("UID被訂定為系統預設的辨識欄位，不允許成為可編輯資料。");
            //    foreach (var item in _Fields[T])
            //    {
            //        if (item.Name.ToLower() == field.Name)
            //            throw new Exception(field.Name + "名稱重複，不允許兩個屬性同時代表一個欄位。");
            //    }
            //    _Fields[T].Add(field);
            //}
            //#endregion
            //var tableName = sampleRecord.TableName();
            //_TableName.Add(T, tableName);
            //XmlElement tableElement = null;
            //while (tableElement == null)
            //{
            //    DSRequest req = new DSRequest();
            //    DSResponse resp = Behavior.Instance.CallService("UDTService.DDL.ListTables", req);
            //    tableElement = resp.GetContent().GetElement("Table[@Name='" + tableName.ToLower() + "']");
            //    if (tableElement == null)
            //    {
            //        DSXmlHelper helper = new DSXmlHelper("ImportRequest");
            //        helper.AddElement("Table").SetAttribute("Name", tableName);
            //        foreach (var field in sampleRecord.fields)
            //        {
            //            if (field.Type == DataType.Boolean && field.Indexed)
            //                throw new Exception(field.Name + "boolean型別無法建立Index。");
            //            XmlElement fieldElement = helper.AddElement("Table", "Field");
            //            fieldElement.SetAttribute("Name", field.Name);
            //            fieldElement.SetAttribute("DataType", "" + field.Type);
            //            fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //            //if ( field.Type == DataType.String && field.Indexed )//如果是string又要index就宣告成String
            //            //{
            //            //    fieldElement.SetAttribute("DataType", "String");
            //            //    fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //            //}
            //            //else
            //            //{
            //            //    fieldElement.SetAttribute("DataType", ( field.Type == DataType.String ? "Text" : "" + field.Type ));//不要index的string宣告成Text
            //            //    fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //            //}
            //        }
            //        Behavior.Instance.CallService("UDTService.DDL.ImportTables", new DSRequest(helper));
            //    }
            //}
            //bool hasNewField = false;
            //DSXmlHelper addFieldHelper = new DSXmlHelper("Request");
            //addFieldHelper.AddElement("TableName").InnerText = tableName;
            //foreach (var field in sampleRecord.fields)
            //{
            //    XmlElement fieldElement = tableElement.SelectSingleNode("Field[@Name='" + field.Name.ToLower() + "']") as XmlElement;
            //    if (fieldElement == null)//這欄位本來不存在
            //    {
            //        hasNewField = true;
            //        if (field.Type == DataType.Boolean && field.Indexed)
            //            throw new Exception(field.Name + "boolean型別無法建立Index。");
            //        fieldElement = addFieldHelper.AddElement("Field");
            //        fieldElement.SetAttribute("Name", field.Name);
            //        fieldElement.SetAttribute("DataType", "" + field.Type);
            //        fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //        //if ( field.Type == DataType.String && field.Indexed )//如果是string又要index就宣告成String
            //        //{
            //        //    fieldElement.SetAttribute("DataType", "String");
            //        //    fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //        //}
            //        //else
            //        //{
            //        //    fieldElement.SetAttribute("DataType", ( field.Type == DataType.String ? "Text" : "" + field.Type ));//不要index的string宣告成Text
            //        //    fieldElement.SetAttribute("Indexed", "" + field.Indexed);
            //        //}
            //    }
            //    else
            //    {
            //        if ((fieldElement.GetAttribute("DataType") == "Text" ? "string" : fieldElement.GetAttribute("DataType")).ToLower() != ("" + field.Type).ToLower())
            //        {
            //            throw new Exception("無法自動變更欄位的型別:" + field.Name);
            //        }
            //        if (bool.Parse(fieldElement.GetAttribute("Indexed")) != field.Indexed)
            //        {
            //            if (field.Type == DataType.Boolean)
            //                throw new Exception(field.Name + "Boolean型別無法建立Index。");
            //            if (field.Type == DataType.String && fieldElement.GetAttribute("DataType") == "Text")
            //                throw new Exception(field.Name + "已被宣告為Text型別無法建立Index。");
            //            DSXmlHelper alterFieldIndexHelper = new DSXmlHelper("Request");
            //            alterFieldIndexHelper.AddElement("TableName").InnerText = tableName;
            //            alterFieldIndexHelper.AddElement("FieldName").InnerText = field.Name;
            //            alterFieldIndexHelper.AddElement("Indexed").InnerText = "" + field.Indexed;
            //            Behavior.Instance.CallService("UDTService.DDL.AlterFieldIndex", new DSRequest(alterFieldIndexHelper));
            //        }
            //    }
            //}
            //if (hasNewField)
            //    Behavior.Instance.CallService("UDTService.DDL.AddField", new DSRequest(addFieldHelper));
        }
        private static T Create<T>() where T : ActiveRecord
        {
            Type type = typeof(T);
            System.Reflection.ConstructorInfo creater;
            lock (_Creater)
            {
                if (!_Creater.ContainsKey(type))
                {
                    creater = type.GetConstructor(Type.EmptyTypes);
                    if (creater == null)
                        throw new Exception(type.Name + "沒有預設建構子:" + type.Name + "()");
                    _Creater.Add(type, creater);
                }
            }
            creater = _Creater[type];
            return creater.Invoke(null) as T;
        }
        private static string GetValue(FieldInfo field)
        {
            if (field.Target.PropertyType == typeof(string))
                return "" + field.Target.GetValue(field.Instance, null);
            if (field.Target.PropertyType == typeof(bool))
            {
                bool value = (bool)field.Target.GetValue(field.Instance, null);
                return value ? "true" : "false";
            }
            if (field.Target.PropertyType == typeof(bool?))
            {
                bool? value = (bool?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : (value.GetValueOrDefault() ? "true" : "false");
            }
            if (field.Target.PropertyType == typeof(int))
            {
                int value = (int)field.Target.GetValue(field.Instance, null);
                return "" + value;
            }
            if (field.Target.PropertyType == typeof(int?))
            {
                int? value = (int?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : ("" + value.GetValueOrDefault());
            }
            if (field.Target.PropertyType == typeof(decimal))
            {
                decimal value = (decimal)field.Target.GetValue(field.Instance, null);
                return "" + value;
            }
            if (field.Target.PropertyType == typeof(decimal?))
            {
                decimal? value = (decimal?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : ("" + value.GetValueOrDefault());
            }
            if (field.Target.PropertyType == typeof(float))
            {
                float value = (float)field.Target.GetValue(field.Instance, null);
                return "" + value;
            }
            if (field.Target.PropertyType == typeof(float?))
            {
                float? value = (float?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : ("" + value.GetValueOrDefault());
            }
            if (field.Target.PropertyType == typeof(double))
            {
                double value = (double)field.Target.GetValue(field.Instance, null);
                return "" + value;
            }
            if (field.Target.PropertyType == typeof(double?))
            {
                double? value = (double?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : ("" + value.GetValueOrDefault());
            }
            if (field.Target.PropertyType == typeof(DateTime))
            {
                DateTime value = (DateTime)field.Target.GetValue(field.Instance, null);
                return value.ToString("yyyy/MM/dd HH:mm:ss");
            }
            if (field.Target.PropertyType == typeof(DateTime?))
            {
                DateTime? value = (DateTime?)field.Target.GetValue(field.Instance, null);
                return value == null ? "null" : (value.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss"));
            }
            throw new Exception("不支援的型別");
        }
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="values">要新增的資料</param>
        /// <returns>新資料的UID的集合</returns>
        internal static List<string> InsertValuesBehavior(IEnumerable<ActiveRecord> values)
        {
            bool hasValue = false;
            List<string> newIDList = new List<string>();
            DSXmlHelper helper = new DSXmlHelper("InsertRequest");
            bool first = true;
            DSConnection conn = null;
            foreach (var item in values)
            {
                if (first)
                {
                    first = false;
                    conn = item.DSConnection;
                }
                else
                {
                    if (conn != item.DSConnection)
                    {
                        throw new Exception("連線主機不相同。集合中的項目包含多個不同的DSConnection，不同主機的資料無法批次處理。");
                    }
                }
            }
            foreach (var item in values)
            {
                if (!hasValue)
                {
                    SyncSchema(conn, item);
                }
                hasValue = true;
                helper.AddElement(item.TableName());
                foreach (var field in item.fields)
                {
                    if (field.Type == DataType.String)
                    {
                        helper.AddElement(item.TableName(), field.Name.ToLower());
                        helper.AddCDataSection(item.TableName() + "/" + field.Name.ToLower(), GetValue(field));
                    }
                    else
                    {
                        helper.AddElement(item.TableName(), field.Name.ToLower()).InnerText = GetValue(field);
                    }
                }
            }
            if (hasValue)
            {
                foreach (var item in conn.CallService("UDTService.DML.Insert", new DSRequest(helper)).GetContent().GetElements("NewUID"))
                {
                    newIDList.Add(item.InnerText);
                }
            }
            return newIDList;
        }
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="values">要更新的資料集合</param>
        [AutoRetryOnWebException]
        internal static void UpdateValuesBehavior(IEnumerable<ActiveRecord> values)
        {
            bool hasValue = false;
            List<string> newIDList = new List<string>();
            DSXmlHelper helper = new DSXmlHelper("UpdateRequest");
            bool first = true;
            DSConnection conn = null;
            foreach (var item in values)
            {
                if (first)
                {
                    first = false;
                    conn = item.DSConnection;
                }
                else
                {
                    if (conn != item.DSConnection)
                    {
                        throw new Exception("連線主機不相同。集合中的項目包含多個不同的DSConnection，不同主機的資料無法批次處理。");
                    }
                }
            }
            foreach (var item in values)
            {
                if (!hasValue)
                {
                    SyncSchema(conn, item);
                }
                hasValue = true;
                helper.AddElement(item.TableName());
                helper.AddElement(item.TableName(), "Field");
                foreach (var field in item.fields)
                {
                    if (field.Type == DataType.String)
                    {
                        helper.AddElement(item.TableName() + "/Field", field.Name.ToLower());
                        helper.AddCDataSection(item.TableName() + "/Field/" + field.Name.ToLower(), GetValue(field));
                    }
                    else
                    {
                        helper.AddElement(item.TableName() + "/Field", field.Name.ToLower()).InnerText = GetValue(field);
                    }
                    //helper.AddElement(item.TableName() + "/Field", field.Name.ToLower()).InnerText = GetValue(field);
                }
                helper.AddElement(item.TableName(), "Condition");
                var condtion = helper.AddElement(item.TableName() + "/Condition", "Equals");
                condtion.SetAttribute("FieldName", "uid");
                condtion.InnerText = item.UID;
            }
            if (hasValue)
            {
                conn.CallService("UDTService.DML.Update", new DSRequest(helper));
            }
            foreach (var item in values)
            {
                item.ResetFieldValues();
            }
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="values">要刪除的資料集合</param>
        [AutoRetryOnWebException]
        internal static void DeletedValuesBehavior(IEnumerable<ActiveRecord> values)
        {
            bool hasValue = false;
            List<string> newIDList = new List<string>();
            DSXmlHelper helper = new DSXmlHelper("DeleteRequest");
            bool first = true;
            DSConnection conn = null;
            foreach (var item in values)
            {
                if (first)
                {
                    first = false;
                    conn = item.DSConnection;
                }
                else
                {
                    if (conn != item.DSConnection)
                    {
                        throw new Exception("連線主機不相同。集合中的項目包含多個不同的DSConnection，不同主機的資料無法批次處理。");
                    }
                }
            }
            foreach (var item in values)
            {
                if (!hasValue)
                {
                    SyncSchema(conn, item);
                }
                hasValue = true;
                helper.AddElement(item.TableName());
                helper.AddElement(item.TableName(), "Condition");
                var condtion = helper.AddElement(item.TableName() + "/Condition", "Equals");
                condtion.SetAttribute("FieldName", "uid");
                condtion.InnerText = item.UID;
            }
            if (hasValue)
                conn.CallService("UDTService.DML.Delete", new DSRequest(helper));
        }
        /// <summary>
        /// 儲存全部，將集合內所有的資料自動依RecordStatus屬性進行新增修改與刪除的動作。
        /// </summary>
        /// <param name="records"></param>
        /// <returns>所有變更資料(含新增)的UID</returns>
        internal static List<string> SaveAllBehavior(IEnumerable<ActiveRecord> records)
        {
            List<ActiveRecord> deletedList = new List<ActiveRecord>();
            List<ActiveRecord> updateList = new List<ActiveRecord>();
            List<ActiveRecord> insertList = new List<ActiveRecord>();
            List<string> changedList = new List<string>();
            foreach (var item in records)
            {
                switch (item.RecordStatus)
                {
                    case RecordStatus.Delete:
                        changedList.Add(item.UID);
                        deletedList.Add(item);
                        break;
                    case RecordStatus.Insert:
                        insertList.Add(item);
                        break;
                    case RecordStatus.Update:
                        changedList.Add(item.UID);
                        updateList.Add(item);
                        break;
                }
            }
            if (deletedList.Count > 0)
            {
                AccessHelper.DeletedValuesBehavior(deletedList);
            }
            if (updateList.Count > 0)
            {
                AccessHelper.UpdateValuesBehavior(updateList);
            }
            if (insertList.Count > 0)
            {
                changedList.AddRange(AccessHelper.InsertValuesBehavior(insertList));
            }
            return changedList;
        }
        internal static List<string> SaveAllBehavior<T>(IEnumerable<T> records) where T : ActiveRecord
        {
            List<ActiveRecord> deletedList = new List<ActiveRecord>();
            List<ActiveRecord> updateList = new List<ActiveRecord>();
            List<ActiveRecord> insertList = new List<ActiveRecord>();
            List<string> changedList = new List<string>();
            foreach (var item in records)
            {
                switch (item.RecordStatus)
                {
                    case RecordStatus.Delete:
                        changedList.Add(item.UID);
                        deletedList.Add(item);
                        break;
                    case RecordStatus.Insert:
                        insertList.Add(item);
                        break;
                    case RecordStatus.Update:
                        changedList.Add(item.UID);
                        updateList.Add(item);
                        break;
                }
            }
            if (deletedList.Count > 0)
            {
                AccessHelper.DeletedValuesBehavior(deletedList);
            }
            if (updateList.Count > 0)
            {
                AccessHelper.UpdateValuesBehavior(updateList);
            }
            if (insertList.Count > 0)
            {
                changedList.AddRange(AccessHelper.InsertValuesBehavior(insertList));
            }
            return changedList;
        }

        /// <summary>
        /// 建構子，使用FISCA.Authentication的預設登入做為入主機。
        /// </summary>
        public AccessHelper()
        {
            if (!FISCA.Authentication.DSAServices.IsLogined)
                throw new Exception("FISCA.Authentication.DSAServices 尚未登入，無法取得預設DSConnection");
            DSConnection = FISCA.Authentication.DSAServices.DefaultConnection;
        }
        /// <summary>
        /// 連線到指定主機的建構子
        /// </summary>
        /// <param name="conn">指定連線主機</param>
        /// <overloads>建構子</overloads>
        public AccessHelper(DSConnection conn) { DSConnection = conn; }

        /// <summary>
        /// 取得連線主機
        /// </summary>
        public DSConnection DSConnection { get; private set; }
        /// <summary>
        /// 取得條件式編譯器
        /// </summary>
        public QueryCompiler GetCompiler<T>() where T : ActiveRecord
        {
            //lock (_Compiler)
            //{
            //    if (!_Compiler.ContainsKey(typeof(T)))
            //    {
            T sample = Create<T>();
            SyncSchema(DSConnection, sample);
            var compiler = new QueryCompiler();
            compiler.Fields.Add("UID", DataType.Number);
            foreach (var field in sample.fields)
            {
                compiler.Fields.Add(field.Name, field.Type);
            }
            return compiler;
            //_Compiler.Add(typeof(T), compiler);
            //    }
            //}
            //return _Compiler[typeof(T)];
        }
        /// <summary>
        /// 取得所有的資料
        /// </summary>
        /// <returns>所有的資料</returns>
        /// <overloads>取得資料</overloads>
        [AutoRetryOnWebException]
        public List<T> Select<T>() where T : ActiveRecord
        {

            T sampleRecord = Create<T>();
            SyncSchema(DSConnection, sampleRecord);
            List<T> reslut = new List<T>();

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.SetAttribute(".", "TableName", sampleRecord.TableName());
            helper.AddElement("Field");
            helper.AddElement("Field", "UID");
            foreach (var field in sampleRecord.fields)
            {
                helper.AddElement("Field", field.Name);
            }
            foreach (var itemRecord in this.DSConnection.CallService("UDTService.DML.Select", new DSRequest(helper)).GetContent().GetElements(sampleRecord.TableName()))
            {
                T t = Create<T>();
                t.UID = itemRecord.SelectSingleNode("uid").InnerText;
                t.DSConnection = this.DSConnection;
                foreach (var f in t.fields)
                {
                    #region 每個欄位得值都填進去
                    if (f.Target.PropertyType == typeof(string))
                        f.Target.SetValue(t, itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, null);
                    if (f.Target.PropertyType == typeof(bool))
                    {
                        bool i = false;
                        bool.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(bool?))
                    {
                        bool i = false;
                        if (bool.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(int))
                    {
                        int i = 0;
                        int.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(int?))
                    {
                        int i = 0;
                        if (int.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(decimal))
                    {
                        decimal i = 0;
                        decimal.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(decimal?))
                    {
                        decimal i = 0;
                        if (decimal.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(float))
                    {
                        float i = 0;
                        float.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(float?))
                    {
                        float i = 0;
                        if (float.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(double))
                    {
                        double i = 0;
                        double.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(double?))
                    {
                        double i = 0;
                        if (double.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(DateTime))
                    {
                        DateTime i = DateTime.MinValue;
                        DateTime.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(DateTime?))
                    {
                        DateTime i = DateTime.MinValue;
                        if (DateTime.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    #endregion
                }
                t.ResetFieldValues();
                reslut.Add(t);
            }
            return reslut;
        }
        /// <summary>
        /// 使用條件式取得資料
        /// </summary>
        /// <remarks>
        /// 條件式支援( ) and or >= &lt;= &lt;> > = &lt; is not in ,等運算子
        /// 可使用如 ( (uid=123 or uid=234 or uid=345) and uid &lt; 200 ) 之語法做為Select條件
        /// </remarks>
        /// <param name="query">條件式</param>
        /// <returns>符合條件的資料集合</returns>
        [AutoRetryOnWebException]
        public List<T> Select<T>(string query) where T : ActiveRecord
        {
            return Select<T>(GetCompiler<T>().Compiler(query));
        }
        /// <summary>
        /// 用UID取得資料
        /// </summary>
        /// <param name="uid">UID集合</param>
        /// <returns>符合條件的資料集合</returns>
        [AutoRetryOnWebException]
        public List<T> Select<T>(IEnumerable<string> uid) where T : ActiveRecord
        {
            InCondition condition = new InCondition() { Field = "UID" };
            condition.Values.AddRange(uid);
            if (condition.Values.Count == 0)
                return Select<T>();
            else
                return Select<T>(condition);
        }
        /// <summary>
        /// 使用已建立的<see cref="ICondition">ICondition</see>取得資料
        /// </summary>
        /// <param name="condtion">條件</param>
        /// <returns>符合條件的資料集合</returns>
        [AutoRetryOnWebException]
        public List<T> Select<T>(ICondition condtion) where T : ActiveRecord
        {
            T sampleRecord = Create<T>();
            SyncSchema(DSConnection, sampleRecord);

            List<T> reslut = new List<T>();

            DSXmlHelper helper = new DSXmlHelper("Request");
            helper.SetAttribute(".", "TableName", sampleRecord.TableName());
            helper.AddElement("Field");
            helper.AddElement("Field", "UID");
            foreach (var field in sampleRecord.fields)
            {
                helper.AddElement("Field", field.Name);
            }
            helper.AddElement("Condition");
            helper.AddElement("Condition", condtion.GetCondtionElement());
            foreach (var itemRecord in this.DSConnection.CallService("UDTService.DML.Select", new DSRequest(helper)).GetContent().GetElements(sampleRecord.TableName()))
            {
                T t = Create<T>();
                t.UID = itemRecord.SelectSingleNode("uid").InnerText;
                t.DSConnection = this.DSConnection;
                foreach (var f in t.fields)
                {
                    #region 每個欄位得值都填進去
                    if (f.Target.PropertyType == typeof(string))
                        f.Target.SetValue(t, itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, null);
                    if (f.Target.PropertyType == typeof(bool))
                    {
                        bool i = false;
                        bool.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(bool?))
                    {
                        bool i = false;
                        if (bool.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(int))
                    {
                        int i = 0;
                        int.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(int?))
                    {
                        int i = 0;
                        if (int.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(decimal))
                    {
                        decimal i = 0;
                        decimal.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(decimal?))
                    {
                        decimal i = 0;
                        if (decimal.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(float))
                    {
                        float i = 0;
                        float.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(float?))
                    {
                        float i = 0;
                        if (float.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(double))
                    {
                        double i = 0;
                        double.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(double?))
                    {
                        double i = 0;
                        if (double.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    if (f.Target.PropertyType == typeof(DateTime))
                    {
                        DateTime i = DateTime.MinValue;
                        DateTime.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i);
                        f.Target.SetValue(t, i, null);
                    }
                    if (f.Target.PropertyType == typeof(DateTime?))
                    {
                        DateTime i = DateTime.MinValue;
                        if (DateTime.TryParse(itemRecord.SelectSingleNode(f.Name.ToLower()).InnerText, out i))
                            f.Target.SetValue(t, i, null);
                        else
                            f.Target.SetValue(t, null, null);
                    }
                    #endregion
                }
                t.ResetFieldValues();
                reslut.Add(t);
            }
            return reslut;
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="values">要新增的資料</param>
        /// <returns>新資料的UID的集合</returns>
        public List<string> InsertValues(IEnumerable<ActiveRecord> values)
        {
            foreach (var item in values)
            {
                item.DSConnection = this.DSConnection;
            }
            return AccessHelper.InsertValuesBehavior(values);
        }
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="values">要更新的資料集合</param>
        public void UpdateValues(IEnumerable<ActiveRecord> values)
        {
            foreach (var item in values)
            {
                if (item.DSConnection != this.DSConnection)
                    throw new Exception("傳入內容包含其他連線主機的資料，操作不合法。");
            }
            AccessHelper.UpdateValuesBehavior(values);
        }
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="values">要刪除的資料集合</param>
        public void DeletedValues(IEnumerable<ActiveRecord> values)
        {
            foreach (var item in values)
            {
                if (item.DSConnection != this.DSConnection)
                    throw new Exception("傳入內容包含其他連線主機的資料，操作不合法。");
            }
            AccessHelper.DeletedValuesBehavior(values);
        }
        /// <summary>
        /// 儲存全部，將集合內所有的資料自動依RecordStatus屬性進行新增修改與刪除的動作
        /// </summary>
        /// <param name="records"></param>
        /// <returns>所有變更資料(含新增)的UID</returns>
        public List<string> SaveAll(IEnumerable<ActiveRecord> records)
        {
            return AccessHelper.SaveAllBehavior(records);
        }
    }
}
