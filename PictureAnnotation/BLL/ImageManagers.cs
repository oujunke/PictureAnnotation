using PictureAnnotation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using PictureAnnotation.Utils;
using System.Drawing;
using System.Runtime.Caching;

namespace PictureAnnotation.BLL
{
    public class ImageManagers
    {
        private static List<ImageItemModel> _currentImageData = new List<ImageItemModel>();
        private static Dictionary<string, ImageItemModel> _kevImageData = new Dictionary<string, ImageItemModel>();
        public static int ImageCount { get => _currentImageData.Count; }
        public static List<ImageItemModel> GetImageModelList(int index = 0, int size = 10)
        {
            return _currentImageData.Skip(index).Take(size).ToList();
        }
        public static List<Bitmap> GetImageList(int index = 0, int size = 10)
        {
            return GetImageModelList(index,size).Select(im=>GetImage(im)).ToList();
        }
        public static Bitmap GetImage(ImageItemModel itemModel)
        {
            var img=MemoryCache.Default.Get(itemModel.Id);
            Bitmap image = null;
            if (img == null)
            {
                image = Image.FromFile(itemModel.Path) as Bitmap;
                MemoryCache.Default.Add(itemModel.Id, image, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
            }
            else
            {
                image = img as Bitmap;
            }
            return image;
        }
        public static ImageItemModel GetImageItemModel(string key)
        {
            if (_kevImageData.ContainsKey(key))
            {
                var result = _kevImageData[key];
                result.Image = GetImage(result);
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 加载Voc数据集文件夹,返回加入数据条数
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
            if (File.Exists(trainListPath))
            {
                success += AddVocTxt(directoryPath, trainListPath);
            }
            if (File.Exists(valListPath))
            {
                success += AddVocTxt(directoryPath, valListPath);
            }
            return success;
        }

        private static int AddVocTxt(string directoryPath, string dataPath)
        {
            int success = 0;
            foreach (var imageXmlPath in File.ReadAllLines(dataPath))
            {
                var paths = imageXmlPath.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (paths.Length == 2)
                {
                    var imagePath = Path.Join(directoryPath, paths[0]);
                    var xmlPath = Path.Join(directoryPath, paths[1]);
                    if (!File.Exists(imagePath) || !File.Exists(xmlPath))
                    {
                        LogUtils.Log($"加载Voc文件夹时出现:图片文件{imagePath}或Xml文件{xmlPath}不存在");
                        continue;
                    }
                    var element = XElement.Load(xmlPath);
                    ImageItemModel imageItemModel = new ImageItemModel();
                    var nameElementValue = element.Element("filename")?.Value;
                    if (!string.IsNullOrWhiteSpace(nameElementValue))
                    {
                        var num = nameElementValue.IndexOf('.');
                        if (num > 0)
                        {
                            imageItemModel.Id = nameElementValue.Substring(0, num);
                        }
                        else
                        {
                            imageItemModel.Id = nameElementValue;
                        }
                    }
                    else
                    {
                        imageItemModel.Id = Guid.NewGuid().ToString("N");
                    }
                    imageItemModel.Path = imagePath;
                    var sizeElement = element.Element("size");
                    if (sizeElement != null)
                    {
                        imageItemModel.Width = sizeElement.Element("width")?.Value.StringToInt() ?? 0;
                        imageItemModel.Height = sizeElement.Element("height")?.Value.StringToInt() ?? 0;
                    }
                    var objectElements = element.Elements("object");
                    foreach (var objectElement in objectElements)
                    {
                        ImageLabelsModel imageLabels = new ImageLabelsModel();
                        imageLabels.Name = objectElement.Element("name").Value;
                        var bndboxElement = objectElement.Element("bndbox");
                        imageLabels.X1 = bndboxElement.Element("xmin").Value.StringToInt();
                        imageLabels.X2 = bndboxElement.Element("xmax").Value.StringToInt();
                        imageLabels.Y1 = bndboxElement.Element("ymin").Value.StringToInt();
                        imageLabels.Y2 = bndboxElement.Element("ymax").Value.StringToInt();
                        imageLabels.ImageItemModel = imageItemModel;
                        imageItemModel.Labels.Add(imageLabels);
                    }
                    imageItemModel.IsComplete = imageItemModel.Labels.Count > 0;
                    if (_kevImageData.ContainsKey(imageItemModel.Id))
                    {
                        LogUtils.Log($"编号为{imageItemModel.Id}在列表中已拥有");
                    }
                    else
                    {
                        _kevImageData.Add(imageItemModel.Id, imageItemModel);
                        _currentImageData.Add(imageItemModel);
                        success++;
                    }
                }
            }

            return success;
        }
        /// <summary>
        /// 导出EasyData数据集
        /// </summary>
        /// <param name="saveDirectoryPath"></param>
        public static void ExportEasyData(string saveDirectoryPath)
        {
            foreach (var item in _currentImageData.Where(cid=>cid.IsComplete))
            {
                var imagePath = Path.Join(saveDirectoryPath,$"{item.Id}.jpg");
                File.Copy(item.Path,imagePath);
                Dictionary<string, List<ImageLabelsModel>> jsonDictionary = new Dictionary<string, List<ImageLabelsModel>>();
                jsonDictionary.Add("labels", item.Labels);
                File.WriteAllText(imagePath.Replace(".jpg",".json"),JsonConvert.SerializeObject(jsonDictionary));
            }
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
