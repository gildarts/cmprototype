using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using FISCA.Presentation;
using System.Xml;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    partial class DetailPane : UserControl
    {
        static DetailPane()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        private Dictionary<string, DetailItemContainer> _Containers = new Dictionary<string, DetailItemContainer>();
        private Dictionary<string, CheckBoxX> _CheckBoxs = new Dictionary<string, CheckBoxX>();
        private Dictionary<string, LinkLabel> _Labels = new Dictionary<string, LinkLabel>();
        private IPreferenceProvider _PreferenceProvider= new DefaultPreferenceProvider();
        private DescriptionPane _DescriptionPane = null;
        private string _PrimaryKey = "";

        public DetailPane()
        {
            InitializeComponent();
            cardPanelEx1.Dock = DockStyle.Fill;
            controlContainerItem1.Control = panelEx3;
        }

        public void SetDescriptionPane(DescriptionPane pane)
        {
            _DescriptionPane = pane;
            pane.Dock = DockStyle.Top;
            labelX1.Visible = false;
            pane.BackColor = Color.Transparent;
            panelEx1.Controls.Add(pane);
            pane.Location = new Point(0, 0);
            if ( panelEx1.Height > pane.Height )
            {
                pane.Top = ( panelEx1.Height - pane.Height ) / 2;
            }
            else
            {
                panelEx1.Height = pane.Height;
            }
            buttonX1.BringToFront();
            buttonX1.Top = ( panelEx1.Height - buttonX1.Height > buttonX1.Height ? 6 : ( panelEx1.Height - buttonX1.Height ) / 2 );
        }

        public void AddDetailItem(Presentation.DetailContent content)
        {
            if (content == null) return;

            DetailItemContainer container;
            if ( !_Containers.ContainsKey(content.Group) )
            {
                container = new DetailItemContainer();
                CheckBoxX checkbox = new CheckBoxX();
                LinkLabel label = new LinkLabel();
                FlowLayoutPanel panel = new FlowLayoutPanel();
                checkbox.Tag = content.Group;
                label.Tag = content.Group;
                label.Text = content.Group;
                label.Click += new EventHandler(label_Click);
                checkbox.CheckedChanged += new EventHandler(checkbox_CheckedChanged);
                container.Visible = false;

                panel.Controls.Add(checkbox);
                panel.Controls.Add(label);
                panel.AutoSize = true;
                panel.Dock = System.Windows.Forms.DockStyle.Fill;
                panel.Margin = new System.Windows.Forms.Padding(2, 0, 10, 0);
                panel.Padding = new System.Windows.Forms.Padding(2,2,2,0);

                checkbox.Anchor = System.Windows.Forms.AnchorStyles.None;
                checkbox.Size = new System.Drawing.Size(20, 15);
                checkbox.FocusCuesEnabled = false;
                checkbox.TextVisible = false;
                checkbox.Margin = new System.Windows.Forms.Padding(0);

                label.Anchor = System.Windows.Forms.AnchorStyles.None;
                label.AutoSize = true;
                label.Margin = new System.Windows.Forms.Padding(0);

                if ( _Containers.Count / 2 > tableLayoutPanel1.RowCount )
                {
                    tableLayoutPanel1.RowCount++;
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                }
                tableLayoutPanel1.Controls.Add(panel, _Containers.Count % 2, _Containers.Count / 2);

                cardPanelEx1.Controls.Add(container);

                _Containers.Add(content.Group, container);
                _CheckBoxs.Add(content.Group, checkbox);
                _Labels.Add(content.Group, label);

                checkbox.Checked = ( PreferenceProvider["ContentsVisible"].GetAttribute(fixName(content.Group)) == "True" );
                
            }
            else
                container = _Containers[content.Group];
            container.AddContent(content);
        }

        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set
            {
                if ( value != _PrimaryKey )
                {
                    _PrimaryKey = value;
                    foreach ( var item in _Containers.Values)
                    {
                        item.SetPrimaryKey(_PrimaryKey);
                    }
                    if ( _DescriptionPane != null )
                        _DescriptionPane.SetPrimaryKey(_PrimaryKey);
                }
            }
        }
        public void ReloadAll()
        {
            foreach ( var item in _Containers.Values )
            {
                item.Reload();
            }
            if ( _DescriptionPane != null )
                _DescriptionPane.SetPrimaryKey(_PrimaryKey);
        }
        public void SetDescription(string description)
        {
            this.labelX1.Text = description;
        }
        public IPreferenceProvider PreferenceProvider
        {
            get { return _PreferenceProvider; }
            set
            {
                _PreferenceProvider = value;
                LoadPreference();
            }
        }
        private string fixName(string p)
        {
            string fixname = p.Replace("/", "_").Replace("(", "_").Replace(")", "_").Replace("[", "_").Replace("]", "_").Replace("^", "_").Replace("!", "_").Replace("?", "_").Replace(" ", "_").Replace("　", "_");
            if ( fixname == "" )
                fixname = "_";
            return fixname;
        }

        private void LoadPreference()
        {
            foreach ( var item in _CheckBoxs.Keys )
            {
                _CheckBoxs[item].Checked = ( PreferenceProvider["ContentsVisible"].GetAttribute(fixName(item)) == "True" );
            }
        }

        private void UpdatePreference()
        {
            XmlElement element = PreferenceProvider["ContentsVisible"];
            foreach ( var item in _CheckBoxs.Keys )
            {
                element.SetAttribute(fixName(item), _CheckBoxs[item].Checked.ToString());
            }
            PreferenceProvider["ContentsVisible"]=element;
        }

        private void label_Click(object sender, EventArgs e)
        {
            string key = "" + ( (Control)sender ).Tag;
            _CheckBoxs[key].Checked = true;
            if ( cardPanelEx1.VerticalScroll.Visible && cardPanelEx1.VerticalScroll.Enabled )
            {
                int newScrollValue = cardPanelEx1.VerticalScroll.Value + _Containers[key].Top;
                newScrollValue = newScrollValue > cardPanelEx1.VerticalScroll.Maximum ? cardPanelEx1.VerticalScroll.Maximum : newScrollValue < cardPanelEx1.VerticalScroll.Minimum ? cardPanelEx1.VerticalScroll.Minimum : newScrollValue;
                if ( cardPanelEx1.VerticalScroll.Value != newScrollValue )
                {
                    cardPanelEx1.VerticalScroll.Value = newScrollValue;
                    cardPanelEx1.AutoScrollPosition = new Point(cardPanelEx1.AutoScrollPosition.X, newScrollValue);
                    cardPanelEx1.ScrollControlIntoView(_Containers[key]);
                }
            }
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            string key = "" + ( (Control)sender ).Tag;
            _Containers[key].Visible = _CheckBoxs[key].Checked;
        }

        private void cardPanelEx1_MouseEnter(object sender, EventArgs e)
        {
            if ( cardPanelEx1.TopLevelControl.ContainsFocus && !cardPanelEx1.ContainsFocus )
                cardPanelEx1.Focus();
        }

        private void buttonX1_MouseEnter(object sender, EventArgs e)
        {
            buttonX1.Expanded = true;
        }

        private void buttonX1_PopupClose(object sender, EventArgs e)
        {
            UpdatePreference();
        }
    }
}
