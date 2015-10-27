using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSA;
using System.Data;
using System.Xml.Linq;

namespace Manager.Interfaces
{
    public interface IAppUpgrader
    {
        string Description { get; }

        void DoUpgrade(Connection adminConn, ISqlCommand cmd, Dictionary<string, string> args);
    }

    public interface ISqlCommand
    {
        XElement ExecuteQuery(string cmd);

        void ExecuteUpdate(List<string> cmds);
    }
}
