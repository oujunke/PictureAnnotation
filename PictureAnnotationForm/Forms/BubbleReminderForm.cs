using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotationForm.Forms
{
    /// <summary>
    /// 气泡提示
    /// </summary>
    public partial class BubbleReminderForm : Form
    {
        /// <summary>
        /// 存放所有气泡提示控件的列表
        /// </summary>
        public static List<BubbleReminderForm> BubbleReminderFormList = new List<BubbleReminderForm>();
        /// <summary>
        /// 当前背景图片
        /// </summary>
        public static Bitmap BackBitmap = Properties.Resources._6f2fda560f1adc2eff6af8c4ff697a8a;
        /// <summary>
        /// 当前窗口
        /// </summary>
        private static Form _currentForm;
        private static bool _isWindow = true;
        private string _data;
        private static object _lockObj = new object();
        //private int _top;
        private DateTime _showDateTime;
        private DateTime _hideDateTime;
        private int _showTime;
        private static Task _task;
        private static int _screenHeight;
        private static int _screenWidth;
        public static void InitForm(Form form = null, bool? isWindow = null)
        {
            if (form == null && _currentForm == null)
            {
                _currentForm = Form.ActiveForm;
            }else if (form != null)
            {
                _currentForm = form;
            }
            _isWindow = isWindow ?? _isWindow;
            if (_currentForm == null)
            {
                return;
            }
            if (_isWindow)
            {
                var screenArea = Screen.GetWorkingArea(form);
                _screenHeight = screenArea.Height;
                _screenWidth = screenArea.Width;
            }
            else
            {
                _screenHeight = _currentForm.Bottom;
                _screenWidth = _currentForm.Right;
            }
        }
        public static void ShowMsg(string txt, int timer=2000)
        {
            InitForm();
            lock (_lockObj)
            {
                int bottom = 0;
                int right = 0;
                _currentForm.Invoke(new Action(() =>
                {
                    var reminderForm = new BubbleReminderForm();
                    reminderForm._showTime = timer;
                    if (BubbleReminderFormList.Count == 0)
                    {
                        bottom = _screenHeight;
                        right = _screenWidth;
                    }
                    else
                    {
                        var bubbleReminderForm = BubbleReminderFormList[BubbleReminderFormList.Count - 1];
                        bottom = bubbleReminderForm.Top;
                        right = bubbleReminderForm.Right;
                    }
                    reminderForm.BackgroundImage = BackBitmap;
                    BubbleReminderFormList.Add(reminderForm);
                    var width = _screenWidth / 2;
                    var graphics = Graphics.FromHwnd(reminderForm.Handle);
                    var sizef = graphics.MeasureString(txt, reminderForm.Font);
                    reminderForm._data = txt;
                    graphics.Dispose();
                    if (sizef.Width * 1.5 < width)
                    {
                        reminderForm.Width = (int)(sizef.Width * 1.5);
                        reminderForm.Height = (int)(sizef.Height * 2);
                    }
                    //reminderForm._top = bottom - reminderForm.Height;
                    reminderForm.Top = _screenHeight;
                    reminderForm.Left = right - reminderForm.Width;

                    reminderForm.Show();
                }));
                if (_task.IsCompleted)
                {
                    _task = _task.ContinueWith(r => RefreshPrompt());
                }
            }
        }
        public BubbleReminderForm()
        {
            InitializeComponent();
        }
        static BubbleReminderForm()
        {
            for (int i = 0; i < BackBitmap.Width; i++)
            {
                for (int j = 0; j < BackBitmap.Height; j++)
                {
                    var color = BackBitmap.GetPixel(i, j);
                    if (color.R > 220 && color.G > 220 && color.B > 220)
                    {
                        BackBitmap.SetPixel(i, j, Color.White);
                    }
                }
            }
            _task = Task.Factory.StartNew(() => RefreshPrompt());
        }
        private static void RefreshPrompt()
        {
            while (BubbleReminderFormList.Count > 0)
            {
                try
                {
                    var lastBotton = _screenHeight;
                    for (int i = BubbleReminderFormList.Count - 1; i >= 0; i--)
                    {
                        var bubbleReminderForm = BubbleReminderFormList[i];
                        #region 上下移动控件
                        if (bubbleReminderForm.Bottom - bubbleReminderForm.Height > lastBotton)
                        {
                            bubbleReminderForm.Invoke(new Action(() => bubbleReminderForm.Top = lastBotton - bubbleReminderForm.Height));
                        }
                        else if (bubbleReminderForm.Bottom > lastBotton)
                        {
                            bubbleReminderForm.Invoke(new Action(() => bubbleReminderForm.Top--));
                        }
                        else if (bubbleReminderForm.Bottom < lastBotton)
                        {
                            bubbleReminderForm.Invoke(new Action(() => bubbleReminderForm.Top += 2));
                        }
                        else if (bubbleReminderForm._showDateTime == DateTime.MinValue)
                        {
                            bubbleReminderForm._showDateTime = DateTime.Now;
                            bubbleReminderForm._hideDateTime = bubbleReminderForm._showDateTime.AddMilliseconds(bubbleReminderForm._showTime);
                        }
                        lastBotton = bubbleReminderForm.Top;
                        #endregion
                        #region 右移控件
                        if (bubbleReminderForm._showDateTime != DateTime.MinValue && DateTime.Now >= bubbleReminderForm._hideDateTime)
                        {
                            if (bubbleReminderForm.Left >= _screenWidth)
                            {
                                BubbleReminderFormList.Remove(bubbleReminderForm);
                                bubbleReminderForm.Invoke(new Action(() => bubbleReminderForm.Close()));
                            }
                            else
                            {
                                bubbleReminderForm.Invoke(new Action(() => bubbleReminderForm.Left += 3));
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    Thread.Sleep(10);
                }
            }
        }
        private void BubbleReminderForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(_data, Font, Brushes.Black, (int)(Width / 5), (int)(Height / 4));
        }
    }
}
