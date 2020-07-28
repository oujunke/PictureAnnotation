using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureAnnotationForm.Models
{
    public class SaveModel
    {
        /// <summary>
        /// 当前显示图片索引
        /// </summary>
        public int ListImgIndex { set; get; }
        /// <summary>
        /// 当前选中的图片列表
        /// </summary>
        public int ListSelectIndex { set; get; }
        /// <summary>
        /// 当前选中的标签
        /// </summary>
        public int SelectLabelIndex { set; get; }
        /// <summary>
        /// 当前图片标注数据
        /// </summary>
        public List<ImageItemModel> CurrentImageData { set; get; }
        /// <summary>
        /// Label标签对应的信息
        /// </summary>
        public Dictionary<string, LabelColor> LabelNameToLabelColor { set; get; }
        /// <summary>
        /// 当前所有的Label
        /// </summary>
        public List<string> LabelNames { set; get; }
    }
}
