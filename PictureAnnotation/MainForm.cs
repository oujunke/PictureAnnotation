using PictureAnnotation.BLL;
using PictureAnnotation.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            var num = ImageManagers.LoadVocDirectory(@"VOCYolo100");
            RefreshLable();
            if (num > 20 && ilMain.Images.Count < 10)
            {
                AddImageItem(20 - ilMain.Images.Count);
            }
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
            if (fbdOpenFolder.ShowDialog() == DialogResult.OK)
            {
                var num = ImageManagers.LoadVocDirectory(fbdOpenFolder.SelectedPath);
                RefreshLable();
                if (num > 10 && ilMain.Images.Count < 10)
                {
                    AddImageItem(20 - ilMain.Images.Count);
                }
                MessageBox.Show($"数据集加载成功,当前加载数据{num}条,共{ImageManagers.ImageCount}条");
            }
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
            var newBit = new Bitmap(imageItemModel.Image);
            Graphics graphics = Graphics.FromImage(newBit);
            foreach (var lable in imageItemModel.Labels)
            {
                var lableColor = LableColorManagers.GetLableColor(lable.Name);
                if (lableColor.IsSelect)
                {
                    graphics.DrawRectangle(lableColor.Pen, lable.X1 * widthMultiple, lable.Y1 * heightMultiple, (lable.X2 - lable.X1) * widthMultiple, (lable.Y2 - lable.Y1) * heightMultiple);
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
            if (_lvLableUpdate|| _listImgKey==null)
            {
                return;
            }
            var item = lvLables.Items[e.Index];
            var lableColor = LableColorManagers.GetLableColor(item.Text);
            lableColor.IsSelect = !item.Checked;
            pbMian.Image = GetDrawBitamp(_listImgKey);
        }
    }
}
