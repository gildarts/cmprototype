using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Presentation
{
    /// <summary>
    /// DetailPane顯示標題的產生器
    /// </summary>
    public interface IDescriptionPaneBulider
    {
        /// <summary>
        /// 建立新的DetailPane顯示標題實體
        /// </summary>
        /// <returns>DetailPane顯示標題</returns>
        DescriptionPane GetContent();
    }
    /// <summary>
    /// DetailPane顯示標題的產生器
    /// </summary>
    /// <typeparam name="T">DetailPane顯示標題的型別，此型別必需實做預設建構子</typeparam>
    public class DescriptionPaneBulider<T> : IDescriptionPaneBulider where T : DescriptionPane, new()
    {
        /// <summary>
        /// 建立新的DetailPane顯示標題實體
        /// </summary>
        /// <returns>DetailPane顯示標題</returns>
        public DescriptionPane GetContent()
        {
            var newContent = new T();
            if ( ContentBulided != null )
                ContentBulided(this, new DescriptionPaneBulidedEventArgs<T>(newContent));
            return newContent;
        }
        /// <summary>
        /// 當一個新的DetailPane顯示標題被建立時
        /// </summary>
        public event EventHandler<DescriptionPaneBulidedEventArgs<T>> ContentBulided;
    }
    /// <summary>
    /// 提供ContentBulided事件的資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DescriptionPaneBulidedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 取得新建的的DetailPane顯示標題
        /// </summary>
        public T Content { get; private set; }
        internal DescriptionPaneBulidedEventArgs(T content)
        {
            Content = content;
        }
    }
}
