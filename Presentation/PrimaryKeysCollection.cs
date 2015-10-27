using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FISCA.Presentation
{
    /// <summary>
    /// 管理PrimaryKey的集合
    /// </summary>
    public class PrimaryKeysCollection : Collection<string>
    {
        private bool _NotInProcess = true;
        /// <summary>
        /// 建構子
        /// </summary>
        /// <overloads>建構子</overloads> 
        public PrimaryKeysCollection() { }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="primaryKeys">指定集合內容</param>
        public PrimaryKeysCollection(IEnumerable<string> primaryKeys) : base(new List<string>(primaryKeys)) { }
        /// <summary>
        /// 大量新增項目。
        /// </summary>
        /// <param name="primaryKeys">新增項目集合</param>
        public void AddRange(IEnumerable<string> primaryKeys)
        {
            _NotInProcess = false;
            foreach (var item in primaryKeys)
            {
                this.Add(item);
            }
            _NotInProcess = true;
            OnItemsChanged();
        }
        /// <summary>
        /// 以新的集合直接取代原有的集合。
        /// </summary>
        /// <param name="primaryKeys">新集合</param>
        public void ReplaceAll(IEnumerable<string> primaryKeys)
        {
            _NotInProcess = false;
            this.Clear();
            foreach (var item in primaryKeys)
            {
                this.Add(item);
            }
            _NotInProcess = true;
            OnItemsChanged();
        }
        /// <summary>
        /// 將所有元素移除。
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            OnItemsChanged();
        }
        /// <summary>
        /// 將項目插入指定的索引處。
        /// </summary>
        /// <param name="index">應該插入item處之以零為始的索引。</param>
        /// <param name="item">要插入的物件，參考型別的值可以是null。</param>
        protected override void InsertItem(int index, string item)
        {
            base.InsertItem(index, item);
            OnItemsChanged();
        }
        /// <summary>
        /// 移除指定索引處的項目。
        /// </summary>
        /// <param name="index">要移除原素之以零為始的索引。</param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnItemsChanged();
        }
        /// <summary>
        /// 取代指定索引處的項目。
        /// </summary>
        /// <param name="index">要取代的項目之以零為起始的索引。</param>
        /// <param name="item">指定之索引處的項目新值，參考型別的值可以是null。</param>
        protected override void SetItem(int index, string item)
        {
            base.SetItem(index, item);
            OnItemsChanged();
        }
        /// <summary>
        /// 當ItemsChanged時。
        /// </summary>
        protected virtual void OnItemsChanged()
        {
            if (_NotInProcess && ItemsChanged != null)
                ItemsChanged(this, new EventArgs());
        }
        /// <summary>
        /// 項目內容變更時。
        /// </summary>
        public event EventHandler ItemsChanged;
    }
}
