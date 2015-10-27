using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace FISCA.Presentation
{
    /// <summary>
    /// 擴充NavContentPresentation的ListPane中的顯示欄位
    /// </summary>
    public class ListPaneField
    {
        /// <summary>
        /// 引發PreloadVariable事件
        /// </summary>
        /// <param name="args">包含事件的資料</param>
        protected void OnPreloadValue(PreloadVariableEventArgs args)
        {
            if ( PreloadVariable != null )
                PreloadVariable(this, args);
        }
        /// <summary>
        /// 引發PreloadVariableBackground事件
        /// </summary>
        /// <param name="args">包含事件的資料</param>
        protected void OnPreloadVariableBackground(PreloadVariableEventArgs args)
        {
            if ( PreloadVariableBackground != null )
                PreloadVariableBackground(this, args);
        }
        /// <summary>
        /// 引發GetVariable事件
        /// </summary>
        /// <param name="args">包含事件的資料</param>
        protected void OnGetVariable(GetVariableEventArgs args)
        {
            if ( GetVariable != null )
                GetVariable(this, args);
        }
        /// <summary>
        /// 引發CompareValue事件
        /// </summary>
        /// <param name="args">包含事件的資料</param>
        protected void OnCompareValue(CompareValueEventArgs args)
        {
            if ( CompareValue != null )
                CompareValue(this, args);
        }
        private BackgroundWorker _BKW = new BackgroundWorker();
        private List<string> _CurrentList = new List<string>();
        private List<string> _RunningList = new List<string>();
        private bool _RunWorkerCompleted = false;
        void _BKW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( _RunningList == _CurrentList )
            {
                _RunWorkerCompleted = true;
                this.Reload();
                _RunWorkerCompleted = false;
            }
            else
            {
                _BKW.RunWorkerAsync();
            }
        }
        void _BKW_DoWork(object sender, DoWorkEventArgs e)
        {
            _RunningList = _CurrentList;
            OnPreloadVariableBackground(new PreloadVariableEventArgs(_RunningList.ToArray()));
        }
        internal void PreloadValue(string[] keys)
        {
            if ( !_RunWorkerCompleted )
            {
                if ( PreloadVariableBackground != null )
                {
                    if ( _BKW.IsBusy )
                    {
                        if ( keys.Length != _RunningList.Count )
                            _CurrentList = new List<string>(keys);
                        else
                        {
                            foreach ( var item in keys )
                            {
                                if ( !_CurrentList.Contains(item) )
                                {
                                    _CurrentList = new List<string>(keys);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        _CurrentList = new List<string>(keys);
                        _BKW.RunWorkerAsync();
                    }
                }
                OnPreloadValue(new PreloadVariableEventArgs(keys));
            }
        }
        internal GetVariableEventArgs GetValue(string key)
        {
            GetVariableEventArgs args = new GetVariableEventArgs(key);
            if ( _BKW.IsBusy )
            {
                if ( this.Column is DataGridViewTextBoxColumn )
                    args.Value = "讀取中...";
                else
                    args.Value = null;
            }
            else
                OnGetVariable(args);
            return args;
        }
        internal int Compare(object a, object b)
        {

            CompareValueEventArgs args = new CompareValueEventArgs(a, b);
            OnCompareValue(args);
            return args.Result;
        }
        /// <summary>
        /// 重新整裡此欄位顯示的內容
        /// </summary>
        public void Reload()
        {
            if ( VariableChanged != null )
                VariableChanged(this, new EventArgs());
        }
        /// <summary>
        /// 建構子，傳入欄位標題
        /// </summary>
        /// <param name="columnHeaderText">欄位標題</param>
        public ListPaneField(string columnHeaderText)
            : this(columnHeaderText, false)
        {
        }
        /// <summary>
        /// 建構子，傳入資料行
        /// </summary>
        /// <param name="column">資料行</param>
        public ListPaneField(DataGridViewColumn column)
            : this(column, false)
        {
        }
        /// <summary>
        /// 建構子，傳入欄位標題，及是否永遠顯示
        /// </summary>
        /// <param name="columnHeaderText">欄位標題</param>
        /// <param name="allwaysVisible">是否永遠顯示</param>
        public ListPaneField(string columnHeaderText, bool allwaysVisible)
            : this(new DataGridViewTextBoxColumn(), false)
        {
            Column.HeaderText = columnHeaderText;
        }
        /// <summary>
        /// 建構子，傳入資料行，及是否永遠顯示
        /// </summary>
        /// <param name="column">資料行</param>
        /// <param name="allwaysVisible">是否永遠顯示</param>
        public ListPaneField(DataGridViewColumn column, bool allwaysVisible)
        {
            Column = column;
            AllwaysVisible = allwaysVisible;
            _BKW.DoWork += new DoWorkEventHandler(_BKW_DoWork);
            _BKW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BKW_RunWorkerCompleted);
        }

        /// <summary>
        /// 取得或設定，此資料行的最小行寬
        /// </summary>
        public int MinimumWidth
        {
            get { return Column.MinimumWidth; }
            set
            {
                Column.FillWeight = Column.MinimumWidth = value;
            }
        }
        /// <summary>
        ///取得，指出是否永遠顯示資料行，true的話將不受使用者自訂顯示欄位功能影響
        /// </summary>
        public bool AllwaysVisible { get; private set; }
        /// <summary>
        /// 取得延伸資料的資料行
        /// </summary>
        public DataGridViewColumn Column { get; private set; }
        /// <summary>
        /// 當顯示的資料發生變動
        /// </summary>
        internal event EventHandler VariableChanged;
        /// <summary>
        /// 當顯示資料即將變更，載入新資料
        /// </summary>
        public event EventHandler<PreloadVariableEventArgs> PreloadVariable;
        /// <summary>
        /// 當顯示資料即將變更，於背景執行續載入新資料
        /// </summary>
        public event EventHandler<PreloadVariableEventArgs> PreloadVariableBackground;
        /// <summary>
        /// 取得要顯示的內容
        /// </summary>
        public event EventHandler<GetVariableEventArgs> GetVariable;
        /// <summary>
        /// 比較資料大小
        /// </summary>
        public event EventHandler<CompareValueEventArgs> CompareValue;
    }
    /// <summary>
    /// 提供PreloadVariable事件的資料
    /// </summary>
    public class PreloadVariableEventArgs : EventArgs
    {
        internal PreloadVariableEventArgs(string[] keys)
        {
            Keys = keys;
        }
        /// <summary>
        /// 取得預先載入的資料的集合
        /// </summary>
        public string[] Keys { get; private set; }
    }
    /// <summary>
    /// 提供GetVariable事件的資料
    /// </summary>
    public class GetVariableEventArgs : EventArgs
    {
        internal GetVariableEventArgs(string key)
        {
            Key = key;
            Value = null;
            Tooltip = null;
        }
        /// <summary>
        /// 取得資料的ID
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// 取得或設定，此ID要顯示的值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 取得或設定，此欄的Tooltip描述
        /// </summary>
        public string Tooltip { get; set; }
    }
    /// <summary>
    /// 提供CompareValue事件的資料
    /// </summary>
    public class CompareValueEventArgs : EventArgs
    {
        internal CompareValueEventArgs(object v1, object v2)
        {
            Value1 = v1;
            Value2 = v2;
            Result = 0;
        }

        /// <summary>
        /// 取得第一個資料
        /// </summary>
        public object Value1 { get; private set; }
        /// <summary>
        /// 取得第二個資料
        /// </summary>
        public object Value2 { get; private set; }
        /// <summary>
        /// 取得或設定，若第一比資料大於第二比資料則大於0，第一比資料小於第二比資料則小於0，第一比資料等於第二比資料則等於0
        /// </summary>
        public int Result { get; set; }
    }
}
