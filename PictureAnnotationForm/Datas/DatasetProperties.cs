using PictureAnnotationForm.Attributes;
using PictureAnnotationForm.Enums;
using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas
{
    public class DatasetProperties
    {
        [Propertys("程序集名称")]
        public string Name { get; set; }
        [Propertys("描述")]
        public string Dec { get; set; }
        /// <summary>
        /// 获取重叠比例
        /// </summary>
        [Propertys("重叠比例")]
        public double OverScale { set; get; }
        [Propertys("类型")]
        public EDatasetPropertiesType Type { get; set; }
        [Propertys("链接类型", nameof(EFileLinkType.硬链接))]
        public EFileLinkType FileLinkType { get; set; }
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
        /// Label标签对应的信息
        /// </summary>
        public Dictionary<string, LabelColor> LabelNameToLabelColor { set; get; }

    }
}
