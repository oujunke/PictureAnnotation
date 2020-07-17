using PictureAnnotation.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotation.BLL
{
    public class LableColorManagers
    {

        /// <summary>
        /// 存储所有标签颜色
        /// </summary>
        private static List<string> _lableColors = new List<string>();
        /// <summary>
        /// 当前所有的标签颜色
        /// </summary>
        public static string[] LableColors { get => _lableColors.ToArray(); }
        /// <summary>
        /// 存储所有标签颜色
        /// </summary>
        private static Dictionary<string, LableColor> _lableColorDictionary = new Dictionary<string, LableColor>();
        private static Dictionary<LableColor, string> _lableColorToLableName = new Dictionary<LableColor, string>();
        private static Dictionary<string, LableColor> _lableNameToLableColor = new Dictionary<string, LableColor>();
        private static object _lableColor = new object(); 
        static LableColorManagers()
        {
            AddLableColors("Red", new LableColor
            {
                Pen = Pens.Red,
                Color = Color.Red,
            });
            AddLableColors("Blue", new LableColor
            {
                Pen = Pens.Blue,
                Color = Color.Blue,
            });
            AddLableColors("Gray", new LableColor
            {
                Pen = Pens.Gray,
                Color = Color.Gray,
            });
            AddLableColors("Yellow", new LableColor
            {
                Pen = Pens.Yellow,
                Color = Color.Yellow,
            });
            AddLableColors("DarkRed", new LableColor
            {
                Pen = Pens.DarkRed,
                Color = Color.DarkRed,
            });
            AddLableColors("DarkGreen", new LableColor
            {
                Pen = Pens.DarkGreen,
                Color = Color.DarkGreen,
            });
            AddLableColors("GhostWhite", new LableColor
            {
                Pen = Pens.GhostWhite,
                Color = Color.GhostWhite,
            });
        }
        private static void AddLableColors(string name, LableColor lableColor)
        {
            lableColor.Name = name;
            _lableColorDictionary.Add(name, lableColor);
            _lableColors.Add(name);
        }
        public static LableColor GetLableColor(string lableName)
        {
            if (_lableNameToLableColor.ContainsKey(lableName))
            {
                return _lableNameToLableColor[lableName];
            }
            else
            {
                if(_lableNameToLableColor.Count>= _lableColors.Count)
                {
                    return LableColor.DefaultLableColor;
                }
                else
                {
                    lock (_lableColor)
                    {
                        if (_lableNameToLableColor.Count >= _lableColors.Count)
                        {
                            return LableColor.DefaultLableColor;
                        }
                        foreach (var item in _lableColorDictionary.Values)
                        {
                            if (!_lableColorToLableName.ContainsKey(item))
                            {
                                _lableColorToLableName.Add(item, lableName);
                                _lableNameToLableColor.Add(lableName, item);
                                return item;
                            }
                        }
                        return LableColor.DefaultLableColor;
                    }
                }
            }
        }
    }
}
