using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureAnnotation.Models
{
    public class ImageLabelsModel
    {
		[JsonProperty("label_id")]
		public string LabelId { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("x1")]
		public int X1 { get; set; }
		[JsonProperty("y1")]
		public int Y1 { get; set; }
		[JsonProperty("x2")]
		public int X2 { get; set; }
		[JsonProperty("y2")]
		public int Y2 { get; set; }
	}
}
