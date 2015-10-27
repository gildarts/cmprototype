using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar.Rendering;
using FISCA.Presentation;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class NavContentPresentation : UserControl//, PresentationFramework.Presentation.NavContentPresentation
    {
        static NavContentPresentation()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        private DisplayStatus _DisplayStatus = DisplayStatus.NavView;
        private IPreferenceProvider _PreferenceProvider = new DefaultPreferenceProvider();
        private bool _PreferenceLoading = false; //當 Preference 正在讀取時...
        private string _DisplayDetailID = "";
        private bool _UpdateNavView = false;
        private bool _UpdateDisplay = false;
        private bool _MarkTemp = false;
        private bool _ReloadDetailPane = false;
        private bool _CheckSelection = false;
        private bool _SelectedAllSource = false;
        private List<string> _FilteredSource = new List<string>();
        private List<string> _DisplaySource = new List<string>();
        private List<string> _SelectedSource = new List<string>();
        private List<string> _TempSource = new List<string>();
        private Font _ListPaneInTempFont = null;
        private FISCA.Presentation.INavView _SelectedView = null;
        private List<FISCA.Presentation.INavView> _NavViewList = new List<FISCA.Presentation.INavView>();
        private List<RadioButton> _Buttons = new List<RadioButton>();
        private List<FISCA.Presentation.ListPaneField> _ListPaneFieldList = new List<FISCA.Presentation.ListPaneField>();
        private List<DataGridViewColumn> _DenyColumns = new List<DataGridViewColumn>();
        private Dictionary<DataGridViewColumn, DevComponents.DotNetBar.ButtonItem> _columnManagerItems = new Dictionary<DataGridViewColumn, DevComponents.DotNetBar.ButtonItem>();
        private Dictionary<string, PupopDetailPane> _PupopForms = new Dictionary<string, PupopDetailPane>();
        private IDescriptionPaneBulider _IDescriptionPane = null;

        public event EventHandler<RequiredDescriptionEventArgs> RequiredDescription;

        public event EventHandler SelectedSourceChanged;
        public event EventHandler TempSourceChanged;


        public NavContentPresentation()
        {
            DotNetBarReferenceFixer.FixIt();
            InitializeComponent();
            _ListPaneInTempFont = new Font(dgvDisplayList.Font, FontStyle.Italic);
            Application.Idle += new EventHandler(Application_Idle);
            ShowLoading = false;

            #region 整理畫面細節
            expandableSplitter3.GripLightColor = expandableSplitter3.GripDarkColor = System.Drawing.Color.Transparent;
            if ( GlobalManager.Renderer is Office2007Renderer )
            {
                ( GlobalManager.Renderer as Office2007Renderer ).ColorTableChanged += delegate
                {
                    this.dgvDisplayList.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
                    SetForeColor(this.eppViewSelector);
                };
                this.dgvDisplayList.AlternatingRowsDefaultCellStyle.BackColor = ( GlobalManager.Renderer as Office2007Renderer ).ColorTable.RibbonBar.MouseOver.TopBackground.End;
            }
            #endregion
            #region 當View縮起時一併拉起viewSelector
            eppView.ExpandedChanged += delegate
            {
                navigationPanePanel1.SuspendLayout();
                pictureBox1.Visible = ( ShowLoading ) & eppView.Expanded;
                this.eppViewSelector.Dock = ( eppView.Expanded ? DockStyle.Bottom : DockStyle.Top );
                eppView.Dock = ( eppView.Expanded ? DockStyle.Fill : DockStyle.Top );
                navigationPanePanel1.Controls.SetChildIndex(eppViewSelector, ( eppView.Expanded ? 3 : 0 ));
                navigationPanePanel1.ResumeLayout();
            };
            #endregion
            eppView.SizeChanged += delegate
            {
                int x = ( eppView.Width - pictureBox1.Width ) / 2;
                int y = ( eppView.Height - pictureBox1.Height ) / 2;
                if ( x < 0 ) x = 0;
                if ( y < 0 ) y = 0;
                pictureBox1.Location = new Point(x, y);
            };
            this.TempSourceChanged += delegate
            {
                this.btnTempory.Text = "待處理" + this.Group + "(" + TempSource.Count + ")";
                if ( _DisplayStatus == DisplayStatus.Temp )
                {
                    _DisplaySource.Clear();
                    _DisplaySource.AddRange(_TempSource);
                    _UpdateDisplay = true;
                    _CheckSelection = true;
                }
                _MarkTemp = true;
            };
            this.PreferenceProviderChanged += delegate
            {
                detailPane1.PreferenceProvider = new SubPreferenceProvider("DetailPane", PreferenceProvider);
                LoadPreference();
            };
            ListPaneContexMenuManager = new MenuButton(itemContainer1);
            NavPaneContexMenuManager = new MenuButton(itemContainer2);
            FilterManager = new MenuButtonControl(btnFilter);
            SearchConditionManager = new MenuButtonControl(btnSearch);
        }

        public DisplayStatus DisplayStatus
        {
            get { return _DisplayStatus; }
            set
            {
                if ( _DisplayStatus != value )
                {
                    if ( value == DisplayStatus.Temp )
                    {
                        btnTempory.Checked = true;
                    }
                    else
                    {
                        btnTempory.Checked = false;
                        if ( value == DisplayStatus.Search )
                        {
                            btnSearch.PerformClick();
                        }
                        else
                        {
                            _UpdateDisplay = true;
                            _DisplaySource.Clear();
                            _DisplaySource.AddRange(_NavViewSource);
                        }
                    }

                }
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            pictureBox1.Visible = ShowLoading & eppView.Expanded;
            #region 重新整理資料
            if ( _UpdateNavView && _SelectedView != null )
            {
                _SelectedView.Source.ReplaceAll(new List<string>(_FilteredSource));
                _UpdateNavView = false;
            }
            #endregion
            #region 檢查Display的資料變更
            if ( _UpdateDisplay )
            {
                if ( _CheckSelection )//先判斷UI上的選取是否已同步到SelectedSource，避免在同步前進行重新填入後遺失UI上的選取資訊
                {
                    CheckSelection();
                    _CheckSelection = true;
                }
                int scrollIndex = dgvDisplayList.FirstDisplayedScrollingRowIndex;
                //清空並重新填入資料
                DataGridViewRow[] newRows = new DataGridViewRow[_DisplaySource.Count];
                _DisplaySource.Sort(Compare);
                for ( int i = 0 ; i < _DisplaySource.Count ; i++ )
                {
                    newRows[i] = new DataGridViewRow();
                    newRows[i].CreateCells(dgvDisplayList);
                }
                dgvDisplayList.Rows.Clear();
                dgvDisplayList.Rows.AddRange(newRows);
                foreach ( var listPaneField in this._ListPaneFieldList )
                {
                    listPaneField.PreloadValue(_DisplaySource.ToArray());
                }
                int index = 0;
                foreach ( string id in _DisplaySource )
                {
                    DataGridViewRow row = newRows[index++];
                    row.Tag = id;
                    object[] list = new object[_ListPaneFieldList.Count];
                    for ( int i = 0 ; i < dgvDisplayList.Columns.Count ; i++ )
                    {
                        var args = _ListPaneFieldList[i].GetValue(id);
                        list[i] = args.Value;
                        row.Cells[i].ToolTipText = args.Tooltip;
                    }
                    bool inTemp = TempSource.Contains(id);
                    row.DefaultCellStyle.Font = inTemp ? _ListPaneInTempFont : null;
                    row.SetValues(list);
                    row.Selected = _SelectedAllSource ? true : _SelectedSource.Contains(id);
                }
                _SelectedAllSource = false;
                if ( DisplaySource.Count > scrollIndex && scrollIndex >= 0 )
                    dgvDisplayList.FirstDisplayedScrollingRowIndex = scrollIndex;
                //把每個row的順序記錄起來，當下次排序時，碰到同樣大小的值，會依照排序前row的先後排列。
                _RowIndex.Clear();
                foreach ( DataGridViewRow row in this.dgvDisplayList.Rows )
                {
                    _RowIndex.Add(row, row.Index);
                }
            }
            #endregion
            #region 在待處理中的資料顯示斜體
            if ( _MarkTemp )
            {
                foreach ( DataGridViewRow var in dgvDisplayList.SelectedRows )
                {
                    string id = "" + var.Tag;
                    bool inTemp = TempSource.Contains(id);
                    var.DefaultCellStyle.Font = inTemp ? _ListPaneInTempFont : null;
                }
                _MarkTemp = false;
            }
            #endregion
            #region 檢查選取資料變更
            CheckSelection();
            #endregion
            #region 如果Display的資料有變更則重新排列
            if ( _UpdateDisplay )
            {
                if ( dgvDisplayList.SortedColumn != null && dgvDisplayList.SortOrder == SortOrder.Ascending | dgvDisplayList.SortOrder == SortOrder.Descending )
                    dgvDisplayList.Sort(dgvDisplayList.SortedColumn, dgvDisplayList.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                _UpdateDisplay = false;
            }
            #endregion
            #region 重新載入DetailPane
            if ( _ReloadDetailPane )
            {
                if ( _SelectedSource.Contains(_DisplayDetailID) )
                {
                    detailPane1.ReloadAll();
                    detailPane1.SetDescription(this.GetDescription(_DisplayDetailID));
                }
                _ReloadDetailPane = false;
            }
            #endregion
        }
        public void SelectAll()
        {
            _UpdateDisplay = true;
            _SelectedAllSource = true;
        }
        private void CheckSelection()
        {
            if ( _CheckSelection )
            {
                List<string> _SelectedList = new List<string>();
                foreach ( DataGridViewRow var in dgvDisplayList.Rows )
                {
                    if ( var.Selected )
                        _SelectedList.Insert(0, "" + var.Tag);
                }
                if ( _SelectedList.Count == _SelectedSource.Count )
                {
                    List<string> l1 = new List<string>(_SelectedList);
                    List<string> l2 = new List<string>(_SelectedSource);
                    l1.Sort();
                    l2.Sort();
                    for ( int i = 0 ; i < l1.Count ; i++ )
                    {
                        if ( l1[i] != l2[i] )
                        {
                            _SelectedSource = _SelectedList;
                            if ( SelectedSourceChanged != null )
                                SelectedSourceChanged(this, new EventArgs());
                            break;
                        }
                    }
                }
                else
                {
                    _SelectedSource = _SelectedList;
                    if ( SelectedSourceChanged != null )
                        SelectedSourceChanged(this, new EventArgs());
                }
                if ( !_SelectedSource.Contains(_DisplayDetailID) && splitterListDetial.Expanded )
                {
                    if ( _SelectedSource.Count == 0 )
                    {
                        _DisplayDetailID = "";
                        detailPane1.Visible = false;
                    }
                    else
                    {
                        _DisplayDetailID = _SelectedSource[0];
                        detailPane1.Visible = true;
                        detailPane1.PrimaryKey = _DisplayDetailID;
                        detailPane1.SetDescription(this.GetDescription(_DisplayDetailID));
                    }
                }
                _CheckSelection = false;
            }
        }
        public IPreferenceProvider PreferenceProvider { get { return _PreferenceProvider; } set { _PreferenceProvider = value; if ( PreferenceProviderChanged != null )PreferenceProviderChanged(this, new EventArgs()); } }
        public event EventHandler PreferenceProviderChanged;
        public event EventHandler<CompareEventArgs> CompareSource;
        public void RefillListPane()
        {
            _UpdateDisplay = true;
        }
        public void ReloadDetailPane()
        {
            _ReloadDetailPane = true;
        }
        public bool ShowLoading { get; set; }
        public List<string> SelectedSource { get { return _SelectedSource; } }
        public List<string> DisplaySource { get { return _DisplaySource; } }
        public List<string> TempSource { get { return _TempSource; } }
        public string DisplayDetailID { get { return _DisplayDetailID; } }
        private int Compare(string v1, string v2)
        {
            CompareEventArgs args = new CompareEventArgs(v1, v2);
            if ( CompareSource != null )
                CompareSource(this, args);
            return args.Result;
        }
        private XmlElement Preference
        {
            get
            {
                XmlElement PreferenceElement = null;
                if ( _PreferenceProvider != null )
                    PreferenceElement = _PreferenceProvider[fixName("GeneralEntity_" + this.buttonItem1.Text)];
                if ( PreferenceElement == null )
                {
                    PreferenceElement = new XmlDocument().CreateElement(fixName("GeneralEntity_" + this.buttonItem1.Text));
                }
                return PreferenceElement;
            }
            set
            {
                if ( _PreferenceProvider != null )
                    _PreferenceProvider[fixName("GeneralEntity_" + this.buttonItem1.Text)] = value;
            }
        }
        private string fixName(string p)
        {
            string fixname = p.Replace("/", "_").Replace("(", "_").Replace(")", "_").Replace("[", "_").Replace("]", "_").Replace("^", "_").Replace("!", "_").Replace("?", "_").Replace(" ", "_").Replace("　", "_");
            if ( fixname == "" )
                fixname = "_";
            return fixname;
        }
        private void UpdatePreference()
        {
            if ( _PreferenceLoading ) return; //因為載入 Preference 而導致的 UpdatePreference 就不動作。

            #region 紀錄欄位顯示
            #region 取得PreferenceElement
            XmlElement PreferenceElement = Preference;
            #endregion
            //紀錄展開
            PreferenceElement.SetAttribute("ListExpanded", splitterListDetial.Expanded ? "False" : "True");
            #region 紀錄欄位顯示隱藏(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                //紀錄資料行顯示隱藏
                foreach ( DataGridViewColumn col in dgvDisplayList.Columns )
                {
                    PreferenceElement.SetAttribute(fixName("col" + col.HeaderText + "Visible"), col.Visible ? "True" : "False");
                }
            }
            else
                foreach ( DataGridViewColumn col in dgvDisplayList.Columns )
                {
                    PreferenceElement.SetAttribute(fixName("col" + col.HeaderText + "ExtVisible"), col.Visible ? "True" : "False");
                }
            #endregion
            //紀錄清單顯示寬度(只有當狀態是收合時記錄)
            if ( splitterListDetial.Expanded )
            {
                PreferenceElement.SetAttribute("PanelListWidth", panelList.Width.ToString());
            }
            #region 紀錄清單顯示順序
            //紀錄資料行顯示隱藏
            foreach ( DataGridViewColumn col in dgvDisplayList.Columns )
            {
                if ( col.DisplayIndex >= 0 )
                    PreferenceElement.SetAttribute(fixName("col" + col.HeaderText + "Index"), col.DisplayIndex.ToString());
            }
            #endregion
            //記錄檢視模式
            if ( _SelectedView != null )
                PreferenceElement.SetAttribute("INavView", _SelectedView.NavText);
            Preference = PreferenceElement;
            #endregion
        }
        private void LoadPreference()
        {
            _PreferenceLoading = true;

            #region 讀取設定檔
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                //設定清單顯示寬度
                if ( PreferenceData.HasAttribute("PanelListWidth") )
                {
                    int width;
                    if ( int.TryParse(PreferenceData.Attributes["PanelListWidth"].Value, out width) )
                        splitterListDetial.SplitPosition = width;
                }
                //設定展開
                if ( PreferenceData.HasAttribute("ListExpanded") && PreferenceData.Attributes["ListExpanded"].Value == "True" )
                {
                    buttonExpand.Text = "<<";
                    buttonExpand.Tooltip = "還原";
                    splitterListDetial.Expanded = false;
                    for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                    {
                        dgvDisplayList.Columns[i].Visible = true;
                    }
                }
                #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                if ( splitterListDetial.Expanded )
                {
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + var.HeaderText + "Visible")) )
                            var.Visible = !( PreferenceData.Attributes[fixName("col" + var.HeaderText + "Visible")].Value == "False" );
                    }
                }
                else
                {
                    //讀取外掛欄的顯式隱藏
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + var.HeaderText + "ExtVisible")) )
                            var.Visible = !( PreferenceData.Attributes[fixName("col" + var.HeaderText + "ExtVisible")].Value == "False" );
                    }
                }
                #endregion
                #region 設定清單顯示順序
                //讀取外掛欄的顯式順序
                foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                {
                    if ( PreferenceData.HasAttribute(fixName("col" + var.HeaderText + "Index")) )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes[fixName("col" + var.HeaderText + "Index")].Value, out displayIndex) && displayIndex >= 0 )
                            var.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                    }
                }
                #endregion

                _PreferenceLoading = false;
            }
            #endregion
        }
        private string GetDescription(string primaryKey)
        {
            RequiredDescriptionEventArgs args = new RequiredDescriptionEventArgs(primaryKey);
            if ( RequiredDescription != null )
                RequiredDescription(this, args);
            return args.Result;
        }
        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if ( splitterListDetial.Expanded )
            {
                //寫入紀錄(目前顯示欄位)
                buttonExpand.Text = "<<";
                buttonExpand.Tooltip = "還原";
                UpdatePreference();
                splitterListDetial.Expanded = false;
            }
            else
            {
                buttonExpand.Text = ">>";
                buttonExpand.Tooltip = "最大化";
                UpdatePreference();
                splitterListDetial.Expanded = true;
            }
        }
        /// <summary>
        /// 設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelList.ResumeLayout();
            panel3.ResumeLayout();
            panelContent.ResumeLayout();
            if ( !splitterListDetial.Expanded )
            {
                for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                {
                    dgvDisplayList.Columns[i].Visible = true;
                }
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + var.HeaderText + "ExtVisible")) )
                            var.Visible = ( PreferenceData.Attributes[fixName("col" + var.HeaderText + "ExtVisible")].Value == "True" );
                    }
                }
                #endregion
            }
            else
            {
                for ( int i = 1 ; i < dgvDisplayList.Columns.Count ; i++ )
                {
                    dgvDisplayList.Columns[i].Visible = false;
                }
                //colName.Visible = true;
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    //讀取外掛資料行的設定
                    foreach ( DataGridViewColumn var in dgvDisplayList.Columns )
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + var.HeaderText + "Visible")) )
                            var.Visible = ( PreferenceData.Attributes[fixName("col" + var.HeaderText + "Visible")].Value == "True" );
                    }
                }
                #endregion
                #region 整理DetailPane
                if ( !_SelectedSource.Contains(_DisplayDetailID) )
                {
                    if ( _SelectedSource.Count == 0 )
                    {
                        _DisplayDetailID = "";
                        detailPane1.Visible = false;
                    }
                    else
                    {
                        _DisplayDetailID = _SelectedSource[0];
                        detailPane1.Visible = true;
                        detailPane1.PrimaryKey = _DisplayDetailID;
                        detailPane1.SetDescription(this.GetDescription(_DisplayDetailID));
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 設定設定縮放
        /// </summary>
        private void splitterListDetial_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panelContent.SuspendLayout();
            panelList.SuspendLayout();
            panel3.SuspendLayout();
            if ( splitterListDetial.Expanded )
            {
                splitterListDetial.Dock = DockStyle.Right;
                panelList.Dock = DockStyle.Fill;
                splitterListDetial.Enabled = false;
                panelContent.Controls.SetChildIndex(panelList, 0);
                splitterListDetial.GripLightColor = splitterListDetial.GripDarkColor = System.Drawing.Color.Transparent;
            }
            else
            {
                panelList.Dock = DockStyle.Left;
                splitterListDetial.Dock = DockStyle.Left;
                splitterListDetial.Enabled = true;
                panelContent.Controls.SetChildIndex(panelList, 3);
                splitterListDetial.ApplyStyle(splitterListDetial.Style);
            }
        }
        private void buttonItem3_PopupClose(object sender, EventArgs e)
        {
            UpdatePreference();
        }
        private void buttonItem2_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            //if ( this._CheckSelection )
            //    FISCA.Presentation.MotherForm.Instance.SetStatusBarMessage("UnChecked");
            //else
            //    FISCA.Presentation.MotherForm.Instance.SetStatusBarMessage("");
            CheckSelection();
            #region 變更清單右鍵選項
            addToTemp.Visible = true;
            removeFormTemp.Visible = true;

            bool canAdd = false;
            bool canRemove = false;
            foreach ( string id in _SelectedSource )
            {
                if ( _TempSource.Contains(id) )
                    canRemove = true;
                else
                    canAdd = true;
            }
            addToTemp.Enabled = canAdd;
            removeFormTemp.Enabled = canRemove;
            #endregion
            e.Cancel = dgvDisplayList.PointToClient(Control.MousePosition).Y < dgvDisplayList.ColumnHeadersHeight + 4;
        }
        private void buttonItem3_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            e.Cancel = dgvDisplayList.PointToClient(Control.MousePosition).Y >= dgvDisplayList.ColumnHeadersHeight + 4;
            if ( !e.Cancel )
            {
                buttonItem3.SubItems.Clear();
                foreach ( DataGridViewColumn column in dgvDisplayList.Columns )
                {
                    if ( !_DenyColumns.Contains(column) )
                    {
                        DevComponents.DotNetBar.ButtonItem item;
                        if ( _columnManagerItems.ContainsKey(column) )
                            item = _columnManagerItems[column];
                        else
                        {
                            item = new DevComponents.DotNetBar.ButtonItem();
                            item.AutoCheckOnClick = true;
                            item.AutoCollapseOnClick = false;
                            item.ClickAutoRepeat = true;
                            item.CheckedChanged += delegate(object s, EventArgs es)
                            {
                                DevComponents.DotNetBar.ButtonItem it = (DevComponents.DotNetBar.ButtonItem)s;
                                foreach ( DataGridViewColumn col in _columnManagerItems.Keys )
                                {
                                    if ( _columnManagerItems[col] == it )
                                    {
                                        if ( col.Visible != it.Checked )
                                        {
                                            col.Visible = it.Checked;
                                        }
                                        break;
                                    }
                                }
                            };
                            _columnManagerItems.Add(column, item);
                        }
                        item.Text = column.HeaderText;
                        item.Checked = column.Visible;
                        buttonItem3.SubItems.Add(item);
                    }
                }
            }
        }
        //把控制項中的RadioButton都改成漂亮顯示
        private void SetForeColor(Control parent)
        {
            foreach ( Control var in parent.Controls )
            {
                if ( var is RadioButton )
                    var.ForeColor = ( (Office2007Renderer)GlobalManager.Renderer ).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }
        private void dgvDisplayList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private Dictionary<DataGridViewRow, int> _RowIndex = new Dictionary<DataGridViewRow, int>();
        private void dgvDisplayList_Sorted(object sender, EventArgs e)
        {
            //把每個row的順序記錄起來，當下次排序時，碰到同樣大小的值，會依照排序前row的先後排列。
            _RowIndex.Clear();
            foreach ( DataGridViewRow row in this.dgvDisplayList.Rows )
            {
                _RowIndex.Add(row, row.Index);
            }
            //把莫名其妙被選起來的取消選取
            foreach ( DataGridViewRow var in dgvDisplayList.SelectedRows )
            {
                if ( var.Selected && !_SelectedSource.Contains("" + var.Tag) )
                    var.Selected = false;
            }
        }

        private void dgvDisplayList_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            foreach ( var item in _ListPaneFieldList )
            {
                if ( item.Column == e.Column )
                {
                    e.SortResult = item.Compare(e.CellValue1, e.CellValue2);
                }
            }
            if ( e.SortResult == 0 )
            {
                e.SortResult = ( "" + e.CellValue1 ).CompareTo("" + e.CellValue2);
            }
            if ( e.SortResult == 0 )
            {
                e.SortResult = ( _RowIndex[dgvDisplayList.Rows[e.RowIndex1]] ).CompareTo(_RowIndex[dgvDisplayList.Rows[e.RowIndex2]]);
            }
            e.Handled = true;
        }

        private void dgvDisplayList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _CheckSelection = true;
        }

        private void dgvDisplayList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            _CheckSelection = true;
        }

        private void dgvDisplayList_SelectionChanged(object sender, EventArgs e)
        {
            _CheckSelection = true;
        }
        private void addToTemp_Click(object sender, EventArgs e)
        {
            CheckSelection();
            AddToTemp(_SelectedSource);
        }
        public void AddToTemp(List<string> primaryKeys)
        {
            List<string> addList = new List<string>();
            foreach ( string id in primaryKeys )
            {
                if ( !_TempSource.Contains(id) )
                    addList.Add(id);
            }
            if ( addList.Count > 0 )
            {
                _TempSource.AddRange(addList);
                if ( TempSourceChanged != null )
                    TempSourceChanged(this, new EventArgs());
            }
        }

        private void memoveFormTemp_Click(object sender, EventArgs e)
        {
            CheckSelection();
            RemoveFromTemp(_SelectedSource);
        }
        public void RemoveFromTemp(List<string> primaryKeys)
        {
            bool hasChanged = false;
            foreach ( string id in primaryKeys )
            {
                if ( _TempSource.Contains(id) )
                {
                    _TempSource.Remove(id);
                    hasChanged = true;
                }
            }
            if ( hasChanged )
            {
                if ( TempSourceChanged != null )
                    TempSourceChanged(this, new EventArgs());
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if ( e.KeyCode == Keys.Enter )
            {
                btnSearch.PerformClick();
            }
        }
        /// <summary>
        /// 設定焦點
        /// </summary>
        private void SetFocus(object sender, EventArgs e)
        {
            Control c = ( (Control)sender );
            if ( c.TopLevelControl.ContainsFocus && !txtSearch.Focused && !c.ContainsFocus )
            {
                List<DataGridViewRow> selectedRow = new List<DataGridViewRow>();
                foreach ( DataGridViewRow var in dgvDisplayList.Rows )
                {
                    if ( var.Selected )
                        selectedRow.Add(var);
                }

                c.Focus();

                foreach ( DataGridViewRow row in dgvDisplayList.Rows )
                {
                    row.Selected = selectedRow.Contains(row);
                }
            }
        }

        private void btnTempory_Click(object sender, EventArgs e)
        {
            if ( btnTempory.Checked )
            {
                foreach ( DataGridViewRow row in dgvDisplayList.Rows )
                {
                    row.Selected = true;
                }
                _CheckSelection = true;
            }
            else
            {
                btnTempory.Checked = true;
            }
        }

        private void btnTempory_CheckedChanged(object sender, EventArgs e)
        {
            if ( TempSource.Count == 0 && btnTempory.Checked )
            {
                btnTempory.Checked = false;
                _DisplayStatus = DisplayStatus.NavView;
                return;
            }
            else
            {
                btnTempory.ColorTable = ( btnTempory.Checked ? DevComponents.DotNetBar.eButtonColor.Office2007WithBackground : DevComponents.DotNetBar.eButtonColor.OrangeWithBackground );
                if ( btnTempory.Checked )
                {
                    _DisplayStatus = DisplayStatus.Temp;
                    _DisplaySource.Clear();
                    _DisplaySource.AddRange(_TempSource);
                    _SelectedAllSource = true;
                    _UpdateDisplay = true;
                    _CheckSelection = true;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if ( txtSearch.Text != "" )
            {
                btnTempory.Checked = false;
                _DisplayStatus = DisplayStatus.Search;
                List<string> allResult = new List<string>();
                if ( _SearchDelegates.Count > 0 )
                {
                    foreach ( var item in _SearchDelegates )
                    {
                        SearchEventArgs args = new SearchEventArgs(this.txtSearch.Text);
                        item.Invoke(this, args);
                        foreach ( var id in args.Result )
                        {
                            if ( !allResult.Contains(id) )
                                allResult.Add(id);
                        }
                    }
                }
                this._DisplaySource = allResult;
                _UpdateDisplay = true;
            }
        }

        internal void SetDisplaySource(List<string> keys)
        {
            this._DisplaySource = keys;
            this._UpdateDisplay = true;
        }

        #region NavContentPresentation 成員
        List<IDetailBulider> builders = new List<IDetailBulider>();

        public void AddDetailBulider(FISCA.Presentation.IDetailBulider item)
        {
            builders.Add(item);
            this.detailPane1.AddDetailItem(item.GetContent());

        }

        public void AddListPaneField(FISCA.Presentation.ListPaneField listPaneField)
        {
            _ListPaneFieldList.Add(listPaneField);
            listPaneField.VariableChanged += delegate(object sender, EventArgs args)
            {
                var field = ( (ListPaneField)sender );
                var columnIndex = field.Column.Index;
                field.PreloadValue(_DisplaySource.ToArray());
                foreach ( DataGridViewRow row in dgvDisplayList.Rows )
                {
                    var a = field.GetValue("" + row.Tag);
                    row.Cells[columnIndex].Value = a.Value;
                    row.Cells[columnIndex].ToolTipText = a.Tooltip;
                }
            };
            DataGridViewColumn newColumn = listPaneField.Column;
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            newColumn.ReadOnly = true;
            dgvDisplayList.Columns.Add(newColumn);
            if ( newColumn.MinimumWidth != newColumn.FillWeight )
                listPaneField.MinimumWidth = newColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, false);
            if ( listPaneField.AllwaysVisible )
                _DenyColumns.Add(newColumn);
            else
            {
                #region 讀取設定檔
                XmlElement PreferenceData = Preference;
                if ( PreferenceData != null )
                {
                    #region 設定欄位顯示隱藏(只有當狀態是收合時設定)
                    if ( splitterListDetial.Expanded )
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + listPaneField.Column.HeaderText + "Visible")) )
                            newColumn.Visible = !( PreferenceData.Attributes[fixName("col" + listPaneField.Column.HeaderText + "Visible")].Value == "False" );
                    }
                    else
                    {
                        if ( PreferenceData.HasAttribute(fixName("col" + listPaneField.Column.HeaderText + "ExtVisible")) )
                            newColumn.Visible = !( PreferenceData.Attributes[fixName("col" + listPaneField.Column.HeaderText + "ExtVisible")].Value == "False" );
                    }
                    #endregion
                    #region 設定清單顯示順序
                    if ( PreferenceData.HasAttribute(fixName("col" + listPaneField.Column.HeaderText + "Index")) )
                    {
                        int displayIndex;
                        if ( int.TryParse(PreferenceData.Attributes[fixName("col" + listPaneField.Column.HeaderText + "Index")].Value, out displayIndex) && displayIndex > 0 )
                            newColumn.DisplayIndex = displayIndex >= dgvDisplayList.Columns.Count ? dgvDisplayList.Columns.Count - 1 : displayIndex;
                    }
                    #endregion
                }
                #endregion
            }
        }

        public void AddView(FISCA.Presentation.INavView navView)
        {
            navView.ListPaneSourceChanged += new EventHandler<FISCA.Presentation.ListPaneSourceChangedEventArgs>(navView_ListPaneSourceChanged);
            _NavViewList.Add(navView);
            this.eppViewSelector.SuspendLayout();
            //_SelectedView = null;
            int itemcount = _Buttons.Count < _NavViewList.Count ? _Buttons.Count : _NavViewList.Count;
            for ( int i = 0 ; i < itemcount ; i++ )
            {
                if ( _Buttons[i].Checked )
                {
                    var sview = (FISCA.Presentation.INavView)_Buttons[i].Tag;
                    if ( sview != _SelectedView )
                    {
                        if ( _SelectedView != null )
                            _SelectedView.Active = false;
                        _SelectedView = sview;
                        _SelectedView.Active = true;
                    }
                }
            }
            //if ( !_NavViewList.Contains(_SelectedView) )
            //    _SelectedView = null;
            int Y = this.eppViewSelector.TitleHeight + 6;
            int speace = 0;
            #region 補按鈕到足夠數量(並設定按紐被選起時委派處理)
            for ( int i = _Buttons.Count ; i < _NavViewList.Count ; i++ )
            {
                RadioButton newButton = new RadioButton();
                newButton.Font = this.Font;
                newButton.TabIndex = 0;
                newButton.TabStop = true;
                newButton.AutoSize = true;
                newButton.CheckedChanged += delegate(object bs, EventArgs be)
                {
                    RadioButton r = (RadioButton)bs;
                    if ( r.Checked )
                    {
                        this.panel2.Controls.Clear();
                        var sview = (FISCA.Presentation.INavView)r.Tag;
                        if ( _SelectedView != sview )
                        {
                            if ( _SelectedView != null )
                                _SelectedView.Active = false;
                            _SelectedView = sview;
                            _SelectedView.Active = true;
                        }
                        this.panel2.Controls.Add(_SelectedView.DisplayPane);
                        _SelectedView.DisplayPane.Dock = DockStyle.Fill;
                        pictureBox1.BackColor = _SelectedView.DisplayPane.BackColor;
                        _UpdateNavView = true;
                    }
                };
                this.eppViewSelector.Controls.Add(newButton);
                _Buttons.Add(newButton);
            }
            #endregion
            #region 隱藏多餘的按鈕
            for ( int i = _Buttons.Count - 1 ; i >= _NavViewList.Count ; i-- )
            {
                _Buttons[i].Visible = false;
            }
            #endregion
            #region 設定按紐顯示並看哪個該被選起來
            for ( int i = 0 ; i < _NavViewList.Count ; i++ )
            {
                if ( _NavViewList[i].NavText == Preference.GetAttribute("INavView") )//選Preference記錄的Planner
                {
                    for ( int j = 0 ; j < i ; j++ )
                    {
                        if ( _Buttons[i].Checked )
                            _Buttons[i].Checked = false;
                    }
                    _SelectedView = _NavViewList[i];
                }
                RadioButton newButton = _Buttons[i];
                newButton.Tag = _NavViewList[i];
                newButton.Text = _NavViewList[i].NavText;
                if ( _SelectedView == null && i == 0 )
                    newButton.Checked = true;
                else
                {
                    if ( _NavViewList[i].Equals(_SelectedView) )
                        newButton.Checked = true;
                    else
                        newButton.Checked = false;
                }
                newButton.Location = new Point(12, Y);
                Y += newButton.Height + speace;
                newButton.Visible = true;
            }
            #endregion
            Y += 6;
            this.eppViewSelector.Size = new Size(this.eppViewSelector.Width, Y);
            SetForeColor(this.eppViewSelector);
            this.eppViewSelector.Visible = _NavViewList.Count > 1;
            this.eppViewSelector.ResumeLayout();
        }
        private List<string> _NavViewSource = new List<string>();
        void navView_ListPaneSourceChanged(object sender, FISCA.Presentation.ListPaneSourceChangedEventArgs e)
        {
            FISCA.Presentation.INavView view = (INavView)sender;
            if ( view == _SelectedView )
            {
                if ( _DisplayStatus == DisplayStatus.NavView || view.DisplayPane.ContainsFocus )
                {
                    _NavViewSource.Clear();
                    _NavViewSource.AddRange(e.PrimaryKeys);
                    _DisplayStatus = DisplayStatus.NavView;
                    btnTempory.Checked = false;
                    _UpdateDisplay = true;
                    _SelectedAllSource = e.SelectedAll;
                    _DisplaySource.Clear();
                    _DisplaySource.AddRange(e.PrimaryKeys);
                    if ( e.AddToTemp )
                    {
                        foreach ( var key in e.PrimaryKeys )
                        {
                            if ( !_TempSource.Contains(key) )
                                _TempSource.Add(key);
                        }
                        if ( TempSourceChanged != null )
                            TempSourceChanged(this, new EventArgs());
                    }
                }
            }
        }

        public MenuButton ListPaneContexMenuManager { get; set; }
        public MenuButton NavPaneContexMenuManager { get; set; }
        public MenuButtonControl FilterManager { get; set; }
        public MenuButtonControl SearchConditionManager { get; set; }


        public void SetFilteredSource(List<string> primaryKeys)
        {
            _FilteredSource = new List<string>(primaryKeys);
            _UpdateNavView = true;
        }

        /// <summary>
        /// 當使用者按下搜尋按鍵時。
        /// </summary>
        private List<EventHandler<FISCA.Presentation.SearchEventArgs>> _SearchDelegates = new List<EventHandler<SearchEventArgs>>();
        public event EventHandler<FISCA.Presentation.SearchEventArgs> Search
        {
            add
            { _SearchDelegates.Add(value); }
            remove
            {
                if ( _SearchDelegates.Contains(value) )
                    _SearchDelegates.Remove(value);
            }
        }

        #endregion

        #region INavContentDivision 成員

        public string Group
        {
            get { return buttonItem1.Text; }
            set
            {
                buttonItem1.Text = value;
                this.btnTempory.Text = "待處理" + this.Group + "(" + TempSource.Count + ")";
                LoadPreference();
            }
        }

        public Control NavigationPane
        {
            get { return navigationPanePanel1; }
        }

        public Control ContentPane
        {
            get { return panelContent; }
        }

        public Image Picture
        {
            get { return buttonItem1.Image; }
            set { buttonItem1.Image = value; }
        }

        #endregion

        private void splitterListDetial_SplitterMoved(object sender, SplitterEventArgs e)
        {
            UpdatePreference();
        }
        private void dgvDisplayList_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if ( e.RowIndex >= 0 )
            {
                string id = "" + dgvDisplayList.Rows[e.RowIndex].Tag;
                PopupDetailPane(id);
            }
        }
        //來自http://www.dreamincode.net/code/snippet1684.htm的路上撿到的Code
        private Icon MakeIcon(Image img, int size, bool keepAspectRatio)
        {
            Bitmap square = new Bitmap(size, size); // create new bitmap
            Graphics g = Graphics.FromImage(square); // allow drawing to it

            int x, y, w, h; // dimensions for new image

            if ( !keepAspectRatio || img.Height == img.Width )
            {
                // just fill the square
                x = y = 0; // set x and y to 0
                w = h = size; // set width and height to size
            }
            else
            {
                // work out the aspect ratio
                float r = (float)img.Width / (float)img.Height;

                // set dimensions accordingly to fit inside size^2 square
                if ( r > 1 )
                { // w is bigger, so divide h by r
                    w = size;
                    h = (int)( (float)size / r );
                    x = 0; y = ( size - h ) / 2; // center the image
                }
                else
                { // h is bigger, so multiply w by r
                    w = (int)( (float)size * r );
                    h = size;
                    y = 0; x = ( size - w ) / 2; // center the image
                }
            }

            // make the image shrink nicely by using HighQualityBicubic mode
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, x, y, w, h); // draw image with specified dimensions
            g.Flush(); // make sure all drawing operations complete before we get the icon

            // following line would work directly on any image, but then
            // it wouldn't look as nice.
            return Icon.FromHandle(square.GetHicon());
        }

        private void pupopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _PupopForms.Remove("" + ( (Form)sender ).Tag);
        }

        private void navContexMenuJumper_Opening(object sender, CancelEventArgs e)
        {
            foreach ( var item in this.NavPaneContexMenuManager.Items )
            {
                if ( item.Visible )
                {
                    buttonItem5.Popup(Control.MousePosition);
                    break;
                }
            }
            e.Cancel = true;
        }

        public void SetDescriptionPaneBulider(IDescriptionPaneBulider bulider)
        {
            _IDescriptionPane = bulider;
            detailPane1.SetDescriptionPane(_IDescriptionPane.GetContent());
            foreach ( var item in _PupopForms.Values )
            {
                item.SetDescriptionPane(_IDescriptionPane.GetContent());
            }
        }

        public void PopupDetailPane(string primaryKey)
        {
            if ( _PupopForms.ContainsKey(primaryKey) )
            {
                if ( _PupopForms[primaryKey].WindowState == FormWindowState.Minimized )
                    _PupopForms[primaryKey].WindowState = FormWindowState.Normal;
                _PupopForms[primaryKey].Activate();
            }
            else
            {
                PupopDetailPane pupopForm = new PupopDetailPane();
                pupopForm.Tag = primaryKey;
                pupopForm.SetDescription(GetDescription(primaryKey));
                if ( _IDescriptionPane != null )
                {
                    pupopForm.SetDescriptionPane(_IDescriptionPane.GetContent());
                }
                pupopForm.PreferenceProvider = detailPane1.PreferenceProvider;
                foreach ( var item in builders )
                {
                    pupopForm.AddDetailItem(item.GetContent());
                }
                if ( Picture != null )
                {
                    pupopForm.Icon = MakeIcon(Picture, 32, false);
                }
                pupopForm.PrimaryKey = primaryKey;
                pupopForm.FormClosed += new FormClosedEventHandler(pupopForm_FormClosed);
                if ( this.TopLevelControl is Form )
                {
                    ( (Form)this.TopLevelControl ).AddOwnedForm(pupopForm);
                }
                _PupopForms.Add(primaryKey, pupopForm);
                pupopForm.Show();
            }
        }
    }
}
