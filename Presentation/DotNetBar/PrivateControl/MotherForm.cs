using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.Xml;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class MotherForm : Office2007RibbonForm, FISCA.Presentation.DotNetBar.PrivateControl.IMotherForm
    {
        private List<IBlankPanel> _BlankDivisions = new List<IBlankPanel>();

        public IPreferenceProvider PreferenceProvider { get { return _PreferenceProvider; } set { _PreferenceProvider = value; if ( PreferenceProviderChanged != null )PreferenceProviderChanged(this, new EventArgs()); } }
        public event EventHandler PreferenceProviderChanged;
        public RibbonBarItemManager RibbonBarItems { get; private set; }
        public MenuButton StartMenu { get; private set; }
        //public Image StartButtonImage { get { return office2007StartButton2.Image; } set { office2007StartButton2.Image = value; } }
        public void SetStatusBarMessage(string labelMessage)
        {
            SetStatusBarMessage(labelMessage, -1);
        }
        public void SetStatusBarMessage(string labelMessage, int progress)
        {
            if ( progress > 100 || progress < 0 )
            {
                progressBarMessage.Visible = false;
            }
            else
            {
                progressBarMessage.Value = progress;
                progressBarMessage.Visible = true;
            }
            lblBarMessage.Text = labelMessage;
        }
        public void AddEntity(INCPanel newEntity)
        {
            //讀取設定檔
            if ( newEntity is FISCA.Presentation.NLDPanel )
            {
                _NavContentPresentations.Add(( (FISCA.Presentation.NLDPanel)newEntity ));
                ( (FISCA.Presentation.NLDPanel)newEntity ).PreferenceProvider = new SubPreferenceProvider(newEntity.Group, this.PreferenceProvider);
            }
            CreateTabs(newEntity.Group);
            //加入新NavigationButton
            ButtonItem newButton = new ButtonItem();
            newButton.Text = newButton.Tooltip = newEntity.Group;
            newButton.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            newButton.OptionGroup = "navBar";
            //Resize圖片成24*24大小
            if ( newEntity.Picture != null )
            {
                Bitmap b = new Bitmap(24, 24);
                using ( Graphics g = Graphics.FromImage(b) )
                    g.DrawImage(newEntity.Picture, 0, 0, 24, 24);
                newButton.Image = b;// newEntity.Picture;
            }
            //newButton的NavigationPanePanel
            NavigationPanePanel navigationPanePanel2 = new DevComponents.DotNetBar.NavigationPanePanel();
            navigationPanePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            navigationPanePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            navigationPanePanel2.Location = new System.Drawing.Point(1, 25);
            navigationPanePanel2.ParentItem = newButton;
            navigationPanePanel2.Size = new System.Drawing.Size(158, 445);
            navigationPanePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            navigationPanePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            navigationPanePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            navigationPanePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            navigationPanePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            navigationPanePanel2.Style.GradientAngle = 90;
            navigationPanePanel2.TabIndex = 2;
            newEntity.NavigationPane.Dock = DockStyle.Fill;
            navigationPanePanel2.Controls.Add(newEntity.NavigationPane);
            //建立集合
            newButton.Tag = newEntity;
            _RibbonTabDictionary[newEntity.Group].Tag = newButton;
            _RibbonTabDictionary[newEntity.Group + "(延伸)"].Tag = newButton;
            newButton.CheckedChanged += new EventHandler(newButton_CheckedChanged);
            //加入至navigationPane1
            navigationPane1.Items.Add(newButton);
            this.navigationPane1.Controls.Add(navigationPanePanel2);

        }
        public void AddEntity(IBlankPanel newEntity)
        {
            _BlankDivisions.Add(newEntity);
            CreateTabs(newEntity.Group);
        }

        public MotherForm()
        {
            InitializeComponent();
            RibbonBarItems = new RibbonBarItemManager(this);
            StartMenu = new StartMenuMenuButton(this, startMenuContainer);
            office2007StartButton2.SubItems.Clear();
            startMenuContainer.SubItemsChanged += delegate
            {
                if ( startMenuContainer.SubItems.Count > 0 && !office2007StartButton2.SubItems.Contains(startMenuContainer) )
                    office2007StartButton2.SubItems.Add(startMenuContainer);
                else if ( startMenuContainer.SubItems.Count == 0 && office2007StartButton2.SubItems.Contains(startMenuContainer) )
                    office2007StartButton2.SubItems.Clear();
            };
            this.navigationPane1.LocalizeString += delegate(object sender, LocalizeEventArgs e)
            {
                switch ( e.Key )
                {
                    case "navbar_showmorebuttons":
                        e.Handled = true;
                        e.LocalizedValue = "顯示較多按鈕";
                        break;
                    case "navbar_showfewerbuttons":
                        e.Handled = true;
                        e.LocalizedValue = "顯示較少按鈕";
                        break;
                    default:
                        break;
                }
            };
            this.PreferenceProviderChanged += delegate
            {
                LoadPreference();
                foreach ( var item in _NavContentPresentations )
                {
                    item.PreferenceProvider = new SubPreferenceProvider(item.Group, this.PreferenceProvider);
                }
            };
        }


        internal RibbonTabItem GetRibbonTab(string tab)
        {
            return _RibbonTabDictionary[tab];
        }
        #region 變換色彩用
        private eOffice2007ColorScheme _ColorSchema;
        private void StyleReset(object sender, EventArgs e)
        {
            if ( ribbonControl1.Office2007ColorTable != _ColorSchema )
            {
                ribbonControl1.Office2007ColorTable = _ColorSchema;
                this.Invalidate();
            }
        }
        private void StyleChange(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            if ( item == buttonStyleOffice2007Blue )
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Blue;
            }
            else if ( item == buttonStyleOffice2007Black )
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Black;
            }
            else if ( item == buttonStyleOffice2007Silver )
            {
                // This is all that is needed to change the color table for all controls on the form
                _ColorSchema = eOffice2007ColorScheme.Silver;
            }
            ribbonControl1.Office2007ColorTable = _ColorSchema;
            this.Invalidate();
        }
        private void StylePreview(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;
            if ( item == buttonStyleOffice2007Blue )
            {
                if ( ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Blue ) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Blue;
            }
            else if ( item == buttonStyleOffice2007Black )
            {
                if ( ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Black ) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Black;
            }
            else if ( item == buttonStyleOffice2007Silver )
            {
                if ( ribbonControl1.Office2007ColorTable == eOffice2007ColorScheme.Silver ) return;
                // This is all that is needed to change the color table for all controls on the form
                ribbonControl1.Office2007ColorTable = eOffice2007ColorScheme.Silver;
            }

            this.Invalidate();
        }
        #endregion
        private List<FISCA.Presentation.NLDPanel> _NavContentPresentations = new List<FISCA.Presentation.NLDPanel>();
        private bool _Loaded = false;
        private IPreferenceProvider _PreferenceProvider = new DefaultPreferenceProvider();
        private Dictionary<string, RibbonTabItem> _RibbonTabDictionary = new Dictionary<string, RibbonTabItem>();
        private Dictionary<string, bool> _RibbonShowSecTab = new Dictionary<string, bool>();
        private XmlElement Preference
        {
            get
            {
                XmlElement PreferenceElement = null;
                if ( _PreferenceProvider != null )
                    PreferenceElement = _PreferenceProvider["MotherForm"];
                if ( PreferenceElement == null )
                {
                    PreferenceElement = new XmlDocument().CreateElement("MotherForm");
                }
                return PreferenceElement;
            }
            set
            {
                if ( _Loaded && _PreferenceProvider != null )
                    _PreferenceProvider["MotherForm"] = value;
            }
        }
        private string fixName(string p)
        {
            string fixname = p.Replace("/", "_").Replace("(", "_").Replace(")", "_").Replace("[", "_").Replace("]", "_").Replace("^", "_").Replace("!", "_").Replace("?", "_").Replace(" ", "_").Replace("　", "_");
            if ( fixname == "" )
                fixname = "_";
            return fixname;
        }
        private void newButton_CheckedChanged(object sender, EventArgs e)
        {
            ButtonItem button = (ButtonItem)sender;
            //DotNetBar很機車喔，如果他自己把按鈕縮起來，在選單上看到的會是另一個object喔，那個object的Tag才是原本的那個按鈕喔，很機車吧。
            INCPanel item = button.Tag is INCPanel ? (INCPanel)button.Tag : (INCPanel)( (PopupItem)button.Tag ).Tag;
            if ( !panelContent.Controls.Contains(item.ContentPane) )
            {
                item.ContentPane.Dock = DockStyle.Fill;
                panelContent.Controls.Add(item.ContentPane);
            }
            //同步選取ProcessTab
            foreach ( var ribbonItem in ribbonControl1.Items )
            {
                if ( ribbonItem is RibbonTabItem )
                {
                    RibbonTabItem ribbonTab = (RibbonTabItem)ribbonItem;
                    if ( ribbonTab.Text == button.Text )
                    {
                        if ( ribbonTab.Checked != button.Checked )
                            ribbonTab.Checked = button.Checked;
                        break;
                    }
                }
            }
            //同步選取ContentPanel
            if ( item.ContentPane.Visible != button.Checked )
                item.ContentPane.Visible = button.Checked;
            foreach ( var bd in _BlankDivisions )
            {
                bd.ContentPane.Visible = false;
            }
            if ( button.Checked )
            {
                expandableSplitter1.Visible = navigationPane1.Visible = true;
            }
            this.Invalidate();
            //如果navigationPane1是縮到左邊狀態則彈出NavPanPanel
            if ( !navigationPane1.Expanded && button.Checked )
            {
                navigationPane1.PopupSelectedPaneVisible = true;
            }
        }
        private void newTab_CheckedChanged(object sender, EventArgs e)
        {
            RibbonTabItem tab = (RibbonTabItem)sender;
            //是主籤
            if ( _RibbonTabDictionary.ContainsKey(tab.Text + "(延伸)") )
            {
                //被選取的Tab用粗體字
                tab.FontBold = tab.Checked | _RibbonTabDictionary[tab.Text + "(延伸)"].Checked;
                if ( tab.Checked )
                {
                    btnShowHidden.Tag = tab.Text;
                    _RibbonTabDictionary[tab.Text + "(延伸)"].Visible = _RibbonShowSecTab[tab.Text] & HasUnDisplayRibbon(tab.Text);
                }
                else if ( !_RibbonTabDictionary[tab.Text + "(延伸)"].Checked )//如果不是被選取且選取項目也不是延伸頁就把延伸頁關掉
                    _RibbonTabDictionary[tab.Text + "(延伸)"].Visible = false;
            }
            else
                tab.Visible = _RibbonTabDictionary[tab.Text.Substring(0, tab.Text.Length - 4)].Checked | tab.Checked;
            if ( tab.Checked )
            {
                if ( tab.Tag is ButtonItem )
                {
                    ButtonItem item = (ButtonItem)tab.Tag;
                    item.Checked = true;
                }
                else
                {
                    foreach ( ButtonItem item in navigationPane1.Items )
                    {
                        item.Checked = false;
                    }
                    foreach ( var item in _BlankDivisions )
                    {
                        if ( item.Group == tab.Text || item.Group + "(延伸)" == tab.Text )
                        {
                            foreach ( Control ctr in panelContent.Controls )
                            {
                                ctr.Visible = false;
                            }
                            if ( !panelContent.Controls.Contains(item.ContentPane) )
                            {
                                item.ContentPane.Dock = DockStyle.Fill;
                                panelContent.Controls.Add(item.ContentPane);
                            }
                            item.ContentPane.Visible = true;
                            expandableSplitter1.Visible = navigationPane1.Visible = false;

                            break;
                        }
                    }
                }
            }
            ribbonControl1.RecalcLayout();
        }
        private void button_CheckedChanged(object sender, EventArgs e)
        {
            ButtonItem buttonItem = (ButtonItem)sender;
            ( (RibbonBarButton)buttonItem.Tag ).Display = buttonItem.Checked;
            foreach ( var item in _RibbonTabDictionary.Values )
            {
                if ( item.Checked )
                {
                    item.Panel.Visible = false;
                    item.Panel.Visible = true;
                }
            }
            if ( !HasUnDisplayRibbon("" + btnShowHidden.Tag) )
            {
                btnShowHidden.Enabled = false;
                string text = "" + btnShowHidden.Tag;
                if ( !_RibbonTabDictionary[text].Checked )
                {
                    _RibbonTabDictionary[text].Checked = true;
                    _RibbonTabDictionary[text + "(延伸)"].Visible = false;
                }
            }
            else
            {
                btnShowHidden.Enabled = true;
            }
        }
        private void button_CheckedChanged2(object sender, EventArgs e)
        {
            ButtonItem buttonItem = (ButtonItem)sender;
            ( (RibbonBarControlContainer)buttonItem.Tag ).Display = buttonItem.Checked;
            foreach ( var item in _RibbonTabDictionary.Values )
            {
                if ( item.Checked )
                {
                    item.Panel.Visible = false;
                    item.Panel.Visible = true;
                }
            }
            if ( !HasUnDisplayRibbon("" + btnShowHidden.Tag) )
            {
                btnShowHidden.Enabled = false;
                string text = "" + btnShowHidden.Tag;
                if ( !_RibbonTabDictionary[text].Checked )
                {
                    _RibbonTabDictionary[text].Checked = true;
                    _RibbonTabDictionary[text + "(延伸)"].Visible = false;
                }
            }
            else
            {
                btnShowHidden.Enabled = true;
            }
        }
        private void CreateTabs(string tabName)
        {
            //加入新ProcessTab
            #region 加入新ProcessTab
            DevComponents.DotNetBar.RibbonTabItem newRibbonTab = new RibbonTabItem();
            DevComponents.DotNetBar.RibbonPanel newRibbonPanel = new RibbonPanel();

            if ( !_RibbonTabDictionary.ContainsKey(tabName) )
            {
                ribbonControl1.Controls.Add(newRibbonPanel);

                newRibbonTab.Text = tabName;
                newRibbonTab.Panel = newRibbonPanel;
                newRibbonPanel.Dock = DockStyle.Fill;
                newRibbonPanel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                ribbonControl1.Items.Insert(ribbonControl1.Items.Count - 2, newRibbonTab);

                _RibbonTabDictionary.Add(tabName, newRibbonTab);
                newRibbonTab.CheckedChanged += new EventHandler(newTab_CheckedChanged);
                //設定要不要顯示副頁籤
                lock ( _RibbonShowSecTab )
                {
                    if ( !_RibbonShowSecTab.ContainsKey(tabName) )
                        _RibbonShowSecTab.Add(tabName, Preference.GetAttribute(fixName("ShowSecTab_" + tabName)) == "True");
                }
            }
            else
            {
                newRibbonTab = _RibbonTabDictionary[tabName];
                newRibbonPanel = newRibbonTab.Panel;
            }
            #endregion
            //加入新ProcessTab的副籤
            #region 加入新ProcessTab的副籤
            DevComponents.DotNetBar.RibbonTabItem newRibbonTabSec = new RibbonTabItem();
            DevComponents.DotNetBar.RibbonPanel newRibbonPanelSec = new RibbonPanel();

            if ( !_RibbonTabDictionary.ContainsKey(tabName + "(延伸)") )
            {
                ribbonControl1.Controls.Add(newRibbonPanelSec);

                newRibbonTabSec.Text = tabName + "(延伸)";
                newRibbonTabSec.Panel = newRibbonPanelSec;
                newRibbonPanelSec.Dock = DockStyle.Fill;
                newRibbonPanelSec.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                //副籤預設不顯示
                newRibbonTabSec.Visible = false;
                ribbonControl1.Items.Insert(ribbonControl1.Items.IndexOf(newRibbonTab) + 1, newRibbonTabSec);

                _RibbonTabDictionary.Add(tabName + "(延伸)", newRibbonTabSec);
                newRibbonTabSec.CheckedChanged += new EventHandler(newTab_CheckedChanged);
            }
            else
            {
                newRibbonTabSec = _RibbonTabDictionary[tabName + "(延伸)"];
                newRibbonPanelSec = newRibbonTabSec.Panel;
            }
            #endregion
            newRibbonTabSec.Visible = newRibbonTab.Checked && _RibbonShowSecTab[tabName];
            ribbonControl1.Refresh();
        }
        private void MotherForm_Load(object sender, EventArgs e)
        {

            //避免顯示錯誤情形
            navigationPane1.RecalcLayout();
            navigationPane1.NavigationBarHeight = 240;
            //自動選取
            foreach ( var var in ribbonControl1.Items )
            {
                //找RibbonTabItem
                if ( var is RibbonTabItem )
                {
                    if ( ( (RibbonTabItem)var ).Tag is ButtonItem )
                    {
                        //如果是Entity的Tab
                        ( (ButtonItem)( ( (RibbonTabItem)var ).Tag ) ).Checked = true;
                        break;
                    }
                    else
                    {
                        //如果不是Entity的Tab
                        ribbonControl1.SelectedRibbonTabItem = (RibbonTabItem)var;
                        break;
                    }
                }
            }
            LoadPreference();
            _Loaded = true;
        }

        private void LoadPreference()
        {
            #region 讀取Preference資料
            this.SuspendLayout();
            XmlElement PreferenceData = Preference;
            if ( PreferenceData != null )
            {
                //顯示項目高度
                if ( PreferenceData.HasAttribute("NavigationBarHeight") )
                {
                    int height = 240;
                    if ( int.TryParse(PreferenceData.Attributes["NavigationBarHeight"].Value, out height) )
                        this.navigationPane1.NavigationBarHeight = height;
                }
                //是否最大化
                if ( PreferenceData.HasAttribute("Maximized") && PreferenceData.GetAttribute("Maximized") == "True" )
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    //如果沒有最大化則嘗試讀取高跟寬
                    if ( PreferenceData.HasAttribute("Height") )
                    {
                        int height;
                        if ( int.TryParse(PreferenceData.GetAttribute("Height"), out height) )
                            this.Height = height;
                    }
                    if ( PreferenceData.HasAttribute("Width") )
                    {
                        int width;
                        if ( int.TryParse(PreferenceData.GetAttribute("Width"), out width) )
                            this.Width = width;
                    }
                }
                //設定navigationPane1的展開設定
                if ( PreferenceData.HasAttribute("NavPanExpanded") && PreferenceData.Attributes["NavPanExpanded"].Value == "False" )
                {
                    navigationPane1.Expanded = false;
                }
                //設定ribbonControl1的展開設定
                if ( PreferenceData.HasAttribute("RibbonControlExpanded") && PreferenceData.Attributes["RibbonControlExpanded"].Value == "False" )
                {
                    ribbonControl1.Expanded = false;
                }
                //設定ColorTable
                if ( PreferenceData.HasAttribute("ColorTable") )
                {
                    switch ( PreferenceData.Attributes["ColorTable"].Value )
                    {
                        default:
                        case "Blue":
                            buttonStyleOffice2007Blue.Checked = true;
                            StyleChange(buttonStyleOffice2007Blue, null);
                            break;
                        case "Black":
                            buttonStyleOffice2007Black.Checked = true;
                            StyleChange(buttonStyleOffice2007Black, null);
                            break;
                        case "Silver":
                            buttonStyleOffice2007Silver.Checked = true;
                            StyleChange(buttonStyleOffice2007Silver, null);
                            break;
                    }
                }
                Dictionary<string, bool> setting = new Dictionary<string, bool>();
                foreach ( var tabName in _RibbonShowSecTab.Keys )
                {
                    foreach ( var bar in this.RibbonBarItems.TabItems(tabName) )
                    {
                        foreach ( var item in bar.GetButtons() )
                        {
                            item.Display = PreferenceData.GetAttribute(fixName("ShowFunction_" + tabName + "_" + bar.Text + "_" + item.Text)) != "False";
                        }
                        foreach ( var item in bar.GetControls() )
                        {
                            item.Display = PreferenceData.GetAttribute(fixName("ShowControl_" + tabName + "_" + bar.Text + "_" + item.Text)) != "False";
                        }
                    }
                    setting.Add(tabName, ( Preference.GetAttribute(fixName("ShowSecTab_" + tabName)) == "True" ));
                }
                foreach ( var key in setting.Keys )
                {
                    _RibbonShowSecTab[key] = setting[key];
                    _RibbonTabDictionary[key + "(延伸)"].Visible = ( _RibbonTabDictionary[key].Checked && _RibbonShowSecTab[key] && HasUnDisplayRibbon(key) );
                }
            }
            this.ResumeLayout();
            #endregion
        }
        internal void EnsureTabs(string tabName)
        {
            if ( !_RibbonTabDictionary.ContainsKey(tabName) )
            {
                CreateTabs(tabName);
            }
        }
        private void UpdatePreference()
        {
            #region 取得PreferenceElement
            XmlElement PreferenceElement = Preference;
            #endregion
            //紀錄navigationPane1的展開設定
            PreferenceElement.SetAttribute("NavPanExpanded", navigationPane1.Expanded ? "True" : "False");
            //紀錄ribbonControl1的展開設定
            PreferenceElement.SetAttribute("RibbonControlExpanded", ribbonControl1.Expanded ? "True" : "False");
            //紀錄ColorTable
            if ( buttonStyleOffice2007Blue.Checked )
                PreferenceElement.SetAttribute("ColorTable", "Blue");
            if ( buttonStyleOffice2007Black.Checked )
                PreferenceElement.SetAttribute("ColorTable", "Black");
            if ( buttonStyleOffice2007Silver.Checked )
                PreferenceElement.SetAttribute("ColorTable", "Silver");
            //紀錄顯示項目高度
            PreferenceElement.SetAttribute("NavigationBarHeight", navigationPane1.NavigationBarHeight.ToString());
            //紀錄是否最大化設定
            PreferenceElement.SetAttribute("Maximized", this.WindowState == FormWindowState.Maximized ? "True" : "False");
            if (this.WindowState != FormWindowState.Minimized)
            {
                //紀錄畫面高
                PreferenceElement.SetAttribute("Height", this.Height.ToString());
                //紀錄畫面寬
                PreferenceElement.SetAttribute("Width", this.Width.ToString());
            }
            //紀錄自訂功能狀況
            //foreach ( string tabKey in _ProcessShowSecTab.Keys )
            //{
            //    PreferenceElement.SetAttribute("ShowSecTab_" + tabKey, _ProcessShowSecTab[tabKey].ToString());
            //    foreach ( IProcess process in _ProcessAllListDictionary[tabKey] )
            //    {
            //        PreferenceElement.SetAttribute("ShowFunction_" + fixName(tabKey) + "_" + fixName(process.ProcessRibbon.NavText), _ProcessListDictionary[tabKey].Contains(process).ToString());
            //    }
            //}
            foreach ( var tabName in _RibbonShowSecTab.Keys )
            {
                PreferenceElement.SetAttribute(fixName("ShowSecTab_" + tabName), _RibbonShowSecTab[tabName].ToString());
                foreach ( var bar in this.RibbonBarItems.TabItems(tabName) )
                {
                    foreach ( var item in bar.GetButtons() )
                    {
                        PreferenceElement.SetAttribute(fixName("ShowFunction_" + tabName + "_" + bar.Text + "_" + item.Text), item.Display.ToString());
                    }
                    foreach ( var item in bar.GetControls() )
                    {
                        PreferenceElement.SetAttribute(fixName("ShowControl_" + tabName + "_" + bar.Text + "_" + item.Text), item.Display.ToString());
                    }
                }
            }
            Preference = PreferenceElement;
        }
        private void UpdatePreference(object sender, EventArgs e)
        {
            if ( _Loaded )
                UpdatePreference();
        }
        private void navigationPane1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            expandableSplitter1.Enabled = navigationPane1.Expanded;
            navigationPane1.Invalidate();
            if ( _Loaded )
                UpdatePreference();
        }
        private bool btnShowHidden_Checked_UIChanging = false;
        private void btnShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            if ( btnShowHidden_Checked_UIChanging )
                return;
            string tabKey = "" + btnShowHidden.Tag;
            if ( btnShowHidden.Enabled )
                _RibbonShowSecTab[tabKey] = btnShowHidden.Checked;
            if ( _RibbonTabDictionary[tabKey + "(延伸)"].Checked && ( btnShowHidden.Checked == false || HasUnDisplayRibbon(tabKey) ) )
                _RibbonTabDictionary[tabKey].Checked = true;
            _RibbonTabDictionary[tabKey + "(延伸)"].Visible = btnShowHidden.Checked & HasUnDisplayRibbon(tabKey);
            ribbonControl1.Refresh();
        }
        private bool HasUnDisplayRibbon(string text)
        {
            if ( !_RibbonTabDictionary.ContainsKey(text) )
                return false;
            foreach ( var item in this.RibbonBarItems.TabItems(text) )
            {
                foreach ( var btn in item.GetButtons() )
                {
                    if ( btn.Display == false )
                        return true;
                }
                foreach ( var btn in item.GetControls() )
                {
                    if ( btn.Display == false )
                        return true;
                }
            }
            return false;
        }
        private void buttonStyleOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            if ( _Loaded && ( (ButtonItem)sender ).Checked )
                UpdatePreference();
        }
        private void MotherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( _Loaded )
                UpdatePreference();
        }
        private void buttonItem2_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            buttonItem2.SubItems.Clear();
            string text = "" + btnShowHidden.Tag;
            RibbonTabItem tab;
            if ( _RibbonTabDictionary.ContainsKey(text) )
            {
                tab = _RibbonTabDictionary[text];
                btnShowHidden_Checked_UIChanging = true;
                btnShowHidden.Checked = _RibbonShowSecTab[tab.Text] & HasUnDisplayRibbon(tab.Text);
                btnShowHidden.Enabled = HasUnDisplayRibbon(tab.Text);
                btnShowHidden_Checked_UIChanging = false;
                _RibbonTabDictionary[tab.Text + "(延伸)"].Visible = ( _RibbonTabDictionary[tab.Text + "(延伸)"].Checked | tab.Checked ) & btnShowHidden.Checked;
                lblProcess.Text = tab.Text + " 所有功能";
                buttonItem2.SubItems.Add(lblProcess);
                foreach ( var bar in RibbonBarItems.TabItems(tab.Text) )
                {
                    ButtonItem barButton = new ButtonItem(bar.Text, bar.Text);
                    barButton.AutoCollapseOnClick = false;
                    barButton.AutoCheckOnClick = false;
                    barButton.AutoExpand = true;
                    //bool allUnVisible = false;
                    foreach ( var item in bar.GetButtons() )
                    {
                        ButtonItem button = new ButtonItem(item.Text, item.Text);
                        button.AutoCollapseOnClick = false;
                        barButton.SubItems.Add(button);
                        button.Checked = item.Display;
                        button.Tag = item;
                        button.AutoCheckOnClick = true;
                        button.CheckedChanged += new EventHandler(button_CheckedChanged);

                        //allUnVisible |= item.Display;
                    }
                    foreach ( var item in bar.GetControls() )
                    {
                        ButtonItem button = new ButtonItem(item.Text, item.Text);
                        button.AutoCollapseOnClick = false;
                        barButton.SubItems.Add(button);
                        button.Checked = item.Display;
                        button.Tag = item;
                        button.AutoCheckOnClick = true;
                        button.CheckedChanged += new EventHandler(button_CheckedChanged2);

                        //allUnVisible |= item.Display;
                    }
                    //barButton.Checked = allUnVisible;
                    buttonItem2.SubItems.Add(barButton);
                }
                btnShowHidden.Tag = tab.Text;
                btnShowHidden.Text = "顯示\"" + tab.Text + "(延伸)\"";
                //lblShowSecTab.NavText = "\"" + tab.NavText + "(延伸)\"";
                //buttonItem2.Items.Add(lblShowSecTab);
                buttonItem2.SubItems.Add(btnShowHidden);
            }
            else
            {
                buttonItem2.SubItems.Add(lblProcess);
            }

        }

        private class StartMenuMenuButton : MenuButton
        {
            private MotherForm _MotherForm;
            public StartMenuMenuButton(MotherForm form, BaseItem item)
                : base(item)
            {
                _MotherForm = form;
            }
            protected internal override void SetImage(Image image)
            {
                _MotherForm.office2007StartButton2.Image = image;
            }
            protected internal override Image GetImage()
            {
                return _MotherForm.office2007StartButton2.Image;
            }
        }
    }
}
