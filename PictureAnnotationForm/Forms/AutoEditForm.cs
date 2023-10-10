using Newtonsoft.Json.Linq;
using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace PictureAnnotationForm.Forms
{
    public partial class AutoEditForm : Form
    {
        
        private int panel1Height;
        private int laveHeight;
        public AutoEditForm()
        {
            InitializeComponent();
            panel1Height = panel1.Height;
            laveHeight = Height - panel2.Height;
        }

        public static T ShowEdit<T>(T obj) where T : class, new()
        {
            Type type = typeof(T);
            AutoEditForm autoEditForm = new AutoEditForm();
            var prefix = obj == null ? "添加" : "修改";
            Propertys propertys = GetPropertys(type);
            if (propertys != null)
            {
                autoEditForm.Text = propertys.Text;
            }
            else
            {
                autoEditForm.Text = prefix + type.Name;
            }
            var memberInfoPropertysBind = InfoPropertysBind(obj, null);
            if (memberInfoPropertysBind.Child.Count == 0)
            {
                Debugger.Break();
            }
            SetControl(memberInfoPropertysBind, autoEditForm.panel2, autoEditForm);
            autoEditForm.memberInfoPropertys = memberInfoPropertysBind;
            if (autoEditForm.ShowDialog() == DialogResult.OK)
            {
                if (obj == null)
                {
                    obj = new T();
                }
                UpdateData(obj, memberInfoPropertysBind);
            }
            return obj;
        }
        private MemberInfoPropertysBind memberInfoPropertys;
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AutoEditForm_Load(object sender, EventArgs e)
        {

        }

        private void AutoEditForm_Shown(object sender, EventArgs e)
        {
            Height = laveHeight + panel2.Height;
            panel1.Height = panel1Height;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
