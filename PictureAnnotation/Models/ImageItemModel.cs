using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotation.Models
{
    public class ImageItemModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { set; get; }
		[JsonProperty("path")]
		public string Path { set; get; }
		[JsonIgnore]
		public Bitmap Image{ set; get; }
		[JsonProperty("labels")]
		public List<ImageLabelsModel> Labels { get; set; }
	}
}
