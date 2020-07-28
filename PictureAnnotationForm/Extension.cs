using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PictureAnnotationForm
{
    public static class Extension
    {
        #region 字符串操作
        public static int StringToInt(this string str)
        {
            int.TryParse(str, out int result);
            return result;
        }
        #endregion
        #region 反射
        /// <summary>
        /// 获得私有字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static T GetPrivateField<T>(this object instance, string fieldname)
        {

            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }
        /// <summary>
        /// 获得私有属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static T GetPrivateProperty<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            return (T)field.GetValue(instance, null);
        }
        /// <summary>
        /// 设置私有字段
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <param name="value"></param>
        public static void SetPrivateField(this object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(instance, value);
        }
        /// <summary>
        /// 设置私有属性
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyname"></param>
        /// <param name="value"></param>
        public static void SetPrivateProperty(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            field.SetValue(instance, value, null);
        }
        /// <summary>
        /// 执行私有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T CallPrivateMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(instance, param);
        }
        #endregion
        #region 控件操作
        /// <summary>
        /// 获得PictureBox控件显示的图片宽高
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        public static ImageShowInfo GetImageShowInfo(this PictureBox pictureBox, Image image)
        {
            if (image != null)
            {
                ImageShowInfo result = new ImageShowInfo
                {
                    ImageHeight = image.Height,
                    ImageWidth = image.Width,
                    Width = image.Width,
                    Height = image.Height,
                    ZoomMultiple = 1,
                };

                float num = Math.Min((float)pictureBox.ClientRectangle.Width / (float)result.ImageWidth, (float)pictureBox.ClientRectangle.Height / (float)result.ImageHeight);
                result.ZoomMultiple = num;
                result.Width = (int)((float)result.ImageWidth * num);
                result.Height = (int)((float)result.ImageHeight * num);
                result.X = (pictureBox.ClientRectangle.Width - result.Width) / 2;
                result.Y = (pictureBox.ClientRectangle.Height - result.Height) / 2;
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获得PictureBox控件显示的图片宽高
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        public static ImageShowInfo GetImageShowInfo(this PictureBox pictureBox)
        {
            return GetImageShowInfo(pictureBox, pictureBox.Image);
        }
        #endregion
        #region 图片操作
        /// <summary>
        /// 图片转Color数组
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Color[,] ToColorArray(this Bitmap bitmap)
        {
            Color[,] result = new Color[bitmap.Width, bitmap.Height];
            BitmapData data = null;
            try
            {
                //获取图像的BitmapData对像 
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                //循环处理 
                unsafe
                {
                    byte* ptr = (byte*)(data.Scan0);
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            result[j, i] = Color.FromArgb(*(ptr++), *(ptr++), *(ptr++));
                        }
                        ptr += data.Stride - data.Width * 3;
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return result;
        }
        /// <summary>
        /// 图片转Color数组
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this Color[,] colors)
        {
            Bitmap bitmap = new Bitmap(colors.GetLength(0), colors.GetLength(1));
            BitmapData data = null;
            try
            {
                //获取图像的BitmapData对像 
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                Color color;
                //循环处理 
                unsafe
                {
                    byte* ptr = (byte*)(data.Scan0);
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            color = colors[j, i];
                            *(ptr++) = color.R;
                            *(ptr++) = color.G;
                            *(ptr++) = color.B;
                        }
                        ptr += data.Stride - data.Width * 3;
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }
        #endregion
    }
}
