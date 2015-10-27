using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace UpgradeWebSetting
{
    [TableName(@"System.PackageDefinition")]
    public class PackageDefinition : ActiveRecord
    {
        /// <summary>
        /// Package 要套用的身份類型。
        /// </summary>
        [Field]
        public string TargetType { get; set; }

        /// <summary>
        /// 使用者應有的 Tag 編號。
        /// </summary>
        [Field]
        public int RefTagID { get; set; }

        /// <summary>
        /// Package 定議。
        /// </summary>
        [Field]
        public string Definition { get; set; }

        [Field]
        public int Order { get; set; }
    }
}
