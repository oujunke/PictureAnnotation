using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Utils
{
    public class WinApiUtils
    {
        [DllImport("Kernel32", CharSet = CharSet.Unicode)]
        public static extern int CreateHardLink(string path, string pathToTarget, nint zero);
    }
}
