using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSA;

namespace Manager
{
    class LogConfiguration
    {
        private FISCA.XHelper _xml;

        public LogConfiguration(XmlElement config)
        {
            if (config == null)
                throw new ArgumentException("找不到 Server Log 相關設定資料。");

            _xml = new FISCA.XHelper(config);

            LogEnabled = _xml.TryGetBoolean("@Enabled", true);
            LogProcess = _xml.TryGetBoolean("Property[@Name='LogProcess']", true);
            LogUDS = _xml.TryGetBoolean("Property[@Name='LogUDS']", false);
            Option = GetOption();
            CompressData = _xml.TryGetBoolean("Property[@Name='CompressData']", true);
            Target = _xml.GetText("Property[@Name='TargetConfig']/Target/@Database");
        }

        private LogOpportunity GetOption()
        {
            string strOption = _xml.GetText("Property[@Name='LogDataOption']");
            LogOpportunity option = LogOpportunity.Always;

            if (Enum.TryParse<LogOpportunity>(strOption, true, out option))
                return option;
            else
                return LogOpportunity.Always;
        }

        /// <summary>
        /// 將內部資料更新到最新狀態。
        /// </summary>
        internal void UpdateValues()
        {
            _xml.Data.RemoveAll();
            _xml.SetAttribute(".", "Enabled", LogEnabled.ToString());

            XmlElement property = _xml.AddElement("Property");
            property.SetAttribute("Name", "LogProcess");
            property.InnerText = LogProcess.ToString();

            property = _xml.AddElement("Property");
            property.SetAttribute("Name", "LogUDS");
            property.InnerText = LogUDS.ToString();

            property = _xml.AddElement("Property");
            property.SetAttribute("Name", "LogDataOption");
            property.InnerText = Option.ToString();

            property = _xml.AddElement("Property");
            property.SetAttribute("Name", "CompressData");
            property.InnerText = CompressData.ToString();

            if (!string.IsNullOrWhiteSpace(Target))
            {
                property = _xml.AddElement("Property");
                property.SetAttribute("Name", "TargetConfig");
                FISCA.XHelper hlpProperty = new FISCA.XHelper(property);
                property = hlpProperty.AddElement("Target");
                property.SetAttribute("Database", Target);
                property.SetAttribute("Type", "NeighborDB");
            }
        }

        /// <summary>
        /// 是否啟用 DSA Server Log 機制。
        /// </summary>
        public bool LogEnabled { get; set; }

        /// <summary>
        /// 是否 Log Process 資料(Service 處理流程記錄)。
        /// </summary>
        public bool LogProcess { get; set; }

        /// <summary>
        /// 是否也一併 Log 各個 Application 自定的 UDS Service。
        /// </summary>
        public bool LogUDS { get; set; }

        /// <summary>
        /// Log 記錄時機。
        /// </summary>
        public LogOpportunity Option { get; set; }

        /// <summary>
        /// 壓縮資料。
        /// </summary>
        public bool CompressData { get; set; }

        /// <summary>
        /// 記錄 Log 的位置。
        /// </summary>
        public string Target { get; set; }
    }
}
