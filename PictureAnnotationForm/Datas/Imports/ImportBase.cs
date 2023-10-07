using PictureAnnotationForm.BLL;
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
    public abstract class ImportBase
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
        [DllImport("Kernel32", CharSet = CharSet.Unicode)]
        public static extern int CreateHardLink(string path, string pathToTarget, nint zero);
        public virtual string HandleImgPath(string dir,string name)
        {
            var path = Path.Combine(dir, name);
            if (ImageManagers.CurrentImageData.DatasetProperties.FileLinkType== Enums.EFileLinkType.路径)
            {
                return path;
            }
            else
            {
                var newPath = Path.Combine(SystemSetting.Default.DataSetDir, ImageManagers.CurrentImageData.DatasetProperties.Name,DataSets.ImgDir,name);
                var absPath=Path.GetFullPath(newPath);
                switch (ImageManagers.CurrentImageData.DatasetProperties.FileLinkType)
                {
                    case Enums.EFileLinkType.软链接:
                        File.CreateSymbolicLink(absPath,path);
                        break;
                    case Enums.EFileLinkType.硬链接:
                        var result=CreateHardLink(absPath,path, nint.Zero);
                        break;
                    case Enums.EFileLinkType.复制:
                        File.Copy(path, newPath);
                        break;
                }
                return name;
            }
        }
    }
}
