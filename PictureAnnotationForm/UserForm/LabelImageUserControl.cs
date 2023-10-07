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
            pbMain.MouseWheel += PbMian_MouseWheel;
            pbMain.KeyDown += PbMian_KeyDown;
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
        /// <summary>
        /// 选择标签修改
        /// </summary>
        public event Action LabelUpdateEvent;
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
        /// <summary>
        /// 图片左坐标
        /// </summary>
        public Point LeftPoint = Point.Empty;
        /// <summary>
        /// 是否填充所有标签
        /// </summary>
        public bool IsFullLabel;
        /// <summary>
        /// 是否检查边框(显示原图边框(固定边框))
        /// </summary>
        public bool IsCheckBox;
        /// <summary>
        /// 是否显示单项标签
        /// </summary>
        public bool IsShowLabel;
        /// <summary>
        /// 显示单项标签索引
        /// </summary>
        public int ShowLabelIndex;
        /// <summary>
        /// 当前单项显示的标签
        /// </summary>
        public ImageLabelsModel ShowLabels;
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
            _currentLabelBitmap = null;
            LeftPoint = Point.Empty;
            UpdatePbMain();
            LabelChange?.Invoke(null);
        }

        private void UpdatePbMain()
        {
            ImageShowInfo = pbMain.GetImageShowInfo(_currentBitmap);
            CurrentImageItemModel.ZoomMultiple = ImageShowInfo.ZoomMultiple;
            UpdateDrawingBoard();
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
            pbMain.Image = CurrentDrawBitmap;
            InitImageLabelShowUserControl();
        }
        public void UpdateLabelChange(ImageLabelsModel imageLabels)
        {
            if (CurrentImageItemModel == null)
            {
                return;
            }
            if (IsShowLabel&&imageLabels==ShowLabels)
            {
                ShowMaxLabel(imageLabels);
            }
            else
            {
                UpdateDrawingBoard();
            }
        }
        public void LabelUpdate()
        {
            if (CurrentImageItemModel == null)
            {
                return;
            }
            LabelUpdateEvent?.Invoke();
        }
        /// <summary>
        /// 更新图片
        /// </summary>
        public void UpdateBitmap()
        {
            CurrentDrawBitmap = GetDrawBitamp();
            pbMain.Image = CurrentDrawBitmap;
        }
        /// <summary>
        /// 触发选中标签事件
        /// </summary>
        /// <param name="label"></param>
        public void SelectLabel(ImageLabelsModel label)
        {
            LabelChange?.Invoke(label);
        }
        /// <summary>
        /// 初始化倍数和坐标
        /// </summary>
        public void InitZoomMultiple()
        {
            CurrentImageItemModel.ZoomMultiple = ImageShowInfo.ZoomMultiple;
            LeftPoint = Point.Empty;
            UpdateDrawingBoard();
        }
        /// <summary>
        /// 标签是否填充
        /// </summary>
        public void FullLabel()
        {
            IsFullLabel = !IsFullLabel;
            UpdateDrawingBoard();
        }
        /// <summary>
        /// 检查边框
        /// </summary>
        public void CheckBox()
        {
            IsCheckBox = !IsCheckBox;
            UpdateDrawingBoard();
        }
        /// <summary>
        /// 是否单项显示
        /// </summary>
        public void ShowLabel()
        {
            IsShowLabel = !IsShowLabel;
            UpdateDrawingBoard();
        }
        #endregion

        #region 控件触发事件
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (IsShowLabel)
            {
                if(ShowLabelIndex>= CurrentImageItemModel.Labels.Count - 1)
                {
                    ShowLabelIndex = 0;
                    if (CurrentImageItemModel.Labels.Count > 0 && CurrentImageItemModel.CompleteLevel == 0)
                    {
                        CurrentImageItemModel.CompleteLevel = 1;
                    }
                    ImageNext?.Invoke();
                }
                else
                {
                    var lable = CurrentImageItemModel.Labels[ShowLabelIndex++];
                    ShowMaxLabel(lable);
                    SelectLabel(lable);
                }
            }
            else
            {
                if (CurrentImageItemModel.Labels.Count > 0 && CurrentImageItemModel.CompleteLevel == 0)
                {
                    CurrentImageItemModel.CompleteLevel = 1;
                }
                ImageNext?.Invoke();
            }
        }

        private void ShowMaxLabel(ImageLabelsModel lable)
        {
            ShowLabels=lable;
            LeftPoint = new Point(lable.X1, lable.Y1);
            var zm = Math.Min(ImageShowInfo.Width*1.0 / lable.Width, ImageShowInfo.Height * 1.0 / lable.Height);
            CurrentImageItemModel.ZoomMultiple = (float)zm;
            UpdateDrawingBoard();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (IsShowLabel)
            {
                if (ShowLabelIndex < 0)
                {
                    ShowLabelIndex = CurrentImageItemModel.Labels.Count-1;
                    if (CurrentImageItemModel.Labels.Count > 0 && CurrentImageItemModel.CompleteLevel == 0)
                    {
                        CurrentImageItemModel.CompleteLevel = 1;
                    }
                    ImageLast?.Invoke();
                }
                else
                {
                    var lable = CurrentImageItemModel.Labels[ShowLabelIndex--];
                    ShowMaxLabel(lable);
                    SelectLabel(lable);
                }
            }
            else
            {
                if (CurrentImageItemModel.Labels.Count > 0&& CurrentImageItemModel.CompleteLevel==0)
                {
                    CurrentImageItemModel.CompleteLevel = 1;
                }
                ImageLast?.Invoke();
            }
        }
        bool isDrag = false;
        ImageLabelsModel dragLabel;
        ImageLabelShowUserControl dragImageLabelShowUserControl;
        Point dragPoint;
        bool isControl = false;
        DateTime lastControlDt = DateTime.MinValue;
        DateTime lastAltDt = DateTime.MinValue;
        bool isAlt = false;
        DateTime lastDt = DateTime.MinValue;
        private void PbMian_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isControl && e.Delta != 0 && (DateTime.Now - lastControlDt).TotalMilliseconds < 50 && (DateTime.Now - lastDt).TotalMilliseconds > 50)
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
                LeftPoint -= new Size((int)Math.Ceiling(nx - ox), (int)Math.Ceiling(ny - oy));
                UpdateDrawingBoard();
            }
        }
        private void PbMian_KeyDown(object sender, KeyEventArgs e)
        {
            isControl = e.Control;
            if (isControl)
            {
                lastControlDt = DateTime.Now;
            }
            isAlt = e.Alt;
            if (isAlt)
            {
                lastAltDt = DateTime.Now;
            }
        }
        Point dragLeftPoint = Point.Empty;
        DateTime lastDragLeftDt = DateTime.Now;
        private void pbMian_MouseDown(object sender, MouseEventArgs e)
        {
            pbMain.Focus();
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
            else if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                dragPoint = new Point(e.X, e.Y);
                dragLeftPoint = LeftPoint;
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
                else if (e.Button == MouseButtons.Left && (DateTime.Now - lastDragLeftDt).TotalMilliseconds > 100)
                {
                    var x = (e.X - dragPoint.X) / CurrentImageItemModel.ZoomMultiple;
                    var y = (e.Y - dragPoint.Y) / CurrentImageItemModel.ZoomMultiple;
                    LeftPoint = new Point(dragLeftPoint.X - (int)Math.Round(x), dragLeftPoint.Y - (int)Math.Round(y));
                    UpdateDrawingBoard();
                    pbMain.Update();
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
        /// 当前带标签的图片
        /// </summary>
        private Bitmap _currentLabelBitmap;
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
            if (IsCheckBox)
            {
                for (int i = 0; i < _imageLabelShowUserControls.Count; i++)
                {
                    _imageLabelShowUserControls[i].Delete();
                }
                return;
            }
            if (_imageLabelShowUserControls.Count < CurrentImageItemModel.Labels.Count)
            {
                for (int i = _imageLabelShowUserControls.Count - 1; i < CurrentImageItemModel.Labels.Count; i++)
                {
                    var labelShowUserControl = new ImageLabelShowUserControl(this);
                    pbMain.Controls.Add(labelShowUserControl);
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
                    pbMain.Controls.Add(labelShowUserControl);
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
            var bit = _currentBitmap;
            if (IsCheckBox)
            {
                if (_currentLabelBitmap == null)
                {
                    _currentLabelBitmap = new Bitmap(_currentBitmap);
                    Graphics graphics1 = Graphics.FromImage(_currentLabelBitmap);
                    for (int i = 0; i < CurrentImageItemModel.Labels.Count; i++)
                    {
                        var label = CurrentImageItemModel.Labels[i];
                        var labelColor = LabelColorManagers.GetLabelColor(label.Name);
                        graphics1.DrawString(label.Name, Font, labelColor.Brush, label.X1 + ImageLabelShowUserControl.BorderWidth, label.Y1 + ImageLabelShowUserControl.BorderWidth);

                        graphics1.FillRectangle(labelColor.Brush, label.X1, label.Y1, label.Width, ImageLabelShowUserControl.BorderWidth);

                        graphics1.FillRectangle(labelColor.Brush, label.X1 + label.Width - ImageLabelShowUserControl.BorderWidth, label.Y1, ImageLabelShowUserControl.BorderWidth, label.Height);

                        graphics1.FillRectangle(labelColor.Brush, label.X1, label.Y2 - ImageLabelShowUserControl.BorderWidth, label.Width - ImageLabelShowUserControl.BorderWidth * 2, ImageLabelShowUserControl.BorderWidth);

                        graphics1.FillRectangle(labelColor.Brush,
                            label.X1,
                            label.Y1 + ImageLabelShowUserControl.BorderWidth,
                            ImageLabelShowUserControl.BorderWidth, label.Height - ImageLabelShowUserControl.BorderWidth * 2);
                    }
                }
                bit = _currentLabelBitmap;
            }
            if (!LeftPoint.IsEmpty || ImageShowInfo.ZoomMultiple != CurrentImageItemModel.ZoomMultiple)
            {
                Size newSize = new Size((int)Math.Ceiling(newBit.Width / CurrentImageItemModel.ZoomMultiple), (int)Math.Ceiling(newBit.Height / CurrentImageItemModel.ZoomMultiple));
                graphics.DrawImage(bit, new Rectangle(Point.Empty, newBit.Size), new Rectangle(LeftPoint, newSize), GraphicsUnit.Pixel);
            }
            else
            {
                graphics.DrawImage(bit, new Rectangle(0, 0, newBit.Width, newBit.Height));
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

        }

        private void pbMian_MouseEnter(object sender, EventArgs e)
        {
            pbMain.Focus();
        }

        private void pbMian_SizeChanged(object sender, EventArgs e)
        {
            if (CurrentImageItemModel == null)
            {
                return;
            }
            UpdatePbMain();
        }
    }
}
