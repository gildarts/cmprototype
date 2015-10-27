using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Xml;
using System.Windows.Forms;

namespace Manager.TextEditor
{
    public class BaseSyntaxEditor : SyntaxEditor
    {
        private FindReplaceForm _find_replace_form;

        public BaseSyntaxEditor()
        {
            Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
        }

        protected override void OnKeyTyping(KeyTypingEventArgs e)
        {
            base.OnKeyTyping(e);

            if (e.Cancel) return;

            if (e.KeyData == (Keys.F1))
                new ShortcutHelp().Show();

            if (e.KeyData == (Keys.Control | Keys.F))
            {
                if (_find_replace_form == null)
                {
                    _find_replace_form = new FindReplaceForm(this, new FindReplaceOptions());
                    //_find_replace_form.StatusChanged += new FindReplaceStatusChangeEventHandler(findReplaceForm_StatusChanged);
                }

                if (TopLevelControl is Form)
                    _find_replace_form.Owner = TopLevelControl as Form;

                _find_replace_form.Show();
            }

            if (e.KeyData == (Keys.Shift | Keys.Control | Keys.F))
            {
                if (Document.Language.FormattingSupported)
                    Document.Language.FormatDocument(Document);
            }

            if (e.KeyData == (Keys.F5))
                new XPathEvaluatorForm(Text).ShowDialog();
        }
    }
}
