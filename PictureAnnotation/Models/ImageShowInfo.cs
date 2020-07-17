using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureAnnotation.Models
{
    public class ImageShowInfo
    {
        public int ImageWidth { set; get; }
        public int ImageHeight { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        public int X { set; get; }
        public int Y { set; get; }
        public float ZoomMultiple { set; get; }
    }
}
