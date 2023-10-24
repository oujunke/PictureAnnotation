using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Servers;
using PictureAnnotationForm.UserForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotationForm.Forms
{
    public partial class ShrinkForm : Form
    {
        public AutoEditServer AutoEditServer;
        public ShrinkForm()
        {
            InitializeComponent();
        }
        public static T ShowEdit<T>(T obj, int x, int y) where T : class, new()
        {
            Type type = typeof(T);
            if (obj == null)
            {
                obj = new T();
            }
            AutoEditServer autoEditServer = GetAutoEditServer(obj, type, x, y);
            return autoEditServer.Data as T;
        }
        public static object ShowEditObj(object obj,Type type, int x, int y)
        {
            if (obj == null)
            {
                obj = Activator.CreateInstance(type);
            }
            AutoEditServer autoEditServer = GetAutoEditServer(obj, type, x, y);
            return autoEditServer.Data;
        }

        private static AutoEditServer GetAutoEditServer(object obj, Type type, int x, int y)
        {
            ShrinkForm shrinkForm = new ShrinkForm();
            AutoEditServer autoEditServer = new AutoEditServer();
            shrinkForm.AutoEditServer = autoEditServer;
            autoEditServer.Init(obj, type, shrinkForm);
            autoEditServer.SetControl();
            shrinkForm.Left = x;
            shrinkForm.Top = y;
            shrinkForm.ShowDialog();
            return autoEditServer;
        }

        private void ShrinkForm_MouseLeave(object sender, EventArgs e)
        {
            AutoEditServer.UpdateData();
            Close();
        }
    }
}
