using Newtonsoft.Json;
using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm
{
    [Propertys("系统设置")]
    public class SystemSetting
    {
        /// <summary>
        /// 数据集路径
        /// </summary>
        [Propertys("数据集路径", type: EPropertysType.Folder)]
        public string DataSetDir { set; get; }
        /// <summary>
        /// 自动备份间隔
        /// </summary>
        [Propertys("自动备份间隔")]
        public int AutoBackInterval { set; get; }
        /// <summary>
        /// 上次打开的数据集
        /// </summary>
        public string LastDataSet { set; get; }
        /// <summary>
        /// 历史数据集
        /// </summary>
        public List<string> HistoryDataSet { set; get; }
        public static SystemSetting Default { private set; get; }
        private static string DefaultDirPath = "Run";
        private static string DefaultIniPath = "Config.ini";
        static SystemSetting()
        {
            var path = $"{DefaultDirPath}/{DefaultIniPath}";
            if (File.Exists(path))
            {
                Default = JsonConvert.DeserializeObject<SystemSetting>(File.ReadAllText(path));
            }
            else
            {
                Default = new SystemSetting
                {
                    DataSetDir = "Data",
                    AutoBackInterval = 30
                };
                Directory.CreateDirectory(DefaultDirPath);
                Directory.CreateDirectory(Default.DataSetDir);
                SaveDefault();
            }
        }
        /// <summary>
        /// 保存系统配置
        /// </summary>
        public static void SaveDefault()
        {
            var path = $"{DefaultDirPath}/{DefaultIniPath}";
            File.WriteAllText(path, JsonConvert.SerializeObject(Default));
        }
    }
}
