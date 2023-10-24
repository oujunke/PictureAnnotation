using Newtonsoft.Json.Linq;
using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using PictureAnnotationForm.Models;
using PictureAnnotationForm.Models.Servers;
using PictureAnnotationForm.Servers;
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
            AutoEditServer autoEditServer = new AutoEditServer();
            autoEditServer.Init(obj,type, autoEditForm.panel2);
            Propertys propertys = autoEditServer.Propertys;
            if (propertys != null)
            {
                autoEditForm.Text = propertys.Text;
            }
            else
            {
                autoEditForm.Text = prefix + type.Name;
            }
            autoEditServer.SetControl();
            if (autoEditForm.ShowDialog() == DialogResult.OK)
            {
                autoEditServer.UpdateData(()=> new T());
            }
            return autoEditServer.Data as T;
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
