using System;
using System.Collections.Generic;
using System.Text;
using FISCA.Presentation.DotNetBar.PrivateControl;
using DevComponents.DotNetBar;

namespace FISCA.Presentation
{
    /// <summary>
    /// 視窗上方的功能列
    /// </summary>
    public class RibbonBarItem
    {
        private Dictionary<string, ButtonItem> _Buttons = new Dictionary<string, ButtonItem>();
        private Dictionary<string, RibbonBarButton> _BarButtons = new Dictionary<string, RibbonBarButton>();
        private Dictionary<string, ControlContainerItem> _ControlContainers = new Dictionary<string, ControlContainerItem>();
        private Dictionary<string, RibbonBarControlContainer> _BarControls = new Dictionary<string, RibbonBarControlContainer>();
        internal RibbonBarFrame DisplayRibbon { get; private set; }
        internal RibbonBarFrame HiddenRibbon { get; private set; }
        /// <summary>
        /// 取得名稱
        /// </summary>
        public string Text { get; private set; }
        internal RibbonBarItem(string ribbonName)
        {
            DisplayRibbon = new RibbonBarFrame() { Text = ribbonName };
            HiddenRibbon = new RibbonBarFrame() { Text = ribbonName };
            Text = ribbonName;
            Items = new RibbonBarButtonManager(this);
            Controls = new RibbonBarControlContainerManager(this);
        }
        /// <summary>
        /// 取得或設定，顯示的順序
        /// </summary>
        public float Index
        {
            get { return ((float)DisplayRibbon.Left) / 1000f; }
            set { DisplayRibbon.Left = (int)(value * 1000); }
        }
        /// <summary>
        /// 取得或設定，RibbonBar可否因介面寬度縮合
        /// </summary>
        public bool AutoOverflowEnabled
        {
            get { return DisplayRibbon.AutoOverflowEnabled; }
            set { DisplayRibbon.AutoOverflowEnabled = HiddenRibbon.AutoOverflowEnabled = value; }
        }
        /// <summary>
        /// 取得或設定，因介面縮合的優先值
        /// </summary>
        public int ResizeOrderIndex
        {
            get { return DisplayRibbon.ResizeOrderIndex; }
            set { DisplayRibbon.ResizeOrderIndex = HiddenRibbon.ResizeOrderIndex = value; }
        }
        /// <summary>
        /// 取得或設定，介面縮合時顯示的圖示
        /// </summary>
        public System.Drawing.Image OverflowButtonImage
        {
            get { return DisplayRibbon.OverflowButtonImage; }
            set { DisplayRibbon.OverflowButtonImage = HiddenRibbon.OverflowButtonImage = value; }
        }
        /// <summary>
        /// 取得子按紐集合
        /// </summary>
        public RibbonBarButtonManager Items { get; private set; }
        /// <summary>
        /// 取得子控制項集合
        /// </summary>
        public RibbonBarControlContainerManager Controls { get; private set; }
        /// <summary>
        /// 建立新按紐，或取得已存在的按紐
        /// </summary>
        /// <param name="text">按紐名稱</param>
        /// <returns></returns>
        public RibbonBarButton this[string text] { get { return Items[text]; } }
        /// <summary>
        /// 將指定的顯示區塊顯示在功能列最前端
        /// </summary>
        /// <param name="type">顯示區塊</param>
        public void SetTopContainer(ContainerType type)
        {
            DisplayRibbon.SetTopContainer(type);
            HiddenRibbon.SetTopContainer(type);
        }


        internal RibbonBarButton GetButton(string text)
        {
            if (_BarButtons.ContainsKey(text))
                return _BarButtons[text];
            else
            {
                ButtonItem newItem = new ButtonItem();
                newItem.Text = text;
                newItem.TextChanged += new EventHandler(newItem_TextChanged);
                RibbonBarButton newButton = new RibbonBarButton(newItem);
                _Buttons.Add(text, newItem);
                _BarButtons.Add(text, newButton);
                newButton.SizeChanged += delegate(object sender, EventArgs e)
                {
                    SetBarDisplay((RibbonBarButton)sender);
                };
                newButton.DisplayChanged += delegate(object sender, EventArgs e)
                {
                    SetBarDisplay((RibbonBarButton)sender);
                };
                SetBarDisplay(newButton);
                return newButton;
            }
        }

        internal List<RibbonBarButton> GetButtons()
        {
            return new List<RibbonBarButton>(_BarButtons.Values);
        }

        private void SetBarDisplay(RibbonBarButton barButton)
        {
            RibbonBarFrame frame = barButton.Display ? DisplayRibbon : HiddenRibbon;
            switch (barButton.Size)
            {
                case RibbonBarButton.MenuButtonSize.ExtraLarge:
                    if (!frame.ExtraLargeButtonContainer.SubItems.Contains(_Buttons[barButton.Text]))
                        frame.ExtraLargeButtonContainer.SubItems.Add(_Buttons[barButton.Text]);
                    break;
                case RibbonBarButton.MenuButtonSize.Large:
                    if (!frame.LargeButtonContainer.SubItems.Contains(_Buttons[barButton.Text]))
                        frame.LargeButtonContainer.SubItems.Add(_Buttons[barButton.Text]);
                    break;
                case RibbonBarButton.MenuButtonSize.Medium:
                    if (!frame.MediumButtonContainer.SubItems.Contains(_Buttons[barButton.Text]))
                        frame.MediumButtonContainer.SubItems.Add(_Buttons[barButton.Text]);
                    break;
                case RibbonBarButton.MenuButtonSize.Small:
                    if (!frame.SmallButtonContainer.SubItems.Contains(_Buttons[barButton.Text]))
                        frame.SmallButtonContainer.SubItems.Add(_Buttons[barButton.Text]);
                    break;
            }
            DisplayRibbon.RecalcLayout();
            HiddenRibbon.RecalcLayout();
            if (DisplayRibbon.Parent != null && DisplayRibbon.Parent.Visible)
            {
                DisplayRibbon.Parent.Visible = false;
                DisplayRibbon.Parent.Visible = true;
            }
            if (HiddenRibbon.Parent != null && HiddenRibbon.Parent.Visible)
            {
                HiddenRibbon.Parent.Visible = false;
                HiddenRibbon.Parent.Visible = true;
            }
        }

        internal RibbonBarControlContainer GetControl(string text)
        {
            if (_BarControls.ContainsKey(text))
                return _BarControls[text];
            else
            {
                ControlContainerItem container = new ControlContainerItem(text, text);
                RibbonBarControlContainer newControl = new RibbonBarControlContainer(container);
                _ControlContainers.Add(text, container);
                _BarControls.Add(text, newControl);
                newControl.DisplayChanged += delegate(object sender, EventArgs e)
                {
                    SetBarDisplay((RibbonBarControlContainer)sender);
                };
                SetBarDisplay(newControl);
                return newControl;
            }
        }

        internal List<RibbonBarControlContainer> GetControls()
        {
            return new List<RibbonBarControlContainer>(_BarControls.Values);
        }

        private void SetBarDisplay(RibbonBarControlContainer barControl)
        {
            RibbonBarFrame frame = barControl.Display ? DisplayRibbon : HiddenRibbon;
            if (!frame.ControlContainer.SubItems.Contains(_ControlContainers[barControl.Text]))
                frame.ControlContainer.SubItems.Add(_ControlContainers[barControl.Text]);
            DisplayRibbon.RecalcLayout();
            HiddenRibbon.RecalcLayout();
            if (DisplayRibbon.Parent != null && DisplayRibbon.Parent.Visible)
            {
                DisplayRibbon.Parent.Visible = false;
                DisplayRibbon.Parent.Visible = true;
            }
            if (HiddenRibbon.Parent != null && HiddenRibbon.Parent.Visible)
            {
                HiddenRibbon.Parent.Visible = false;
                HiddenRibbon.Parent.Visible = true;
            }
        }

        void newItem_TextChanged(object sender, EventArgs e)
        {
            throw new Exception("RibbonBarButton不允許變更Text屬性");
        }

    }
    /// <summary>
    /// 功能列所包含的顯示區塊
    /// </summary>
    public enum ContainerType
    {
        /// <summary>
        /// 放置<see cref="FISCA.Presentation.RibbonBarButton.MenuButtonSize">MenuButtonSize</see>.ExtraLarge的區塊。
        /// </summary>
        ExtraLarge,
        /// <summary>
        /// 放置<see cref="FISCA.Presentation.RibbonBarButton.MenuButtonSize">MenuButtonSize</see>.Large的區塊。
        /// </summary>
        Large,
        /// <summary>
        /// 放置<see cref="FISCA.Presentation.RibbonBarButton.MenuButtonSize">MenuButtonSize</see>.Medium的區塊。
        /// </summary>
        Medium,
        /// <summary>
        /// 放置<see cref="FISCA.Presentation.RibbonBarButton.MenuButtonSize">MenuButtonSize</see>.Small的區塊。
        /// </summary>
        Small,
        /// <summary>
        /// 放置自訂控制項的區塊。
        /// </summary>
        Control
    }
}
