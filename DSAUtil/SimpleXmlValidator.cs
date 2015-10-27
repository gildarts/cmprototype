/*
 * Create Date：2005/12/01
 * Author Name：YaoMing Huang
 */
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    ///// <summary>
    ///// 標準常用的Schema常數，類別庫內部使用，不建議在程式中使用。
    ///// </summary>
    //public static class StandardXmlSchema
    //{
    //    /// <summary>
    //    /// 基本Envelope的XmlSchema文件(此常數內部使用)。
    //    /// </summary>
    //    public const string Envelope = "EnvelopeSchema";

    //    /// <summary>
    //    /// 基本DSRequest的XmlSchema文件(此常數內部使用)。
    //    /// </summary>
    //    public const string Request = "RequestSchema";

    //    /// <summary>
    //    /// 基本DSResponse的XmlSchema文件(此常數內部使用)。
    //    /// </summary>
    //    public const string Response = "ResponseSchema";

    //    /// <summary>
    //    /// 舊版DSRequest的XmlSchema文件(此常數內部使用)。
    //    /// </summary>
    //    public const string RequestOld = "RequestSchemaOld";
    //}

    ///// <summary>
    ///// 代表 XmlSchema 的文件驗證工具類別。
    ///// </summary>
    //public class SimpleXmlValidator
    //{
    //    private static SimpleXmlValidator _standardValidator;

    //    static SimpleXmlValidator()
    //    {
    //        _standardValidator = new SimpleXmlValidator();
    //        _standardValidator.AddSchema("RequestSchema", CommonResources.RequestSchema);
    //        _standardValidator.AddSchema("ResponseSchema", CommonResources.ResponseSchema);
    //        _standardValidator.AddSchema("EnvelopeSchema", CommonResources.EnvelopeSchema);
    //        _standardValidator.AddSchema("RequestSchemaOld", CommonResources.RequestSchemaOld);
    //    }

    //    /// <summary>
    //    /// 此屬性內部使用。
    //    /// </summary>
    //    public static SimpleXmlValidator Standard
    //    {
    //        get { return _standardValidator; }
    //    }

    //    /// <summary>
    //    /// 儲存內部所有的 <see cref="SchemaSet"/> 物件集合。
    //    /// </summary>
    //    private Dictionary<string, XmlReaderSettings> _XmlSchemas = new Dictionary<string, XmlReaderSettings>();

    //    /// <summary>
    //    /// 從檔案讀取 XmlSchema，並放入快取區。
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema 別名。</param>
    //    /// <param name="targetNS">目標命名空間。</param>
    //    /// <param name="fileName">XmlSchema 檔案。</param>
    //    public void AddSchemaFromFile(string nickName, string targetNS, string fileName)
    //    {
    //        AddSchema(nickName, targetNS, File.ReadAllText(fileName));
    //    }

    //    /// <summary>
    //    /// 從檔案讀取 XmlSchema，並放入快取區。
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema 別名。</param>
    //    /// <param name="fileName">XmlSchema 檔案。</param>
    //    public void AddSchemaFromFile(string nickName, string fileName)
    //    {
    //        AddSchemaFromFile(nickName, "", fileName);
    //    }

    //    /// <summary>
    //    /// 從字串載入 XmlSchema 文件。
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema 別名。</param>
    //    /// <param name="targetNS">XmlSchema 的目標命名空間。</param>
    //    /// <param name="schemaContent"></param>
    //    public void AddSchema(string nickName, string targetNS, string schemaContent)
    //    {
    //        XmlReader xr = XmlReader.Create(new StringReader(schemaContent), null);
    //        AddSchema(nickName, targetNS, xr);
    //    }

    //    /// <summary>
    //    /// 從字串載入 XmlSchema 文件。預設會使用空字串(String.Emtpy)的命名空間。
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema 別名。</param>
    //    /// <param name="schemaContent">XmlSchema 的內容。</param>
    //    public void AddSchema(string nickName, string schemaContent)
    //    {
    //        AddSchema(nickName, "", schemaContent);
    //    }

    //    /// <summary>
    //    /// 新增 XmlSchema 文件到記憶體中，方便之後驗證 Xml 文件。
    //    /// </summary>
    //    /// <param name="Name">XmlSchema 別名。</param>
    //    /// <param name="Schema">XmlSchema 內容。</param>
    //    public void AddSchema(string nickName, string targetNS, XmlReader schema)
    //    {
    //        XmlSchemaSet oSchema = new XmlSchemaSet();
    //        oSchema.Add(targetNS, schema);
    //        oSchema.Compile();
    //        schema.Close();

    //        XmlReaderSettings Settings = new XmlReaderSettings();
    //        Settings.ValidationType = ValidationType.Schema;
    //        //Settings.Schemas.Add(oSchema);
    //        Settings.ValidationEventHandler += new ValidationEventHandler(ValidateDocumentEventHandler);

    //        _XmlSchemas.Add(nickName, Settings);
    //    }

    //    public bool ContainSchema(string nickName)
    //    {
    //        return _XmlSchemas.ContainsKey(nickName);
    //    }

    //    /// <summary>
    //    /// 驗證Xml文件。
    //    /// </summary>
    //    /// <param name="SchemaName">要取用的XmlSchema名稱。</param>
    //    /// <param name="XmlContent">要驗證的Xml資料。</param>
    //    /// <returns>驗證是否成功，成功回傳True，失敗回傳False。</returns>
    //    public bool ValidateDocument(string nickName, string xmlContent)
    //    {
    //        if (!_XmlSchemas.ContainsKey(nickName))
    //            throw new Exception("指定的 XmlSchema 別名不存在：" + nickName);

    //        XmlReaderSettings Settings = _XmlSchemas[nickName];

    //        StringReader sr = new StringReader(xmlContent);
    //        XmlReader ContentReader = XmlReader.Create(sr, Settings);

    //        _documentIsValidate = true;

    //        try
    //        {
    //            while (ContentReader.Read())
    //                if (!_documentIsValidate) break;
    //        }
    //        catch (Exception E)
    //        {
    //            _documentIsValidate = false;
    //            _errorMessage = E.Message;
    //        }

    //        sr.Close();

    //        return _documentIsValidate;
    //    }

    //    private bool _documentIsValidate = true;
    //    private void ValidateDocumentEventHandler(object sender, ValidationEventArgs E)
    //    {
    //        _documentIsValidate = false;
    //        _errorMessage = E.Message;
    //    }

    //    private string _errorMessage = "";
    //    /// <summary>
    //    /// 取得最後一次認驗證的錯誤訊息。
    //    /// </summary>
    //    /// <returns>錯誤訊息。</returns>
    //    public string GetLastError()
    //    {
    //        return _errorMessage;
    //    }

    //}
}
