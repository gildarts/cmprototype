using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FISCA.DSA;
using System.Text.RegularExpressions;

namespace Manager
{
    class Application
    {
        public const string SharedName = "shared";

        public Application(Server owner)
        {
            Owner = owner;
        }

        public void LoadDefinition(XmlElement definition)
        {
            FISCA.XHelper Definition = new FISCA.XHelper(definition);

            Name = Definition.GetText("@Name");
            Enabled = bool.Parse(Definition.GetText("Property[@Name='Enabled']"));

            DatabaseFullName = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_url']");
            Regex rx = new Regex(Database.ParserPattern);
            DatabaseName = rx.Replace(DatabaseFullName, "${db}");

            DMLUserName = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_user']");
            DMLPassword = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_pwd']");
            DDLUserName = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_udt_user']");
            DDLPassword = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_udt_pwd']");
            SchoolCode = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='school_code']");
            Comment = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='app_comment']");

            if (string.Equals(Name, SharedName, StringComparison.OrdinalIgnoreCase))
                IsShared = true;
            else
                IsShared = false;
        }

        public Server Owner { get; private set; }

        /// <summary>
        /// 不可直接修改此屬性資料。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 不可直接修改此屬性資料。
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// 不可直接修改此屬性資料。
        /// </summary>
        public string DatabaseFullName { get; private set; }

        public string DatabaseName { get; private set; }

        public string DMLUserName { get; private set; }

        public string DMLPassword { get; private set; }

        public string DDLUserName { get; private set; }

        public string DDLPassword { get; private set; }

        public bool IsShared { get; private set; }

        /// <summary>
        /// 不可直接修改此屬性資料。
        /// </summary>
        public string Comment { get; private set; }

        public string SchoolCode { get; private set; }

        public void RaiseNameChanged()
        {
            if (NameChanged != null) NameChanged(this, EventArgs.Empty);
        }

        public void RaiseConfigChanged()
        {
            if (ConfigChanged != null) ConfigChanged(this, EventArgs.Empty);
        }

        public Argument GetArgument()
        {
            Argument arg = new Argument(Name);
            arg.DatabaseFullName = DatabaseFullName;
            arg.DMLUserName = DMLUserName;
            arg.DMLPassword = DMLPassword;
            arg.DDLUserName = DDLUserName;
            arg.DDLPassword = DDLPassword;
            arg.SchoolCode = SchoolCode;
            arg.Comment = Comment;

            return arg;
        }

        /// <summary>
        /// 當應用程式名稱變更時。
        /// </summary>
        public event EventHandler NameChanged;

        /// <summary>
        /// 當 params 變更時。
        /// </summary>
        public event EventHandler ConfigChanged;

        public class Argument
        {
            internal Argument(string appName)
            {
                Name = appName;
            }

            public string Name { get; set; }

            public string DatabaseFullName { get; set; }

            public string DatabaseName
            {
                get
                {
                    Regex rx = new Regex(Database.ParserPattern);
                    return rx.Replace(DatabaseFullName, "${db}");
                }
            }

            public string DMLUserName { get; set; }

            public string DMLPassword { get; set; }

            public string DDLUserName { get; set; }

            public string DDLPassword { get; set; }

            public string SchoolCode { get; set; }

            public string Comment { get; set; }

            public void SetDatabaseName(string name)
            {
                Regex rx = new Regex(Database.ParserPattern);
                DatabaseFullName = rx.Replace(DatabaseFullName, "${pgstring}" + name);
            }

            public XmlElement ToXml()
            {
                FISCA.XHelper helper = new FISCA.XHelper("<Application/>");
                FISCA.XHelper param = null;

                helper.SetAttribute(".", "Name", Name);
                param = new FISCA.XHelper(helper.AddElement(".", "Param", DatabaseFullName));
                param.SetAttribute(".", "Name", "db_url");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", DMLUserName));
                param.SetAttribute(".", "Name", "db_user");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", DMLPassword));
                param.SetAttribute(".", "Name", "db_pwd");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", DDLUserName));
                param.SetAttribute(".", "Name", "db_udt_user");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", DDLPassword));
                param.SetAttribute(".", "Name", "db_udt_pwd");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", SchoolCode));
                param.SetAttribute(".", "Name", "school_code");

                param = new FISCA.XHelper(helper.AddElement(".", "Param", Comment));
                param.SetAttribute(".", "Name", "app_comment");

                return helper.Data;
            }
        }
    }
}
