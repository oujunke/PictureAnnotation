using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
namespace PictureAnnotationForm.Models
{
    public class LabelColor
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 画笔
        /// </summary>
        [JsonIgnore]
        public Pen Pen;
        /// <summary>
        /// 颜色
        /// </summary>
        [JsonIgnore]
        public Color Color;
        /// <summary>
        /// 填充对象
        /// </summary>
        [JsonIgnore]
        public SolidBrush Brush;
        /// <summary>
        /// 是否填充
        /// </summary>
        public bool IsFill;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelect = true;
        /// <summary>
        /// 默认颜色
        /// </summary>
        public static LabelColor DefaultLabelColor;
        /// <summary>
        /// 默认字体
        /// </summary>
        public static Font DefaultFont;
        static LabelColor()
        {
            DefaultLabelColor = new LabelColor(Color.Black)
            {
                Name = "Black",
            };
            DefaultFont = new Font("微软雅黑",12);
        }
        public LabelColor(Color color)
        {
            Color = color;
            Pen = new Pen(color,2);
            Brush = new SolidBrush(Color);
        }
    }
}
