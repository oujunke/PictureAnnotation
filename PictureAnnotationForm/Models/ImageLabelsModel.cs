using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotationForm.Models
{
    public class ImageLabelsModel
    {
        /// <summary>
        /// 标注id
        /// </summary>
		[JsonProperty("label_id", NullValueHandling = NullValueHandling.Ignore)]
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
        [JsonIgnore]
        public int Width { get => X2 - X1; }
        /// <summary>
        /// 高度
        /// </summary>
        [JsonIgnore]
        public int Height { get => Y2 - Y1; }
        /// <summary>
        /// 父图片
        /// </summary>
        [JsonIgnore]
        public ImageItemModel ImageItemModel { set; get; }
        /// <summary>
        /// 显示缩放倍数
        /// </summary>
        [JsonIgnore]
        public float ZoomMultiple { get => ImageItemModel?.ZoomMultiple ?? 0; }
        /// <summary>
        /// 标签显示位置
        /// </summary>
        [JsonIgnore]
        public Rectangle LabelShowRectangle
        {
            get
            {
                return new Rectangle((int)Math.Floor(X1 * ZoomMultiple), (int)Math.Floor(Y1 * ZoomMultiple), (int)Math.Floor(Width * ZoomMultiple), (int)Math.Floor(Height * ZoomMultiple));
            }
            set
            {
                X1 = (int)Math.Floor(value.X / ZoomMultiple);
                Y1= (int)Math.Floor(value.Y / ZoomMultiple);
                X2 = (int)Math.Floor(value.Right / ZoomMultiple);
                Y2 = (int)Math.Floor(value.Bottom / ZoomMultiple);
            }
        }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        [JsonIgnore]
        public bool IsHide { set; get; }
        /// <summary>
        /// 复制到新成员中
        /// </summary>
        /// <returns></returns>
        public ImageLabelsModel Copy()
        {
            return null;
        }
    }
}
