using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Presentation
{
    /// <summary>
    /// 已經預先定義各種操作的NavContentDivision
    /// </summary>
    /// <remarks>
    /// 此物件定義了：
    /// 1.   待處裡的集合
    /// 2.   左方NavPane的篩選功能
    /// 3.   左方NavPane的可擴充的NavView
    /// 4.   左方NavPane的可擴充的右鍵選單
    /// 5.   ContentPane中分割為ListPane及DetailPane，以及雙擊ListPane中的項目可跳出PopupDetailPane的視窗
    /// 6.   ListPane含有搜尋功能以及可擴充的搜尋條件選單
    /// 7.   ListPane含有可擴充的欄位
    /// 8.   ListPane含有可擴充的右鍵選單(已內建"將選取加入待處裡"及"將選取從待處裡中移除"的功能)
    /// 9.   可擴充的DetailPane表頭顯示樣貌及PopupDetailPane的標題
    /// 10.可擴充的DetailPane顯示項目
    /// </remarks>
    public class NLDPanel : INCPanel
    {
        /// <summary>
        /// 加入DetailPane顯示項目的產生器
        /// </summary>
        /// <param name="item">顯示項目的產生器</param>
        public void AddDetailBulider(IDetailBulider item) { _Content.AddDetailBulider(item); }
        /// <summary>
        /// 加入DetailPane顯示項目的產生器
        /// </summary>
        /// <typeparam name="T">DetailPane顯示項目的型別，此型別必需實做預設建構子</typeparam>
        public void AddDetailBulider<T>() where T : DetailContent, new() { _Content.AddDetailBulider(new DetailBulider<T>()); }
        /// <summary>
        /// 加入ListPane的欄位
        /// </summary>
        /// <param name="listPaneField">欄位</param>
        public void AddListPaneField(ListPaneField listPaneField) { _Content.AddListPaneField(listPaneField); }
        /// <summary>
        /// 加入NavPane的NavView
        /// </summary>
        /// <param name="navView">瀏覽</param>
        public void AddView(INavView navView) { _Content.AddView(navView); }
        /// <summary>
        /// 加入ListPane的右鍵選單
        /// </summary>
        public MenuButton ListPaneContexMenu { get { return _Content.ListPaneContexMenuManager; } }
        /// <summary>
        /// 加入NavPane的右鍵選單
        /// </summary>
        public MenuButton NavPaneContexMenu { get { return _Content.NavPaneContexMenuManager; } }
        /// <summary>
        /// 取得篩選功能的選單按紐
        /// </summary>
        public MenuButtonControl FilterMenu { get { return _Content.FilterManager; } }
        /// <summary>
        /// 取得搜尋選項的選單按紐
        /// </summary>
        public MenuButtonControl SearchConditionMenu { get { return _Content.SearchConditionManager; } }
        /// <summary>
        /// 設定經過篩選後的資料集合
        /// </summary>
        /// <param name="primaryKeys">資料的集合</param>
        public void SetFilteredSource(List<string> primaryKeys) { _Content.SetFilteredSource(primaryKeys); }
        /// <summary>
        /// 設定DetailPane表頭顯示樣貌
        /// </summary>
        /// <param name="bulider">表頭顯示樣貌的產生器</param>
        public void SetDescriptionPaneBulider(IDescriptionPaneBulider bulider)
        {
            _Content.SetDescriptionPaneBulider(bulider);
        }
        /// <summary>
        /// 設定DetailPane表頭顯示樣貌
        /// </summary>
        /// <typeparam name="T">DetailPane顯示標題的型別，此型別必需實做預設建構子</typeparam>
        public void SetDescriptionPaneBulider<T>() where T : DescriptionPane, new()
        {
            _Content.SetDescriptionPaneBulider(new DescriptionPaneBulider<T>());
        }
        /// <summary>
        /// 取得功能列
        /// </summary>
        public DivisionBarManager RibbonBarItems
        {
            get
            {
                if ( _RibbonBarItems == null )
                    _RibbonBarItems = new DivisionBarManager(this.Group);
                return _RibbonBarItems;
            }
        }
        /// <summary>
        /// 顯示PopupDetailPane視窗
        /// </summary>
        /// <param name="primaryKey">顯示資料的鍵值</param>
        public void PopupDetailPane(string primaryKey) { _Content.PopupDetailPane(primaryKey); }
        /// <summary>
        /// 取得DetailPane的表頭簡述或PopupDetailPane的標題時
        /// </summary>
        public event EventHandler<RequiredDescriptionEventArgs> RequiredDescription;
        /// <summary>
        /// 當搜尋時
        /// </summary>
        public event EventHandler<SearchEventArgs> Search { add { _Content.Search += value; } remove { _Content.Search -= value; } }

        internal IPreferenceProvider PreferenceProvider { get { return _Content.PreferenceProvider; } set { _Content.PreferenceProvider = value; } }
        /// <summary>
        /// 當選取的資料集合變更時
        /// </summary>
        public event EventHandler SelectedSourceChanged;
        /// <summary>
        /// 當待處裡的資料集合變更時
        /// </summary>
        public event EventHandler TempSourceChanged;
        /// <summary>
        /// 當比較兩個資料大小時
        /// </summary>
        public event EventHandler<CompareEventArgs> CompareSource;
        /// <summary>
        /// 取得選取資料的資料集合
        /// </summary>
        public List<string> SelectedSource { get { return new List<string>(_Content.SelectedSource); } }
        /// <summary>
        /// 取得待處裡資料的資料集合
        /// </summary>
        public List<string> TempSource { get { return new List<string>(_Content.TempSource); } }
        /// <summary>
        /// 取得ListPane中顯示資料的資料集合
        /// </summary>
        protected List<string> DisplaySource { get { return _Content.DisplaySource; } }
        /// <summary>
        /// 取得目前DetailPane中顯示資料的primaryKey
        /// </summary>
        protected string DisplayDetailID { get { return _Content.DisplayDetailID; } }
        /// <summary>
        /// 將資料加入至待處裡
        /// </summary>
        /// <param name="primaryKeys">要加入待處裡的資料</param>
        public void AddToTemp(List<string> primaryKeys)
        {
            _Content.AddToTemp(primaryKeys);
        }
        /// <summary>
        /// 將資料移出待處裡
        /// </summary>
        /// <param name="primaryKeys">要移出待處裡的資料</param>
        public void RemoveFromTemp(List<string> primaryKeys)
        {
            _Content.RemoveFromTemp(primaryKeys);
        }
        /// <summary>
        /// 重新整裡ListPane
        /// </summary>
        public void RefillListPane()
        {
            _Content.RefillListPane();
        }
        /// <summary>
        /// 重新載入DetailPane
        /// </summary>
        protected void ReloadDetailPane()
        {
            _Content.ReloadDetailPane();
        }
        private DotNetBar.PrivateControl.NavContentPresentation _Content = new FISCA.Presentation.DotNetBar.PrivateControl.NavContentPresentation();
        private DivisionBarManager _RibbonBarItems = null;
        /// <summary>
        /// 建構子
        /// </summary>
        public NLDPanel()
        {
            _Content.RequiredDescription += delegate(object sender, RequiredDescriptionEventArgs e)
            {
                OnRequiredDescription(e);
            };
            _Content.CompareSource += delegate(object sender, CompareEventArgs e)
            {
                OnCompareSource(e);
            };
            _Content.SelectedSourceChanged += delegate(object sender, EventArgs e)
            {
                OnSelectedSourceChanged(e);
            };
            _Content.TempSourceChanged += delegate(object sender, EventArgs e)
            {
                OnTempSourceChanged(e);
            };
        }

        /// <summary>
        /// 引發TempSourceChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnTempSourceChanged(EventArgs e)
        {
            if ( TempSourceChanged != null )
                TempSourceChanged(this, e);
        }

        /// <summary>
        /// 引發SelectedChanged事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnSelectedSourceChanged(EventArgs e)
        {
            if ( SelectedSourceChanged != null )
                SelectedSourceChanged(this, e);
        }
        /// <summary>
        /// 引發Search事件。此方法於1.0.1.2之後的版本已無作用
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        protected virtual void OnSearch(SearchEventArgs e)
        {
        }
        /// <summary>
        /// 引發RequiredDescription事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnRequiredDescription(RequiredDescriptionEventArgs e)
        {
            if ( RequiredDescription != null )
                RequiredDescription(this, e);
        }
        /// <summary>
        /// 引發CompareSource事件
        /// </summary>
        /// <param name="e">包含事件的資料</param>
        protected virtual void OnCompareSource(CompareEventArgs e)
        {
            if ( CompareSource != null )
                CompareSource(this, e);
        }
        /// <summary>
        /// 取得或設定檢視模式
        /// </summary>
        public DisplayStatus DisplayStatus { get { return _Content.DisplayStatus; } set { _Content.DisplayStatus = value; } }
        /// <summary>
        /// 選取選單中所有項目
        /// </summary>
        public void SelectAll() { _Content.SelectAll(); }
        #region INavContentDivision 成員

        /// <summary>
        /// 取得或設定，Panel對應的標題
        /// </summary>
        public string Group
        {
            get { return _Content.Group; }
            set { _Content.Group = value; }
        }
        /// <summary>
        /// 取得NavigationPane。
        /// </summary>
        public System.Windows.Forms.Control NavigationPane
        {
            get { return _Content.NavigationPane; }
        }

        /// <summary>
        /// 取得ContentPane。
        /// </summary>
        public System.Windows.Forms.Control ContentPane
        {
            get { return _Content.ContentPane; }
        }
        /// <summary>
        /// 取得或設定，Panel的圖示。
        /// </summary>
        public System.Drawing.Image Picture
        {
            get { return _Content.Picture; }
            set { _Content.Picture = value; }
        }
        /// <summary>
        /// 取得或設定，指定Panel是否正在載入中。
        /// </summary>
        public bool ShowLoading
        {
            get { return _Content.ShowLoading; }
            set { _Content.ShowLoading = value; }
        }
        #endregion
    }
    /// <summary>
    /// 提供Search事件的資料
    /// </summary>
    public class SearchEventArgs : EventArgs
    {
        internal SearchEventArgs(string condition)
        {
            Result = new List<string>();
            Condition = condition;
        }
        /// <summary>
        /// 取得搜尋條件
        /// </summary>
        public string Condition { get; private set; }
        /// <summary>
        ///取得符合條件的資料集合
        /// </summary>
        public List<string> Result { get; private set; }
    }
    /// <summary>
    /// 提供RequiredDescription事件的資料
    /// </summary>
    public class RequiredDescriptionEventArgs : EventArgs
    {
        internal RequiredDescriptionEventArgs(string primaryKey)
        {
            PrimaryKey = primaryKey;
        }
        /// <summary>
        /// 取得引發事件的資料的ID
        /// </summary>
        public string PrimaryKey { get; private set; }
        /// <summary>
        /// 取得或設定，此ID的描述
        /// </summary>
        public string Result { get; set; }
    }
    /// <summary>
    /// 提供Compare事件的資料
    /// </summary>
    public class CompareEventArgs : EventArgs
    {
        internal CompareEventArgs(string v1, string v2)
        {
            Value1 = v1;
            Value2 = v2;
        }
        /// <summary>
        /// 取得第一個資料的ID
        /// </summary>
        public string Value1 { get; private set; }
        /// <summary>
        /// 取得第二個資料的ID
        /// </summary>
        public string Value2 { get; private set; }
        /// <summary>
        /// 取得或設定，若第一比資料大於第二比資料則大於0，第一比資料小於第二比資料則小於0，第一比資料等於第二比資料則等於0
        /// </summary>
        public int Result { get; set; }
    }
    /// <summary>
    /// 顯示模式
    /// </summary>
    public enum DisplayStatus {
        /// <summary>
        /// NavView選取內容。
        /// </summary>
        NavView, 
        /// <summary>
        /// SearchResult內容。
        /// </summary>
        Search, 
        /// <summary>
        /// 待處理中之內容。
        /// </summary>
        Temp };
}
