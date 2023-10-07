﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Drawing;
using System.Runtime.Caching;
using PictureAnnotationForm.Utils;
using PictureAnnotationForm.Models;
using System.Diagnostics;
using PictureAnnotationForm.Datas;
using PictureAnnotationForm.Datas.Imports;


namespace PictureAnnotationForm.BLL
{
    public class ImageManagers
    {
        public static DataSets CurrentImageData { set; get; }
        /// <summary>
        /// 当前图片标注数据
        /// </summary>
        public static List<ImageItemModel> CurrentImageDataList { get => CurrentImageData.Images; }
        /// <summary>
        /// 当前图片编号对应的图片数据
        /// </summary>
        public static Dictionary<string, ImageItemModel> KevImageData = new Dictionary<string, ImageItemModel>();
        /// <summary>
        /// 当前所有的Label
        /// </summary>
        public static Dictionary<string, string> LabelNameData = new Dictionary<string, string>();
        /// <summary>
        /// 标签Id对应的标签
        /// </summary>
        public static Dictionary<string, ImageLabelsModel> LabelKeyDictionary = new Dictionary<string, ImageLabelsModel>();
        /// <summary>
        /// 当前所有的Label
        /// </summary>
        public static List<string> LabelNames = new List<string>();
        public static int ImageCount { get => CurrentImageDataList.Count; }
        /// <summary>
        /// 获得图片对象列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<ImageItemModel> GetImageModelList(int index = 0, int size = 10)
        {
            return CurrentImageDataList.Skip(index).Take(size).ToList();
        }
        /// <summary>
        /// 获得图片列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<Bitmap> GetImageList(int index = 0, int size = 10)
        {
            return GetImageModelList(index, size).Select(im => GetImage(im)).ToList();
        }
        /// <summary>
        /// 更新标签名字
        /// </summary>
        /// <param name="itemModel"></param>
        /// <param name="oldName"></param>
        public static void UpdateLabelName(ImageLabelsModel itemModel)
        {
            if (!LabelNameData.ContainsKey(itemModel.Name))
            {
                LabelNameData.Add(itemModel.Name, null);
                LabelNames.Add(itemModel.Name);
            }
        }
        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="saveModel"></param>
        public static void LoadDataSets(string name)
        {
            var filePath=Path.Combine(SystemSetting.Default.DataSetDir, name, $"{name}.ds");
            if (File.Exists(filePath))
            {
                DataSets dataSets = JsonConvert.DeserializeObject<DataSets>(File.ReadAllText(filePath));
                LoadDataSets(dataSets);
            }
        }
        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="saveModel"></param>
        public static void LoadDataSets()
        {
            DataSets dataSets = JsonConvert.DeserializeObject<DataSets>(File.ReadAllText(Path.Combine(SystemSetting.Default.DataSetDir, SystemSetting.Default.LastDataSet, $"{SystemSetting.Default.LastDataSet}.ds")));
            LoadDataSets(dataSets);
        }
        /// <summary>
        /// 保存数据集
        /// </summary>
        /// <param name="saveModel"></param>
        public static void SaveDataSets()
        {
            var dir = Path.Combine(SystemSetting.Default.DataSetDir, CurrentImageData.DatasetProperties.Name);
            var path = Path.Combine(dir, $"{CurrentImageData.DatasetProperties.Name}.ds");
            SaveDataSets(path);
        }
        /// <summary>
        /// 保存数据集
        /// </summary>
        /// <param name="saveModel"></param>
        public static void SaveDataSets(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(CurrentImageData));
        }
        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="saveModel"></param>
        public static void LoadDataSets(DataSets dataSets)
        {
            CurrentImageData = dataSets;
            LabelNameData = new Dictionary<string, string>();
            foreach (var item in CurrentImageDataList)
            {
                if (string.IsNullOrWhiteSpace(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString("N");
                }
                KevImageData.Add(item.Id, item);
                foreach (var labelsModel in item.Labels)
                {
                    labelsModel.ImageItemModel = item;
                    if (!LabelNameData.ContainsKey(labelsModel.Name))
                    {
                        LabelNameData.Add(labelsModel.Name, null);
                        LabelNames.Add(labelsModel.Name);
                    }
                    if (!string.IsNullOrWhiteSpace(labelsModel.LabelId))
                    {
                        LabelKeyDictionary.Add(labelsModel.LabelId, labelsModel);
                    }
                }
            }
        }
        /// <summary>
        /// 获得文件完整路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetImgPath(string path)
        {
            var fullPath = MemoryCache.Default.Get(path);
            if(fullPath == null)
            {
                var newPath = Path.Combine(SystemSetting.Default.DataSetDir, ImageManagers.CurrentImageData.DatasetProperties.Name, DataSets.ImgDir, path);
                MemoryCache.Default.Add(path, newPath, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                return newPath;
            }
            return fullPath.ToString();
        }
        /// <summary>
        /// 获得图片(有缓存则获取缓存图片)
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        public static Bitmap GetImage(ImageItemModel itemModel)
        {
            var img = MemoryCache.Default.Get(itemModel.Id);
            Bitmap image = null;
            if (img == null)
            {
                image = Image.FromFile(GetImgPath(itemModel.Path)) as Bitmap;
                MemoryCache.Default.Add(itemModel.Id, image, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
            }
            else
            {
                image = img as Bitmap;
            }
            return image;
        }
        /// <summary>
        /// 通过图片Key获取图片对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ImageItemModel GetImageItemModel(string key)
        {
            if (KevImageData.ContainsKey(key))
            {
                var result = KevImageData[key];
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过标签Key获取标签对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ImageLabelsModel GetImageLabelsModel(string key)
        {
            if (LabelKeyDictionary.ContainsKey(key))
            {
                var result = LabelKeyDictionary[key];
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得重叠的标签
        /// </summary>
        /// <param name="startIndex">开始索引</param>
        /// <param name="currentIndex">当前索引</param>
        /// <returns>找到的标签</returns>
        public static ImageLabelsModel GetOverlappingLabel(int startIndex, out int currentIndex)
        {
            List<ImageLabelsModel> imageLabelsModelList = new List<ImageLabelsModel>();
            for (int i = startIndex; i < CurrentImageDataList.Count; i++)
            {
                var imageItemModel = CurrentImageDataList[i];
                imageLabelsModelList.Clear();
                foreach (var labelsModel in imageItemModel.Labels)
                {
                    var labelsModelRectangle = new Rectangle(labelsModel.X1, labelsModel.Y1, labelsModel.Width, labelsModel.Height);
                    var area = labelsModelRectangle.Width * labelsModelRectangle.Height;
                    foreach (var imageLabel in imageLabelsModelList)
                    {
                        //获取两个位置的交集
                        var intersectRectangle = Rectangle.Intersect(labelsModelRectangle, new Rectangle(imageLabel.X1, imageLabel.Y1, imageLabel.Width, imageLabel.Height));
                        var currArea = intersectRectangle.Width * intersectRectangle.Height;
                        //如果80%面积重复则认定是重复框
                        if (currArea > area * 0.8 || currArea > imageLabel.Width * imageLabel.Height * 0.8)
                        {
                            currentIndex = i;
                            return labelsModel;
                        }
                    }
                    imageLabelsModelList.Add(labelsModel);
                }

            }
            currentIndex = CurrentImageDataList.Count;
            return null;
        }
        /// <summary>
        /// 获得重叠的标签
        /// </summary>
        /// <param name="index">开始索引</param>
        /// <returns>找到的标签</returns>
        public static ImageLabelsModel GetOverlappingLabel(ref int index)
        {
            var result = GetOverlappingLabel(index, out int currIndex);
            index = currIndex;
            return result;
        }
        /// <summary>
        /// 打开子标签为空
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ImageLabelsModel GetSonEmptyLabel(ref int index)
        {
            for (int i = index; i < CurrentImageDataList.Count; i++)
            {
                foreach (var item in CurrentImageDataList[i].Labels)
                {
                    if ((item.Name == "cd" || item.Name == "zd") && string.IsNullOrWhiteSpace(item.SubName))
                    {
                        index = i;
                        return item;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 打开子标签为空
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ImageLabelsModel GetUnknownLabel(ref int index)
        {
            for (int i = index; i < CurrentImageDataList.Count; i++)
            {
                foreach (var item in CurrentImageDataList[i].Labels)
                {
                    if (item.Name == "未知")
                    {
                        if (item.Width < 3)
                        {
                            item.X2 = item.X1 + 20;
                        }
                        if (item.Height < 3)
                        {
                            item.Y2 = item.Y1 + 20;
                        }
                        index = i;
                        return item;
                    }
                }
            }
            return null;
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
            var trainListPath = Path.Combine(directoryPath, "train_list.txt");
            var valListPath = Path.Combine(directoryPath, "val_list.txt");
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
                    var imagePath = Path.Combine(directoryPath, paths[0]);
                    var xmlPath = Path.Combine(directoryPath, paths[1]);
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
                        if (!LabelNameData.ContainsKey(imageLabels.Name))
                        {
                            LabelNameData.Add(imageLabels.Name, null);
                            LabelNames.Add(imageLabels.Name);
                        }
                        var bndboxElement = objectElement.Element("bndbox");
                        imageLabels.X1 = bndboxElement.Element("xmin").Value.StringToInt();
                        imageLabels.X2 = bndboxElement.Element("xmax").Value.StringToInt();
                        imageLabels.Y1 = bndboxElement.Element("ymin").Value.StringToInt();
                        imageLabels.Y2 = bndboxElement.Element("ymax").Value.StringToInt();
                        imageLabels.ImageItemModel = imageItemModel;
                        imageItemModel.Labels.Add(imageLabels);
                    }
                    imageItemModel.CompleteLevel = imageItemModel.Labels.Count > 0?1:0;
                    if (KevImageData.ContainsKey(imageItemModel.Id))
                    {
                        LogUtils.Log($"编号为{imageItemModel.Id}在列表中已拥有");
                    }
                    else
                    {
                        KevImageData.Add(imageItemModel.Id, imageItemModel);
                        CurrentImageDataList.Add(imageItemModel);
                        success++;
                    }
                }
            }

            return success;
        }
        public static int LoadPaddleOcrDetData(string filePath)
        {
            return new ImportPaddleOcrDet().ImportLabelName(filePath);
        }
        /// <summary>
        /// 加载BoxWord数据集文件夹,返回加入数据条数
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static int LoadBoxWordDirectory(string directoryPath)
        {
            var success = 0;
            if (!Directory.Exists(directoryPath))
            {
                LogUtils.Log($"加载BoxWord文件夹时出现:文件夹{directoryPath}不存在");
                return success;
            }
            var dataPath = Path.Combine(directoryPath, "data.txt");
            if (!File.Exists(dataPath))
            {
                return 0;
            }
            var boxWordModelList = JsonConvert.DeserializeObject<List<BoxWordModel>>(File.ReadAllText(dataPath));
            foreach (var boxWordModel in boxWordModelList)
            {
                ImageItemModel imageItemModel = new ImageItemModel();
                var path = boxWordModel.Path;
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var num = path.IndexOf('.');
                    if (num > 0)
                    {
                        var startIndex = path.LastIndexOf('\\') + 1;
                        imageItemModel.Id = path.Substring(startIndex, num - startIndex);
                    }
                    else
                    {
                        imageItemModel.Id = path;
                    }
                }
                else
                {
                    imageItemModel.Id = Guid.NewGuid().ToString("N");
                }
                imageItemModel.Path = Path.Combine(directoryPath, path);
                foreach (var dataModel in boxWordModel.Data)
                {
                    if (dataModel.Score < 0.3)
                    {
                        continue;
                    }
                    ImageLabelsModel imageLabels = new ImageLabelsModel();
                    imageLabels.Name = dataModel.Category;
                    if (!LabelNameData.ContainsKey(imageLabels.Name))
                    {
                        LabelNameData.Add(imageLabels.Name, null);
                        LabelNames.Add(imageLabels.Name);
                    }
                    if (!string.IsNullOrWhiteSpace(dataModel.LabelId))
                    {
                        imageLabels.LabelId = dataModel.LabelId;
                        if (!LabelKeyDictionary.ContainsKey(imageLabels.LabelId))
                        {
                            LabelKeyDictionary.Add(imageLabels.LabelId, imageLabels);
                        }
                    }
                    var x1 = (int)dataModel.Bbox[0];
                    var y1 = (int)dataModel.Bbox[1];
                    var x2 = (int)dataModel.Bbox[2];
                    var y2 = (int)dataModel.Bbox[3];
                    imageLabels.X1 = x1;
                    imageLabels.Y1 = y1;
                    imageLabels.X2 = x1 + x2;
                    imageLabels.Y2 = y1 + y2;
                    imageLabels.SubName = dataModel.SubName;
                    imageLabels.ImageItemModel = imageItemModel;
                    imageItemModel.Labels.Add(imageLabels);
                }
                imageItemModel.CompleteLevel = imageItemModel.Labels.Count > 0 ? 1 : 0;
                if (KevImageData.ContainsKey(imageItemModel.Id))
                {
                    LogUtils.Log($"编号为{imageItemModel.Id}在列表中已拥有");
                }
                else
                {
                    KevImageData.Add(imageItemModel.Id, imageItemModel);
                    CurrentImageDataList.Add(imageItemModel);
                    success++;
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
            foreach (var item in CurrentImageDataList.Where(cid => cid.CompleteLevel>0))
            {
                var imagePath = Path.Combine(saveDirectoryPath, $"{item.Id}.jpg");
                File.Copy(item.Path, imagePath);
                Dictionary<string, List<ImageLabelsModel>> jsonDictionary = new Dictionary<string, List<ImageLabelsModel>>();
                jsonDictionary.Add("labels", item.Labels);
                File.WriteAllText(imagePath.Replace(".jpg", ".json"), JsonConvert.SerializeObject(jsonDictionary));
            }
        }
        static ImageManagers()
        {
            var root = "image";
            if (File.Exists("data.ini"))
            {
                KevImageData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("data.ini"));
            }
        }
    }
}
