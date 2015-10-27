using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    class ServerCollection : IEnumerable<Server>
    {
        private Dictionary<string, Server> Servers { get; set; }

        public ServerCollection()
        {
            Servers = new Dictionary<string, Server>();
        }

        public void Add(Server server)
        {
            if (Servers.ContainsKey(server.AccessPointUrl))
                throw new ArgumentException("無法加入相同的 DSA Server。");
            else
                Servers.Add(server.AccessPointUrl, server);
        }

        public bool Contains(Server server)
        {
            return Servers.ContainsKey(server.AccessPointUrl);
        }

        public bool Contains(string accessPointUrl)
        {
            return Servers.ContainsKey(accessPointUrl.ToLower());
        }

        public void Remove(string accessPointUrl)
        {
            Servers.Remove(accessPointUrl);
        }

        public void Remove(Server srv)
        {
            Servers.Remove(srv.AccessPointUrl);
        }

        #region IEnumerable<Server> 成員

        public IEnumerator<Server> GetEnumerator()
        {
            return Servers.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Servers.Values.GetEnumerator();
        }

        #endregion

        public IEnumerable<string> Keys { get { return Servers.Keys; } }

        internal void Clear()
        {
            Servers = new Dictionary<string, Server>();
        }
    }
}
