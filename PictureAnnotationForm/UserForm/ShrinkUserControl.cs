using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Forms;
using PictureAnnotationForm.Servers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotationForm.UserForm
{
    public partial class ShrinkUserControl : UserControl
    {
        public bool IsShrink;
        public string Name;
        public AutoEditServer AutoEditServer;
        public ShrinkUserControl(bool isShrink = false)
        {
            InitializeComponent();
            IsShrink = isShrink;
        }
        public static ShrinkUserControl GetShrinkUserControl<T>(T obj, Control control) where T : class, new()
        {
            ShrinkUserControl shrinkUserControl = new ShrinkUserControl();
            GetShrinkUserControl(shrinkUserControl, obj, control);
            return shrinkUserControl;
        }
        public static ShrinkUserControl GetShrinkUserControl<T>(ShrinkUserControl shrinkUserControl, T obj, Control control) where T : class, new()
        {
            Type type = typeof(T);
            var prefix = obj == null ? "添加" : "修改";
            AutoEditServer autoEditServer = new AutoEditServer();
            shrinkUserControl.AutoEditServer = autoEditServer;
            autoEditServer.Init(obj, type, shrinkUserControl.MainPanel);
            Propertys propertys = autoEditServer.Propertys;
            if (propertys != null)
            {
                shrinkUserControl.Name = propertys.Text;
            }
            else
            {
                shrinkUserControl.Name = prefix + type.Name;
            }
            autoEditServer.SetControl();
            shrinkUserControl.Init();
            return shrinkUserControl;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IsShrink = !IsShrink;
            Init();
        }

        private void ShrinkUserControl_MouseLeave(object sender, EventArgs e)
        {

        }

        private void ShrinkUserControl_VisibleChanged(object sender, EventArgs e)
        {

        }
        public void Init()
        {
            var prefix = IsShrink ? "▼" : "▶";
            button1.Text = $"{prefix}           {Name}";
            if (IsShrink)
            {
                Height = button1.Bottom;
            }
            else
            {
                Height = MainPanel.Bottom;
            }
        }

        private void ShrinkUserControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 3), this.ClientRectangle);
        }
    }
}
