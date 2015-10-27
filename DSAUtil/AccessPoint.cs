using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;
    using System.Threading;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;

    internal enum ResolveType
    {
        Http, Dsa, Local, Undefined
    }

    /// <summary>
    /// 代表DSA的區域名稱。
    /// </summary>
    public class AccessPoint
    {
        private const string DoorwayRegex = @"^(?<prefix>\w+)://(?<path>.*)$";

        private string _accessPointUrl = "";
        private string _accessPointName = "";

        /// <summary>
        /// 初始化 Doorway 的新執行個體。
        /// </summary>
        /// <param name="accessPointName">DSA 的 AccessPoint 名稱。</param>
        /// <param name="accessPointUrl">實體位置。</param>
        public AccessPoint(string accessPointName, string accessPointUrl)
        {
            _accessPointName = accessPointName;
            _accessPointUrl = accessPointUrl;
        }

        /// <summary>
        /// DSA 的 AccessPoint 名稱。
        /// </summary>
        public string Name
        {
            get { return _accessPointName; }
        }

        /// <summary>
        /// DSA AccessPoint 的實體位置。
        /// </summary>
        public string Url
        {
            get { return _accessPointUrl; }
        }

        /// <summary>
        /// 使用預設的 DSA Name Server 解析 DSA 的 AccessPoint 名稱。
        /// </summary>
        /// <param name="doorway">要解析的 AccessPoint 名稱。</param>
        /// <returns>AccessPoint 的執行個體。</returns>
        /// <remarks>doorwayName 參數也接受 DoorwayUrl 的格式。</remarks>
        public static AccessPoint Resolve(string accessPoint)
        {
            List<String> dsnsServers = new List<string>();
            TextReader reader = new StringReader(CommonResources.DSNSServerList);
            while (reader.Peek() > 0) dsnsServers.Add(reader.ReadLine());

            return Resolve(accessPoint, dsnsServers.ToArray());
        }

        /// <summary>
        /// 指定 DSA Name Server 解析 DSA 的 AccessPoint 名稱。
        /// </summary>
        /// <param name="accessPoint">要解析的 AccessPoint 名稱。</param>
        /// <param name="dsnsUrl">負責解析 AccessPoint 名稱的主機實體位置。</param>
        /// <returns>AccessPoint 的執行個體。</returns>
        public static AccessPoint Resolve(string accessPointName, params string[] dsnsUrls)
        {
            string strUri = accessPointName;

            //如果是使用舊式的「!」，就將其取代成新式的「local://」。
            if (accessPointName.StartsWith("!"))
                strUri = accessPointName.Replace("!", "local://"); //!c:\ds -> local://c:\ds

            //如果是使用舊式的「#http://」，就將其取代成新式的「http://」。
            else if (accessPointName.StartsWith("#"))
                strUri = accessPointName.Replace("#", ""); //#http://yahoo.com -> http://yahoo.com

            AccessPoint apName = null;
            switch (DecideURIType(strUri))
            {
                case ResolveType.Http:
                    apName = new AccessPoint(strUri, strUri);
                    break;

                case ResolveType.Dsa:
                    apName = new AccessPoint(accessPointName, ResolveAccessPointName(GetUriPath(strUri), dsnsUrls));
                    break;

                case ResolveType.Local:
                    apName = new AccessPoint(strUri, GetUriPath(accessPointName));
                    break;

                default:
                    throw new ArgumentException(em.Get("UriPrefixNotSupport", new Replace("Prefix", accessPointName)), "accessPoint");
            }

            return apName;
        }

        private static string ResolveAccessPointName(string uri, string[] dsnsUrls)
        {
            DSNameResolve resolver = new DSNameResolve(dsnsUrls);

            string result = resolver.Resolve(uri);

            if (result == string.Empty)
                throw new ArgumentException(em.Get("ResolveAccessPointNameFail"));

            return result;
        }

        private static ResolveType DecideURIType(string URI)
        {
            switch (GetPrefix(URI).ToUpper())
            {
                case "HTTP":
                case "HTTPS":
                    return ResolveType.Http;

                case "DSA":
                    return ResolveType.Dsa;

                case "LOCAL":
                    return ResolveType.Local;

                case "": //沒有指定的話就使用DSA
                    return ResolveType.Dsa;

                default:
                    return ResolveType.Undefined;
            }
        }

        private static string GetPrefix(string URI)
        {
            Regex rex = new Regex(DoorwayRegex);

            Match m = rex.Match(URI);

            if (m.Success)
                return m.Result("${prefix}");
            else
                return "";
        }

        private static string GetUriPath(string URI)
        {
            Regex rex = new Regex(DoorwayRegex);

            Match m = rex.Match(URI);

            if (m.Success)
                return m.Result("${path}");
            else
                return URI;
        }
    }

    internal class DSNameResolve
    {
        private string[] _server_list;

        public DSNameResolve(string[] nameServerList)
        {
            _server_list = nameServerList;
        }

        public string Resolve(string name)
        {
            List<ResolveExecuter> resolvers = CreateExecutersByServerList(name);

            DoExecutersWork(resolvers);

            return ReceiveResult(resolvers);
        }

        private List<ResolveExecuter> CreateExecutersByServerList(string name)
        {
            List<ResolveExecuter> resolvers = new List<ResolveExecuter>();
            foreach (string each in _server_list)
                resolvers.Add(new ResolveExecuter(each, name));

            return resolvers;
        }

        private static void DoExecutersWork(List<ResolveExecuter> resolvers)
        {
            foreach (ResolveExecuter each in resolvers)
                each.DoResolve();
        }

        private string ReceiveResult(List<ResolveExecuter> resolvers)
        {
            List<ManualResetEvent> waitHandlers = GetUnCompleteResolversWaitHandle(resolvers);

            string result = string.Empty;
            while (waitHandlers.Count > 0)
            {
                WaitHandle.WaitAny(waitHandlers.ToArray());

                result = GetResolveResult(resolvers);

                //如果有資料代表已經取得解析後的資料，可以中斷了。
                if (result != string.Empty)
                {
                    //Debug.WriteLine("Resolve Result：" + result);
                    return result;
                }

                waitHandlers = GetUnCompleteResolversWaitHandle(resolvers);
            }

            //以免有漏網之魚。
            result = GetResolveResult(resolvers);
            return result;
        }

        private List<ManualResetEvent> GetUnCompleteResolversWaitHandle(List<ResolveExecuter> resolvers)
        {
            List<ManualResetEvent> waitHandlers = new List<ManualResetEvent>();

            foreach (ResolveExecuter each in resolvers)
                if (each.WaitRequired) waitHandlers.Add(each.WaitHandler);

            return waitHandlers;
        }

        private string GetResolveResult(List<ResolveExecuter> resolvers)
        {
            foreach (ResolveExecuter each in resolvers)
            {
                if (each.IsSuccess)
                {
                    //Debug.WriteLine("Resolve Success for：" + each.NameServerUrl);
                    return each.Result;
                }
            }

            return string.Empty;
        }
    }

    internal class ResolveExecuter
    {
        private string _name_server, _domain_name, _resolve_result;
        private bool _wait_required, _success;
        private ManualResetEvent _wait_handler;
        private BackgroundWorker _resolve_worker;

        public ResolveExecuter(string nameServerUrl, string name)
        {
            _name_server = nameServerUrl;
            _domain_name = name;
            _resolve_result = string.Empty;
            _success = false;
            _wait_handler = new ManualResetEvent(true);

            _resolve_worker = new BackgroundWorker();
            _resolve_worker.DoWork += new DoWorkEventHandler(ResolveWorker_DoWork);
        }

        public void DoResolve()
        {
            _wait_handler.Reset();
            _wait_required = true;
            _resolve_worker.RunWorkerAsync();
        }

        private void ResolveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DSConnection conn = new DSConnection(_name_server, "anonymous", "");
            conn.UseSession = false;

            try
            {
                conn.Connect();    //執行連線
                //Debug.WriteLine(_name_server + "：Connected");
            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("連線 DSNS 錯誤：" + _name_server);
                builder.AppendLine("錯誤內容：\n" + ex.ToString());
                //Debug.WriteLine(builder.ToString());
            }

            if (conn.IsConnected)
            {
                DSXmlHelper helper = new DSXmlHelper("Name");
                helper.AddText(".", _domain_name);

                DSResponse rsp;
                try
                {
                    rsp = conn.SendRequest("DS.NameService.GetDoorwayURL", new DSRequest(helper));

                    if (rsp.HasContent)
                    {
                        _resolve_result = rsp.GetContent().GetText(".");

                        if (string.IsNullOrEmpty(_resolve_result))
                            _success = false;
                        else
                            _success = true;
                    }
                    else
                    {    //發生這種錯誤的狀況很少，但是還是防止一下好了！
                        StringBuilder build = new StringBuilder();
                        build.AppendLine("連線後呼叫 GetDoorwayUrl ，但是沒有回傳內容，主機位置：" + _name_server);
                        build.AppendLine("Request 內容：" + rsp.GetRawXml());
                        //Debug.WriteLine(build.ToString());
                    }
                }
                catch (Exception ex)
                {
                    StringBuilder build = new StringBuilder();
                    build.AppendLine(_name_server + "：" + em.Get("ResolveAccessPointNameFail"));
                    build.AppendLine(ex.ToString());
                    //Debug.WriteLine(build.ToString());
                }
            }

            _wait_required = false;
            _wait_handler.Set();
        }

        public bool WaitRequired
        {
            get { return _wait_required; }
        }

        public bool IsSuccess
        {
            get { return _success && (!WaitRequired); }
        }

        public string Result
        {
            get { return _resolve_result; }
        }

        public ManualResetEvent WaitHandler
        {
            get { return _wait_handler; }
        }

        public string NameServerUrl
        {
            get { return _name_server; }
        }
    }
}
