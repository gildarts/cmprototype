using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FISCA.Presentation
{
    /// <summary>
    /// NavContentPresentation中左方區塊外掛的瀏覽方式
    /// </summary>
    /// <remarks>當Layout時應以傳入的key值做為瀏覽的清單(而非所有可能的key值)，當被選取時觸發ListPaneSourceChanged事件將被選取的key值傳給ListPane</remarks>
    public interface INavView
    {
        /// <summary>
        /// 取德或設定，指出是否做用中
        /// </summary>
        bool Active { get; set; }
        /// <summary>
        /// 取得名稱
        /// </summary>
        string NavText { get; }
        /// <summary>
        /// 取得描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 取得顯示區域內容
        /// </summary>
        Control DisplayPane { get; }
        /// <summary>
        /// 取得，所有顯示資料之集合
        /// </summary>
        PrimaryKeysCollection Source
        {
            get;
        }
        /// <summary>
        /// 要顯示於ListPane的資料變更時
        /// </summary>
        event EventHandler<ListPaneSourceChangedEventArgs> ListPaneSourceChanged;
    }
    /// <summary>
    /// NavView所選取的資料集變更時
    /// </summary>
    public class ListPaneSourceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="primaryKeys">選取得key的集合</param>
        public ListPaneSourceChangedEventArgs(IEnumerable<string> primaryKeys)
        {
            PrimaryKeys = new ListPaneSource(primaryKeys);
        }
        /// <summary>
        /// 取得選取的key的集合
        /// </summary>
        public ListPaneSource PrimaryKeys { get; private set; }
        /// <summary>
        /// 取得或設定，指出是否在ListPane顯示資料後是否全選顯示的資料
        /// </summary>
        public bool SelectedAll { get; set; }
        /// <summary>
        /// 取得或設定，指出是否在ListPane顯示資料後同時將資料加入至NavView的待處裡集合中
        /// </summary>
        public bool AddToTemp { get; set; }
    }
    /// <summary>
    /// 選取得key的集合
    /// </summary>
    public class ListPaneSource : IEnumerable<string>
    {
        private List<string> _Source = null;
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="source">集合內容</param>
        public ListPaneSource(IEnumerable<string> source)
        {
            _Source = new List<string>(source);
        }
        /// <summary>
        /// 取得項目總數。
        /// </summary>
        public int Count { get { return _Source.Count; } }
        /// <summary>
        /// 取得指定索引的值
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <returns>值</returns>
        public string this[int index] { get { return _Source[index]; } }

        #region IEnumerable<string> 成員
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return _Source.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Source.GetEnumerator();
        }

        #endregion
    }

}
