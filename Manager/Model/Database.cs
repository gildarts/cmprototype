using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    class Database
    {
        public const string ParserPattern = @"(?<pgstring>jdbc:postgresql://[a-zA-Z0-9.\-_:]+/)(?<db>.*)";

        public Database()
        {
        }

        public string Name { get; set; }

        public string OID { get; set; }

        public string Description { get; set; }
    }
}
