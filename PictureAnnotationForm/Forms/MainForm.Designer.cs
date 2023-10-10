namespace PictureAnnotationForm.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            liShow = new UserForm.LabelImageUserControl();
            panel1 = new System.Windows.Forms.Panel();
            lvMain = new System.Windows.Forms.ListView();
            ilMain = new System.Windows.Forms.ImageList(components);
            btnNext = new System.Windows.Forms.Button();
            btnLast = new System.Windows.Forms.Button();
            msMain = new System.Windows.Forms.MenuStrip();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            打开数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            历史数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            修改程序集属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导入Voc数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            导入图片数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            导入VocXml标注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导出Voc数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导出EasyData数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            导出选中分类到ImageNet数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            减少标注图片等级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            增加标注图片等级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tcMain = new System.Windows.Forms.TabControl();
            tpLabelInfo = new System.Windows.Forms.TabPage();
            liMain = new UserForm.LabelInfoUserControl();
            panel3 = new System.Windows.Forms.Panel();
            btnSetAllLabel = new System.Windows.Forms.Button();
            btnShowLabel = new System.Windows.Forms.Button();
            btnCheckBox = new System.Windows.Forms.Button();
            btnFullLabel = new System.Windows.Forms.Button();
            btnRevert = new System.Windows.Forms.Button();
            btnUnknown = new System.Windows.Forms.Button();
            btnSonEmpty = new System.Windows.Forms.Button();
            btnLabelOverlapping = new System.Windows.Forms.Button();
            tpLabelSelect = new System.Windows.Forms.TabPage();
            lvLabels = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            panel2 = new System.Windows.Forms.Panel();
            btOpenImg = new System.Windows.Forms.TabPage();
            groupBox3 = new System.Windows.Forms.GroupBox();
            tbLabelId = new System.Windows.Forms.TextBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            btnOpenImg = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            tbImgId = new System.Windows.Forms.TextBox();
            fbdOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            timeAutoSave = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            msMain.SuspendLayout();
            tcMain.SuspendLayout();
            tpLabelInfo.SuspendLayout();
            panel3.SuspendLayout();
            tpLabelSelect.SuspendLayout();
            btOpenImg.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(liShow);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(msMain);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tcMain);
            splitContainer1.Size = new System.Drawing.Size(1875, 920);
            splitContainer1.SplitterDistance = 1499;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // liShow
            // 
            liShow.Dock = System.Windows.Forms.DockStyle.Fill;
            liShow.Location = new System.Drawing.Point(0, 30);
            liShow.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            liShow.Name = "liShow";
            liShow.Size = new System.Drawing.Size(1499, 690);
            liShow.TabIndex = 2;
            liShow.ImageLast += liShow_ImageLast;
            liShow.ImageNext += liShow_ImageNext;
            liShow.LabelChange += liShow_LabelChange;
            liShow.LabelUpdateEvent += liShow_LabelUpdateEvent;
            // 
            // panel1
            // 
            panel1.Controls.Add(lvMain);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(btnLast);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 720);
            panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1499, 200);
            panel1.TabIndex = 1;
            // 
            // lvMain
            // 
            lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            lvMain.LargeImageList = ilMain;
            lvMain.Location = new System.Drawing.Point(68, 0);
            lvMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lvMain.MultiSelect = false;
            lvMain.Name = "lvMain";
            lvMain.Scrollable = false;
            lvMain.Size = new System.Drawing.Size(1363, 200);
            lvMain.TabIndex = 2;
            lvMain.UseCompatibleStateImageBehavior = false;
            lvMain.SizeChanged += lvMain_SizeChanged;
            lvMain.MouseClick += lvMain_MouseClick;
            // 
            // ilMain
            // 
            ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            ilMain.ImageSize = new System.Drawing.Size(240, 120);
            ilMain.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnNext
            // 
            btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            btnNext.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            btnNext.Location = new System.Drawing.Point(1431, 0);
            btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(68, 200);
            btnNext.TabIndex = 1;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnLast
            // 
            btnLast.Dock = System.Windows.Forms.DockStyle.Left;
            btnLast.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            btnLast.Location = new System.Drawing.Point(0, 0);
            btnLast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnLast.Name = "btnLast";
            btnLast.Size = new System.Drawing.Size(68, 200);
            btnLast.TabIndex = 0;
            btnLast.Text = "<";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += btnLast_Click;
            // 
            // msMain
            // 
            msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem3, 导入ToolStripMenuItem, 导出ToolStripMenuItem, 操作ToolStripMenuItem });
            msMain.Location = new System.Drawing.Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            msMain.Size = new System.Drawing.Size(1499, 30);
            msMain.TabIndex = 0;
            msMain.Text = "menuStrip1";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { 打开数据集ToolStripMenuItem, 历史数据集ToolStripMenuItem, 修改程序集属性ToolStripMenuItem, 系统设置ToolStripMenuItem });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(53, 24);
            toolStripMenuItem3.Text = "文件";
            // 
            // 打开数据集ToolStripMenuItem
            // 
            打开数据集ToolStripMenuItem.Name = "打开数据集ToolStripMenuItem";
            打开数据集ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            打开数据集ToolStripMenuItem.Text = "打开数据集";
            打开数据集ToolStripMenuItem.Click += 打开数据集ToolStripMenuItem_Click;
            // 
            // 历史数据集ToolStripMenuItem
            // 
            历史数据集ToolStripMenuItem.Name = "历史数据集ToolStripMenuItem";
            历史数据集ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            历史数据集ToolStripMenuItem.Text = "历史数据集";
            // 
            // 修改程序集属性ToolStripMenuItem
            // 
            修改程序集属性ToolStripMenuItem.Name = "修改程序集属性ToolStripMenuItem";
            修改程序集属性ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            修改程序集属性ToolStripMenuItem.Text = "程序集设置";
            修改程序集属性ToolStripMenuItem.Click += 修改程序集属性ToolStripMenuItem_Click;
            // 
            // 系统设置ToolStripMenuItem
            // 
            系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            系统设置ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            系统设置ToolStripMenuItem.Text = "系统设置";
            系统设置ToolStripMenuItem.Click += 系统设置ToolStripMenuItem_Click;
            // 
            // 导入ToolStripMenuItem
            // 
            导入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { 导入Voc数据集ToolStripMenuItem, toolStripMenuItem1, 导入图片数据集ToolStripMenuItem, toolStripMenuItem2, 导入VocXml标注ToolStripMenuItem });
            导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            导入ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            导入ToolStripMenuItem.Text = "导入";
            // 
            // 导入Voc数据集ToolStripMenuItem
            // 
            导入Voc数据集ToolStripMenuItem.Name = "导入Voc数据集ToolStripMenuItem";
            导入Voc数据集ToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            导入Voc数据集ToolStripMenuItem.Text = "导入Voc数据集";
            导入Voc数据集ToolStripMenuItem.Click += 导入Voc数据集ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(269, 26);
            toolStripMenuItem1.Text = "导入BoxWord数据集";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // 导入图片数据集ToolStripMenuItem
            // 
            导入图片数据集ToolStripMenuItem.Name = "导入图片数据集ToolStripMenuItem";
            导入图片数据集ToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            导入图片数据集ToolStripMenuItem.Text = "导入图片数据集";
            导入图片数据集ToolStripMenuItem.Click += 导入图片数据集ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(269, 26);
            toolStripMenuItem2.Text = "导入PaddleOcrDet数据集";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // 导入VocXml标注ToolStripMenuItem
            // 
            导入VocXml标注ToolStripMenuItem.Name = "导入VocXml标注ToolStripMenuItem";
            导入VocXml标注ToolStripMenuItem.Size = new System.Drawing.Size(269, 26);
            导入VocXml标注ToolStripMenuItem.Text = "导入Voc-Xml标注";
            导入VocXml标注ToolStripMenuItem.Click += 导入VocXml标注ToolStripMenuItem_Click;
            // 
            // 导出ToolStripMenuItem
            // 
            导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { 导出Voc数据集ToolStripMenuItem, 导出EasyData数据集ToolStripMenuItem, 导出选中分类到ImageNet数据集ToolStripMenuItem });
            导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            导出ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            导出ToolStripMenuItem.Text = "导出";
            // 
            // 导出Voc数据集ToolStripMenuItem
            // 
            导出Voc数据集ToolStripMenuItem.Name = "导出Voc数据集ToolStripMenuItem";
            导出Voc数据集ToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            导出Voc数据集ToolStripMenuItem.Text = "导出Voc数据集";
            导出Voc数据集ToolStripMenuItem.Click += 导出Voc数据集ToolStripMenuItem_Click;
            // 
            // 导出EasyData数据集ToolStripMenuItem
            // 
            导出EasyData数据集ToolStripMenuItem.Name = "导出EasyData数据集ToolStripMenuItem";
            导出EasyData数据集ToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            导出EasyData数据集ToolStripMenuItem.Text = "导出EsayData数据集";
            导出EasyData数据集ToolStripMenuItem.Click += 导出EasyData数据集ToolStripMenuItem_Click;
            // 
            // 导出选中分类到ImageNet数据集ToolStripMenuItem
            // 
            导出选中分类到ImageNet数据集ToolStripMenuItem.Name = "导出选中分类到ImageNet数据集ToolStripMenuItem";
            导出选中分类到ImageNet数据集ToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            导出选中分类到ImageNet数据集ToolStripMenuItem.Text = "导出选中分类到ImageNet数据集";
            导出选中分类到ImageNet数据集ToolStripMenuItem.Click += 导出选中分类到ImageNet数据集ToolStripMenuItem_Click;
            // 
            // 操作ToolStripMenuItem
            // 
            操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { 减少标注图片等级ToolStripMenuItem, 增加标注图片等级ToolStripMenuItem });
            操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            操作ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            操作ToolStripMenuItem.Text = "操作";
            // 
            // 减少标注图片等级ToolStripMenuItem
            // 
            减少标注图片等级ToolStripMenuItem.Name = "减少标注图片等级ToolStripMenuItem";
            减少标注图片等级ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            减少标注图片等级ToolStripMenuItem.Text = "减少标注图片等级";
            减少标注图片等级ToolStripMenuItem.Click += 减少标注图片等级ToolStripMenuItem_Click;
            // 
            // 增加标注图片等级ToolStripMenuItem
            // 
            增加标注图片等级ToolStripMenuItem.Name = "增加标注图片等级ToolStripMenuItem";
            增加标注图片等级ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            增加标注图片等级ToolStripMenuItem.Text = "增加标注图片等级";
            增加标注图片等级ToolStripMenuItem.Click += 增加标注图片等级ToolStripMenuItem_Click;
            // 
            // tcMain
            // 
            tcMain.Controls.Add(tpLabelInfo);
            tcMain.Controls.Add(tpLabelSelect);
            tcMain.Controls.Add(btOpenImg);
            tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.Location = new System.Drawing.Point(0, 0);
            tcMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new System.Drawing.Size(370, 920);
            tcMain.TabIndex = 0;
            tcMain.Selecting += tcMain_Selecting;
            // 
            // tpLabelInfo
            // 
            tpLabelInfo.Controls.Add(liMain);
            tpLabelInfo.Controls.Add(panel3);
            tpLabelInfo.Location = new System.Drawing.Point(4, 29);
            tpLabelInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tpLabelInfo.Name = "tpLabelInfo";
            tpLabelInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tpLabelInfo.Size = new System.Drawing.Size(362, 887);
            tpLabelInfo.TabIndex = 1;
            tpLabelInfo.Text = "标签设置";
            tpLabelInfo.UseVisualStyleBackColor = true;
            // 
            // liMain
            // 
            liMain.Dock = System.Windows.Forms.DockStyle.Fill;
            liMain.Location = new System.Drawing.Point(4, 214);
            liMain.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            liMain.Name = "liMain";
            liMain.Size = new System.Drawing.Size(354, 668);
            liMain.TabIndex = 1;
            liMain.LabelChange += liMain_LabelChange;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnSetAllLabel);
            panel3.Controls.Add(btnShowLabel);
            panel3.Controls.Add(btnCheckBox);
            panel3.Controls.Add(btnFullLabel);
            panel3.Controls.Add(btnRevert);
            panel3.Controls.Add(btnUnknown);
            panel3.Controls.Add(btnSonEmpty);
            panel3.Controls.Add(btnLabelOverlapping);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(4, 5);
            panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(354, 209);
            panel3.TabIndex = 0;
            // 
            // btnSetAllLabel
            // 
            btnSetAllLabel.Location = new System.Drawing.Point(177, 151);
            btnSetAllLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSetAllLabel.Name = "btnSetAllLabel";
            btnSetAllLabel.Size = new System.Drawing.Size(165, 39);
            btnSetAllLabel.TabIndex = 9;
            btnSetAllLabel.Text = "推广未标记位置标签";
            btnSetAllLabel.UseVisualStyleBackColor = true;
            btnSetAllLabel.Click += btnSetAllLabel_Click;
            // 
            // btnShowLabel
            // 
            btnShowLabel.Location = new System.Drawing.Point(4, 151);
            btnShowLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnShowLabel.Name = "btnShowLabel";
            btnShowLabel.Size = new System.Drawing.Size(165, 39);
            btnShowLabel.TabIndex = 8;
            btnShowLabel.Text = "单项预览";
            btnShowLabel.UseVisualStyleBackColor = true;
            btnShowLabel.Click += btnShowLabel_Click;
            // 
            // btnCheckBox
            // 
            btnCheckBox.Location = new System.Drawing.Point(177, 102);
            btnCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnCheckBox.Name = "btnCheckBox";
            btnCheckBox.Size = new System.Drawing.Size(165, 39);
            btnCheckBox.TabIndex = 7;
            btnCheckBox.Text = "检查边框";
            btnCheckBox.UseVisualStyleBackColor = true;
            btnCheckBox.Click += btnCheckBox_Click;
            // 
            // btnFullLabel
            // 
            btnFullLabel.Location = new System.Drawing.Point(4, 102);
            btnFullLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnFullLabel.Name = "btnFullLabel";
            btnFullLabel.Size = new System.Drawing.Size(165, 39);
            btnFullLabel.TabIndex = 6;
            btnFullLabel.Text = "填充标签";
            btnFullLabel.UseVisualStyleBackColor = true;
            btnFullLabel.Click += btnFullLabel_Click;
            // 
            // btnRevert
            // 
            btnRevert.Location = new System.Drawing.Point(176, 53);
            btnRevert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnRevert.Name = "btnRevert";
            btnRevert.Size = new System.Drawing.Size(165, 39);
            btnRevert.TabIndex = 5;
            btnRevert.Text = "图片还原";
            btnRevert.UseVisualStyleBackColor = true;
            btnRevert.Click += btnRevert_Click;
            // 
            // btnUnknown
            // 
            btnUnknown.Location = new System.Drawing.Point(4, 53);
            btnUnknown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnUnknown.Name = "btnUnknown";
            btnUnknown.Size = new System.Drawing.Size(165, 39);
            btnUnknown.TabIndex = 4;
            btnUnknown.Text = "打开未命名标签";
            btnUnknown.UseVisualStyleBackColor = true;
            btnUnknown.Click += btnUnknown_Click;
            // 
            // btnSonEmpty
            // 
            btnSonEmpty.Location = new System.Drawing.Point(176, 5);
            btnSonEmpty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSonEmpty.Name = "btnSonEmpty";
            btnSonEmpty.Size = new System.Drawing.Size(165, 39);
            btnSonEmpty.TabIndex = 3;
            btnSonEmpty.Text = "打开子标签为空";
            btnSonEmpty.UseVisualStyleBackColor = true;
            btnSonEmpty.Click += btnSonEmpty_Click;
            // 
            // btnLabelOverlapping
            // 
            btnLabelOverlapping.Location = new System.Drawing.Point(4, 5);
            btnLabelOverlapping.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnLabelOverlapping.Name = "btnLabelOverlapping";
            btnLabelOverlapping.Size = new System.Drawing.Size(165, 39);
            btnLabelOverlapping.TabIndex = 2;
            btnLabelOverlapping.Text = "打开标签重叠图片";
            btnLabelOverlapping.UseVisualStyleBackColor = true;
            btnLabelOverlapping.Click += btnLabelOverlapping_Click;
            // 
            // tpLabelSelect
            // 
            tpLabelSelect.Controls.Add(lvLabels);
            tpLabelSelect.Controls.Add(panel2);
            tpLabelSelect.Location = new System.Drawing.Point(4, 29);
            tpLabelSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tpLabelSelect.Name = "tpLabelSelect";
            tpLabelSelect.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tpLabelSelect.Size = new System.Drawing.Size(362, 887);
            tpLabelSelect.TabIndex = 0;
            tpLabelSelect.Text = "标签筛选";
            tpLabelSelect.UseVisualStyleBackColor = true;
            // 
            // lvLabels
            // 
            lvLabels.CheckBoxes = true;
            lvLabels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            lvLabels.Location = new System.Drawing.Point(4, 92);
            lvLabels.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lvLabels.MultiSelect = false;
            lvLabels.Name = "lvLabels";
            lvLabels.Size = new System.Drawing.Size(354, 790);
            lvLabels.TabIndex = 1;
            lvLabels.UseCompatibleStateImageBehavior = false;
            lvLabels.View = System.Windows.Forms.View.Details;
            lvLabels.ItemCheck += lvLabels_ItemCheck;
            lvLabels.MouseClick += lvLabels_MouseClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "标签名";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "标签颜色";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "填充";
            // 
            // panel2
            // 
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(4, 5);
            panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(354, 87);
            panel2.TabIndex = 0;
            // 
            // btOpenImg
            // 
            btOpenImg.Controls.Add(groupBox3);
            btOpenImg.Controls.Add(groupBox2);
            btOpenImg.Controls.Add(groupBox1);
            btOpenImg.Location = new System.Drawing.Point(4, 29);
            btOpenImg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOpenImg.Name = "btOpenImg";
            btOpenImg.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btOpenImg.Size = new System.Drawing.Size(362, 887);
            btOpenImg.TabIndex = 2;
            btOpenImg.Text = "图片搜索";
            btOpenImg.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tbLabelId);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox3.Location = new System.Drawing.Point(4, 82);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox3.Size = new System.Drawing.Size(354, 77);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "标签编号";
            // 
            // tbLabelId
            // 
            tbLabelId.Dock = System.Windows.Forms.DockStyle.Fill;
            tbLabelId.Location = new System.Drawing.Point(4, 25);
            tbLabelId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbLabelId.Name = "tbLabelId";
            tbLabelId.Size = new System.Drawing.Size(346, 27);
            tbLabelId.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnOpenImg);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox2.Location = new System.Drawing.Point(4, 715);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox2.Size = new System.Drawing.Size(354, 167);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作";
            // 
            // btnOpenImg
            // 
            btnOpenImg.Location = new System.Drawing.Point(10, 35);
            btnOpenImg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnOpenImg.Name = "btnOpenImg";
            btnOpenImg.Size = new System.Drawing.Size(112, 39);
            btnOpenImg.TabIndex = 0;
            btnOpenImg.Text = "打开图片";
            btnOpenImg.UseVisualStyleBackColor = true;
            btnOpenImg.Click += btnOpenImg_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbImgId);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(4, 5);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Size = new System.Drawing.Size(354, 77);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "图片编号";
            // 
            // tbImgId
            // 
            tbImgId.Dock = System.Windows.Forms.DockStyle.Fill;
            tbImgId.Location = new System.Drawing.Point(4, 25);
            tbImgId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbImgId.Name = "tbImgId";
            tbImgId.Size = new System.Drawing.Size(346, 27);
            tbImgId.TabIndex = 0;
            // 
            // ofdOpenFile
            // 
            ofdOpenFile.FileName = "Save";
            // 
            // timeAutoSave
            // 
            timeAutoSave.Enabled = true;
            timeAutoSave.Interval = 30000;
            timeAutoSave.Tick += timeAutoSave_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1875, 920);
            Controls.Add(splitContainer1);
            KeyPreview = true;
            MainMenuStrip = msMain;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "繁星标注  V1.0";
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            KeyDown += MainForm_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            tcMain.ResumeLayout(false);
            tpLabelInfo.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tpLabelSelect.ResumeLayout(false);
            btOpenImg.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入Voc数据集ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 导入图片数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入VocXml标注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出Voc数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出EasyData数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出选中分类到ImageNet数据集ToolStripMenuItem;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ImageList ilMain;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.FolderBrowserDialog fbdOpenFolder;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpLabelSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tpLabelInfo;
        private System.Windows.Forms.ListView lvLabels;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private UserForm.LabelImageUserControl liShow;
        private UserForm.LabelInfoUserControl liMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.Timer timeAutoSave;
        private System.Windows.Forms.TabPage btOpenImg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbImgId;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbLabelId;
        private System.Windows.Forms.Button btnLabelOverlapping;
        private System.Windows.Forms.Button btnSonEmpty;
        private System.Windows.Forms.Button btnUnknown;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 打开数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 历史数据集ToolStripMenuItem;
        private System.Windows.Forms.Button btnCheckBox;
        private System.Windows.Forms.Button btnFullLabel;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 减少标注图片等级ToolStripMenuItem;
        private System.Windows.Forms.Button btnShowLabel;
        private System.Windows.Forms.ToolStripMenuItem 修改程序集属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.Button btnSetAllLabel;
        private System.Windows.Forms.ToolStripMenuItem 增加标注图片等级ToolStripMenuItem;
    }
}

