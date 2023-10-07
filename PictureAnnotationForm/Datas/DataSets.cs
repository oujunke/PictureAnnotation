using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas
{
    [Propertys("程序集")]
    public class DataSets
    {
        [Propertys("属性")]
        public DatasetProperties DatasetProperties { set; get; }
        public List<ImageItemModel> Images { set; get; }
        public const string BackDir = "Back";
        public const string ImgDir = "Img";
        public static DataSets Create(string name, EDatasetPropertiesType type, EFileLinkType fileLinkType, string dec = null)
        {
            DataSets dataSets = new DataSets();
            dataSets.DatasetProperties = new DatasetProperties
            {
                Name = name,
                Dec = dec,
                Type = type,
                FileLinkType = fileLinkType
            };
            Init(dataSets);
            return dataSets;
        }

        public static void Init(DataSets dataSets)
        {
            dataSets.Images = new List<ImageItemModel>();
            Directory.CreateDirectory(Path.Combine(SystemSetting.Default.DataSetDir, dataSets.DatasetProperties.Name));
            Directory.CreateDirectory(Path.Combine(SystemSetting.Default.DataSetDir, dataSets.DatasetProperties.Name, BackDir));
            Directory.CreateDirectory(Path.Combine(SystemSetting.Default.DataSetDir, dataSets.DatasetProperties.Name, ImgDir));
        }
    }
}
