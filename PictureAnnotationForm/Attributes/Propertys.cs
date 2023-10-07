using Newtonsoft.Json.Linq;
using PictureAnnotationForm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Attributes
{
    public class Propertys : Attribute
    {
        public string Text { get; set; }
        public string Tip { set; get; }
        public string Value { set; get; }
        public EPropertysType Type { get; set; }
        /// <summary>
        /// 创建用于显示的属性
        /// </summary>
        /// <param name="text">标题栏显示</param>
        /// <param name="value">默认值</param>
        /// <param name="type">指定类型</param>
        /// <param name="tip">提示</param>
        public Propertys(string text = null,string value=null, EPropertysType type = 0, string tip = null)
        {
            Text = text;
            Value = value;
            Tip =tip; 
            Type=type;
        }
    }
}
