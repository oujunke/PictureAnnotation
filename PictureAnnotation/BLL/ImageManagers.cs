using PictureAnnotation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using PictureAnnotation.Utils;

namespace PictureAnnotation.BLL
{
    public class ImageManagers
    {
        private static List<ImageItemModel> _currentImageData = new List<ImageItemModel>();
        private static Dictionary<string, ImageItemModel> _kevImageData = new Dictionary<string, ImageItemModel>();
        public static List<ImageItemModel> GetImageList(int index=0,int size = 10)
        {
            return _currentImageData.Skip(index).Take(size).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static int LoadVocDirectory(string directoryPath)
        {
            var success = 0;
            if (!Directory.Exists(directoryPath))
            {
                LogUtils.Log($"加载Voc文件夹时出现:文件夹{directoryPath}不存在");
                return success;
            }
            var trainListPath = Path.Join(directoryPath, "train_list.txt");
            var valListPath = Path.Join(directoryPath, "val_list.txt");
            var fileData = new Dictionary<string, string>();
            if (File.Exists(trainListPath))
            {
                foreach (var imageXmlPath in File.ReadAllLines(trainListPath))
                {
                    var paths = imageXmlPath.Split(new [] {' ','\t' },StringSplitOptions.RemoveEmptyEntries);
                    if (paths.Length == 2)
                    {
                        var imagePath= Path.Join(directoryPath, paths[0]);
                        var xmlPath = Path.Join(directoryPath, paths[1]);
                        if(!File.Exists(imagePath)|| !File.Exists(xmlPath))
                        {
                            LogUtils.Log($"加载Voc文件夹时出现:图片文件{imagePath}或Xml文件{xmlPath}不存在");
                            continue;
                        }
                        var element=XElement.Load(xmlPath);
                        
                    }
                }
            }
            return success;
        }
        static ImageManagers()
        {
            var root = "image";
            if (File.Exists("data.ini"))
            {
                _kevImageData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("data.ini"));
            }
        }
    }
}
