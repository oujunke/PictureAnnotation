using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotation.Models
{
    public class LableColor
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 画笔
        /// </summary>
        public Pen Pen;
        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color;
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
        public static LableColor DefaultLableColor;
        static LableColor()
        {
            DefaultLableColor = new LableColor
            {
                Name = "Black",
                Color = Color.Black,
                Pen = Pens.Black,
            };
        }
    }
}
