using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PictureAnnotationForm.BLL
{
    public class LabelColorManagers
    {

        /// <summary>
        /// 存储所有标签颜色
        /// </summary>
        private static List<string> _labelColors = new List<string>();
        /// <summary>
        /// 当前所有的标签颜色
        /// </summary>
        public static string[] LabelColors { get => _labelColors.ToArray(); }
        /// <summary>
        /// 存储所有标签颜色
        /// </summary>
        private static Dictionary<string, LabelColor> _labelColorDictionary = new Dictionary<string, LabelColor>();
        private static Dictionary<LabelColor, string> _labelColorToLabelName = new Dictionary<LabelColor, string>();
        private static Dictionary<string, LabelColor> _labelNameToLabelColor = new Dictionary<string, LabelColor>();
        private static object _labelColor = new object();
        private static LabelColor OcrDefaultLabelColor;
        static LabelColorManagers()
        {
            AddLabelColors("Red", new LabelColor(Color.Red));
            AddLabelColors("Blue", new LabelColor(Color.Blue));
            AddLabelColors("Gray", new LabelColor(Color.Gray));
            AddLabelColors("Yellow", new LabelColor(Color.Yellow));
            AddLabelColors("DarkRed", new LabelColor(Color.DarkRed));
            AddLabelColors("DarkGreen", new LabelColor(Color.DarkGreen));
            AddLabelColors("DarkOrange", new LabelColor(Color.DarkOrange));
            OcrDefaultLabelColor = _labelColorDictionary["Red"];
        }
        private static void AddLabelColors(string name, LabelColor labelColor)
        {
            labelColor.Name = name;
            _labelColorDictionary.Add(name, labelColor);
            _labelColors.Add(name);
        }
        public static LabelColor GetLabelColor(string labelName)
        {
            if(ImageManagers.CurrentImageData.DatasetProperties.Type== Enums.EDatasetPropertiesType.Ocr)
            {
                return OcrDefaultLabelColor;
            }
            if (_labelNameToLabelColor.ContainsKey(labelName))
            {
                return _labelNameToLabelColor[labelName];
            }
            else
            {
                if (_labelNameToLabelColor.Count >= _labelColors.Count)
                {
                    return LabelColor.DefaultLabelColor;
                }
                else
                {
                    lock (_labelColor)
                    {
                        if (_labelNameToLabelColor.Count >= _labelColors.Count)
                        {
                            return LabelColor.DefaultLabelColor;
                        }
                        foreach (var item in _labelColorDictionary.Values)
                        {
                            if (!_labelColorToLabelName.ContainsKey(item))
                            {
                                _labelColorToLabelName.Add(item, labelName);
                                _labelNameToLabelColor.Add(labelName, item);
                                return item;
                            }
                        }
                        return LabelColor.DefaultLabelColor;
                    }
                }
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="saveModel"></param>
        public static void SaveData()
        {
            ImageManagers.CurrentImageData.DatasetProperties.LabelNameToLabelColor = _labelNameToLabelColor;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="saveModel"></param>
        public static void LoadData()
        {
            _labelNameToLabelColor = ImageManagers.CurrentImageData.DatasetProperties.LabelNameToLabelColor;
            if (_labelNameToLabelColor == null)
            {
                _labelNameToLabelColor = new Dictionary<string, LabelColor>();
            }
            _labelColorToLabelName.Clear();
            foreach (var item in _labelNameToLabelColor.ToList())
            {
                LabelColor labelColor = null;
                if (_labelColorDictionary.ContainsKey(item.Value.Name))
                {
                    labelColor = _labelColorDictionary[item.Value.Name];
                }
                else
                {
                    labelColor = LabelColor.DefaultLabelColor;
                }
                labelColor.Name = item.Value.Name;
                labelColor.IsFill = item.Value.IsFill;
                labelColor.IsSelect = item.Value.IsSelect;
                _labelNameToLabelColor[item.Key] = labelColor;
                _labelColorToLabelName.Add(labelColor, item.Key);
            }
        }
    }
}
