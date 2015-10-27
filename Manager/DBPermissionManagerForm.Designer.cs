namespace Manager
{
    partial class DBPermissionManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.advTree1 = new DevComponents.AdvTree.AdvTree();
            this.chObjectType = new DevComponents.AdvTree.ColumnHeader();
            this.chOwner = new DevComponents.AdvTree.ColumnHeader();
            this.chDDL = new DevComponents.AdvTree.ColumnHeader();
            this.chDML = new DevComponents.AdvTree.ColumnHeader();
            this.chExecute = new DevComponents.AdvTree.ColumnHeader();
            this.chUsage = new DevComponents.AdvTree.ColumnHeader();
            this.node6 = new DevComponents.AdvTree.Node();
            this.cell6 = new DevComponents.AdvTree.Cell();
            this.EditableStyle = new DevComponents.DotNetBar.ElementStyle();
            this.EditableStyleSelected = new DevComponents.DotNetBar.ElementStyle();
            this.cell7 = new DevComponents.AdvTree.Cell();
            this.cell8 = new DevComponents.AdvTree.Cell();
            this.cell11 = new DevComponents.AdvTree.Cell();
            this.cell12 = new DevComponents.AdvTree.Cell();
            this.node7 = new DevComponents.AdvTree.Node();
            this.cell13 = new DevComponents.AdvTree.Cell();
            this.cell16 = new DevComponents.AdvTree.Cell();
            this.cell20 = new DevComponents.AdvTree.Cell();
            this.cell21 = new DevComponents.AdvTree.Cell();
            this.cell24 = new DevComponents.AdvTree.Cell();
            this.node4 = new DevComponents.AdvTree.Node();
            this.cSDTOwner = new DevComponents.AdvTree.Cell();
            this.cSDTAll = new DevComponents.AdvTree.Cell();
            this.cSDTCrud = new DevComponents.AdvTree.Cell();
            this.cell9 = new DevComponents.AdvTree.Cell();
            this.cell10 = new DevComponents.AdvTree.Cell();
            this.node5 = new DevComponents.AdvTree.Node();
            this.cUDTOwner = new DevComponents.AdvTree.Cell();
            this.cUDTAll = new DevComponents.AdvTree.Cell();
            this.cUDTCrud = new DevComponents.AdvTree.Cell();
            this.cell14 = new DevComponents.AdvTree.Cell();
            this.cell15 = new DevComponents.AdvTree.Cell();
            this.node2 = new DevComponents.AdvTree.Node();
            this.cSeqOwner = new DevComponents.AdvTree.Cell();
            this.cell17 = new DevComponents.AdvTree.Cell();
            this.cell18 = new DevComponents.AdvTree.Cell();
            this.cell19 = new DevComponents.AdvTree.Cell();
            this.cSeqUsage = new DevComponents.AdvTree.Cell();
            this.node3 = new DevComponents.AdvTree.Node();
            this.cFunOwner = new DevComponents.AdvTree.Cell();
            this.cell22 = new DevComponents.AdvTree.Cell();
            this.cell23 = new DevComponents.AdvTree.Cell();
            this.cFunExecute = new DevComponents.AdvTree.Cell();
            this.cell25 = new DevComponents.AdvTree.Cell();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.DefaultNodeStyle = new DevComponents.DotNetBar.ElementStyle();
            this.cell1 = new DevComponents.AdvTree.Cell();
            this.cell2 = new DevComponents.AdvTree.Cell();
            this.cell3 = new DevComponents.AdvTree.Cell();
            this.cell4 = new DevComponents.AdvTree.Cell();
            this.cell5 = new DevComponents.AdvTree.Cell();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.cell26 = new DevComponents.AdvTree.Cell();
            this.cell27 = new DevComponents.AdvTree.Cell();
            this.cell28 = new DevComponents.AdvTree.Cell();
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
            this.SuspendLayout();
            // 
            // advTree1
            // 
            this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree1.AllowDrop = true;
            this.advTree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.advTree1.BackColor = System.Drawing.Color.Silver;
            // 
            // 
            // 
            this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree1.CellEdit = true;
            this.advTree1.Columns.Add(this.chObjectType);
            this.advTree1.Columns.Add(this.chOwner);
            this.advTree1.Columns.Add(this.chDDL);
            this.advTree1.Columns.Add(this.chDML);
            this.advTree1.Columns.Add(this.chExecute);
            this.advTree1.Columns.Add(this.chUsage);
            this.advTree1.DoubleClickTogglesNode = false;
            this.advTree1.DragDropEnabled = false;
            this.advTree1.GridColumnLines = false;
            this.advTree1.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.advTree1.GridRowLines = true;
            this.advTree1.HideSelection = true;
            this.advTree1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advTree1.Location = new System.Drawing.Point(12, 12);
            this.advTree1.Name = "advTree1";
            this.advTree1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node6,
            this.node7,
            this.node4,
            this.node5,
            this.node2,
            this.node3});
            this.advTree1.NodesConnector = this.nodeConnector1;
            this.advTree1.NodeStyle = this.DefaultNodeStyle;
            this.advTree1.PathSeparator = ";";
            this.advTree1.SelectionBox = false;
            this.advTree1.Size = new System.Drawing.Size(844, 529);
            this.advTree1.Styles.Add(this.DefaultNodeStyle);
            this.advTree1.Styles.Add(this.EditableStyle);
            this.advTree1.Styles.Add(this.EditableStyleSelected);
            this.advTree1.TabIndex = 0;
            this.advTree1.Text = "advTree1";
            this.advTree1.BeforeCollapse += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.advTree1_BeforeCollapse);
            // 
            // chObjectType
            // 
            this.chObjectType.Name = "chObjectType";
            this.chObjectType.Text = "權限分類";
            this.chObjectType.Width.Relative = 25;
            // 
            // chOwner
            // 
            this.chOwner.Name = "chOwner";
            this.chOwner.Text = "角色名稱";
            this.chOwner.Width.Relative = 73;
            // 
            // chDDL
            // 
            this.chDDL.Name = "chDDL";
            this.chDDL.Text = "ALL";
            this.chDDL.Visible = false;
            this.chDDL.Width.Relative = 15;
            // 
            // chDML
            // 
            this.chDML.Name = "chDML";
            this.chDML.Text = "CRUD";
            this.chDML.Visible = false;
            this.chDML.Width.Relative = 15;
            // 
            // chExecute
            // 
            this.chExecute.Name = "chExecute";
            this.chExecute.Text = "Execute";
            this.chExecute.Visible = false;
            this.chExecute.Width.Relative = 15;
            // 
            // chUsage
            // 
            this.chUsage.Name = "chUsage";
            this.chUsage.Text = "Usage";
            this.chUsage.Visible = false;
            this.chUsage.Width.Relative = 15;
            // 
            // node6
            // 
            this.node6.Cells.Add(this.cell6);
            this.node6.Cells.Add(this.cell7);
            this.node6.Cells.Add(this.cell8);
            this.node6.Cells.Add(this.cell11);
            this.node6.Cells.Add(this.cell12);
            this.node6.Editable = false;
            this.node6.Expanded = true;
            this.node6.Name = "node6";
            this.node6.Text = "資料庫";
            // 
            // cell6
            // 
            this.cell6.Editable = false;
            this.cell6.Name = "cell6";
            this.cell6.StyleMouseOver = null;
            this.cell6.StyleNormal = this.EditableStyle;
            this.cell6.StyleSelected = this.EditableStyleSelected;
            this.cell6.Text = "pgsql";
            // 
            // EditableStyle
            // 
            this.EditableStyle.BackColor = System.Drawing.Color.White;
            this.EditableStyle.Name = "EditableStyle";
            this.EditableStyle.PaddingBottom = 3;
            this.EditableStyle.PaddingLeft = 3;
            this.EditableStyle.PaddingRight = 3;
            this.EditableStyle.PaddingTop = 3;
            this.EditableStyle.TextColor = System.Drawing.Color.Black;
            // 
            // EditableStyleSelected
            // 
            this.EditableStyleSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(198)))));
            this.EditableStyleSelected.Name = "EditableStyleSelected";
            this.EditableStyleSelected.TextColor = System.Drawing.Color.Black;
            // 
            // cell7
            // 
            this.cell7.Editable = false;
            this.cell7.Name = "cell7";
            this.cell7.StyleMouseOver = null;
            // 
            // cell8
            // 
            this.cell8.Editable = false;
            this.cell8.Name = "cell8";
            this.cell8.StyleMouseOver = null;
            // 
            // cell11
            // 
            this.cell11.Editable = false;
            this.cell11.Name = "cell11";
            this.cell11.StyleMouseOver = null;
            // 
            // cell12
            // 
            this.cell12.Editable = false;
            this.cell12.Name = "cell12";
            this.cell12.StyleMouseOver = null;
            // 
            // node7
            // 
            this.node7.Cells.Add(this.cell13);
            this.node7.Cells.Add(this.cell16);
            this.node7.Cells.Add(this.cell20);
            this.node7.Cells.Add(this.cell21);
            this.node7.Cells.Add(this.cell24);
            this.node7.Editable = false;
            this.node7.Expanded = true;
            this.node7.Name = "node7";
            this.node7.Text = "綱目(Public Schema)";
            // 
            // cell13
            // 
            this.cell13.Name = "cell13";
            this.cell13.StyleMouseOver = null;
            this.cell13.StyleNormal = this.EditableStyle;
            this.cell13.StyleSelected = this.EditableStyleSelected;
            this.cell13.Text = "pgsql";
            // 
            // cell16
            // 
            this.cell16.Name = "cell16";
            this.cell16.StyleMouseOver = null;
            this.cell16.StyleNormal = this.EditableStyle;
            this.cell16.StyleSelected = this.EditableStyleSelected;
            this.cell16.Text = "SSchool_Admin";
            // 
            // cell20
            // 
            this.cell20.Editable = false;
            this.cell20.Name = "cell20";
            this.cell20.StyleMouseOver = null;
            // 
            // cell21
            // 
            this.cell21.Editable = false;
            this.cell21.Name = "cell21";
            this.cell21.StyleMouseOver = null;
            // 
            // cell24
            // 
            this.cell24.Name = "cell24";
            this.cell24.StyleMouseOver = null;
            this.cell24.StyleNormal = this.EditableStyle;
            this.cell24.StyleSelected = this.EditableStyleSelected;
            this.cell24.Text = "SSchool_User";
            // 
            // node4
            // 
            this.node4.Cells.Add(this.cSDTOwner);
            this.node4.Cells.Add(this.cSDTAll);
            this.node4.Cells.Add(this.cSDTCrud);
            this.node4.Cells.Add(this.cell9);
            this.node4.Cells.Add(this.cell10);
            this.node4.Editable = false;
            this.node4.Expanded = true;
            this.node4.Name = "node4";
            this.node4.Text = "系統資料表";
            // 
            // cSDTOwner
            // 
            this.cSDTOwner.Name = "cSDTOwner";
            this.cSDTOwner.StyleMouseOver = null;
            this.cSDTOwner.StyleNormal = this.EditableStyle;
            this.cSDTOwner.StyleSelected = this.EditableStyleSelected;
            this.cSDTOwner.Text = "pgsql";
            // 
            // cSDTAll
            // 
            this.cSDTAll.Name = "cSDTAll";
            this.cSDTAll.StyleMouseOver = null;
            this.cSDTAll.StyleNormal = this.EditableStyle;
            this.cSDTAll.StyleSelected = this.EditableStyleSelected;
            this.cSDTAll.Text = "SSchool_Admin";
            // 
            // cSDTCrud
            // 
            this.cSDTCrud.Name = "cSDTCrud";
            this.cSDTCrud.StyleMouseOver = null;
            this.cSDTCrud.StyleNormal = this.EditableStyle;
            this.cSDTCrud.StyleSelected = this.EditableStyleSelected;
            this.cSDTCrud.Text = "SSchool_User";
            // 
            // cell9
            // 
            this.cell9.Editable = false;
            this.cell9.Name = "cell9";
            this.cell9.StyleMouseOver = null;
            // 
            // cell10
            // 
            this.cell10.Editable = false;
            this.cell10.Name = "cell10";
            this.cell10.StyleMouseOver = null;
            // 
            // node5
            // 
            this.node5.Cells.Add(this.cUDTOwner);
            this.node5.Cells.Add(this.cUDTAll);
            this.node5.Cells.Add(this.cUDTCrud);
            this.node5.Cells.Add(this.cell14);
            this.node5.Cells.Add(this.cell15);
            this.node5.DragDropEnabled = false;
            this.node5.Editable = false;
            this.node5.Expanded = true;
            this.node5.Name = "node5";
            this.node5.Text = "使用者資料表(UDT)";
            // 
            // cUDTOwner
            // 
            this.cUDTOwner.Name = "cUDTOwner";
            this.cUDTOwner.StyleMouseOver = null;
            this.cUDTOwner.StyleNormal = this.EditableStyle;
            this.cUDTOwner.StyleSelected = this.EditableStyleSelected;
            this.cUDTOwner.Text = "SSchool_Admin";
            // 
            // cUDTAll
            // 
            this.cUDTAll.Name = "cUDTAll";
            this.cUDTAll.StyleMouseOver = null;
            this.cUDTAll.StyleNormal = this.EditableStyle;
            this.cUDTAll.StyleSelected = this.EditableStyleSelected;
            this.cUDTAll.Text = "SSchool_Admin";
            // 
            // cUDTCrud
            // 
            this.cUDTCrud.Name = "cUDTCrud";
            this.cUDTCrud.StyleMouseOver = null;
            this.cUDTCrud.StyleNormal = this.EditableStyle;
            this.cUDTCrud.StyleSelected = this.EditableStyleSelected;
            this.cUDTCrud.Text = "SSchool_User";
            // 
            // cell14
            // 
            this.cell14.Editable = false;
            this.cell14.Name = "cell14";
            this.cell14.StyleMouseOver = null;
            // 
            // cell15
            // 
            this.cell15.Editable = false;
            this.cell15.Name = "cell15";
            this.cell15.StyleMouseOver = null;
            // 
            // node2
            // 
            this.node2.Cells.Add(this.cSeqOwner);
            this.node2.Cells.Add(this.cell17);
            this.node2.Cells.Add(this.cell18);
            this.node2.Cells.Add(this.cell19);
            this.node2.Cells.Add(this.cSeqUsage);
            this.node2.Editable = false;
            this.node2.Expanded = true;
            this.node2.Name = "node2";
            this.node2.Text = "序列數(Sequence)";
            // 
            // cSeqOwner
            // 
            this.cSeqOwner.Name = "cSeqOwner";
            this.cSeqOwner.StyleMouseOver = null;
            this.cSeqOwner.StyleNormal = this.EditableStyle;
            this.cSeqOwner.StyleSelected = this.EditableStyleSelected;
            this.cSeqOwner.Text = "pgsql";
            // 
            // cell17
            // 
            this.cell17.Editable = false;
            this.cell17.Name = "cell17";
            this.cell17.StyleMouseOver = null;
            // 
            // cell18
            // 
            this.cell18.Editable = false;
            this.cell18.Name = "cell18";
            this.cell18.StyleMouseOver = null;
            // 
            // cell19
            // 
            this.cell19.Editable = false;
            this.cell19.Name = "cell19";
            this.cell19.StyleMouseOver = null;
            // 
            // cSeqUsage
            // 
            this.cSeqUsage.Name = "cSeqUsage";
            this.cSeqUsage.StyleMouseOver = null;
            this.cSeqUsage.StyleNormal = this.EditableStyle;
            this.cSeqUsage.StyleSelected = this.EditableStyleSelected;
            this.cSeqUsage.Text = "SSchool_Admin,SSchool_User";
            // 
            // node3
            // 
            this.node3.Cells.Add(this.cFunOwner);
            this.node3.Cells.Add(this.cell22);
            this.node3.Cells.Add(this.cell23);
            this.node3.Cells.Add(this.cFunExecute);
            this.node3.Cells.Add(this.cell25);
            this.node3.Editable = false;
            this.node3.Expanded = true;
            this.node3.Name = "node3";
            this.node3.Text = "觸發器函數(Trigger)";
            // 
            // cFunOwner
            // 
            this.cFunOwner.Name = "cFunOwner";
            this.cFunOwner.StyleMouseOver = null;
            this.cFunOwner.StyleNormal = this.EditableStyle;
            this.cFunOwner.StyleSelected = this.EditableStyleSelected;
            this.cFunOwner.Text = "pgsql";
            // 
            // cell22
            // 
            this.cell22.Editable = false;
            this.cell22.Name = "cell22";
            this.cell22.StyleMouseOver = null;
            // 
            // cell23
            // 
            this.cell23.Editable = false;
            this.cell23.Name = "cell23";
            this.cell23.StyleMouseOver = null;
            // 
            // cFunExecute
            // 
            this.cFunExecute.Name = "cFunExecute";
            this.cFunExecute.StyleMouseOver = null;
            this.cFunExecute.StyleNormal = this.EditableStyle;
            this.cFunExecute.StyleSelected = this.EditableStyleSelected;
            this.cFunExecute.Text = "SSchool_Admin,SSchool_User";
            // 
            // cell25
            // 
            this.cell25.Editable = false;
            this.cell25.Name = "cell25";
            this.cell25.StyleMouseOver = null;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // DefaultNodeStyle
            // 
            this.DefaultNodeStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.DefaultNodeStyle.BorderLeftWidth = 1;
            this.DefaultNodeStyle.BorderRightWidth = 1;
            this.DefaultNodeStyle.BorderTopWidth = 1;
            this.DefaultNodeStyle.Name = "DefaultNodeStyle";
            this.DefaultNodeStyle.PaddingBottom = 3;
            this.DefaultNodeStyle.PaddingLeft = 3;
            this.DefaultNodeStyle.PaddingRight = 3;
            this.DefaultNodeStyle.PaddingTop = 3;
            this.DefaultNodeStyle.TextColor = System.Drawing.Color.Black;
            // 
            // cell1
            // 
            this.cell1.Editable = false;
            this.cell1.Name = "cell1";
            this.cell1.StyleMouseOver = null;
            // 
            // cell2
            // 
            this.cell2.Editable = false;
            this.cell2.Name = "cell2";
            this.cell2.StyleMouseOver = null;
            // 
            // cell3
            // 
            this.cell3.Editable = false;
            this.cell3.Name = "cell3";
            this.cell3.StyleMouseOver = null;
            // 
            // cell4
            // 
            this.cell4.Editable = false;
            this.cell4.Name = "cell4";
            this.cell4.StyleMouseOver = null;
            // 
            // cell5
            // 
            this.cell5.Editable = false;
            this.cell5.Name = "cell5";
            this.cell5.StyleMouseOver = null;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Location = new System.Drawing.Point(699, 553);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "套用";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(781, 553);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cell26
            // 
            this.cell26.Name = "cell26";
            this.cell26.StyleMouseOver = null;
            this.cell26.Text = "pgsql";
            // 
            // cell27
            // 
            this.cell27.Name = "cell27";
            this.cell27.StyleMouseOver = null;
            this.cell27.Text = "SSchool_Admin";
            // 
            // cell28
            // 
            this.cell28.Name = "cell28";
            this.cell28.StyleMouseOver = null;
            this.cell28.Text = "SSchool_User";
            // 
            // ResetDBPermissionForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(868, 582);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.advTree1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ResetDBPermissionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重設資料庫權限";
            this.Load += new System.EventHandler(this.ResetDBPermissionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree advTree1;
        private DevComponents.AdvTree.ColumnHeader chObjectType;
        private DevComponents.AdvTree.ColumnHeader chOwner;
        private DevComponents.AdvTree.ColumnHeader chDDL;
        private DevComponents.AdvTree.ColumnHeader chDML;
        private DevComponents.AdvTree.ColumnHeader chExecute;
        private DevComponents.AdvTree.ColumnHeader chUsage;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.AdvTree.Node node5;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.Node node3;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.AdvTree.Cell cell1;
        private DevComponents.AdvTree.Cell cell2;
        private DevComponents.AdvTree.Cell cell3;
        private DevComponents.AdvTree.Cell cell4;
        private DevComponents.AdvTree.Cell cell5;
        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.AdvTree.Cell cSDTOwner;
        private DevComponents.AdvTree.Cell cSDTAll;
        private DevComponents.AdvTree.Cell cSDTCrud;
        private DevComponents.AdvTree.Cell cell9;
        private DevComponents.AdvTree.Cell cell10;
        private DevComponents.AdvTree.Cell cUDTOwner;
        private DevComponents.AdvTree.Cell cUDTAll;
        private DevComponents.AdvTree.Cell cUDTCrud;
        private DevComponents.AdvTree.Cell cell14;
        private DevComponents.AdvTree.Cell cell15;
        private DevComponents.AdvTree.Cell cSeqOwner;
        private DevComponents.AdvTree.Cell cell17;
        private DevComponents.AdvTree.Cell cell18;
        private DevComponents.AdvTree.Cell cell19;
        private DevComponents.AdvTree.Cell cSeqUsage;
        private DevComponents.AdvTree.Cell cFunOwner;
        private DevComponents.AdvTree.Cell cell22;
        private DevComponents.AdvTree.Cell cell23;
        private DevComponents.AdvTree.Cell cFunExecute;
        private DevComponents.AdvTree.Cell cell25;
        private DevComponents.DotNetBar.ElementStyle DefaultNodeStyle;
        private DevComponents.DotNetBar.ElementStyle EditableStyle;
        private DevComponents.DotNetBar.ElementStyle EditableStyleSelected;
        private DevComponents.AdvTree.Node node6;
        private DevComponents.AdvTree.Cell cell6;
        private DevComponents.AdvTree.Cell cell7;
        private DevComponents.AdvTree.Cell cell8;
        private DevComponents.AdvTree.Cell cell11;
        private DevComponents.AdvTree.Cell cell12;
        private DevComponents.AdvTree.Node node7;
        private DevComponents.AdvTree.Cell cell13;
        private DevComponents.AdvTree.Cell cell16;
        private DevComponents.AdvTree.Cell cell20;
        private DevComponents.AdvTree.Cell cell21;
        private DevComponents.AdvTree.Cell cell24;
        private DevComponents.AdvTree.Cell cell26;
        private DevComponents.AdvTree.Cell cell27;
        private DevComponents.AdvTree.Cell cell28;
    }
}