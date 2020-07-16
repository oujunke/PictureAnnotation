using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotation.Models
{
    public class ImageItemModel
    {
        /// <summary>
        /// 图片id
        /// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }
        /// <summary>
        /// 图片命名
        /// </summary>
		[JsonProperty("name")]
		public string Name { set; get; }
        /// <summary>
        /// 图片路径
        /// </summary>
		[JsonProperty("path")]
		public string Path { set; get; }
        /// <summary>
        /// 标注的图片宽度
        /// </summary>
        [JsonProperty("width")]
        public int Width { set; get; }
        /// <summary>
        /// 标注的图片高度
        /// </summary>
        [JsonProperty("height")]
        public int Height { set; get; }
        /// <summary>
        /// 是否标注完成
        /// </summary>
        [JsonProperty("is_complete")]
        public bool IsComplete { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
		[JsonIgnore]
		public Bitmap Image{ set; get; }

        /// <summary>
        /// 图片标注
        /// </summary>
        [JsonProperty("labels")]
        public List<ImageLabelsModel> Labels { get; set; } = new List<ImageLabelsModel>();
	}
}
