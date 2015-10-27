using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Manager
{
    public partial class ErrorForm : Office2007Form
    {
        private Exception _exp;
        private string _detail;

        public ErrorForm()
        {
            InitializeComponent();

            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            ErrorDetailForm detail = new ErrorDetailForm();

            if (_exp != null)
                detail.ShowDetail(_exp);
            else
                detail.ShowDetail(_detail);
        }

        public void Display(string message, Exception ex)
        {
            ResizeForm(message);

            lblMessage.Text = message;
            _exp = ex;

            ShowDialog();
        }

        public void Display(string message, string detail)
        {
            ResizeForm(message);

            lblMessage.Text = message;
            _detail = detail;

            ShowDialog();
        }

        private void ResizeForm(string msg)
        {
            Graphics gx = lblMessage.CreateGraphics();
            SizeF sf = gx.MeasureString(msg, lblMessage.Font);
            sf.Width = sf.Width + 25;
            sf.Height = sf.Height + 25;

            gx.Dispose();

            Size = Size.Add(sf.ToSize(), Size.Subtract(Size, lblMessage.Size));
        }

        public static void Show(string msg, Exception ex)
        {
            ErrorForm err = new ErrorForm();
            err.Display(msg, ex);
        }
    }
}