using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSA;
using System.Windows.Forms;

namespace Manager
{
    class LogQuery
    {
        public enum GroupRange
        {
            Month,
            Week,
            Day,
            Hour,
            Minute,
            Second
        }

        internal enum ResponseTimeOrderField
        {
            group_time,
            pcount,
            avg_rsp_time,
            max_rsp_time,
            min_rsp_time,
            total_rsp_time
        }

        private ServerManager Manager { get; set; }

        private DatabaseManager DBManager { get; set; }

        private string LogDatabase { get; set; }

        private AccountData Account { get; set; }

        public XmlElement ExecuteManualQuery(string sql)
        {
            return Manager.ExecuteQueryCommand(sql, LogDatabase);
        }

        public LogQuery(ServerManager manager, string logDatabase, AccountData account)
        {
            Manager = manager;
            DBManager = new DatabaseManager(manager, logDatabase);
            LogDatabase = logDatabase;
            Account = account;
        }

        public int GetLogTotalCount()
        {
            string sql = "select count(*) from log";

            XmlElement rsp = Manager.ExecuteQueryCommand(sql, LogDatabase);
            FISCA.XHelper hlp = new FISCA.XHelper(rsp);

            return hlp.TryGetInteger("Record/count", -1);
        }

        public long GetLogDatabaseSize()
        {
            return DBManager.GetTableSize("log");
        }

        public XmlElement GetLogDetail(string id, string identity, string logTime)
        {
            string sql = "select *,(exception!='') as occure_exception from log where {0}";

            List<string> conds = new List<string>();
            if (!string.IsNullOrWhiteSpace(id))
                conds.Add(string.Format("id='{0}'", id));

            if (!string.IsNullOrWhiteSpace(identity))
                conds.Add(string.Format("identity='{0}'", identity));

            if (!string.IsNullOrWhiteSpace(logTime))
                conds.Add(string.Format("log_time='{0}'", logTime));

            string args = string.Empty;
            if (conds.Count > 0)
                args = string.Join(" and ", conds.ToArray());
            else
                throw new ArgumentException("必須指定其中一個條件。");

            return Manager.ExecuteQueryCommand(string.Format(sql, args), LogDatabase);
        }

        /*
select date_trunc('{0}',log_time) group_time,count(*) pcount,avg(process_time) avg_rsp_time,max(process_time) max_rsp_time,min(process_time) min_rsp_time,sum(process_time) total_rsp_time
from log
{1} --where date_trunc('second',log_time) > '2010/9/7 14:10:00' and date_trunc('second',log_time) <'2010/9/7 14:20:00' and dsa_application_name='chhs.tw' and contract_name = 'schoolaccess' and dsa_service_name='Student.DoSomething'
group by group_time
order by {2}
limit {3}
         * group_time,pcount,avg_rsp_time,max_rsp_time,min_rsp_time,total_rsp_time
 **/

        public XmlElement CalcResponseTime(GroupRange range,
            string school,
            string contract,
            string service,
            DateTime? start,
            DateTime? end,
            ResponseTimeOrderField order,
            SortOrder sortOrder,
            int returnLimit,
            out string execSql)
        {
            string arg0 = range.ToString();

            string arg1 = string.Empty;
            List<string> conds = new List<string>();

            if (!string.IsNullOrWhiteSpace(school))
                conds.Add(string.Format("dsa_application_name='{0}'", school));

            if (!string.IsNullOrWhiteSpace(contract))
                conds.Add(string.Format("dsa_contract_name='{0}'", contract));

            if (!string.IsNullOrWhiteSpace(service))
                conds.Add(string.Format("dsa_service_name='{0}'", service));

            if (start.HasValue)
                conds.Add(string.Format("log_time > '{0}'", start.Value.ToString("yyyy/MM/dd HH:mm:ss")));

            if (end.HasValue)
                conds.Add(string.Format("log_time < '{0}'", end.Value.ToString("yyyy/MM/dd HH:mm:ss")));

            if (conds.Count > 0)
                arg1 = "where " + string.Join(" and ", conds.ToArray());

            string arg2 = order.ToString() + ((sortOrder == SortOrder.Ascending) ? " asc" : " desc");

            int arg3 = returnLimit;

            string sql = string.Format(SQL.ServerRspTime, arg0, arg1, arg2, arg3);
            execSql = sql;

            return Manager.ExecuteQueryCommand(sql, LogDatabase);
        }

    }
}
