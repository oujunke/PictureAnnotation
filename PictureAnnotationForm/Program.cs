using PictureAnnotationForm.Forms;
using PictureAnnotationForm.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PictureAnnotationForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AutoEditServer editServer = new AutoEditServer(null);
            object obj = null;
            editServer.Init(ref obj);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
