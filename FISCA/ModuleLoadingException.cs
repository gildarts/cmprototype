using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA
{
    class ModuleLoadingException : Exception
    {
        public ModuleLoadingException(string msg, string assemblyString, Exception innerException)
            : base(msg, innerException)
        {
            AssemblyString = assemblyString;
        }

        public string AssemblyString { get; private set; }
    }
}