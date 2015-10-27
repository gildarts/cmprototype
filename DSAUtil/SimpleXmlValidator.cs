/*
 * Create Date�G2005/12/01
 * Author Name�GYaoMing Huang
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
    ///// �зǱ`�Ϊ�Schema�`�ơA���O�w�����ϥΡA����ĳ�b�{�����ϥΡC
    ///// </summary>
    //public static class StandardXmlSchema
    //{
    //    /// <summary>
    //    /// ��Envelope��XmlSchema���(���`�Ƥ����ϥ�)�C
    //    /// </summary>
    //    public const string Envelope = "EnvelopeSchema";

    //    /// <summary>
    //    /// ��DSRequest��XmlSchema���(���`�Ƥ����ϥ�)�C
    //    /// </summary>
    //    public const string Request = "RequestSchema";

    //    /// <summary>
    //    /// ��DSResponse��XmlSchema���(���`�Ƥ����ϥ�)�C
    //    /// </summary>
    //    public const string Response = "ResponseSchema";

    //    /// <summary>
    //    /// �ª�DSRequest��XmlSchema���(���`�Ƥ����ϥ�)�C
    //    /// </summary>
    //    public const string RequestOld = "RequestSchemaOld";
    //}

    ///// <summary>
    ///// �N�� XmlSchema ��������Ҥu�����O�C
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
    //    /// ���ݩʤ����ϥΡC
    //    /// </summary>
    //    public static SimpleXmlValidator Standard
    //    {
    //        get { return _standardValidator; }
    //    }

    //    /// <summary>
    //    /// �x�s�����Ҧ��� <see cref="SchemaSet"/> ���󶰦X�C
    //    /// </summary>
    //    private Dictionary<string, XmlReaderSettings> _XmlSchemas = new Dictionary<string, XmlReaderSettings>();

    //    /// <summary>
    //    /// �q�ɮ�Ū�� XmlSchema�A�é�J�֨��ϡC
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema �O�W�C</param>
    //    /// <param name="targetNS">�ؼЩR�W�Ŷ��C</param>
    //    /// <param name="fileName">XmlSchema �ɮסC</param>
    //    public void AddSchemaFromFile(string nickName, string targetNS, string fileName)
    //    {
    //        AddSchema(nickName, targetNS, File.ReadAllText(fileName));
    //    }

    //    /// <summary>
    //    /// �q�ɮ�Ū�� XmlSchema�A�é�J�֨��ϡC
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema �O�W�C</param>
    //    /// <param name="fileName">XmlSchema �ɮסC</param>
    //    public void AddSchemaFromFile(string nickName, string fileName)
    //    {
    //        AddSchemaFromFile(nickName, "", fileName);
    //    }

    //    /// <summary>
    //    /// �q�r����J XmlSchema ���C
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema �O�W�C</param>
    //    /// <param name="targetNS">XmlSchema ���ؼЩR�W�Ŷ��C</param>
    //    /// <param name="schemaContent"></param>
    //    public void AddSchema(string nickName, string targetNS, string schemaContent)
    //    {
    //        XmlReader xr = XmlReader.Create(new StringReader(schemaContent), null);
    //        AddSchema(nickName, targetNS, xr);
    //    }

    //    /// <summary>
    //    /// �q�r����J XmlSchema ���C�w�]�|�ϥΪŦr��(String.Emtpy)���R�W�Ŷ��C
    //    /// </summary>
    //    /// <param name="nickName">XmlSchema �O�W�C</param>
    //    /// <param name="schemaContent">XmlSchema �����e�C</param>
    //    public void AddSchema(string nickName, string schemaContent)
    //    {
    //        AddSchema(nickName, "", schemaContent);
    //    }

    //    /// <summary>
    //    /// �s�W XmlSchema ����O���餤�A��K�������� Xml ���C
    //    /// </summary>
    //    /// <param name="Name">XmlSchema �O�W�C</param>
    //    /// <param name="Schema">XmlSchema ���e�C</param>
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
    //    /// ����Xml���C
    //    /// </summary>
    //    /// <param name="SchemaName">�n���Ϊ�XmlSchema�W�١C</param>
    //    /// <param name="XmlContent">�n���Ҫ�Xml��ơC</param>
    //    /// <returns>���ҬO�_���\�A���\�^��True�A���Ѧ^��False�C</returns>
    //    public bool ValidateDocument(string nickName, string xmlContent)
    //    {
    //        if (!_XmlSchemas.ContainsKey(nickName))
    //            throw new Exception("���w�� XmlSchema �O�W���s�b�G" + nickName);

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
    //    /// ���o�̫�@���{���Ҫ����~�T���C
    //    /// </summary>
    //    /// <returns>���~�T���C</returns>
    //    public string GetLastError()
    //    {
    //        return _errorMessage;
    //    }

    //}
}
