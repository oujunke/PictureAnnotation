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
using PictureAnnotationForm.Utils;

namespace PictureAnnotationForm.UserForm
{
    public partial class LabelImageUserControl : UserControl
    {
        public LabelImageUserControl()
        {
            InitializeComponent();
        }
        #region 对外事件
        /// <summary>
        /// 触发上一张图片
        /// </summary>
        public event Action ImageLast;
        /// <summary>
        /// 触发下一张图片
        /// </summary>
        public event Action ImageNext;
        /// <summary>
        /// 选择标签变动
        /// </summary>
        public event Action<ImageLabelsModel> LabelChange;
        #endregion
        #region 对外属性
        /// <summary>
        /// 当前的ImageItemModel
        /// </summary>
        public ImageItemModel CurrentImageItemModel { private set; get; }
        #endregion
        #region 对外方法
        /// <summary>
        /// 设置当前的标签模型
        /// </summary>
        /// <param name="imageItemModel"></param>
        public void SetImageItemModel(ImageItemModel imageItemModel)
        {
            CurrentImageItemModel = imageItemModel;
            _currentBitmap = ImageManagers.GetImage(CurrentImageItemModel);
            _currentLabelList.Clear();
            _currentLabelList.AddRange(CurrentImageItemModel.Labels);
            UpdateBitamp();
            LabelChange?.Invoke(null);
        }
        /// <summary>
        /// 设置当前的标签模型
        /// </summary>
        /// <param name="key"></param>
        public void SetImageItemModel(string key)
        {
            var imageItemModel = ImageManagers.GetImageItemModel(key);
            if (imageItemModel == null)
            {
                LogUtils.Log($"图片:{key}未找到");
                return;
            }
            SetImageItemModel(imageItemModel);
        }
        /// <summary>
        /// 更新图片画板
        /// </summary>
        public void UpdateBitamp()
        {
            if (CurrentImageItemModel == null)
            {
                return;
            }
            _currentDrawBitmap = GetDrawBitamp();
            pbMian.Image = _currentDrawBitmap;
        }
        /// <summary>
        /// 设置选中标签
        /// </summary>
        /// <param name="label"></param>
        public void HighlightLabelImage(ImageLabelsModel label)
        {
            _lastMoveLabel = label;
            pbMian.Image = HighlightLabelImage(label, _currentDrawBitmap);
        }
        #endregion

        #region 控件触发事件
        private void btnNext_Click(object sender, EventArgs e)
        {
            ImageNext?.Invoke();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            ImageLast?.Invoke();
        }
        private void pbMian_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var label = GetImageLabelsModelAtPoint(e.X, e.Y);
                if (label != null)
                {
                    LabelChange?.Invoke(label);
                }
            }
        }
        bool isDrag = false;
        ImageLabelsModel dragLabel;
        Rectangle dragLabelRectangle;
        Point dragPoint;
        DateTime lastDragTime;
        private void pbMian_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var label = GetImageLabelsModelAtPoint(e.X, e.Y);
                if (label == null)
                {
                    return;
                }
                isDrag = true;
                dragLabel = label;
                dragPoint = new Point(e.X, e.Y);
                dragLabelRectangle = new Rectangle(dragLabel.X1, dragLabel.Y1, dragLabel.Width, dragLabel.Height);
            }
        }

        private void pbMian_MouseUp(object sender, MouseEventArgs e)
        {
            ClertDrag();
        }
        private void pbMian_MouseLeave(object sender, EventArgs e)
        {
            ClertDrag();
        }

        private void ClertDrag()
        {
            isDrag = false;
            dragLabel = null;
            _lastMoveLabel = null;
        }

        private void pbMian_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                if (e.Button != MouseButtons.Left)
                {
                    ClertDrag();
                    return;
                }
                if ((DateTime.Now- lastDragTime).Milliseconds>10)
                {
                    return;
                }
                lastDragTime = DateTime.Now;
                var x = (int)((e.Location.X - dragPoint.X) / dragLabel.ZoomMultiple);
                var y = (int)((e.Location.Y - dragPoint.Y) / dragLabel.ZoomMultiple);
                dragLabel.X1 = dragLabelRectangle.X + x;
                dragLabel.X2 = dragLabelRectangle.Right + x;
                dragLabel.Y1 = dragLabelRectangle.Top + y;
                dragLabel.Y2 = dragLabelRectangle.Bottom + y;
                var bitmap = GetDrawBitamp();
                pbMian.Image = HighlightLabelImage(dragLabel, bitmap);
            }
            else
            {
                var label = GetImageLabelsModelAtPoint(e.X, e.Y);
                if (label == null || _lastMoveLabel == label)
                {
                    return;
                }
                _lastMoveLabel = label;
                pbMian.Image = HighlightLabelImage(label, _currentDrawBitmap);
            }
        }
        /// <summary>
        /// 突出标签图片
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private Bitmap HighlightLabelImage(ImageLabelsModel label, Bitmap bitmap)
        {
            var labelColor = LabelColorManagers.GetLabelColor(label.Name);
            var colors = bitmap.ToColorArray();
            var right = label.LabelShowRectangle.Right;
            var bottom = label.LabelShowRectangle.Bottom;
            for (int x = label.LabelShowRectangle.X; x < right; x++)
            {
                for (int y = label.LabelShowRectangle.Y; y < bottom; y++)
                {
                    var color = colors[x, y];
                    colors[x, y] = Color.FromArgb((color.R + labelColor.Color.R) / 2, (color.G + labelColor.Color.G) / 2, (color.B + labelColor.Color.B) / 2);
                }
            }

            return colors.ToBitmap();
        }
        #endregion
        #region 私有变量
        /// <summary>
        /// 当前图片
        /// </summary>
        private Bitmap _currentBitmap;
        /// <summary>
        /// 当前绘制图片
        /// </summary>
        private Bitmap _currentDrawBitmap;
        /// <summary>
        /// 当前标签
        /// </summary>
        private List<ImageLabelsModel> _currentLabelList = new List<ImageLabelsModel>();
        /// <summary>
        /// 图片显示信息
        /// </summary>
        private ImageShowInfo _imageShowInfo;
        /// <summary>
        /// 上次突出显示的Label
        /// </summary>
        private ImageLabelsModel _lastMoveLabel;
        #endregion
        #region 私有方法
        private Bitmap GetDrawBitamp()
        {
            return GetDrawBitamp(_currentLabelList);
        }
        private Bitmap GetDrawBitamp(List<ImageLabelsModel> imageLabelsModels)
        {
            _imageShowInfo = pbMian.GetImageShowInfo(_currentBitmap);
            var newBit = new Bitmap(_imageShowInfo.Width, _imageShowInfo.Height);
            var graphics = Graphics.FromImage(newBit);
            graphics.DrawImage(_currentBitmap, new Rectangle(0, 0, newBit.Width, newBit.Height));
            foreach (var label in imageLabelsModels)
            {
                label.ZoomMultiple = _imageShowInfo.ZoomMultiple;
                var labelColor = LabelColorManagers.GetLabelColor(label.Name);
                if (labelColor.IsSelect)
                {
                    graphics.DrawRectangle(labelColor.Pen, label.LabelShowRectangle);
                    graphics.DrawString(label.Name, LabelColor.DefaultFont, labelColor.Brush, label.LabelShowRectangle.X + 3, label.LabelShowRectangle.Y + 3);
                }
            }
            graphics.Dispose();
            return newBit;
        }
        /// <summary>
        /// 根据坐标点获取标签
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        private ImageLabelsModel GetImageLabelsModelAtPoint(int px, int py)
        {
            if (CurrentImageItemModel == null)
            {
                return null;
            }
            var x = px - _imageShowInfo.X;
            var y = py - _imageShowInfo.Y;
            foreach (var label in _currentLabelList.ToList())
            {
                var rectangle = label.LabelShowRectangle;
                if (rectangle.Left <= x && rectangle.Top <= y && rectangle.Right >= x && rectangle.Bottom >= y)
                {
                    return label;
                }
            }
            return null;
        }
        #endregion
    }
}
