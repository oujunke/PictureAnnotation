using PictureAnnotationForm.BLL;
using PictureAnnotationForm.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas
{
    public abstract class DataBase
    {
        public virtual string GetNewPath(string name,string dir=null)
        {
            return Path.Combine(SystemSetting.Default.DataSetDir, ImageManagers.CurrentImageData.DatasetProperties.Name, DataSets.ImgDir, name);
        }
        public virtual string HandleImgPath(string dir, string name)
        {
            var path = Path.Combine(dir, name);
            if (ImageManagers.CurrentImageData.DatasetProperties.FileLinkType == Enums.EFileLinkType.路径)
            {
                return path;
            }
            else
            {
                var newPath = GetNewPath(name, dir);
                var absPath = Path.GetFullPath(newPath);
                switch (ImageManagers.CurrentImageData.DatasetProperties.FileLinkType)
                {
                    case Enums.EFileLinkType.软链接:
                        File.CreateSymbolicLink(absPath, path);
                        break;
                    case Enums.EFileLinkType.硬链接:
                        var result = WinApiUtils.CreateHardLink(absPath, path, nint.Zero);
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
