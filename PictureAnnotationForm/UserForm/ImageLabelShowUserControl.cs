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
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Drawing.Imaging;

namespace PictureAnnotationForm.UserForm
{
    public partial class ImageLabelShowUserControl : UserControl
    {
        /// <summary>
        /// 所属的LabelImageUserControl控件
        /// </summary>
        public LabelImageUserControl LabelImageUserControl;
        /// <summary>
        /// 当前标签类
        /// </summary>
        public ImageLabelsModel CurrentImageLabelsModel;
        /// <summary>
        /// 当前标签颜色
        /// </summary>
        private LabelColor _currentLabelColor;
        /// <summary>
        /// 当前绘制图片
        /// </summary>
        private Bitmap _currentDrawBitmap;
        /// <summary>
        /// 当前图片画板
        /// </summary>
        private Graphics _currentDrawGraphics;
        /// <summary>
        /// 当前操作
        /// </summary>
        private int _currentOp;
        /// <summary>
        /// 是否重绘图片
        /// </summary>
        private ConcurrentQueue<bool> _updateBitamp = new ConcurrentQueue<bool>();
        /// <summary>
        /// 当前获取焦点的标签控件
        /// </summary>
        public static ImageLabelShowUserControl CurrentImageLabelShowUserControl;
        /// <summary>
        /// 图片是否更新成功
        /// </summary>
        private bool _bitmapUpdateSuccess;
        /// <summary>
        /// 标签重命名
        /// </summary>
        private string _labelNewName;
        /// <summary>
        /// 边框宽度
        /// </summary>
        public static int BorderWidth = 2;
        public ImageLabelShowUserControl(LabelImageUserControl labelImageUserControl)
        {
            InitializeComponent();
            LabelImageUserControl = labelImageUserControl;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="imageLabelsModel"></param>
        public void Init(ImageLabelsModel imageLabelsModel)
        {
            if (imageLabelsModel == null)
            {
                Delete();
                return;
            }
            _isHighlight = false;
            CurrentImageLabelsModel = imageLabelsModel;
            _currentLabelColor = LabelColorManagers.GetLabelColor(CurrentImageLabelsModel.Name);
            SuspendLayout();
            Left = imageLabelsModel.LabelShowRectangle.Left + LabelImageUserControl.ImageShowInfo.X;
            Top = imageLabelsModel.LabelShowRectangle.Top + LabelImageUserControl.ImageShowInfo.Y;
            Width = imageLabelsModel.LabelShowRectangle.Width;
            Height = imageLabelsModel.LabelShowRectangle.Height;
            _currentDrawBitmap = new Bitmap(Width, Height);
            _currentDrawGraphics = Graphics.FromImage(_currentDrawBitmap);
            _labelNewName = CurrentImageLabelsModel.Name;
            UpdateDraw();
            ResumeLayout(true);
        }
        public void Delete()
        {
            SuspendLayout();
            Left = 0;
            Top = 0;
            Width = 0;
            Height = 0;
            ResumeLayout(true);
        }
        /// <summary>
        /// 设置当前标签突出显示
        /// </summary>
        public void SetHighlight()
        {
            _isHighlight = true;
            this.BringToFront();
            UpdateDraw();
        }
        /// <summary>
        /// 更新绘制图片
        /// </summary>
        private void UpdateDrawBitmap()
        {
            if (_currentLabelColor == null || CurrentImageLabelsModel == null)
            {
                _currentDrawGraphics.Clear(Color.Black);
                return;
            }
            var x = Left - LabelImageUserControl.ImageShowInfo.X;
            var y = Top - LabelImageUserControl.ImageShowInfo.Y;
            lock (LabelImageUserControl.CurrentDrawBitmap)
            {
                _currentDrawGraphics.DrawImage(LabelImageUserControl.CurrentDrawBitmap, new Rectangle(0, 0, Width, Height), new Rectangle(x, y, Width, Height), GraphicsUnit.Pixel);
            }
            _currentDrawGraphics.DrawString(_labelNewName, Font, _currentLabelColor.Brush, BorderWidth, BorderWidth);
            if (!string.IsNullOrWhiteSpace(CurrentImageLabelsModel.SubName))
            {
                _currentDrawGraphics.DrawString(CurrentImageLabelsModel.SubName, Font, _currentLabelColor.Brush, BorderWidth, 20);
            }
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, 0, 0, BorderWidth, Height);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, Width - BorderWidth * 2, 0, BorderWidth, Height);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, BorderWidth, 0, Width - BorderWidth * 2, BorderWidth);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, BorderWidth, Height - BorderWidth, Width - BorderWidth * 2, BorderWidth);
            var index = CurrentImageLabelsModel.ImageItemModel.Labels.IndexOf(CurrentImageLabelsModel);
            var bitmapRectangle = CurrentImageLabelsModel.LabelShowRectangle; //new Rectangle(x, y, Width, Height);
            for (int i = 0; i < CurrentImageLabelsModel.ImageItemModel.Labels.Count; i++)
            {
                if (i == index)
                {
                    continue;
                }
                var labelsModel = CurrentImageLabelsModel.ImageItemModel.Labels[i];
                var rectangle = labelsModel.LabelShowRectangle;
                var intersectRectangle = Rectangle.Intersect(rectangle, bitmapRectangle);
                var labelColor = LabelColorManagers.GetLabelColor(labelsModel.Name);
                if (!intersectRectangle.IsEmpty)
                {
                    if (intersectRectangle.X > x)
                    {
                        _currentDrawGraphics.FillRectangle(labelColor.Brush, intersectRectangle.X - x, intersectRectangle.Y - y, BorderWidth, intersectRectangle.Height);
                    }
                    if (intersectRectangle.Y > y)
                    {
                        _currentDrawGraphics.FillRectangle(labelColor.Brush, intersectRectangle.X - x, intersectRectangle.Y - y, intersectRectangle.Width, BorderWidth);
                    }
                    if (intersectRectangle.Right < bitmapRectangle.Right)
                    {
                        _currentDrawGraphics.FillRectangle(labelColor.Brush, intersectRectangle.Right - x - BorderWidth, intersectRectangle.Y - y, BorderWidth, intersectRectangle.Height);
                    }
                    if (intersectRectangle.Bottom < bitmapRectangle.Bottom)
                    {
                        _currentDrawGraphics.FillRectangle(labelColor.Brush, intersectRectangle.X - x, intersectRectangle.Bottom - y - BorderWidth, intersectRectangle.Width, BorderWidth);
                    }
                }
            }
            if (_isHighlight)
            {
                HighlightLabelImage();
            }
            _bitmapUpdateSuccess = true;
        }
        /// <summary>
        /// 控件重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_Paint(object sender, PaintEventArgs e)
        {
            if (_currentLabelColor == null || CurrentImageLabelsModel == null)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }
            if (_bitmapUpdateSuccess)
            {
                _bitmapUpdateSuccess = false;
                e.Graphics.DrawImage(_currentDrawBitmap, 0, 0);
                return;
            }
            var left = Left - LabelImageUserControl.ImageShowInfo.X;
            var x = left + e.ClipRectangle.X;
            var top = Top - LabelImageUserControl.ImageShowInfo.Y;
            var y = top + e.ClipRectangle.Y;
            e.Graphics.DrawImage(LabelImageUserControl.CurrentDrawBitmap, e.ClipRectangle, new Rectangle(x, y, e.ClipRectangle.Width, e.ClipRectangle.Height), GraphicsUnit.Pixel);
            e.Graphics.DrawString(_labelNewName, Font, _currentLabelColor.Brush, BorderWidth, BorderWidth);
            if (!string.IsNullOrWhiteSpace(CurrentImageLabelsModel.SubName))
            {
                e.Graphics.DrawString(CurrentImageLabelsModel.SubName, Font, _currentLabelColor.Brush, BorderWidth, 20);
            }
            if (e.ClipRectangle.X < BorderWidth)
            {
                e.Graphics.FillRectangle(_currentLabelColor.Brush, e.ClipRectangle.X, e.ClipRectangle.Y, BorderWidth - e.ClipRectangle.X, e.ClipRectangle.Height);
            }
            if (e.ClipRectangle.Right >= Width - BorderWidth)
            {
                e.Graphics.FillRectangle(_currentLabelColor.Brush, Width - BorderWidth, e.ClipRectangle.Y, e.ClipRectangle.Right - Width + BorderWidth, e.ClipRectangle.Height);
            }
            if (e.ClipRectangle.Y < BorderWidth)
            {
                e.Graphics.FillRectangle(_currentLabelColor.Brush, BorderWidth, e.ClipRectangle.Y, e.ClipRectangle.Right - BorderWidth, BorderWidth - e.ClipRectangle.Y);
            }
            if (e.ClipRectangle.Bottom >= Height - BorderWidth)
            {
                e.Graphics.FillRectangle(_currentLabelColor.Brush, e.ClipRectangle.X, Height - BorderWidth, e.ClipRectangle.Width, e.ClipRectangle.Bottom - Height + BorderWidth);
            }
            var index = CurrentImageLabelsModel.ImageItemModel.Labels.IndexOf(CurrentImageLabelsModel);
            for (int i = 0; i < CurrentImageLabelsModel.ImageItemModel.Labels.Count; i++)
            {
                if (i == index)
                {
                    continue;
                }
                var labelsModel = CurrentImageLabelsModel.ImageItemModel.Labels[i];
                var rectangle = labelsModel.LabelShowRectangle;
                var bitmapRectangle = new Rectangle(x, y, Width, Height);
                var intersectRectangle = Rectangle.Intersect(rectangle, bitmapRectangle);
                var labelColor = LabelColorManagers.GetLabelColor(labelsModel.Name);
                if (!intersectRectangle.IsEmpty)
                {
                    if (intersectRectangle.X > x)
                    {
                        e.Graphics.FillRectangle(labelColor.Brush, intersectRectangle.X - left, intersectRectangle.Y - top, BorderWidth, intersectRectangle.Height);
                    }
                    if (intersectRectangle.Y > y)
                    {
                        e.Graphics.FillRectangle(labelColor.Brush, intersectRectangle.X - left, intersectRectangle.Y - top, intersectRectangle.Width, BorderWidth);
                    }
                    if (intersectRectangle.Right < bitmapRectangle.Right)
                    {
                        e.Graphics.FillRectangle(labelColor.Brush, intersectRectangle.Right - left - BorderWidth, intersectRectangle.Y - top, BorderWidth, intersectRectangle.Height);
                    }
                    if (intersectRectangle.Bottom < bitmapRectangle.Bottom)
                    {
                        e.Graphics.FillRectangle(labelColor.Brush, intersectRectangle.X - left, intersectRectangle.Bottom - top - BorderWidth, intersectRectangle.Width, BorderWidth);
                    }
                }
            }
        }
        /// <summary>
        /// 是否拖动
        /// </summary>
        private bool _isDrag;
        /// <summary>
        /// 开始鼠标按下的位置
        /// </summary>
        private Point _lastDownPoint;
        /// <summary>
        /// 开始位置
        /// </summary>
        private Rectangle _initialRectangle;
        /// <summary>
        /// 是否突出显示
        /// </summary>
        private bool _isHighlight;
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentImageLabelShowUserControl = this;
            _isDrag = true;
            LabelImageUserControl?.SelectLabel(CurrentImageLabelsModel);
            _lastDownPoint = e.Location;
            _initialRectangle = new Rectangle(0, 0, Width, Height);
            this.DoubleBuffered = true;
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_MouseLeave(object sender, EventArgs e)
        {
            if (_isHighlight)
            {
                _isHighlight = false;
                UpdateDraw();
            }
            Clear();
        }
        /// <summary>
        /// 清空选中并保存最新坐标
        /// </summary>
        private void Clear()
        {
            Cursor = Cursors.Default;
            if (CurrentImageLabelsModel.Name != _labelNewName)
            {
                if (string.IsNullOrWhiteSpace(_labelNewName))
                {
                    _labelNewName = CurrentImageLabelsModel.Name;
                }
                else
                {
                    CurrentImageLabelsModel.Name = _labelNewName;
                    ImageManagers.UpdateLabelName(CurrentImageLabelsModel);
                }
            }
            if (_isDrag)
            {
                LabelImageUserControl?.SelectLabel(CurrentImageLabelsModel);
            }
            _isDrag = false;
            if (CurrentImageLabelsModel.IsHide)
            {
                CurrentImageLabelsModel.IsHide = false;
                var x = (int)Math.Round((Left - LabelImageUserControl.ImageShowInfo.X) / LabelImageUserControl.CurrentImageItemModel.ZoomMultiple) - CurrentImageLabelsModel.X1;
                var y = (int)Math.Round((Top - LabelImageUserControl.ImageShowInfo.Y) / LabelImageUserControl.CurrentImageItemModel.ZoomMultiple) - CurrentImageLabelsModel.Y1;
                var width = (int)Math.Round(Width / LabelImageUserControl.CurrentImageItemModel.ZoomMultiple);
                var heigth = (int)Math.Round(Height / LabelImageUserControl.CurrentImageItemModel.ZoomMultiple);
                CurrentImageLabelsModel.X1 += x;
                CurrentImageLabelsModel.X2 = CurrentImageLabelsModel.X1 + width;
                CurrentImageLabelsModel.Y1 += y;
                CurrentImageLabelsModel.Y2 = CurrentImageLabelsModel.Y1 + heigth;
                LabelImageUserControl.UpdateDrawingBoard();
            }
            this.DoubleBuffered = false;

        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            Clear();
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrag && e.Button == MouseButtons.Left)
            {
                if (!CurrentImageLabelsModel.IsHide)
                {
                    CurrentImageLabelsModel.IsHide = true;
                    LabelImageUserControl.UpdateBitmap();
                }
                switch (_currentOp)
                {
                    case 1:
                        Left += e.X - _lastDownPoint.X;
                        Width -= e.X - _lastDownPoint.X;
                        Top += e.Y - _lastDownPoint.Y;
                        Height -= e.Y - _lastDownPoint.Y;
                        break;
                    case 2:
                        Left += e.X - _lastDownPoint.X;
                        Width -= e.X - _lastDownPoint.X;
                        Height = _initialRectangle.Height + e.Y - _lastDownPoint.Y;
                        break;
                    case 3:
                        Top += e.Y - _lastDownPoint.Y;
                        Height -= e.Y - _lastDownPoint.Y;
                        Width = _initialRectangle.Width + e.X - _lastDownPoint.X;
                        break;
                    case 4:
                        Height = _initialRectangle.Height + e.Y - _lastDownPoint.Y;
                        Width = _initialRectangle.Width + e.X - _lastDownPoint.X;
                        break;
                    case 5:
                        Left += e.X - _lastDownPoint.X;
                        Width -= e.X - _lastDownPoint.X;
                        break;
                    case 6:
                        Top += e.Y - _lastDownPoint.Y;
                        Height -= e.Y - _lastDownPoint.Y;
                        break;
                    case 7:
                        Width = _initialRectangle.Width + e.X - _lastDownPoint.X;
                        break;
                    case 8:
                        Height = _initialRectangle.Height + e.Y - _lastDownPoint.Y;
                        break;
                    default:
                        Left += e.X - _lastDownPoint.X;
                        Top += e.Y - _lastDownPoint.Y;
                        break;
                }
                Invalidate();
            }
            else
            {
                if (e.X < 5 && e.Y < 5)
                {//左上角
                    _currentOp = 1;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (e.X < 5 && e.Y > Height - 5)
                { //左下角
                    _currentOp = 2;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.X > Width - 5 && e.Y < 5)
                {//右上角
                    _currentOp = 3;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.X > Width - 5 && e.Y > Height - 5)
                { //右下角
                    _currentOp = 4;
                    Cursor = Cursors.SizeNWSE;

                }
                else if (e.X < 5)
                {//左
                    _currentOp = 5;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Y < 5)
                {//上
                    _currentOp = 6;
                    Cursor = Cursors.SizeNS;
                }
                else if (e.X > Width - 5)
                {//右
                    Cursor = Cursors.SizeWE;
                    _currentOp = 7;
                }
                else if (e.Y > Height - 5)
                {//下
                    Cursor = Cursors.SizeNS;
                    _currentOp = 8;
                }
                else
                {
                    Cursor = Cursors.Hand;
                    _currentOp = 0;
                }
            }
        }
        /// <summary>
        /// 开始拖动重绘
        /// </summary>
        private void UpdateBitmap()
        {
            Task.Factory.StartNew(() =>
            {
                while (_isDrag)
                {
                    if (_updateBitamp.TryDequeue(out _))
                    {
                        UpdateDraw();
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            });
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_MouseEnter(object sender, EventArgs e)
        {
            _isHighlight = true;
            BringToFront();
            Cursor = Cursors.Hand;
            UpdateDraw();
        }

        private void UpdateDraw()
        {
            UpdateDrawBitmap();
            Invalidate();
        }

        /// <summary>
        /// 突出标签图片
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private void HighlightLabelImage()
        {
            BitmapData data = null;
            try
            {
                //获取图像的BitmapData对像 
                data = _currentDrawBitmap.LockBits(new Rectangle(0, 0, _currentDrawBitmap.Width, _currentDrawBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                //循环处理 
                unsafe
                {
                    byte* ptr = (byte*)(data.Scan0);
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            int r = *ptr;
                            int g = *(ptr + 1);
                            int b = *(ptr + 2);
                            *(ptr++) = (byte)((_currentLabelColor.Color.R + r) / 2);
                            *(ptr++) = (byte)((_currentLabelColor.Color.G + g) / 2);
                            *(ptr++) = (byte)((_currentLabelColor.Color.B + b) / 2);
                        }
                        ptr += data.Stride - data.Width * 3;
                    }
                }
            }
            finally
            {
                _currentDrawBitmap.UnlockBits(data);
            }
        }
        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLabelShowUserControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (CurrentImageLabelShowUserControl == null || CurrentImageLabelShowUserControl.CurrentImageLabelsModel == null)
            {
                return;
            }
            if (e.KeyValue >= 37 && e.KeyValue <= 40)
            {
                var x = LabelImageUserControl.CurrentImageItemModel.ZoomMultiple;
                if (e.KeyCode == Keys.Left)
                {
                    Cursor.Position = new Point((int)Math.Round(Cursor.Position.X - x), Cursor.Position.Y);
                    if (e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 -= 1;
                    }
                    else if (e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 += 1;
                    }
                    else
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 -= 1;
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 -= 1;
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    Cursor.Position = new Point((int)Math.Round(Cursor.Position.X + x), Cursor.Position.Y);
                    if (e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 += 1;
                    }
                    else if (e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 -= 1;
                    }
                    else
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 += 1;
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 += 1;
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    Cursor.Position = new Point(Cursor.Position.X, (int)Math.Round(Cursor.Position.Y - x));
                    if (e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 -= 1;
                    }
                    else if (e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 += 1;
                    }
                    else
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 -= 1;
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 -= 1;
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    Cursor.Position = new Point(Cursor.Position.X, (int)Math.Round(Cursor.Position.Y + x));
                    if (e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 += 1;
                    }
                    else if (e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 -= 1;
                    }
                    else
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 += 1;
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 += 1;
                    }
                }
                CurrentImageLabelShowUserControl.Init(CurrentImageLabelShowUserControl.CurrentImageLabelsModel);
                this.LabelImageUserControl?.SelectLabel(CurrentImageLabelShowUserControl.CurrentImageLabelsModel);
            }
            else if (e.Control)
            {
                if ((e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 96 && e.KeyValue <= 105))
                {
                    var data = e.KeyCode.ToString();
                    if (int.TryParse(data.Substring(data.Length - 1), out int num))
                    {
                        if (num == 0)
                        {
                            num = 10;
                        }
                        if (num > ImageManagers.LabelNames.Count)
                        {
                            return;
                        }
                        _labelNewName = ImageManagers.LabelNames[num - 1];
                        CurrentImageLabelsModel.Name = _labelNewName;
                        this._currentLabelColor = LabelColorManagers.GetLabelColor(_labelNewName);
                        UpdateDraw();
                        this.LabelImageUserControl?.SelectLabel(CurrentImageLabelsModel);
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    LabelImageUserControl.CurrentImageItemModel.Labels.Remove(CurrentImageLabelsModel);
                    this.Delete();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    _labelNewName = _labelNewName.Remove(_labelNewName.Length - 1);
                    UpdateDraw();
                }
                else if ((e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 65 && e.KeyValue <= 90) || (e.KeyValue >= 96 && e.KeyValue <= 105))
                {
                    var data = e.KeyCode.ToString();
                    if ((Console.CapsLock && e.Shift) || (!Console.CapsLock && !e.Shift))
                    {
                        data = data.ToLower();
                    }
                    _labelNewName += data.Substring(data.Length - 1);
                    UpdateDraw();
                }
            }
        }
    }
}
