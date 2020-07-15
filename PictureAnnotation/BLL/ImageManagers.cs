using PictureAnnotation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace PictureAnnotation.BLL
{
    public class ImageManagers
    {
        private static List<ImageItemModel> _currentImageData = new List<ImageItemModel>();
        private static Dictionary<string, ImageItemModel> _kevImageData = new Dictionary<string, ImageItemModel>();
        public static List<ImageItemModel> GetImageList(int index=0,int size = 10)
        {
            return _currentImageData.Skip(index).Take(size).ToList();
        }

        static ImageManagers()
        {
            var root = "image";
            if (File.Exists("data.ini"))
            {
                _kevImageData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("data.ini"));
            }
        }
    }
}
