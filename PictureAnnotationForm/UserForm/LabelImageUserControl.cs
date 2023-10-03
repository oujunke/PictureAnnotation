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
using System.Diagnostics;

namespace PictureAnnotationForm.UserForm
{
    public partial class LabelImageUserControl : UserControl
    {
        public LabelImageUserControl()
        {
            InitializeComponent();
            pbMian.MouseWheel += PbMian_MouseWheel;
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
        public Point LeftPoint = Point.Empty;
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
            ImageShowInfo = pbMian.GetImageShowInfo(_currentBitmap);
            imageItemModel.ZoomMultiple = ImageShowInfo.ZoomMultiple;
            LeftPoint = Point.Empty;
            UpdateDrawingBoard();
            LabelChange?.Invoke(null);
        }
        /// <summary>
        /// 设置当前的标签模型
        /// </summary>
        /// <param name="imageItemModel"></param>
        public void SetImageItemModel(ImageLabelsModel imageLabelsModel)
        {
            SetImageItemModel(imageLabelsModel.ImageItemModel);
            LabelShowDictionary[imageLabelsModel].SetHighlight();
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
        bool isDrag = false;
        ImageLabelsModel dragLabel;
        ImageLabelShowUserControl dragImageLabelShowUserControl;
        Point dragPoint;
        bool isControl = false;
        bool isAlt = false;
        DateTime lastDt = DateTime.MinValue;
        private void PbMian_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isControl && e.Delta != 0&&(DateTime.Now- lastDt).TotalMilliseconds>50)
            {
                lastDt = DateTime.Now;
                var ox = (e.X - ImageShowInfo.X) / CurrentImageItemModel.ZoomMultiple;
                var oy = (e.Y - ImageShowInfo.Y) / CurrentImageItemModel.ZoomMultiple;
                if (e.Delta > 0)
                {
                    CurrentImageItemModel.ZoomMultiple += 0.1f;
                }
                else if (e.Delta < 0)
                {
                    CurrentImageItemModel.ZoomMultiple -= 0.1f;
                }
                var nx = (e.X - ImageShowInfo.X) / CurrentImageItemModel.ZoomMultiple;
                var ny = (e.Y - ImageShowInfo.Y) / CurrentImageItemModel.ZoomMultiple;
                LeftPoint-=new Size((int)Math.Ceiling(nx -ox),(int)Math.Ceiling(ny-oy));
                UpdateDrawingBoard();
            }
        }
        private void pbMian_MouseDown(object sender, MouseEventArgs e)
        {
            pbMian.Focus();
            if (e.Button == MouseButtons.Right && CurrentImageItemModel != null)
            {
                isDrag = true;
                var tempLabel = new ImageLabelsModel
                {
                    ImageItemModel = CurrentImageItemModel,
                    LabelId = Guid.NewGuid().ToString("N"),
                    Name = "未知",
                    X1 = e.X,
                    X2 = e.X + 1,
                    Y1 = e.Y,
                    Y2 = e.Y + 1,
                };
                CurrentImageItemModel.Labels.Add(tempLabel);
                dragImageLabelShowUserControl = AddLabelControl(CurrentImageItemModel.Labels.Count - 1);
                dragImageLabelShowUserControl.BringToFront();
                dragLabel = tempLabel;
                dragPoint = new Point(e.X, e.Y);
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
        }

        private void pbMian_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.X > dragPoint.X + CurrentImageItemModel.ZoomMultiple && e.Y > dragPoint.Y + CurrentImageItemModel.ZoomMultiple)
                    {
                        dragLabel.LabelShowRectangle = new Rectangle(new Point(dragPoint.X - ImageShowInfo.X, dragPoint.Y - ImageShowInfo.Y), new Size(e.X - dragPoint.X, e.Y - dragPoint.Y));
                        dragImageLabelShowUserControl.Init(dragLabel);
                    }
                }
            }
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
                AddLabelControl(i);
            }
        }

        private ImageLabelShowUserControl AddLabelControl(int i)
        {
            var label = CurrentImageItemModel.Labels[i];
            var labelColor = LabelColorManagers.GetLabelColor(label.Name);
            ImageLabelShowUserControl result = null;
            if (labelColor.IsSelect)
            {
                if (_imageLabelShowUserControls.Count <= i)
                {
                    var labelShowUserControl = new ImageLabelShowUserControl(this);
                    pbMian.Controls.Add(labelShowUserControl);
                    _imageLabelShowUserControls.Add(labelShowUserControl);
                }
                result = _imageLabelShowUserControls[i];
                LabelShowDictionary.Add(label, result);
                _imageLabelShowUserControls[i].Init(label);
            }
            return result;
        }

        /// <summary>
        /// 获得绘制图片
        /// </summary>
        /// <returns></returns>
        private Bitmap GetDrawBitamp()
        {
            var newBit = new Bitmap(ImageShowInfo.Width, ImageShowInfo.Height);
            var graphics = Graphics.FromImage(newBit);
            if (!LeftPoint.IsEmpty || ImageShowInfo.ZoomMultiple != CurrentImageItemModel.ZoomMultiple)
            {
                Size newSize = new Size((int)Math.Ceiling(newBit.Width / CurrentImageItemModel.ZoomMultiple), (int)Math.Ceiling(newBit.Height / CurrentImageItemModel.ZoomMultiple));
                graphics.DrawImage(_currentBitmap, new Rectangle(Point.Empty, newBit.Size), new Rectangle(LeftPoint, newSize), GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(_currentBitmap, new Rectangle(0, 0, newBit.Width, newBit.Height));
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

        private void pbMian_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            isControl = e.Control;
            isAlt = e.Alt;
        }
    }
}
