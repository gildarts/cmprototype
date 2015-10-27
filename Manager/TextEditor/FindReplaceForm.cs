using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ActiproSoftware.Products.SyntaxEditor;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.WinUICore;

namespace Manager.TextEditor
{

    /// <summary>
    /// Represents a built-in Find and Replace form.
    /// </summary>
    public class FindReplaceForm : System.Windows.Forms.Form
    {

        private FindReplaceOptions options;
        private SyntaxEditor syntaxEditor;

        //

        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Label findWhatLabel;
        private System.Windows.Forms.CheckBox searchTypeCheckBox;
        private System.Windows.Forms.ComboBox searchTypeDropDownList;
        private System.Windows.Forms.CheckBox matchCaseCheckBox;
        private System.Windows.Forms.CheckBox matchWholeWordCheckBox;
        private System.Windows.Forms.CheckBox searchUpCheckBox;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button replaceAllButton;
        private System.Windows.Forms.Button markAllButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label replaceWithLabel;
        private System.Windows.Forms.TextBox replaceTextBox;
        private System.Windows.Forms.CheckBox markWithCheckBox;
        private System.Windows.Forms.CheckBox searchInSelectionCheckBox;
        private System.Windows.Forms.CheckBox searchHiddenTextCheckBox;
        private System.Windows.Forms.Button findInsertButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // EVENTS
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Occurs when there is a status change.
        /// </summary>
        /// <eventdata>
        /// The event handler receives an argument of type <c>FindReplaceStatusChangeEventArgs</c> containing data related to this event.
        /// </eventdata>
        [
            Category("Action"),
            Description("Occurs when there is a status change.")
        ]
        public event FindReplaceStatusChangeEventHandler StatusChanged;

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBJECT
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Initializes a new instance of the <c>FindReplaceForm</c> class.
        /// </summary>
        /// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> for which to display the form.</param>
        /// <param name="options">The <see cref="FindReplaceOptions"/> to use.</param>
        public FindReplaceForm(SyntaxEditor syntaxEditor, FindReplaceOptions options)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Ensure there are options
            if (options == null)
                throw new ArgumentNullException("options");

            // Initalize parameters
            this.syntaxEditor = syntaxEditor;
            this.options = options;

            // Load text from resources
            //this.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Text");
            //closeButton.Text = AssemblyInfo.Instance.Resources.GetString("General_CloseButton_Text");
            //findButton.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_FindButton_Text");
            //findWhatLabel.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_FindWhatLabel_Text");
            //markAllButton.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_MarkAllButton_Text");
            //markWithCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_MarkWithCheckBox_Text");
            //matchCaseCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_MatchCaseCheckBox_Text");
            //matchWholeWordCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_MatchWholeWordCheckBox_Text");
            //replaceButton.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_ReplaceButton_Text");
            //replaceAllButton.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_ReplaceAllButton_Text");
            //replaceWithLabel.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_ReplaceWithLabel_Text");
            //searchHiddenTextCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchHiddenTextCheckBox_Text");
            //searchInSelectionCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchInSelectionCheckBox_Text");
            //searchTypeCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchTypeCheckBox_Text");
            //this.searchTypeDropDownList.Items.AddRange(new object[] {
            //    AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchTypeDropDownList_Item_RegularExpressions"),
            //    AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchTypeDropDownList_Item_Wildcards")
            //    });
            //searchUpCheckBox.Text = AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_SearchUpCheckBox_Text");

            this.searchTypeDropDownList.Items.AddRange(new object[] { "規則運算式", "萬用字元" });

            // Select the first search type
            searchTypeDropDownList.SelectedIndex = 0;

            // Update options
            this.UpdateUIFromFindReplaceOptions();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.findWhatLabel = new System.Windows.Forms.Label();
            this.searchTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.searchTypeDropDownList = new System.Windows.Forms.ComboBox();
            this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.matchWholeWordCheckBox = new System.Windows.Forms.CheckBox();
            this.searchUpCheckBox = new System.Windows.Forms.CheckBox();
            this.replaceButton = new System.Windows.Forms.Button();
            this.replaceAllButton = new System.Windows.Forms.Button();
            this.markAllButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.replaceWithLabel = new System.Windows.Forms.Label();
            this.replaceTextBox = new System.Windows.Forms.TextBox();
            this.markWithCheckBox = new System.Windows.Forms.CheckBox();
            this.searchInSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.searchHiddenTextCheckBox = new System.Windows.Forms.CheckBox();
            this.findInsertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // findTextBox
            // 
            this.findTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findTextBox.Location = new System.Drawing.Point(101, 9);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(236, 22);
            this.findTextBox.TabIndex = 1;
            this.findTextBox.TextChanged += new System.EventHandler(this.findTextBox_TextChanged);
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.Enabled = false;
            this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.findButton.Location = new System.Drawing.Point(365, 9);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(102, 27);
            this.findButton.TabIndex = 13;
            this.findButton.Text = "尋找下一個(&F)";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // findWhatLabel
            // 
            this.findWhatLabel.AutoSize = true;
            this.findWhatLabel.Location = new System.Drawing.Point(8, 12);
            this.findWhatLabel.Name = "findWhatLabel";
            this.findWhatLabel.Size = new System.Drawing.Size(65, 12);
            this.findWhatLabel.TabIndex = 0;
            this.findWhatLabel.Text = "尋找目標：";
            // 
            // searchTypeCheckBox
            // 
            this.searchTypeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchTypeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchTypeCheckBox.Location = new System.Drawing.Point(11, 140);
            this.searchTypeCheckBox.Name = "searchTypeCheckBox";
            this.searchTypeCheckBox.Size = new System.Drawing.Size(58, 28);
            this.searchTypeCheckBox.TabIndex = 8;
            this.searchTypeCheckBox.Text = "使用";
            this.searchTypeCheckBox.CheckedChanged += new System.EventHandler(this.searchTypeCheckBox_CheckedChanged);
            // 
            // searchTypeDropDownList
            // 
            this.searchTypeDropDownList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchTypeDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchTypeDropDownList.Enabled = false;
            this.searchTypeDropDownList.Location = new System.Drawing.Point(58, 142);
            this.searchTypeDropDownList.Name = "searchTypeDropDownList";
            this.searchTypeDropDownList.Size = new System.Drawing.Size(267, 20);
            this.searchTypeDropDownList.TabIndex = 9;
            // 
            // matchCaseCheckBox
            // 
            this.matchCaseCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.matchCaseCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.matchCaseCheckBox.Location = new System.Drawing.Point(11, 64);
            this.matchCaseCheckBox.Name = "matchCaseCheckBox";
            this.matchCaseCheckBox.Size = new System.Drawing.Size(186, 28);
            this.matchCaseCheckBox.TabIndex = 5;
            this.matchCaseCheckBox.Text = "大小寫須相符 (&C)";
            // 
            // matchWholeWordCheckBox
            // 
            this.matchWholeWordCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.matchWholeWordCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.matchWholeWordCheckBox.Location = new System.Drawing.Point(11, 88);
            this.matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
            this.matchWholeWordCheckBox.Size = new System.Drawing.Size(186, 28);
            this.matchWholeWordCheckBox.TabIndex = 6;
            this.matchWholeWordCheckBox.Text = "全字拼寫須相符 (&W)";
            // 
            // searchUpCheckBox
            // 
            this.searchUpCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchUpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchUpCheckBox.Location = new System.Drawing.Point(11, 113);
            this.searchUpCheckBox.Name = "searchUpCheckBox";
            this.searchUpCheckBox.Size = new System.Drawing.Size(186, 27);
            this.searchUpCheckBox.TabIndex = 7;
            this.searchUpCheckBox.Text = "向上搜尋 (&U)";
            // 
            // replaceButton
            // 
            this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceButton.Enabled = false;
            this.replaceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.replaceButton.Location = new System.Drawing.Point(365, 39);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(102, 27);
            this.replaceButton.TabIndex = 14;
            this.replaceButton.Text = "取代 (&R)";
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceAllButton.Enabled = false;
            this.replaceAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.replaceAllButton.Location = new System.Drawing.Point(365, 70);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(102, 27);
            this.replaceAllButton.TabIndex = 15;
            this.replaceAllButton.Text = "取代全部 (&A)";
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // markAllButton
            // 
            this.markAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.markAllButton.Enabled = false;
            this.markAllButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.markAllButton.Location = new System.Drawing.Point(365, 102);
            this.markAllButton.Name = "markAllButton";
            this.markAllButton.Size = new System.Drawing.Size(102, 26);
            this.markAllButton.TabIndex = 16;
            this.markAllButton.Text = "標記全部 (&M)";
            this.markAllButton.Click += new System.EventHandler(this.markAllButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closeButton.Location = new System.Drawing.Point(365, 133);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(102, 26);
            this.closeButton.TabIndex = 17;
            this.closeButton.Text = "關閉 (&X)";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // replaceWithLabel
            // 
            this.replaceWithLabel.AutoSize = true;
            this.replaceWithLabel.Location = new System.Drawing.Point(8, 42);
            this.replaceWithLabel.Name = "replaceWithLabel";
            this.replaceWithLabel.Size = new System.Drawing.Size(53, 12);
            this.replaceWithLabel.TabIndex = 3;
            this.replaceWithLabel.Text = "取代成：";
            // 
            // replaceTextBox
            // 
            this.replaceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceTextBox.Location = new System.Drawing.Point(101, 38);
            this.replaceTextBox.Name = "replaceTextBox";
            this.replaceTextBox.Size = new System.Drawing.Size(258, 22);
            this.replaceTextBox.TabIndex = 4;
            // 
            // markWithCheckBox
            // 
            this.markWithCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.markWithCheckBox.Checked = true;
            this.markWithCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.markWithCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.markWithCheckBox.Location = new System.Drawing.Point(203, 113);
            this.markWithCheckBox.Name = "markWithCheckBox";
            this.markWithCheckBox.Size = new System.Drawing.Size(156, 27);
            this.markWithCheckBox.TabIndex = 12;
            this.markWithCheckBox.Text = "標記書簽";
            this.markWithCheckBox.Visible = false;
            // 
            // searchInSelectionCheckBox
            // 
            this.searchInSelectionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchInSelectionCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchInSelectionCheckBox.Location = new System.Drawing.Point(203, 88);
            this.searchInSelectionCheckBox.Name = "searchInSelectionCheckBox";
            this.searchInSelectionCheckBox.Size = new System.Drawing.Size(156, 28);
            this.searchInSelectionCheckBox.TabIndex = 11;
            this.searchInSelectionCheckBox.Text = "只搜尋選擇範圍";
            // 
            // searchHiddenTextCheckBox
            // 
            this.searchHiddenTextCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchHiddenTextCheckBox.Checked = true;
            this.searchHiddenTextCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchHiddenTextCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchHiddenTextCheckBox.Location = new System.Drawing.Point(203, 64);
            this.searchHiddenTextCheckBox.Name = "searchHiddenTextCheckBox";
            this.searchHiddenTextCheckBox.Size = new System.Drawing.Size(156, 28);
            this.searchHiddenTextCheckBox.TabIndex = 10;
            this.searchHiddenTextCheckBox.Text = "尋找穩藏文字";
            // 
            // findInsertButton
            // 
            this.findInsertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findInsertButton.Enabled = false;
            this.findInsertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.findInsertButton.Location = new System.Drawing.Point(343, 8);
            this.findInsertButton.Name = "findInsertButton";
            this.findInsertButton.Size = new System.Drawing.Size(20, 25);
            this.findInsertButton.TabIndex = 2;
            this.findInsertButton.Text = ">";
            this.findInsertButton.Click += new System.EventHandler(this.findInsertButton_Click);
            // 
            // FindReplaceForm
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(475, 175);
            this.Controls.Add(this.findInsertButton);
            this.Controls.Add(this.searchHiddenTextCheckBox);
            this.Controls.Add(this.searchInSelectionCheckBox);
            this.Controls.Add(this.markWithCheckBox);
            this.Controls.Add(this.replaceWithLabel);
            this.Controls.Add(this.findWhatLabel);
            this.Controls.Add(this.replaceTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.markAllButton);
            this.Controls.Add(this.replaceAllButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.searchUpCheckBox);
            this.Controls.Add(this.matchWholeWordCheckBox);
            this.Controls.Add(this.matchCaseCheckBox);
            this.Controls.Add(this.searchTypeDropDownList);
            this.Controls.Add(this.searchTypeCheckBox);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.findTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindReplaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "尋找/取代";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // EVENT HANDLERS
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void closeButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void findButton_Click(object sender, System.EventArgs e)
        {
            // Update find/replace options
            this.UpdateFindReplaceOptionsFromUI();

            if (syntaxEditor == null)
                return;

            // Raise an event
            this.OnStatusChanged(new FindReplaceStatusChangeEventArgs(FindReplaceStatusChangeType.Find, options));

            // Perform a find operation
            FindReplaceResultSet resultSet;
            try
            {
                resultSet = syntaxEditor.SelectedView.FindReplace.Find(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Error_Message") + "\r\n" + ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the status
            if (resultSet.PastDocumentEnd)
            {
                // Raise an event
                this.OnStatusChanged(new FindReplaceStatusChangeEventArgs(FindReplaceStatusChangeType.PastDocumentEnd, options));
            }

            // Find if the search went past the starting point
            if (resultSet.PastSearchStartOffset)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_PastSearchStartOffset_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If no matches were found...			
            if (resultSet.Count == 0)
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_NotFound_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Occurs when the Text property is changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void findTextBox_TextChanged(object sender, System.EventArgs e)
        {
            findButton.Enabled = (findTextBox.Text.Length > 0);
            replaceButton.Enabled = findButton.Enabled;
            replaceAllButton.Enabled = (findTextBox.Text.Length > 0);
            markAllButton.Enabled = replaceAllButton.Enabled;
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void findInsertButton_Click(object sender, System.EventArgs e)
        {
            OwnerDrawContextMenu menu = new OwnerDrawContextMenu();
            if (searchTypeDropDownList.SelectedIndex == 0)
            {
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_SingleCharacter"), -1, new EventHandler(menuItem_Click), Shortcut.None, "."));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_ZeroOrMore"), -1, new EventHandler(menuItem_Click), Shortcut.None, "*"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_OneOrMore"), -1, new EventHandler(menuItem_Click), Shortcut.None, "+"));
                menu.MenuItems.Add(new OwnerDrawMenuItem("-"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_BeginningOfLine"), -1, new EventHandler(menuItem_Click), Shortcut.None, "^"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_EndOfLine"), -1, new EventHandler(menuItem_Click), Shortcut.None, "$"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_WordBoundary"), -1, new EventHandler(menuItem_Click), Shortcut.None, "\\b"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_Whitespace"), -1, new EventHandler(menuItem_Click), Shortcut.None, "\\s"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_LineBreak"), -1, new EventHandler(menuItem_Click), Shortcut.None, "\\n"));
                menu.MenuItems.Add(new OwnerDrawMenuItem("-"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_OneCharacterInSet"), -1, new EventHandler(menuItem_Click), Shortcut.None, "[ ]"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_OneCharacterNotInSet"), -1, new EventHandler(menuItem_Click), Shortcut.None, "[^ ]"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_Or"), -1, new EventHandler(menuItem_Click), Shortcut.None, "|"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_CharacterEscape"), -1, new EventHandler(menuItem_Click), Shortcut.None, "\\"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_RegularExpression_Group"), -1, new EventHandler(menuItem_Click), Shortcut.None, "( )"));
            }
            else
            {
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Wildcard_ZeroOrMore"), -1, new EventHandler(menuItem_Click), Shortcut.None, "*"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Wildcard_AnySingleCharacter"), -1, new EventHandler(menuItem_Click), Shortcut.None, "?"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Wildcard_AnySingleDigit"), -1, new EventHandler(menuItem_Click), Shortcut.None, "#"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Wildcard_OneCharacterInSet"), -1, new EventHandler(menuItem_Click), Shortcut.None, "[ ]"));
                menu.MenuItems.Add(new OwnerDrawMenuItem(AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Wildcard_OneCharacterNotInSet"), -1, new EventHandler(menuItem_Click), Shortcut.None, "[! ]"));
            }
            menu.Show(findInsertButton, new Point(findInsertButton.ClientRectangle.Right, findInsertButton.ClientRectangle.Top));
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void markAllButton_Click(object sender, System.EventArgs e)
        {
            // Update find/replace options
            this.UpdateFindReplaceOptionsFromUI();

            if (syntaxEditor == null)
                return;

            // Perform a mark all operation
            FindReplaceResultSet resultSet;
            try
            {
                if (markWithCheckBox.Checked)
                    resultSet = syntaxEditor.SelectedView.FindReplace.MarkAll(options);
                else
                    resultSet = syntaxEditor.SelectedView.FindReplace.MarkAll(options, typeof(GrammarErrorSpanIndicator));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Error_Message") + "\r\n" + ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If no matches were found
            if (resultSet.Count == 0)
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_NotFound_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Occurs when the menu item is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void menuItem_Click(object sender, System.EventArgs e)
        {
            findTextBox.SelectedText = ((OwnerDrawMenuItem)sender).Tag.ToString();
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void replaceButton_Click(object sender, System.EventArgs e)
        {
            // Update find/replace options
            this.UpdateFindReplaceOptionsFromUI();

            if (syntaxEditor == null)
                return;

            // Raise an event
            this.OnStatusChanged(new FindReplaceStatusChangeEventArgs(FindReplaceStatusChangeType.Replace, options));

            // Perform a find operation
            FindReplaceResultSet resultSet;
            try
            {
                resultSet = syntaxEditor.SelectedView.FindReplace.Replace(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Error_Message") + "\r\n" + ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the status
            if (resultSet.PastDocumentEnd)
            {
                // Raise an event
                this.OnStatusChanged(new FindReplaceStatusChangeEventArgs(FindReplaceStatusChangeType.PastDocumentEnd, options));
            }

            // If no matches were found...			
            if (resultSet.Count == 0)
            {
                if ((resultSet.PastDocumentEnd) && (resultSet.ReplaceOccurred))
                    MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_PastSearchStartOffset_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_NotFound_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void replaceAllButton_Click(object sender, System.EventArgs e)
        {
            // Update find/replace options
            this.UpdateFindReplaceOptionsFromUI();

            if (syntaxEditor == null)
                return;

            // Perform a mark all operation
            FindReplaceResultSet resultSet;
            try
            {
                resultSet = syntaxEditor.SelectedView.FindReplace.ReplaceAll(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_Error_Message") + "\r\n" + ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If no matches were found...
            if (resultSet.Count == 0)
            {
                MessageBox.Show(this, AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_NotFound_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Display the number of replacements
            MessageBox.Show(this, resultSet.Count + " " + AssemblyInfo.Instance.Resources.GetString("FindReplaceForm_OccurrencesReplaced_Message"), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Occurs when the Checked property is changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void searchTypeCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            findInsertButton.Enabled = searchTypeCheckBox.Checked;
            searchTypeDropDownList.Enabled = searchTypeCheckBox.Checked;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // NON-PUBLIC PROCEDURES
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Updates the find/replace options from the UI.
        /// </summary>
        private void UpdateFindReplaceOptionsFromUI()
        {
            options.FindText = findTextBox.Text;
            options.ReplaceText = replaceTextBox.Text;
            options.MatchCase = matchCaseCheckBox.Checked;
            options.MatchWholeWord = matchWholeWordCheckBox.Checked;
            options.SearchHiddenText = searchHiddenTextCheckBox.Checked;
            options.SearchInSelection = searchInSelectionCheckBox.Checked;
            options.SearchUp = searchUpCheckBox.Checked;
            options.SearchType = (!searchTypeCheckBox.Checked ? FindReplaceSearchType.Normal :
                                            (searchTypeDropDownList.SelectedIndex == 0 ? FindReplaceSearchType.RegularExpression : FindReplaceSearchType.Wildcard));
        }

        /// <summary>
        /// Updates the UI from the find/replace options.
        /// </summary>
        private void UpdateUIFromFindReplaceOptions()
        {
            findTextBox.Text = options.FindText;
            replaceTextBox.Text = options.ReplaceText;
            matchCaseCheckBox.Checked = options.MatchCase;
            matchWholeWordCheckBox.Checked = options.MatchWholeWord;
            searchUpCheckBox.Checked = options.SearchUp;
            searchHiddenTextCheckBox.Checked = options.SearchHiddenText;
            searchInSelectionCheckBox.Checked = options.SearchInSelection;
            searchTypeCheckBox.Checked = (options.SearchType != FindReplaceSearchType.Normal);
            searchTypeDropDownList.SelectedIndex = (options.SearchType != FindReplaceSearchType.Wildcard ? 0 : 1);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // PUBLIC PROCEDURES
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Occurs before the form is activated.
        /// </summary>
        /// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            // Call the base method
            base.OnActivated(e);

            // Select the find text
            findTextBox.SelectAll();
            findTextBox.Focus();
        }

        /// <summary>
        /// Occurs before the form is closed.
        /// </summary>
        /// <param name="e">A <c>CancelEventArgs</c> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            // Raise an event
            this.OnStatusChanged(new FindReplaceStatusChangeEventArgs(FindReplaceStatusChangeType.Ready, options));

            // Call the base method
            base.OnClosing(e);

            // Cancel the close and hide the form instead (for reuse)
            e.Cancel = true;
            if (this.Owner != null)
                this.Owner.Activate();
            this.Hide();
        }

        /// <summary>
        /// Raises the <c>StatusChanged</c> event.
        /// </summary>
        /// <param name="e">A <c>FindReplaceStatusChangeEventArgs</c> that contains the event data.</param>
        protected virtual void OnStatusChanged(FindReplaceStatusChangeEventArgs e)
        {
            if (this.StatusChanged != null)
                this.StatusChanged(this, e);
        }

        /// <summary>
        /// Gets or sets the <see cref="FindReplaceOptions"/> to use.
        /// </summary>
        /// <value>The <see cref="FindReplaceOptions"/> to use.</value>
        public FindReplaceOptions Options
        {
            get
            {
                return options;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Options");

                options = value;

                this.UpdateUIFromFindReplaceOptions();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ActiproSoftware.SyntaxEditor.SyntaxEditor"/> that is currently the focus of the find/replace form.
        /// </summary>
        /// <value>The <see cref="ActiproSoftware.SyntaxEditor.SyntaxEditor"/> that is currently the focus of the find/replace form.</value>
        public SyntaxEditor SyntaxEditor
        {
            get
            {
                return syntaxEditor;
            }
            set
            {
                syntaxEditor = value;
            }
        }

    }
}
