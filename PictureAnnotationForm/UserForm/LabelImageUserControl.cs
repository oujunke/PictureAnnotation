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
        /// <summary>
        /// 当前绘制图片
        /// </summary>
        public Bitmap CurrentDrawBitmap;
        /// <summary>
        /// 图片显示信息
        /// </summary>
        public ImageShowInfo ImageShowInfo;
        /// <summary>
        /// 标签对应的显示控件
        /// </summary>
        public Dictionary<ImageLabelsModel, ImageLabelShowUserControl> LabelShowDictionary = new Dictionary<ImageLabelsModel, ImageLabelShowUserControl>();
        #endregion
        #region 对外方法
        /// <summary>
        /// 设置当前的标签模型
        /// </summary>
        /// <param name="imageItemModel"></param>
        public void SetImageItemModel(ImageItemModel imageItemModel)
        {
            CurrentImageItemModel = imageItemModel;
            ImageShowInfo = null;
            _currentBitmap = ImageManagers.GetImage(CurrentImageItemModel);
            UpdateDrawingBoard();
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
        public void UpdateDrawingBoard()
        {
            if (CurrentImageItemModel == null)
            {
                return;
            }
            UpdateBitmap();
            pbMian.Image = CurrentDrawBitmap;
            InitImageLabelShowUserControl();
        }
        /// <summary>
        /// 更新图片
        /// </summary>
        public void UpdateBitmap()
        {
            CurrentDrawBitmap = GetDrawBitamp();
            pbMian.Image = CurrentDrawBitmap;
        }
        /// <summary>
        /// 设置选中标签
        /// </summary>
        /// <param name="label"></param>
        public void HighlightLabelImage(ImageLabelsModel label)
        {
            _lastMoveLabel = label;
            pbMian.Image = HighlightLabelImage(label, CurrentDrawBitmap);
        }
        /// <summary>
        /// 触发选中标签事件
        /// </summary>
        /// <param name="label"></param>
        public void SelectLabel(ImageLabelsModel label)
        {
            LabelChange?.Invoke(label);
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
                if ((DateTime.Now - lastDragTime).Milliseconds > 10)
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
                pbMian.Image = HighlightLabelImage(label, CurrentDrawBitmap);
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
        /// 上次突出显示的Label
        /// </summary>
        private ImageLabelsModel _lastMoveLabel;
        /// <summary>
        /// 存放所有ImageLabel控件的集合
        /// </summary>
        private List<ImageLabelShowUserControl> _imageLabelShowUserControls = new List<ImageLabelShowUserControl>();
        #endregion
        #region 私有方法
        /// <summary>
        /// 初始化标签
        /// </summary>
        private void InitImageLabelShowUserControl()
        {
            LabelShowDictionary.Clear();
            if (_imageLabelShowUserControls.Count < CurrentImageItemModel.Labels.Count)
            {
                for (int i = _imageLabelShowUserControls.Count - 1; i < CurrentImageItemModel.Labels.Count; i++)
                {
                    var labelShowUserControl = new ImageLabelShowUserControl(this);
                    pbMian.Controls.Add(labelShowUserControl);
                    _imageLabelShowUserControls.Add(labelShowUserControl);
                }
            }
            else if (_imageLabelShowUserControls.Count > CurrentImageItemModel.Labels.Count)
            {
                for (int i = CurrentImageItemModel.Labels.Count - 1; i < _imageLabelShowUserControls.Count; i++)
                {
                    _imageLabelShowUserControls[i].Delete();
                }
            }
            for (int i = 0; i < CurrentImageItemModel.Labels.Count; i++)
            {
                var label = CurrentImageItemModel.Labels[i];
                label.ZoomMultiple = ImageShowInfo.ZoomMultiple;
                var labelColor = LabelColorManagers.GetLabelColor(label.Name);
                if (labelColor.IsSelect)
                {
                    LabelShowDictionary.Add(label, _imageLabelShowUserControls[i]);
                    _imageLabelShowUserControls[i].Init(label);
                }
            }
        }
        /// <summary>
        /// 获得绘制图片
        /// </summary>
        /// <returns></returns>
        private Bitmap GetDrawBitamp()
        {
            if (ImageShowInfo == null)
            {
                ImageShowInfo = pbMian.GetImageShowInfo(_currentBitmap);
            }
            var newBit = new Bitmap(ImageShowInfo.Width, ImageShowInfo.Height);
            var graphics = Graphics.FromImage(newBit);
            graphics.DrawImage(_currentBitmap, new Rectangle(0, 0, newBit.Width, newBit.Height));
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
            var x = px - ImageShowInfo.X;
            var y = py - ImageShowInfo.Y;
            foreach (var label in CurrentImageItemModel.Labels.ToList())
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
