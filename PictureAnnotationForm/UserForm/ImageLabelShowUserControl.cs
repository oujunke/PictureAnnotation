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
        public ImageLabelsModel CurrentImageLabelsModel;
        private LabelColor _currentLabelColor;
        private Bitmap _currentDrawBitmap;
        private Graphics _currentDrawGraphics;
        private ConcurrentQueue<bool> _updateBitamp = new ConcurrentQueue<bool>();
        public static ImageLabelShowUserControl CurrentImageLabelShowUserControl;
        /// <summary>
        /// 图片是否更新成功
        /// </summary>
        private bool _bitmapUpdateSuccess;
        /// <summary>
        /// 边框宽度
        /// </summary>
        public static int BorderWidth = 2;
        public ImageLabelShowUserControl(LabelImageUserControl labelImageUserControl)
        {
            InitializeComponent();
            LabelImageUserControl = labelImageUserControl;
        }
        public void Init(ImageLabelsModel imageLabelsModel)
        {
            if (imageLabelsModel == null)
            {
                Delete();
                return;
            }
            CurrentImageLabelsModel = imageLabelsModel;
            CurrentImageLabelsModel.ZoomMultiple = LabelImageUserControl.ImageShowInfo.ZoomMultiple;
            _currentLabelColor = LabelColorManagers.GetLabelColor(CurrentImageLabelsModel.Name);
            SuspendLayout();
            Left = imageLabelsModel.LabelShowRectangle.Left + LabelImageUserControl.ImageShowInfo.X;
            Top = imageLabelsModel.LabelShowRectangle.Top + LabelImageUserControl.ImageShowInfo.Y;
            Width = imageLabelsModel.LabelShowRectangle.Width;
            Height = imageLabelsModel.LabelShowRectangle.Height;
            _currentDrawBitmap = new Bitmap(Width, Height);
            _currentDrawGraphics = Graphics.FromImage(_currentDrawBitmap);
            UpdateDrawBitmap();
            Invalidate();
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
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, 0, 0, BorderWidth, Height);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, Width - BorderWidth * 2, 0, BorderWidth, Height);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, BorderWidth, 0, Width - BorderWidth * 2, BorderWidth);
            _currentDrawGraphics.FillRectangle(_currentLabelColor.Brush, BorderWidth, Height - BorderWidth, Width - BorderWidth * 2, BorderWidth);
            var index = CurrentImageLabelsModel.ImageItemModel.Labels.IndexOf(CurrentImageLabelsModel);
            var bitmapRectangle = new Rectangle(x, y, Width, Height);
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
        private void ImageLabelShowUserControl_Paint(object sender, PaintEventArgs e)
        {
            if (_currentLabelColor == null || CurrentImageLabelsModel == null)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }
            UpdateBitmap();
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
        private Point _lastDownPoint;
        private bool _isHighlight;
        private void ImageLabelShowUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentImageLabelShowUserControl = this;
            _isDrag = true;
            LabelImageUserControl?.SelectLabel(CurrentImageLabelsModel);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                if (_isDrag)
                {
                    Invoke(new Action(() => {
                        Cursor = Cursors.Hand;
                    }));
                }
            });
            _lastDownPoint = e.Location;
            this.DoubleBuffered = true;
            //UpdateBitmap();
        }

        private void ImageLabelShowUserControl_MouseLeave(object sender, EventArgs e)
        {
            if (_isHighlight)
            {
                _isHighlight = false;
                UpdateDrawBitmap();
                Invalidate();
            }
            Clear();
        }

        private void Clear()
        {
            Cursor = Cursors.Default;
            _isDrag = false;
            if (CurrentImageLabelsModel.IsHide)
            {
                CurrentImageLabelsModel.IsHide = false;
                var x = (int)Math.Round(((Left - LabelImageUserControl.ImageShowInfo.X) / CurrentImageLabelsModel.ZoomMultiple)) - CurrentImageLabelsModel.X1;
                var y = (int)Math.Round((Top - LabelImageUserControl.ImageShowInfo.Y) / CurrentImageLabelsModel.ZoomMultiple) - CurrentImageLabelsModel.Y1;
                CurrentImageLabelsModel.X1 += x;
                CurrentImageLabelsModel.X2 += x;
                CurrentImageLabelsModel.Y1 += y;
                CurrentImageLabelsModel.Y2 += y;
                LabelImageUserControl.UpdateDrawingBoard();
            }
            this.DoubleBuffered = false;
            LabelImageUserControl?.SelectLabel(CurrentImageLabelsModel);
        }

        private void ImageLabelShowUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            Clear();
        }
        private void ImageLabelShowUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrag && e.Button == MouseButtons.Left)
            {
                if (!CurrentImageLabelsModel.IsHide)
                {
                    CurrentImageLabelsModel.IsHide = true;
                    LabelImageUserControl.UpdateBitmap();
                }
                Left += e.X - _lastDownPoint.X;
                Top += e.Y - _lastDownPoint.Y;
                Invalidate();
            }
        }

        private void UpdateBitmap()
        {
            Task.Factory.StartNew(() =>
            {
                while (_isDrag)
                {
                    if (_updateBitamp.TryDequeue(out _))
                    {
                        UpdateDrawBitmap();
                        Invalidate();
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            });
        }

        private void ImageLabelShowUserControl_MouseEnter(object sender, EventArgs e)
        {
            _isHighlight = true;
            this.BringToFront();
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

        private void ImageLabelShowUserControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (CurrentImageLabelShowUserControl == null || CurrentImageLabelShowUserControl.CurrentImageLabelsModel == null)
            {
                return;
            }
            if ((int)e.KeyCode>=37&& (int)e.KeyCode<=40)
            {
                var x = CurrentImageLabelShowUserControl.CurrentImageLabelsModel.ZoomMultiple;
                if (e.KeyCode == Keys.Left)
                {
                    Cursor.Position = new Point((int)Math.Round(Cursor.Position.X - x), Cursor.Position.Y);
                    if (!e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 -= 1;
                    }
                    if (!e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 -= 1;
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    Cursor.Position = new Point((int)Math.Round(Cursor.Position.X + x), Cursor.Position.Y);
                    if (!e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X1 += 1;
                    }
                    if (!e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.X2 += 1;
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    Cursor.Position = new Point(Cursor.Position.X, (int)Math.Round(Cursor.Position.Y - x));
                    if (!e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 -= 1;
                    }
                    if (!e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 -= 1;
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    Cursor.Position = new Point(Cursor.Position.X, (int)Math.Round(Cursor.Position.Y + x));
                    if (!e.Alt)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y1 += 1;
                    }
                    if (!e.Control)
                    {
                        CurrentImageLabelShowUserControl.CurrentImageLabelsModel.Y2 += 1;
                    }
                    
                   
                    
                }
                CurrentImageLabelShowUserControl.Init(CurrentImageLabelShowUserControl.CurrentImageLabelsModel);
                this.LabelImageUserControl?.SelectLabel(CurrentImageLabelShowUserControl.CurrentImageLabelsModel);
            }
        }
    }
}
