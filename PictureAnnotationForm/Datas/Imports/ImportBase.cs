using PictureAnnotationForm.BLL;
using PictureAnnotationForm.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas.Imports
{
    public abstract class ImportBase: DataBase
    {
        public string Dir { set; get; }
        public string LabelName { set; get; }
        public abstract int Import();
        public int ImportDir(string dir)
        {
            Dir = dir;
            return Import();
        }
        public int ImportLabelName(string labelName)
        {
            LabelName = labelName;
            return Import();
        }
        
        
    }
}
