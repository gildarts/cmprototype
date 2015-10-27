using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Manager
{
    class ExtraSecurityToken
    {
        private FISCA.DSAClient.SecurityToken Token { get; set; }

        public ExtraSecurityToken(FISCA.DSAClient.SecurityToken token)
        {
            Token = token;
        }

        public string TokenType { get { return Token.TokenType; } }

        public string XmlString
        {
            get
            {
                Type t = Token.GetType();
                PropertyInfo method = t.GetProperty("XmlString", BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic);
                object val = method.GetValue(Token, null);

                return val + "";
            }
        }
    }

    class CloneSecurityToken : FISCA.DSA.SecurityToken
    {
        public CloneSecurityToken(string type, string content)
        {
            token_type = type;
            xml_string = content;
        }

        private string token_type;
        public override string TokenType
        {
            get { return token_type; }
        }

        private string xml_string;
        protected override string XmlString
        {
            get { return xml_string; }
        }
    }
}
