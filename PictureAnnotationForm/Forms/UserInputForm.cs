using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureAnnotationForm.Forms
{
    public partial class UserInputForm : Form
    {
        public UserInputForm()
        {
            InitializeComponent();
        }
        private string _body;
        /// <summary>
        /// 获得输入框输入
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetInputText(string title)
        {
            UserInputForm userInputForm = new UserInputForm();
            userInputForm.Text = title;
            if (userInputForm.ShowDialog() == DialogResult.Yes)
            {
                return userInputForm._body;
            }
            return null;
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            _body = tbBody.Text;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
