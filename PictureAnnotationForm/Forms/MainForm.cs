﻿using Newtonsoft.Json;
using PictureAnnotationForm.BLL;
using PictureAnnotationForm.Datas;
using PictureAnnotationForm.Models;
using PictureAnnotationForm.Utils;
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

namespace PictureAnnotationForm.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 当前图片加载下标
        /// </summary>
        private int _loadImgIndex;
        /// <summary>
        /// 当前显示图片列表开头的的索引
        /// </summary>
        private int _listImgIndex;
        /// <summary>
        /// 当前选中的图片项
        /// </summary>
        private int _listSelectIndex = -1;
        /// <summary>
        /// 标签列是否在更新
        /// </summary>
        private bool _lvLabelUpdate;
        /// <summary>
        /// 图片列表显示的图片数量
        /// </summary>
        private int _showImgNum;
        /// <summary>
        /// 标题
        /// </summary>
        public static new string Tag = "繁星标注  V1.0";
        public MainForm()
        {
            InitializeComponent();
            Text = Tag;
            BubbleReminderForm.InitForm(this, false);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _showImgNum = lvMain.Width / 283;
            timeAutoSave.Interval = SystemSetting.Default.AutoBackInterval * 1000;
        }
        private void RefreshLabel()
        {
            _lvLabelUpdate = true;
            lvLabels.BeginUpdate();
            lvLabels.SuspendLayout();
            lvLabels.Items.Clear();
            foreach (var name in ImageManagers.LabelNames)
            {
                var labelColor = LabelColorManagers.GetLabelColor(name);
                var listViewItem = lvLabels.Items.Add(name);
                listViewItem.SubItems.Add(labelColor.Name);
                listViewItem.SubItems.Add(labelColor.IsFill ? "填充" : "不填充");
                listViewItem.Checked = labelColor.IsSelect;
            }
            lvLabels.EndUpdate();
            lvLabels.ResumeLayout();
            _lvLabelUpdate = false;
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
                Text = $"{Tag}-当前第{_listImgIndex + 1}张,共{ImageManagers.ImageCount}张";
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
                if (_listSelectIndex < 0)
                {
                    _listSelectIndex = 0;
                }
                lvMain.Items[_listSelectIndex].Selected = true;
                lvMain.EndUpdate();
                liShow.SetImageItemModel(lvMain.Items[_listSelectIndex].ImageKey);
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
                    for (int i = 0; i < 10; i++)
                    {
                        ilMain.Images.RemoveAt(0);
                    }
                }
                AddImageItem(20);
            }
            _listImgIndex++;
            Text = $"{Tag}-当前第{_listImgIndex + 1}张,共{ImageManagers.ImageCount}张";
            lvMain.Items.RemoveAt(0);
            if (_listSelectIndex < 0)
            {
                _listSelectIndex = 0;
            }
            lvMain.Items[_listSelectIndex].Selected = true;
            liShow.SetImageItemModel(lvMain.Items[_listSelectIndex].ImageKey);
        }


        private void lvMain_MouseClick(object sender, MouseEventArgs e)
        {
            var item = lvMain.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                _listSelectIndex = item.Index;
                liShow.SetImageItemModel(item.ImageKey);
            }
        }

        private void lvLabels_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void lvLabels_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_lvLabelUpdate)
            {
                return;
            }
            var item = lvLabels.Items[e.Index];
            var labelColor = LabelColorManagers.GetLabelColor(item.Text);
            labelColor.IsSelect = !item.Checked;
            liShow.UpdateDrawingBoard();
        }
        #region 导出数据集
        private void 导出Voc数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var imgPath = Path.Combine(fbdOpenFolder.SelectedPath, "image");
            var xmlPath = Path.Combine(fbdOpenFolder.SelectedPath, "xml");
            Directory.CreateDirectory(imgPath);
            Directory.CreateDirectory(xmlPath);
            var imageItemModelList = ImageManagers.GetImageModelList(0, ImageManagers.ImageCount);
            StringBuilder builder = new StringBuilder();
            Dictionary<string, string> labelData = new Dictionary<string, string>();
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
                    foreach (var label in imageItemModel.Labels)
                    {
                        if (!labelData.ContainsKey(label.Name))
                        {
                            labelData.Add(label.Name, "");
                            streamWriterLabels.WriteLine(label.Name);
                        }
                        builder.AppendFormat("\n\t<object>\n\t\t<name>{0}</name>\n\t\t<pose>Unspecified</pose>\n\t\t<truncated>0</truncated>\n\t\t<difficult>0</difficult>\n\t\t<bndbox>\n\t\t\t<xmin>{1}</xmin>\n\t\t\t<ymin>{2}</ymin>\n\t\t\t<xmax>{3}</xmax>\n\t\t\t<ymax>{4}</ymax>\n\t\t</bndbox>\n\t</object>", label.Name, label.X1, label.Y1, label.X2, label.Y2);
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

        private void 导出选中分类到ImageNet数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var index = 1;
            var imageItemModelList = ImageManagers.GetImageModelList(0, ImageManagers.ImageCount);
            for (int i = 0; i < imageItemModelList.Count; i++)
            {
                var imageItemModel = imageItemModelList[i];
                var bitmap = ImageManagers.GetImage(imageItemModel);
                foreach (var label in imageItemModel.Labels)
                {
                    var labelColor = LabelColorManagers.GetLabelColor(label.Name);
                    if (!labelColor.IsSelect)
                    {
                        continue;
                    }
                    var width = label.Width;
                    var height = label.Height;
                    Bitmap saveBitmap = new Bitmap(width, height);
                    Graphics graphics = Graphics.FromImage(saveBitmap);
                    graphics.DrawImage(bitmap, new Rectangle(0, 0, width, height), new Rectangle(label.X1, label.Y1, width, height), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    var path = Path.Combine(fbdOpenFolder.SelectedPath, label.Name);
                    Directory.CreateDirectory(path);
                    saveBitmap.Save($"{path}/{index++}.jpg");
                    saveBitmap.Dispose();
                }
            }
            MessageBox.Show("导出成功");
        }

        private void 导出EasyData数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() == DialogResult.OK)
            {
                ImageManagers.ExportEasyData(fbdOpenFolder.SelectedPath);
                MessageBox.Show("数据集导出成功");
            }
        }
        #endregion

        private void liShow_ImageLast()
        {
            btnLast_Click(null, null);
        }

        private void liShow_ImageNext()
        {
            btnNext_Click(null, null);
        }

        private void liShow_LabelChange(ImageLabelsModel obj)
        {
            if (obj != null)
            {
                tcMain.SelectedTab = tpLabelInfo;
            }
            liMain.SetLabel(obj);
        }

        private void lvMain_SizeChanged(object sender, EventArgs e)
        {
            _showImgNum = lvMain.Width / 283;
        }

        private void liMain_LabelChange(ImageLabelsModel obj)
        {
            liShow.UpdateLabelChange(obj);
        }

        private void SaveData(string path = null)
        {
            DataSets dataSets = ImageManagers.CurrentImageData;
            dataSets.DatasetProperties.ListImgIndex = _listImgIndex;
            dataSets.DatasetProperties.ListSelectIndex = _listSelectIndex;
            LabelColorManagers.SaveData();
            liMain.SaveData();
            if (path == null)
            {
                ImageManagers.SaveDataSets();
            }
            else
            {
                ImageManagers.SaveDataSets(path);
            }
        }
        private void LoadData(string name)
        {
            ImageManagers.LoadDataSets(name);
            _listImgIndex = ImageManagers.CurrentImageData.DatasetProperties.ListImgIndex;
            _listSelectIndex = ImageManagers.CurrentImageData.DatasetProperties.ListSelectIndex < 0 ? 0 : ImageManagers.CurrentImageData.DatasetProperties.ListSelectIndex;
            _loadImgIndex = _listImgIndex;

            LabelColorManagers.LoadData();
            lvMain.Items.Clear();
            ilMain.Images.Clear();
            RefreshLabel();
            AddImageItem(60);
            liShow.SetImageItemModel(lvMain.Items[_listSelectIndex].ImageKey);
            if (ImageManagers.CurrentImageData.DatasetProperties.SelectLabelIndex > -1)
            {
                var imageItemModel = ImageManagers.GetImageItemModel(lvMain.Items[_listSelectIndex].ImageKey);
                if (ImageManagers.CurrentImageData.DatasetProperties.SelectLabelIndex < imageItemModel.Labels.Count)
                {
                    var label = imageItemModel.Labels[ImageManagers.CurrentImageData.DatasetProperties.SelectLabelIndex];
                    liMain.SetLabel(label);
                }
            }
            lvMain.Items[_listSelectIndex].Selected = true;
            Text = $"{Tag}-当前第{_listImgIndex + 1}张,共{ImageManagers.ImageCount}张";
        }
        #region 加载数据集
        private void 导入图片数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导入VocXml标注ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 导入Voc数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var num = ImageManagers.LoadVocDirectory(fbdOpenFolder.SelectedPath);
            RefreshLabel();
            if (num > 10 && ilMain.Images.Count < 10)
            {
                AddImageItem(20 - ilMain.Images.Count);
            }
            MessageBox.Show($"数据集加载成功,当前加载数据{num}条,共{ImageManagers.ImageCount}条");
        }
        /// <summary>
        /// 加载框字数据集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var num = ImageManagers.LoadBoxWordDirectory(fbdOpenFolder.SelectedPath);
            RefreshLabel();
            if (num > 10 && ilMain.Images.Count < 10)
            {
                AddImageItem(20 - ilMain.Images.Count);
            }
            MessageBox.Show($"数据集加载成功,当前加载数据{num}条,共{ImageManagers.ImageCount}条");
        }
        /// <summary>
        /// 加载PaddleOcrDet数据集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var num = ImageManagers.LoadPaddleOcrDetData(ofdOpenFile.FileName);
            RefreshLabel();
            if (num > 10 && ilMain.Images.Count < 10)
            {
                AddImageItem(20 - ilMain.Images.Count);
            }
            MessageBox.Show($"数据集加载成功,当前加载数据{num}条,共{ImageManagers.ImageCount}条");
        }
        #endregion

        private void timeAutoSave_Tick(object sender, EventArgs e)
        {
            if (ImageManagers.CurrentImageData == null)
            {
                return;
            }
            var path = Path.Combine(SystemSetting.Default.DataSetDir, ImageManagers.CurrentImageData.DatasetProperties.Name, DataSets.BackDir);
            Directory.CreateDirectory(path);
            SaveData($"{path}\\{DateTime.Now:yyyy-MM-dd HH}-AutoSave.data");
        }

        private void btnOpenImg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbImgId.Text))
            {
                var imageItemModel = ImageManagers.GetImageItemModel(tbImgId.Text);
                if (imageItemModel == null)
                {
                    MessageBox.Show("Id不存在");
                    return;
                }
                liShow.SetImageItemModel(imageItemModel);
                tbImgId.Clear();
            }
            else if (!string.IsNullOrWhiteSpace(tbLabelId.Text))
            {
                var imageLabelsModel = ImageManagers.GetImageLabelsModel(tbLabelId.Text);
                if (imageLabelsModel == null)
                {
                    MessageBox.Show("Id不存在");
                    return;
                }
                SetImageLabel(imageLabelsModel);
            }
        }

        private void SetImageLabel(ImageLabelsModel imageLabelsModel)
        {
            liShow.SetImageItemModel(imageLabelsModel);
            tcMain.SelectedTab = tpLabelInfo;
            liMain.SetLabel(imageLabelsModel);
            tbLabelId.Clear();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveData();
                BubbleReminderForm.ShowMsg($"数据保存成功");
            }
        }
        private int _currentOverlappingLabelIndex = 0;
        private void btnLabelOverlapping_Click(object sender, EventArgs e)
        {
            var imageLabelsModel = ImageManagers.GetOverlappingLabel(ref _currentOverlappingLabelIndex);
            if (imageLabelsModel == null)
            {
                MessageBox.Show("重叠标签已查找完毕");
                _currentOverlappingLabelIndex = 0;
                return;
            }
            _currentOverlappingLabelIndex++;
            SetImageLabel(imageLabelsModel);
        }
        private int _currentSonEmptyLabelIndex = 0;
        private void btnSonEmpty_Click(object sender, EventArgs e)
        {
            var imageLabelsModel = ImageManagers.GetSonEmptyLabel(ref _currentSonEmptyLabelIndex);
            if (imageLabelsModel == null)
            {
                MessageBox.Show("二级未分类标签已查找完毕");
                _currentSonEmptyLabelIndex = 0;
                return;
            }
            _currentSonEmptyLabelIndex++;
            SetImageLabel(imageLabelsModel);
        }

        private void btnUnknown_Click(object sender, EventArgs e)
        {
            var imageLabelsModel = ImageManagers.GetUnknownLabel(ref _currentSonEmptyLabelIndex);
            if (imageLabelsModel == null)
            {
                MessageBox.Show("二级未分类标签已查找完毕");
                _currentSonEmptyLabelIndex = 0;
                return;
            }
            _currentSonEmptyLabelIndex++;
            SetImageLabel(imageLabelsModel);
        }

        private void tcMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            _lvLabelUpdate = true;
            Task.Delay(5000).ContinueWith(r => _lvLabelUpdate = false);
        }

        private void 打开数据集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fbdOpenFolder.InitialDirectory = SystemSetting.Default.DataSetDir;
            fbdOpenFolder.SelectedPath = SystemSetting.Default.DataSetDir;
            if (fbdOpenFolder.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            var name = Path.GetDirectoryName(fbdOpenFolder.SelectedPath);
            LoadData(name);
        }

        private void 修改程序集属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoEditForm.ShowEdit(ImageManagers.CurrentImageData);
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoEditForm.ShowEdit(SystemSetting.Default);
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            liShow.InitZoomMultiple();
        }

        private void btnFullLabel_Click(object sender, EventArgs e)
        {
            liShow.FullLabel();
        }

        private void btnCheckBox_Click(object sender, EventArgs e)
        {
            liShow.CheckBox();
        }

        private void btnShowLabel_Click(object sender, EventArgs e)
        {
            liShow.ShowLabel();
        }

        private void btnSetAllLabel_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SystemSetting.Default.LastDataSet) || !Directory.Exists(Path.Combine(SystemSetting.Default.DataSetDir, SystemSetting.Default.LastDataSet)))
            {
                var dataSets = AutoEditForm.ShowEdit<DataSets>(null);
                if (dataSets == null)
                {
                    MessageBox.Show("请创建或选择数据集");
                    Close();
                    return;
                }
                DataSets.Init(dataSets);
                SystemSetting.Default.LastDataSet = dataSets.DatasetProperties.Name;
                LoadData(SystemSetting.Default.LastDataSet);
                SystemSetting.SaveDefault();
            }
            else
            {

                LoadData(SystemSetting.Default.LastDataSet);
            }
        }

        private void liShow_LabelUpdateEvent()
        {
            liMain.LabelsUodate();
        }

        private void 减少标注图片等级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in ImageManagers.CurrentImageDataList)
            {
                if (item.CompleteLevel > 0)
                {
                    item.CompleteLevel--;
                }
            }
        }

        private void 增加标注图片等级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in ImageManagers.CurrentImageDataList)
            {
                if (item.CompleteLevel > 0)
                {
                    item.CompleteLevel++;
                }
            }
        }
    }
}
