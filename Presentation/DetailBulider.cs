using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation
{
    /// <summary>
    /// DetailPane顯示項目的產生器
    /// </summary>
    public interface IDetailBulider
    {
        /// <summary>
        /// 建立新的DetailPane顯示項目實體
        /// </summary>
        /// <returns>DetailPane顯示項目</returns>
        DetailContent GetContent();
    }
    /// <summary>
    /// DetailPane顯示項目的產生器
    /// </summary>
    /// <typeparam name="T">DetailPane顯示項目的型別，此型別必需實做預設建構子</typeparam>
    public class DetailBulider<T> : IDetailBulider where T : DetailContent, new()
    {
        /// <summary>
        /// 建立新的DetailPane顯示項目實體
        /// </summary>
        /// <returns>DetailPane顯示項目</returns>
        public DetailContent GetContent()
        {
            var newContent = new T();
            if ( ContentBulided != null )
                ContentBulided(this, new ContentBulidedEventArgs<T>(newContent));
            return newContent;
        }
        /// <summary>
        /// 當一個新的DetailPane顯示項目被建立時
        /// </summary>
        public event EventHandler<ContentBulidedEventArgs<T>> ContentBulided;
    }
    /// <summary>
    /// 提供ContentBulided事件的資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ContentBulidedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 取得新建的的DetailPane顯示項目
        /// </summary>
        public T Content { get; private set; }
        internal ContentBulidedEventArgs(T content)
        {
            Content = content;
        }
    }

}
