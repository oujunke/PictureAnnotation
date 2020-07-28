using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureAnnotationForm.Models
{
    public class BoxWordModel
    {
		[JsonProperty("path")]
		public string Path { get; set; }
		[JsonProperty("data")]
		public List<DataModel> Data { get; set; }

		public class DataModel
		{
			[JsonProperty("category_id")]
			public int CategoryId { get; set; }
			[JsonProperty("category")]
			public string Category { get; set; }
			[JsonProperty("subName")]
			public string SubName { get; set; }
			[JsonProperty("labelId")]
			public string LabelId { get; set; }
			[JsonProperty("bbox")]
			public List<double> Bbox { get; set; }
			[JsonProperty("score")]
			public double Score { set; get; }
		}


	}
}
