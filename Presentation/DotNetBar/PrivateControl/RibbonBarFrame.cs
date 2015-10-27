using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Drawing;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    class RibbonBarFrame : RibbonBar
    {
        static RibbonBarFrame()
        {
            DotNetBarReferenceFixer.FixIt();
        }
        internal DevComponents.DotNetBar.ItemContainer MediumButtonContainer { get; private set; }
        internal DevComponents.DotNetBar.ItemContainer SmallButtonContainer { get; private set; }
        internal DevComponents.DotNetBar.ItemContainer LargeButtonContainer { get; private set; }
        internal DevComponents.DotNetBar.ItemContainer ExtraLargeButtonContainer { get; private set; }
        internal DevComponents.DotNetBar.ItemContainer ControlContainer { get; private set; }

        public RibbonBarFrame()
        {
            this.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ResizeItemsToFit = true;
            this.AutoSizeItems = false;
            this.Visible = false;

            #region ExtraLargeButtonContainer
            this.ExtraLargeButtonContainer = new DevComponents.DotNetBar.ItemContainer();
            this.ExtraLargeButtonContainer.ItemSpacing = 0;
            this.ExtraLargeButtonContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Horizontal;
            this.ExtraLargeButtonContainer.MinimumSize = new System.Drawing.Size(0, 70);
            this.ExtraLargeButtonContainer.ResizeItemsToFit = true;
            this.ExtraLargeButtonContainer.SubItemsChanged += delegate
            {
                foreach ( var item in ExtraLargeButtonContainer.SubItems )
                {
                    if ( item is ButtonItem )
                    {
                        ButtonItem button = (ButtonItem)item;
                        if ( button.Image == null )
                        {
                            Bitmap b = new Bitmap(48, 48);
                            button.Image = b;
                        }
                        button.ImageFixedSize = new Size(48, 48);
                        button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.Default;
                        button.ImagePaddingHorizontal = 8;
                        button.SubItemsExpandWidth = 14;
                        button.ImagePosition = DevComponents.DotNetBar.eImagePosition.Left;
                        button.AutoExpandOnClick = true;
                        button.AutoCollapseOnClick = false;
                        button.ShowSubItems = true;
                    }
                }
                SetVisible();
            };
            #endregion

            #region LargeButtonContainer
            this.LargeButtonContainer = new DevComponents.DotNetBar.ItemContainer();
            this.LargeButtonContainer.ItemSpacing = 0;
            this.LargeButtonContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Horizontal;
            this.LargeButtonContainer.MinimumSize = new System.Drawing.Size(0, 70);
            this.LargeButtonContainer.ResizeItemsToFit = true;
            this.LargeButtonContainer.SubItemsChanged += delegate
            {
                foreach ( var item in LargeButtonContainer.SubItems )
                {
                    if ( item is ButtonItem )
                    {
                        ButtonItem button = (ButtonItem)item;
                        if ( button.Image == null )
                        {
                            Bitmap b = new Bitmap(46, 46);
                            button.Image = b;
                        }
                        button.ImageFixedSize = button.SubItems.Count > 0 ? new Size(33, 33) : new Size(46, 46);
                        button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                        button.ImagePaddingHorizontal = 8;
                        button.SubItemsExpandWidth = 14;
                        button.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
                        button.AutoExpandOnClick = true;
                        button.AutoCollapseOnClick = false;
                        button.ShowSubItems = true;
                        button.SubItemsChanged += delegate(object sender, System.ComponentModel.CollectionChangeEventArgs e)
                        {
                            ButtonItem b = (ButtonItem)sender;
                            if ( LargeButtonContainer.SubItems.Contains(b) )
                                b.ImageFixedSize = b.SubItems.Count > 0 ? new Size(33, 33) : new Size(46, 46);
                        };
                    }
                }
                SetVisible();
            };
            #endregion

            #region MediumButtonContainer
            this.MediumButtonContainer = new DevComponents.DotNetBar.ItemContainer();
            this.MediumButtonContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.MediumButtonContainer.MinimumSize = new System.Drawing.Size(0, 70);
            this.MediumButtonContainer.MultiLine = true;
            this.MediumButtonContainer.ResizeItemsToFit = false;
            this.MediumButtonContainer.SubItemsChanged += delegate
            {
                foreach ( var item in MediumButtonContainer.SubItems )
                {
                    if ( item is ButtonItem )
                    {
                        ButtonItem button = (ButtonItem)item;
                        button.ImagePaddingHorizontal = 3;
                        button.ImagePaddingVertical = 0;
                        button.ImageFixedSize = new System.Drawing.Size(32, 32);
                        button.AutoExpandOnClick = true;
                        button.AutoCollapseOnClick = false;
                        button.ShowSubItems = true;
                        button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                    }
                }
                SetVisible();
            };
            #endregion

            #region SmallButtonContainer
            this.SmallButtonContainer = new DevComponents.DotNetBar.ItemContainer();
            this.SmallButtonContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.SmallButtonContainer.MinimumSize = new System.Drawing.Size(0, 70);
            this.SmallButtonContainer.MultiLine = true;
            this.SmallButtonContainer.ResizeItemsToFit = false;
            this.SmallButtonContainer.SubItemsChanged += delegate
            {
                foreach ( var item in SmallButtonContainer.SubItems )
                {
                    if ( item is ButtonItem )
                    {
                        ButtonItem button = (ButtonItem)item;
                        button.ImageFixedSize = new System.Drawing.Size(16, 16);
                        button.ImagePaddingHorizontal = 3;
                        button.ImagePaddingVertical = 3;
                        button.AutoExpandOnClick = true;
                        button.AutoCollapseOnClick = false;
                        button.ShowSubItems = true;
                        button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                    }
                }
                SetVisible();
            };
            #endregion

            #region ControlContainer
            this.ControlContainer = new ItemContainer();
            this.ControlContainer.SubItemsChanged += delegate
            {
                SetVisible();
            };
            #endregion

            this.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] { this.ExtraLargeButtonContainer, this.LargeButtonContainer, this.MediumButtonContainer, this.SmallButtonContainer, this.ControlContainer });
        }

        public void SetTopContainer(ContainerType type)
        {
            switch ( type )
            { 
                case ContainerType.Control:
                    this.Items.Remove(ControlContainer);
                    this.Items.Insert(0, ControlContainer);
                    break;
                case ContainerType.ExtraLarge:
                    this.Items.Remove(ExtraLargeButtonContainer);
                    this.Items.Insert(0, ExtraLargeButtonContainer);
                    break;
                case ContainerType.Large:
                    this.Items.Remove(LargeButtonContainer);
                    this.Items.Insert(0, LargeButtonContainer);
                    break;
                case ContainerType.Medium:
                    this.Items.Remove(MediumButtonContainer);
                    this.Items.Insert(0, MediumButtonContainer);
                    break;
                case ContainerType.Small:
                    this.Items.Remove(SmallButtonContainer);
                    this.Items.Insert(0, SmallButtonContainer);
                    break;
            }
        }

        private void SetVisible()
        {
            ExtraLargeButtonContainer.Visible = ExtraLargeButtonContainer.SubItems.Count > 0;
            LargeButtonContainer.Visible = LargeButtonContainer.SubItems.Count > 0;
            MediumButtonContainer.Visible = MediumButtonContainer.SubItems.Count > 0;
            SmallButtonContainer.Visible = SmallButtonContainer.SubItems.Count > 0;
            ControlContainer.Visible = ControlContainer.SubItems.Count > 0;
            this.Visible = ExtraLargeButtonContainer.SubItems.Count > 0 || LargeButtonContainer.SubItems.Count > 0 || MediumButtonContainer.SubItems.Count > 0 || SmallButtonContainer.SubItems.Count > 0 || ControlContainer.SubItems.Count > 0;
        }
    }
}
