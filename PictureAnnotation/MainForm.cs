using PictureAnnotation.BLL;
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
        private int _clickImgIndex;
        private int _listImgIndex;
        private int _ilImgIndex;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var num = ImageManagers.LoadVocDirectory(@"E:\数据集\拼多多\VovYoloPdd");
            if (num > 10 && ilMain.Images.Count < 10)
            {
                AddImageItem(10 - ilMain.Images.Count);
            }
            //pbMian.Image = ImageManagers.GetImageList()[0].Image;
        }
        private void msMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (fbdOpenFolder.ShowDialog() == DialogResult.OK)
            {
                var num = ImageManagers.LoadVocDirectory(fbdOpenFolder.SelectedPath);
                if (num > 10 && ilMain.Images.Count < 10)
                {
                    AddImageItem(10 - ilMain.Images.Count);
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
            if (_listImgIndex<=0)
            {
                return;
            }
            var imageItemModels=ImageManagers.GetImageModelList(_listImgIndex - 1, 1);
            if (imageItemModels.Count == 1)
            {
                lvMain.BeginUpdate();
                _listImgIndex --;
                var image = ImageManagers.GetImage(imageItemModels[0]);
                if (!ilMain.Images.ContainsKey(imageItemModels[0].Id))
                {
                    ilMain.Images.Add(imageItemModels[0].Id, image);
                }
                ListViewItem[] listViewItems = new ListViewItem[lvMain.Items.Count];
                lvMain.Items.CopyTo(listViewItems,0);
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
            if (lvMain.Items.Count <= 3)
            {
                if (ilMain.Images.Count > 20)
                {
                    _ilImgIndex += 10;
                    for (int i = 0; i < 10; i++)
                    {
                        ilMain.Images.RemoveAt(0);
                    }
                }
                AddImageItem(10);
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
                pbMian.Image = ilMain.Images[item.ImageKey];
            }
        }

        private void pbMian_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pbMian_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pbMian_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
