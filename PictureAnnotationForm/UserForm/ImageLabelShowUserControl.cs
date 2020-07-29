using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PictureAnnotationForm.Models;
using PictureAnnotationForm.BLL;

namespace PictureAnnotationForm.UserForm
{
    public partial class ImageLabelShowUserControl : UserControl
    {
        private ImageLabelsModel _currentImageLabelsModel;
        private Bitmap _currentDrawBitmap;
        private LabelColor _currentLabelColor;
        /// <summary>
        /// 图片显示信息
        /// </summary>
        public ImageShowInfo ImageShowInfo;
        /// <summary>
        /// 边框宽度
        /// </summary>
        public int BorderWidth = 2;
        public ImageLabelShowUserControl()
        {
            InitializeComponent();
        }
        public void Init(ImageLabelsModel imageLabelsModel, Bitmap bitmap)
        {
            _currentImageLabelsModel = imageLabelsModel;
            _currentDrawBitmap = bitmap;
            _currentLabelColor = LabelColorManagers.GetLabelColor(_currentImageLabelsModel.Name);
            SuspendLayout();
            Left = imageLabelsModel.LabelShowRectangle.Left;
            Top= imageLabelsModel.LabelShowRectangle.Top;
            Width= imageLabelsModel.LabelShowRectangle.Width;
            Height= imageLabelsModel.LabelShowRectangle.Height;

            ResumeLayout(true);
        }

        private void ImageLabelShowUserControl_Paint(object sender, PaintEventArgs e)
        {
            var x = (ImageShowInfo?.X) ?? 0 + Left + e.ClipRectangle.X;
            var y = (ImageShowInfo?.Y) ?? 0 + Top + e.ClipRectangle.Y;
            e.Graphics.DrawImage(_currentDrawBitmap, new Rectangle(x, y, e.ClipRectangle.Width, e.ClipRectangle.Height), e.ClipRectangle, GraphicsUnit.Pixel);

            
        }
    }
}
