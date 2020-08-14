﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureAnnotation.Models
{
    public class ImageLabelsModel
    {
        /// <summary>
        /// 标注id
        /// </summary>
		[JsonProperty("label_id",NullValueHandling =NullValueHandling.Ignore)]
		public string LabelId { get; set; }
        /// <summary>
        /// 标注名称
        /// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
        /// <summary>
        /// 子分类
        /// </summary>
        [JsonProperty("sub_name")]
        public string SubName { get; set; }
        /// <summary>
        /// 框左上角Y
        /// </summary>
		[JsonProperty("x1")]
		public int X1 { get; set; }
        /// <summary>
        /// 框左上角Y
        /// </summary>
		[JsonProperty("y1")]
		public int Y1 { get; set; }
        /// <summary>
        /// 框右下角X
        /// </summary>
		[JsonProperty("x2")]
		public int X2 { get; set; }
        /// <summary>
        /// 框右下角Y
        /// </summary>
		[JsonProperty("y2")]
		public int Y2 { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get => X2 - X1; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get => Y2 - Y1; }
        /// <summary>
        /// 父图片
        /// </summary>
        [JsonIgnore]
        public ImageItemModel ImageItemModel { set; get; }
	}
}