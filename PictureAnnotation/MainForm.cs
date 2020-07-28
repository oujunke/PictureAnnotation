using PictureAnnotation.BLL;
using PictureAnnotation.Models;
using PictureAnnotation.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotation
{
    public partial class MainForm : Form
    {
        private int _loadImgIndex;
        private int _listImgIndex;
        private string _listImgKey;
        private int _ilImgIndex;
        private bool _lvLableUpdate;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //var fileInfos = File.ReadAllLines("ImageNet/train_list.txt");
            //List<string> paths = new List<string>();
            //foreach (var item in fileInfos)
            //{
            //    var items = item.Split('\t');
            //    var path = $"ImageNet/{items[0]}";
            //    if (!File.Exists(path))
            //    {
            //        paths.Add(path);
            //    }
            //}
            //foreach (var item in File.ReadAllLines("ImageNet/val_list.txt"))
            //{
            //    var items = item.Split('\t');
            //    var path = $"ImageNet/{items[0]}";
            //    if (!File.Exists(path))
            //    {
            //        paths.Add(path);
            //    }
            //}
            var num = ImageManagers.LoadVocDirectory(@"VOCYolo100");
            RefreshLable();
            if (num > 20 && ilMain.Images.Count < 10)
            {
                AddImageItem(60 - ilMain.Images.Count);
            }
            //pbMian.Image = GetDrawBitamp("01d1990a30a62459dda5b8611c62ffccc3878b4682a6ea478408b6752c52a0d8");
            //pbMian.Image = ImageManagers.GetImageList()[0].Image;
        }
        private void RefreshLable()
        {
            _lvLableUpdate = true;
            lvLables.BeginUpdate();
            lvLables.Items.Clear();
            foreach (var name in ImageManagers.LableNames)
            {
                var lableColor = LableColorManagers.GetLableColor(name);
                var listViewItem = lvLables.Items.Add(name);
                listViewItem.SubItems.Add(lableColor.Name);
                listViewItem.SubItems.Add(lableColor.IsFill ? "填充" : "不填充");
                listViewItem.Checked = lableColor.IsSelect;
            }
            lvLables.EndUpdate();
            _lvLableUpdate = false;
        }
        private void msMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var num = ImageManagers.LoadVocDirectory(fbdOpenFolder.SelectedPath);
            RefreshLable();
            if (num > 10 && ilMain.Images.Count < 10)
            {
                AddImageItem(20 - ilMain.Images.Count);
            }
            MessageBox.Show($"数据集加载成功,当前加载数据{num}条,共{ImageManagers.ImageCount}条");
        }

        private void AddImageItem(int count)
        {
            var imageItemModels = ImageManagers.GetImageModelList(_loadImgIndex, count);
            _loadImgIndex += imageItemModels.Count;
            foreach (var imageItemModel in imageItemModels)
            {
                var image = ImageManagers.GetImage(imageItemModel);
                ilMain.Images.Add(imageItemModel.Id, image);
                lvMain.Items.Add(imageItemModel.Name, imageItemModel.Id);
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() == DialogResult.OK)
            {
                ImageManagers.ExportEasyData(fbdOpenFolder.SelectedPath);
                MessageBox.Show("数据集导出成功");
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (_listImgIndex <= 0)
            {
                return;
            }
            var imageItemModels = ImageManagers.GetImageModelList(_listImgIndex - 1, 1);
            if (imageItemModels.Count == 1)
            {
                lvMain.BeginUpdate();
                _listImgIndex--;
                var image = ImageManagers.GetImage(imageItemModels[0]);
                if (!ilMain.Images.ContainsKey(imageItemModels[0].Id))
                {
                    ilMain.Images.Add(imageItemModels[0].Id, image);
                }
                ListViewItem[] listViewItems = new ListViewItem[lvMain.Items.Count];
                lvMain.Items.CopyTo(listViewItems, 0);
                lvMain.Items.Clear();
                lvMain.Items.Add(imageItemModels[0].Name, imageItemModels[0].Id);
                foreach (var item in listViewItems)
                {
                    lvMain.Items.Add(item);
                }
                lvMain.EndUpdate();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_listImgIndex >= ImageManagers.ImageCount - 3)
            {
                return;
            }
            if (lvMain.Items.Count <= 6)
            {
                if (ilMain.Images.Count > 40)
                {
                    _ilImgIndex += 20;
                    for (int i = 0; i < 10; i++)
                    {
                        ilMain.Images.RemoveAt(0);
                    }
                }
                AddImageItem(20);
            }
            _listImgIndex++;
            lvMain.Items.RemoveAt(0);
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var key = lvMain.SelectedItems[0].ImageKey;
            //pbMian.Image = ilMain.Images[key];
        }

        private void lvMain_ItemActivate(object sender, EventArgs e)
        {

        }

        private void lvMain_MouseClick(object sender, MouseEventArgs e)
        {
            var item = lvMain.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                pbMian.Image = GetDrawBitamp(item.ImageKey);
            }
        }
        private Bitmap GetDrawBitamp(string key)
        {
            var imageItemModel = ImageManagers.GetImageItemModel(key);
            _listImgKey = key;
            if (imageItemModel == null)
            {
                LogUtils.Log($"图片:{key}未找到");
                return null;
            }
            var heightMultiple = 1.0f;
            var widthMultiple = 1.0f;
            //if(imageItemModel.Image.Width>0&& imageItemModel.Image.Width != imageItemModel.Width)
            //{
            //    widthMultiple = imageItemModel.Image.Width * 1.0f / imageItemModel.Width;
            //}
            //if (imageItemModel.Image.Height > 0 && imageItemModel.Image.Height != imageItemModel.Height)
            //{
            //    heightMultiple = imageItemModel.Image.Height * 1.0f/ imageItemModel.Height;
            //}
            var newBit = new Bitmap(ImageManagers.GetImage(imageItemModel));
            Graphics graphics = Graphics.FromImage(newBit);
            foreach (var lable in imageItemModel.Labels)
            {
                var lableColor = LableColorManagers.GetLableColor(lable.Name);
                if (lableColor.IsSelect)
                {
                    graphics.DrawRectangle(lableColor.Pen, lable.X1 * widthMultiple, lable.Y1 * heightMultiple, lable.Width * widthMultiple, lable.Height * heightMultiple);
                }
            }
            graphics.Dispose();
            return newBit;
        }
        bool isRightMouseDown;
        DateTime lastRightMouseDownDateTime;
        Point lastRightMouseDownPoint;
        Graphics bpGraphics;
        private void pbMian_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Right)
            {
                lastRightMouseDownDateTime = DateTime.Now;
                lastRightMouseDownPoint = e.Location;
                isRightMouseDown = true;
            }
        }

        private void pbMian_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRightMouseDown && (DateTime.Now - lastRightMouseDownDateTime).TotalSeconds > 0.5 && Math.Abs(e.X - lastRightMouseDownPoint.X) + Math.Abs(e.Y - lastRightMouseDownPoint.Y) > 10)
            {
                if (bpGraphics == null)
                {
                    bpGraphics = Graphics.FromImage(pbMian.Image);
                }
                bpGraphics.DrawRectangle(Pens.Red, lastRightMouseDownPoint.X, lastRightMouseDownPoint.Y, e.X - lastRightMouseDownPoint.X, e.Y - lastRightMouseDownPoint.Y);
            }
        }

        private void pbMian_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isRightMouseDown = false;
                bpGraphics?.Dispose();
            }
        }

        private void lvLables_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void lvLables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_lvLableUpdate)
            {
                return;
            }
            var item = lvLables.Items[e.Index];
            var lableColor = LableColorManagers.GetLableColor(item.Text);
            lableColor.IsSelect = !item.Checked;
            if (!string.IsNullOrWhiteSpace(_listImgKey))
            {
                pbMian.Image = GetDrawBitamp(_listImgKey);
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //IdentifyImageNetData();
            //IdentifySubName();
           // return;
            var index = 1;
            var imageItemModelList = ImageManagers.GetImageModelList(0, ImageManagers.ImageCount);
            for (int i = 0; i < imageItemModelList.Count; i++)
            {
                var imageItemModel = imageItemModelList[i];
                var bitmap = ImageManagers.GetImage(imageItemModel);
                foreach (var lable in imageItemModel.Labels)
                {
                    var lableColor = LableColorManagers.GetLableColor(lable.Name);
                    if (!lableColor.IsSelect)
                    {
                        continue;
                    }
                    var width = lable.Width;
                    var height = lable.Height;
                    Bitmap saveBitmap = new Bitmap(width, height);
                    Graphics graphics = Graphics.FromImage(saveBitmap);
                    graphics.DrawImage(bitmap, new Rectangle(0, 0, width, height), new Rectangle(lable.X1, lable.Y1, width, height), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    var path = Path.Join(fbdOpenFolder.SelectedPath, lable.Name);
                    Directory.CreateDirectory(path);
                    saveBitmap.Save($"{path}/{index++}.jpg");
                    saveBitmap.Dispose();
                }
            }
            MessageBox.Show("导出成功");
        }
        private void IdentifySubName()
        {
            var imageItemModelList = ImageManagers.GetImageModelList(0, ImageManagers.ImageCount);
            List<ImageLabelsModel> labelsModelList = new List<ImageLabelsModel>();
            foreach (var imageItemModel in imageItemModelList)
            {
                labelsModelList.AddRange(imageItemModel.Labels.Where(im => im.Name == "zd" || im.Name == "cd"));
            }
            var length = 6;
            int i = 870;
            for (; i < labelsModelList.Count; i += length)
            {
                int width = 0;
                int heigth = 0;
                for (int j = 0; j < length; j++)
                {
                    var labelsModel = labelsModelList[i + j];
                    heigth = Math.Max(heigth, labelsModel.Height);
                    width += labelsModel.Width + 10;
                }
                Bitmap bitmap = new Bitmap(width, heigth);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Black);
                int x = 0;
                for (int j = 0; j < length; j++)
                {
                    var labelsModel = labelsModelList[i + j];
                    var soureBitmap = ImageManagers.GetImage(labelsModel.ImageItemModel);
                    graphics.DrawImage(soureBitmap, new Rectangle(x, 0, labelsModel.Width, labelsModel.Height), new Rectangle(labelsModel.X1, labelsModel.Y1, labelsModel.Width, labelsModel.Height), GraphicsUnit.Pixel);
                    x += labelsModel.Width + 10;
                }
                graphics.Dispose();
                bitmap.Save("t1.jpg");
                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                string result = string.Empty;
                result = VerificationCodeTest.SuAnTool.SuAnDiscern(memoryStream);
                if (result.Length != length)
                {
                    MessageBox.Show("苏安调起失败");
                    return;
                }
                for (int j = 0; j < length; j++)
                {
                    var labelsModel = labelsModelList[i + j];
                    var soureBitmap = ImageManagers.GetImage(labelsModel.ImageItemModel);
                    labelsModel.SubName = result[j].ToString();
                    var labelWidth = labelsModel.Width;
                    var labelHeight = labelsModel.Height;
                    Bitmap saveBitmap = new Bitmap(labelWidth, labelHeight);
                    Graphics saveGraphics = Graphics.FromImage(saveBitmap);
                    saveGraphics.DrawImage(soureBitmap, new Rectangle(0, 0, labelWidth, labelHeight), new Rectangle(labelsModel.X1, labelsModel.Y1, labelWidth, labelHeight), GraphicsUnit.Pixel);
                    saveGraphics.Dispose();
                    saveBitmap.Save($"{fbdOpenFolder.SelectedPath}\\{Guid.NewGuid():N}_{result[j]}.jpg");
                    saveBitmap.Dispose();
                }
                bitmap.Dispose();
            }
        }
        private void IdentifyImageNetData()
        {
            var fileInfo = new DirectoryInfo("cs").GetFiles();
            Dictionary<string, bool> ignoreCase = new Dictionary<string, bool> {
                {"C",false },
               {"S",false },
                {"V",false },
                {"W",false },
                {"K",false },
                 {"Z",false },
                  {"X",false },
            };
            int trainNum = (int)(fileInfo.Length * 0.9);
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> lableData = new Dictionary<string, string>();
            List<string> lableIndex = new List<string>();
            StreamWriter streamWriterTrain = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\train_list.txt");
            StreamWriter streamWriterVal = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\val_list.txt");
            StreamWriter streamWriterLabels = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\labels.txt");
            StreamWriter streamWriter = null;
            try
            {
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    FileInfo imgFile = fileInfo[i];
                    var name = imgFile.Name[imgFile.Name.Length - 5].ToString();
                    if (ignoreCase.ContainsKey(name))
                    {
                        name = name.ToLower();
                    }
                    if (name[0] >= 'A' && name[0] <= 'Z')
                    {
                        name = $"d{name.ToLower()}";
                    }
                    var path = Path.Join(fbdOpenFolder.SelectedPath, name);
                    Directory.CreateDirectory(path);
                    imgFile.CopyTo($"{path}\\{imgFile.Name}");
                    if (!lableData.ContainsKey(name))
                    {
                        lableData.Add(name, "");
                        lableIndex.Add(name);
                        streamWriterLabels.WriteLine(name);
                    }
                    streamWriter = i > trainNum ? streamWriterVal : streamWriterTrain;
                    streamWriter.WriteLine($"{name}\\{imgFile.Name}\t{lableIndex.IndexOf(name)}");
                    builder.Clear();
                }
            }
            finally
            {
                streamWriterTrain.Dispose();
                streamWriterVal.Dispose();
                streamWriterLabels.Dispose();
            }
            MessageBox.Show("导出成功");
        }
        /// <summary>
        /// 修复文件夹中图片的分类，重新生成训练集和验证集
        /// </summary>
        private void ReorganizeImageNetDataSet()
        {
            var directorieInfos = new DirectoryInfo(fbdOpenFolder.SelectedPath).GetDirectories();
            Dictionary<string, bool> lableDictionary = new Dictionary<string, bool>();
            List<FileInfo> fileInfo = new List<FileInfo>();
            foreach (var directoryInfo in directorieInfos)
            {
                fileInfo.AddRange(directoryInfo.GetFiles());
            }
            int trainNum = (int)(fileInfo.Count * 0.9);
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> lableData = new Dictionary<string, string>();
            List<string> lableIndex = new List<string>();
            /*StreamWriter streamWriterTrain = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\train_list.txt");
            StreamWriter streamWriterVal = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\val_list.txt");
            StreamWriter streamWriter = null;
            try
            {
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    FileInfo imgFile = fileInfo[i];
                    var name = imgFile.Name[imgFile.Name.Length - 5].ToString();
                    if (ignoreCase.ContainsKey(name))
                    {
                        name = name.ToLower();
                    }
                    if (name[0] >= 'A' && name[0] <= 'Z')
                    {
                        name = $"d{name.ToLower()}";
                    }
                    var path = Path.Join(fbdOpenFolder.SelectedPath, name);
                    Directory.CreateDirectory(path);
                    imgFile.CopyTo($"{path}\\{imgFile.Name}");
                    if (!lableData.ContainsKey(name))
                    {
                        lableData.Add(name, "");
                        lableIndex.Add(name);
                        streamWriterLabels.WriteLine(name);
                    }
                    streamWriter = i > trainNum ? streamWriterVal : streamWriterTrain;
                    streamWriter.WriteLine($"{name}\\{imgFile.Name}\t{lableIndex.IndexOf(name)}");
                    builder.Clear();
                }
            }
            finally
            {
                streamWriterTrain.Dispose();
                streamWriterVal.Dispose();
            }*/
            MessageBox.Show("导出成功");
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var imgPath = Path.Join(fbdOpenFolder.SelectedPath, "image");
            var xmlPath = Path.Join(fbdOpenFolder.SelectedPath, "xml");
            Directory.CreateDirectory(imgPath);
            Directory.CreateDirectory(xmlPath);
            var imageItemModelList = ImageManagers.GetImageModelList(0, ImageManagers.ImageCount);
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> lableData = new Dictionary<string, string>();
            StreamWriter streamWriterTrain = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\train_list.txt");
            StreamWriter streamWriterVal = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\val_list.txt");
            StreamWriter streamWriterLabels = new StreamWriter($"{fbdOpenFolder.SelectedPath}\\labels.txt");
            StreamWriter streamWriter = null;
            int trainNum = (int)(imageItemModelList.Count * 0.9);
            try
            {
                for (int i = 0; i < imageItemModelList.Count; i++)
                {
                    var imageItemModel = imageItemModelList[i];
                    FileInfo imgFile = new FileInfo(imageItemModel.Path);
                    imgFile.CopyTo($"{imgPath}\\{imgFile.Name}");
                    var img = ImageManagers.GetImage(imageItemModel);
                    builder.AppendFormat("<annotation>\n\t<folder>image</folder>\n\t<filename>{0}</filename>\n\t<source>\n\t\t<database>Unknown</database>\n\t</source>\n\t<size>\n\t\t<width>{1}</width>\n\t\t<height>{2}</height>\n\t\t<depth>3</depth>\n\t</size>\n\t<segmented>0</segmented>", imgFile.Name, img.Width, img.Height);
                    foreach (var lable in imageItemModel.Labels)
                    {
                        if (!lableData.ContainsKey(lable.Name))
                        {
                            lableData.Add(lable.Name, "");
                            streamWriterLabels.WriteLine(lable.Name);
                        }
                        builder.AppendFormat("\n\t<object>\n\t\t<name>{0}</name>\n\t\t<pose>Unspecified</pose>\n\t\t<truncated>0</truncated>\n\t\t<difficult>0</difficult>\n\t\t<bndbox>\n\t\t\t<xmin>{1}</xmin>\n\t\t\t<ymin>{2}</ymin>\n\t\t\t<xmax>{3}</xmax>\n\t\t\t<ymax>{4}</ymax>\n\t\t</bndbox>\n\t</object>", lable.Name, lable.X1, lable.Y1, lable.X2, lable.Y2);
                    }
                    builder.Append("\n</annotation>\n");
                    var imgName = imgFile.Name.Substring(0, imgFile.Name.Length - imgFile.Extension.Length);
                    File.WriteAllText($"{xmlPath}/{imgName}.xml", builder.ToString());
                    streamWriter = i > trainNum ? streamWriterVal : streamWriterTrain;
                    streamWriter.WriteLine($"image\\{imgName}.jpg\txml\\{imgName}.xml");
                    builder.Clear();
                }
            }
            finally
            {
                streamWriterTrain.Dispose();
                streamWriterVal.Dispose();
                streamWriterLabels.Dispose();
            }
            MessageBox.Show("导出成功");
        }
    }
}
