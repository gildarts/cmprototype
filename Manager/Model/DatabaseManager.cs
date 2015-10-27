using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace Manager
{
    class DatabaseManager
    {
        private ServerManager Manager { get; set; }

        //private AccountData Account { get; set; }

        public string TargetDatabase { get; set; }

        public DatabaseManager(ServerManager manager, string database)
        {
            Manager = manager;
            //Account = account;
            TargetDatabase = database;
        }

        public XmlElement ExecuteUpdate(string cmd)
        {
            List<string> cmds = SplitCommand(cmd);
            return Manager.ExecuteUpdateCommands(cmds, TargetDatabase);
        }

        public XmlElement ExecuteQuery(string cmd)
        {
            return Manager.ExecuteQueryCommand(cmd, TargetDatabase);
        }

        public string GetDBMSVersionString()
        {
            XmlElement db = ExecuteQuery("select  version();");
            return (db.SelectSingleNode("Record/version").InnerText);
        }

        public string GetTargetDatabase()
        {
            XmlElement db = ExecuteQuery("select current_database();");
            return (db.SelectSingleNode("Record/current_database").InnerText);
        }

        public long GetDatabaseSize()
        {
            XmlElement size = ExecuteQuery(string.Format("select pg_database_size('{0}')", TargetDatabase));
            return long.Parse(size.SelectSingleNode("Record/pg_database_size").InnerText);
        }

        public long GetTableSize(string tableName)
        {
            XmlElement size = ExecuteQuery(string.Format("select (pg_total_relation_size('{0}'::regclass)) as tablesize", tableName));
            return long.Parse(size.SelectSingleNode("Record/tablesize").InnerText);
        }

        public List<string> ListTables()
        {
            XmlElement data = ExecuteQuery("SELECT table_name FROM information_schema.tables where table_schema='public';");
            List<string> infos = new List<string>();
            foreach (XmlElement each in data.SelectNodes("Record"))
            {
                infos.Add(each.SelectSingleNode("table_name").InnerText);
            }
            return infos;
        }

        public List<string> ListViews()
        {
            XmlElement data = ExecuteQuery("SELECT table_name FROM information_schema.views where table_schema='public';");
            List<string> infos = new List<string>();
            foreach (XmlElement each in data.SelectNodes("Record"))
            {
                infos.Add(each.SelectSingleNode("table_name").InnerText);
            }
            return infos;
        }

        public List<string> ListSequences()
        {
            XmlElement data = ExecuteQuery("SELECT sequence_name FROM information_schema.sequences where sequence_schema='public';");
            List<string> infos = new List<string>();
            foreach (XmlElement each in data.SelectNodes("Record"))
            {
                infos.Add(each.SelectSingleNode("sequence_name").InnerText);
            }
            return infos;
        }

        public List<string> ListTrigger()
        {
            XmlElement data = ExecuteQuery("SELECT routine_name FROM information_schema.routines where routine_schema='public' and data_type='trigger';");
            List<string> infos = new List<string>();
            foreach (XmlElement each in data.SelectNodes("Record"))
            {
                infos.Add(each.SelectSingleNode("routine_name").InnerText);
            }
            return infos;
        }

        public List<RoleData> ListRoles()
        {
            //select rolname from pg_roles
            XmlElement data = ExecuteQuery("select rolname,rolcanlogin from pg_roles order by rolcanlogin,rolname;");
            List<RoleData> infos = new List<RoleData>();
            foreach (XmlElement each in data.SelectNodes("Record"))
            {
                RoleData role = new RoleData();
                role.Name = each.SelectSingleNode("rolname").InnerText;
                role.CanLogin = each.SelectSingleNode("rolcanlogin").InnerText == "t" ? true : false;
                infos.Add(role);
            }
            return infos;
        }

        private List<string> SplitCommand(string command)
        {
            List<StringBuilder> commands = new List<StringBuilder>();

            Regex spliter = new Regex(@";\s*$", RegexOptions.Compiled);

            StringReader cmdReader = new StringReader(command);
            StringBuilder singleCmd = new StringBuilder();
            commands.Add(singleCmd);

            while (cmdReader.Peek() >= 0)
            {
                string matchSource = cmdReader.ReadLine();
                if (string.IsNullOrWhiteSpace(matchSource)) continue;

                singleCmd.AppendLine(matchSource);

                if (spliter.Match(matchSource).Success)
                {
                    singleCmd = new StringBuilder();
                    commands.Add(singleCmd);
                }
            }
            cmdReader.Close();

            return new List<string>(
                from each in commands
                where !string.IsNullOrWhiteSpace(each.ToString())
                select each.ToString());
        }

        public class RoleData
        {
            public string Name { get; set; }

            public bool CanLogin { get; set; }
        }
    }
}
