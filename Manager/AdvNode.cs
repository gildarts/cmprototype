using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    class AdvNode<T> : DevComponents.AdvTree.Node where T : class
    {
        public AdvNode()
        {
            BehindObject = null;
        }

        public AdvNode(T behindObject)
            : this()
        {
            BehindObject = behindObject;
        }

        /// <summary>
        /// 取得或設定所服務的物件。
        /// </summary>
        public T BehindObject { get; set; }
    }
}
