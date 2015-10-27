using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSA;
using System.Xml;
using System.Threading.Tasks;
using System.IO;

namespace Manager
{
    /// <summary>
    /// 代表 DSA Server 的管理介面 API。
    /// </summary>
    class ServerManager
    {
        public Connection Connection { get; set; }

        private Server Srv { get; set; }

        internal ServerManager(Server server)
        {
            Connection = server.Connection;
            Srv = server;
        }

        /// <summary>
        /// 取出 Server 所有資訊。
        /// </summary>
        /// <returns></returns>
        internal XmlElement GetServerInfo()
        {
            return Connection.SendRequest("Server.GetServerInfo", new Envelope(new FISCA.XHelper())).ResponseBody();
        }

        internal XmlElement ListApplication()
        {
            return Connection.SendRequest("Server.ListApplication", new Envelope(new FISCA.XHelper())).ResponseBody();
        }

        internal XmlElement ListApplication(string name)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", name);
            return Connection.SendRequest("Server.ListApplication", new Envelope(req)).ResponseBody();
        }

        internal XmlElement UpdateServerConfiguration(XmlElement config)
        {
            return Connection.SendRequest("LoadBalance.UpdateServerConfiguration", new Envelope(new FISCA.XHelper(config))).ResponseBody();
        }

        internal void CloneApplication(string target)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "To", target);

            Connection.SendRequest("Server.CloneApplication", new Envelope(req));
        }

        internal void RemoveApplication(string target)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", target);

            Connection.SendRequest("Server.RemoveApplication", new Envelope(req));
        }

        internal XmlElement UpdateServices(bool enforceUpdate)
        {
            if (enforceUpdate)
            {
                FISCA.XHelper req = new FISCA.XHelper();
                req.AddElement(".", "EnforceUpdate", "true");
                return Connection.SendRequest("LoadBalance.UpdateServices", new Envelope(req)).ResponseBody(); ;
            }
            else
                return Connection.SendRequest("LoadBalance.UpdateServices", new Envelope(new FISCA.XHelper())).ResponseBody();
        }

        /// <summary>
        /// 列出所有資料庫
        /// </summary>
        /// <returns></returns>
        internal List<Database> ListDatabases()
        {
            FISCA.XHelper rsp = new FISCA.XHelper(ExecuteQueryCommand(SQL.ListDatabase, string.Empty));
            List<Database> databases = new List<Database>();
            foreach (XmlElement each in rsp.GetElements("Record"))
            {
                FISCA.XHelper record = new FISCA.XHelper(each);
                Database db = new Database();
                db.OID = record.GetText("oid");
                db.Name = record.GetText("datname");
                db.Description = record.GetText("description");

                databases.Add(db);
            }
            return databases;
        }

        internal Exception TestAccount(AccountData account)
        {
            try
            {
                FISCA.XHelper req = new FISCA.XHelper();
                req.AddElement(".", "Command", "select now();");

                if (Srv.SuperUser != AccountData.Default)
                {
                    req.SetAttribute("Command", "UserName", account.UserName);
                    req.SetAttribute("Command", "Password", account.Password);
                }

                FISCA.XHelper rsp = Connection.SendRequest("Database.Query", new Envelope(req)).XResponseBody();

                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        internal XmlElement ReloadServer()
        {
            return Connection.SendRequest("LoadBalance.ReloadServer", new Envelope(new FISCA.XHelper())).ResponseBody();
        }

        internal void SetApplicationArgument(XmlElement arg)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", arg);
            Connection.SendRequest("Server.SetApplicationParam", new Envelope(req));
        }

        internal void SetApplicationsArgument(XmlElement arg)
        {
            FISCA.XHelper req = new FISCA.XHelper(arg);
            Connection.SendRequest("Server.SetApplicationParam", new Envelope(req));
        }

        internal void RenameApplication(string oldName, string newName)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", oldName);
            req.AddElement(".", "NewName", newName);
            Connection.SendRequest("Server.RenameApplication", new Envelope(req));
        }

        internal void SetApplicationEnable(string name, bool enabled)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", name);
            req.AddElement(".", "Enabled", enabled.ToString());
            Connection.SendRequest("Server.SetApplicationEnabled", new Envelope(req));
        }

        internal XmlElement TestConnection(string appName)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", appName);
            return Connection.SendRequest("Server.TestDefaultConnections", new Envelope(req)).ResponseBody(); ;
        }

        internal XmlElement TestConnection()
        {
            return Connection.SendRequest("Server.TestDefaultConnections", new Envelope(new FISCA.XHelper())).ResponseBody();
        }

        internal bool DatabaseExists(string dbName)
        {
            List<Database> dbs = ListDatabases();

            foreach (Database each in dbs)
            {
                if (each.Name == dbName)
                    return true;
            }

            return false;
        }

        internal void CreateNewDatabase(string dbName, string templateDB)
        {
            string cmd = string.Format(SQL.NewDatabase, dbName, templateDB);

            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "Command", cmd);

            if (Srv.SuperUser != AccountData.Default)
            {
                req.SetAttribute("Command", "UserName", Srv.SuperUser.UserName);
                req.SetAttribute("Command", "Password", Srv.SuperUser.Password);
            }

            Connection.SendRequest("Database.Command", new Envelope(req)).XResponseBody();
        }

        internal XmlElement ResetLicenseCode(string dbName, string code)
        {
            string sql = string.Format("update sys_security set secure_code ='{0}'", code);

            return ExecuteUpdateCommand(sql, dbName);
        }

        internal XmlElement GetLog(string logGuid)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "LogID", logGuid);
            return Connection.SendRequest("Log.GetProcessLog", new Envelope(req)).XResponseBody().GetElement("Log");
        }

        internal XmlElement GetLogs(IEnumerable<string> logGuids)
        {
            FISCA.XHelper req = new FISCA.XHelper();

            foreach (string each in logGuids)
                req.AddElement(".", "LogID", each);

            return Connection.SendRequest("Log.GetLogs", new Envelope(req)).ResponseBody(); ;
        }

        internal void TerminalDBConnection(string dbName)
        {
            string sql = string.Format("select procpid from pg_stat_activity where datname='{0}'", dbName);
            FISCA.XHelper rsp = new FISCA.XHelper(ExecuteQueryCommand(sql, dbName));

            Parallel.ForEach(rsp.GetElements("Record"), each =>
            {
                string pid = each.SelectSingleNode("procpid").InnerText;

                string termsql = string.Format("select pg_terminate_backend({0});", pid);
                ExecuteQueryCommand(termsql, dbName);
            });
        }

        //internal int GetDatabaseSize(string dbName, AccountData account)
        //{
        //    XmlElement size = ExecuteQueryCommand(string.Format("select pg_database_size('{0}')", dbName), string.Empty, account);
        //    return int.Parse(size.SelectSingleNode("Record/pg_database_size").InnerText);
        //}

        //internal long GetTableSize(string dbName, string tableName, AccountData account)
        //{
        //    //select pg_size_pretty(pg_total_relation_size('log'::regclass))
        //    XmlElement size = ExecuteQueryCommand(string.Format("select (pg_total_relation_size('{0}'::regclass)) as tablesize", tableName), dbName, account);
        //    return long.Parse(size.SelectSingleNode("Record/tablesize").InnerText);
        //}

        internal XmlElement ExecuteQueryCommand(string cmd, string database)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "Command", cmd);

            if (!string.IsNullOrWhiteSpace(database))
                req.SetAttribute("Command", "Database", database);

            if (Srv.SuperUser != AccountData.Default)
            {
                req.SetAttribute("Command", "UserName", Srv.SuperUser.UserName);
                req.SetAttribute("Command", "Password", Srv.SuperUser.Password);
            }

            FISCA.XHelper rsp = Connection.SendRequest("Database.Query", new Envelope(req)).XResponseBody();

            return rsp.Data;
        }

        internal XmlElement ExecuteUpdateCommand(string cmd, string database, string userName, string password)
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "Command", cmd);

            if (!string.IsNullOrWhiteSpace(database))
                req.SetAttribute("Command", "Database", database);

            if (Srv.SuperUser != AccountData.Default)
            {
                req.SetAttribute("Command", "UserName", userName);
                req.SetAttribute("Command", "Password", password);
            }

            FISCA.XHelper rsp = Connection.SendRequest("Database.Command", new Envelope(req)).XResponseBody();

            return rsp.Data;
        }

        internal XmlElement ExecuteUpdateCommand(string cmd, string database)
        {
            return ExecuteUpdateCommand(cmd, database, Srv.SuperUser.UserName, Srv.SuperUser.Password);
        }

        internal XmlElement ExecuteUpdateCommands(List<string> cmds, string database)
        {
            if (cmds.Count == 1)
                return ExecuteUpdateCommand(cmds[0], database);

            Version statableVer = new Version(4, 1, 7, 1);
            Version curVer = GetCurrentVersion();

            if (curVer >= statableVer)
                return UseCommandsService(cmds, database);
            else
                return SaveStatementsToLocal(cmds, database);
        }

        private XmlElement SaveStatementsToLocal(List<string> cmds, string database)
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, database + ".sql");
            StreamWriter statements = new StreamWriter(path, false, Encoding.UTF8);
            cmds.ForEach(x => statements.WriteLine(x));
            statements.Close();

            throw new Exception("因為您的 DSA 版本早於 4.1.7.1，不支援此功能。");
        }

        private XmlElement UseCommandsService(List<string> cmds, string database)
        {
            FISCA.XHelper req = new FISCA.XHelper();

            req.AddElement(".", "Commands");

            if (!string.IsNullOrWhiteSpace(database))
                req.SetAttribute("Commands", "Database", database);

            if (Srv.SuperUser != AccountData.Default)
            {
                req.SetAttribute("Commands", "UserName", Srv.SuperUser.UserName);
                req.SetAttribute("Commands", "Password", Srv.SuperUser.Password);
            }

            foreach (string each in cmds)
                req.AddElement("Commands", "Command", each);

            FISCA.XHelper rsp = Connection.SendRequest("Database.Commands", new Envelope(req)).XResponseBody();

            return rsp.Data;
        }

        private Version GetCurrentVersion()
        {
            Version ver;
            if (Version.TryParse(Srv.CoreVersion, out ver))
                return ver;
            else
                return new Version(4, 0, 0, 0);
        }
    }
}
