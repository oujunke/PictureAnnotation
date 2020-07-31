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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.liShow = new PictureAnnotationForm.UserForm.LabelImageUserForm();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvMain = new System.Windows.Forms.ListView();
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载Voc数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.加载图片数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载VocXml标注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出Voc数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出EasyData数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出选中分类到ImageNet数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出子分类到ImageNet数据集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpLabelInfo = new System.Windows.Forms.TabPage();
            this.liMain = new PictureAnnotationForm.UserForm.LabelInfoUserForm();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnOpenImg = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbLabelId = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbImgId = new System.Windows.Forms.TextBox();
            this.tpLabelSelect = new System.Windows.Forms.TabPage();
            this.lvLabels = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.fbdOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.timeAutoSave = new System.Windows.Forms.Timer(this.components);
            this.导出子分类对错集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpLabelInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tpLabelSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.liShow);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.msMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcMain);
            this.splitContainer1.Size = new System.Drawing.Size(1250, 552);
            this.splitContainer1.SplitterDistance = 1000;
            this.splitContainer1.TabIndex = 0;
            // 
            // liShow
            // 
            this.liShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liShow.Location = new System.Drawing.Point(0, 25);
            this.liShow.Name = "liShow";
            this.liShow.Size = new System.Drawing.Size(1000, 407);
            this.liShow.TabIndex = 2;
            this.liShow.ImageLast += new System.Action(this.liShow_ImageLast);
            this.liShow.ImageNext += new System.Action(this.liShow_ImageNext);
            this.liShow.LabelChange += new System.Action<PictureAnnotationForm.Models.ImageLabelsModel>(this.liShow_LabelChange);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvMain);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 432);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 120);
            this.panel1.TabIndex = 1;
            // 
            // lvMain
            // 
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.HideSelection = false;
            this.lvMain.LargeImageList = this.ilMain;
            this.lvMain.Location = new System.Drawing.Point(45, 0);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Scrollable = false;
            this.lvMain.Size = new System.Drawing.Size(910, 120);
            this.lvMain.TabIndex = 2;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.SizeChanged += new System.EventHandler(this.lvMain_SizeChanged);
            this.lvMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseClick);
            // 
            // ilMain
            // 
            this.ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ilMain.ImageSize = new System.Drawing.Size(240, 120);
            this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.Location = new System.Drawing.Point(955, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 120);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLast.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLast.Location = new System.Drawing.Point(0, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 120);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "<";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.导出ToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1000, 25);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载Voc数据集ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.加载图片数据集ToolStripMenuItem,
            this.加载VocXml标注ToolStripMenuItem,
            this.toolStripSeparator1,
            this.保存数据ToolStripMenuItem,
            this.加载数据ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 加载Voc数据集ToolStripMenuItem
            // 
            this.加载Voc数据集ToolStripMenuItem.Name = "加载Voc数据集ToolStripMenuItem";
            this.加载Voc数据集ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.加载Voc数据集ToolStripMenuItem.Text = "加载Voc数据集";
            this.加载Voc数据集ToolStripMenuItem.Click += new System.EventHandler(this.加载Voc数据集ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem1.Text = "加载BoxWord数据集";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 加载图片数据集ToolStripMenuItem
            // 
            this.加载图片数据集ToolStripMenuItem.Name = "加载图片数据集ToolStripMenuItem";
            this.加载图片数据集ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.加载图片数据集ToolStripMenuItem.Text = "加载图片数据集";
            this.加载图片数据集ToolStripMenuItem.Click += new System.EventHandler(this.加载图片数据集ToolStripMenuItem_Click);
            // 
            // 加载VocXml标注ToolStripMenuItem
            // 
            this.加载VocXml标注ToolStripMenuItem.Name = "加载VocXml标注ToolStripMenuItem";
            this.加载VocXml标注ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.加载VocXml标注ToolStripMenuItem.Text = "加载Voc-Xml标注";
            this.加载VocXml标注ToolStripMenuItem.Click += new System.EventHandler(this.加载VocXml标注ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // 保存数据ToolStripMenuItem
            // 
            this.保存数据ToolStripMenuItem.Name = "保存数据ToolStripMenuItem";
            this.保存数据ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.保存数据ToolStripMenuItem.Text = "保存数据";
            this.保存数据ToolStripMenuItem.Click += new System.EventHandler(this.保存数据ToolStripMenuItem_Click);
            // 
            // 加载数据ToolStripMenuItem
            // 
            this.加载数据ToolStripMenuItem.Name = "加载数据ToolStripMenuItem";
            this.加载数据ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.加载数据ToolStripMenuItem.Text = "加载数据";
            this.加载数据ToolStripMenuItem.Click += new System.EventHandler(this.加载数据ToolStripMenuItem_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出Voc数据集ToolStripMenuItem,
            this.导出EasyData数据集ToolStripMenuItem,
            this.导出选中分类到ImageNet数据集ToolStripMenuItem,
            this.导出子分类到ImageNet数据集ToolStripMenuItem,
            this.导出子分类对错集ToolStripMenuItem});
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.导出ToolStripMenuItem.Text = "导出";
            // 
            // 导出Voc数据集ToolStripMenuItem
            // 
            this.导出Voc数据集ToolStripMenuItem.Name = "导出Voc数据集ToolStripMenuItem";
            this.导出Voc数据集ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.导出Voc数据集ToolStripMenuItem.Text = "导出Voc数据集";
            this.导出Voc数据集ToolStripMenuItem.Click += new System.EventHandler(this.导出Voc数据集ToolStripMenuItem_Click);
            // 
            // 导出EasyData数据集ToolStripMenuItem
            // 
            this.导出EasyData数据集ToolStripMenuItem.Name = "导出EasyData数据集ToolStripMenuItem";
            this.导出EasyData数据集ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.导出EasyData数据集ToolStripMenuItem.Text = "导出EsayData数据集";
            this.导出EasyData数据集ToolStripMenuItem.Click += new System.EventHandler(this.导出EasyData数据集ToolStripMenuItem_Click);
            // 
            // 导出选中分类到ImageNet数据集ToolStripMenuItem
            // 
            this.导出选中分类到ImageNet数据集ToolStripMenuItem.Name = "导出选中分类到ImageNet数据集ToolStripMenuItem";
            this.导出选中分类到ImageNet数据集ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.导出选中分类到ImageNet数据集ToolStripMenuItem.Text = "导出选中分类到ImageNet数据集";
            this.导出选中分类到ImageNet数据集ToolStripMenuItem.Click += new System.EventHandler(this.导出选中分类到ImageNet数据集ToolStripMenuItem_Click);
            // 
            // 导出子分类到ImageNet数据集ToolStripMenuItem
            // 
            this.导出子分类到ImageNet数据集ToolStripMenuItem.Name = "导出子分类到ImageNet数据集ToolStripMenuItem";
            this.导出子分类到ImageNet数据集ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.导出子分类到ImageNet数据集ToolStripMenuItem.Text = "导出子分类到ImageNet数据集";
            this.导出子分类到ImageNet数据集ToolStripMenuItem.Click += new System.EventHandler(this.导出子分类到ImageNet数据集ToolStripMenuItem_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpLabelInfo);
            this.tcMain.Controls.Add(this.tpLabelSelect);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(246, 552);
            this.tcMain.TabIndex = 0;
            // 
            // tpLabelInfo
            // 
            this.tpLabelInfo.Controls.Add(this.liMain);
            this.tpLabelInfo.Controls.Add(this.panel3);
            this.tpLabelInfo.Location = new System.Drawing.Point(4, 22);
            this.tpLabelInfo.Name = "tpLabelInfo";
            this.tpLabelInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpLabelInfo.Size = new System.Drawing.Size(238, 526);
            this.tpLabelInfo.TabIndex = 1;
            this.tpLabelInfo.Text = "标签设置";
            this.tpLabelInfo.UseVisualStyleBackColor = true;
            // 
            // liMain
            // 
            this.liMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liMain.Location = new System.Drawing.Point(3, 143);
            this.liMain.Name = "liMain";
            this.liMain.Size = new System.Drawing.Size(232, 380);
            this.liMain.TabIndex = 1;
            this.liMain.LabelChange += new System.Action<PictureAnnotationForm.Models.ImageLabelsModel>(this.liMain_LabelChange);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 140);
            this.panel3.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnOpenImg);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox6.Location = new System.Drawing.Point(0, 91);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(232, 49);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "操作";
            // 
            // btnOpenImg
            // 
            this.btnOpenImg.Location = new System.Drawing.Point(7, 21);
            this.btnOpenImg.Name = "btnOpenImg";
            this.btnOpenImg.Size = new System.Drawing.Size(75, 23);
            this.btnOpenImg.TabIndex = 0;
            this.btnOpenImg.Text = "打开图片";
            this.btnOpenImg.UseVisualStyleBackColor = true;
            this.btnOpenImg.Click += new System.EventHandler(this.btnOpenImg_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbLabelId);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 46);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 46);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "标签编号";
            // 
            // tbLabelId
            // 
            this.tbLabelId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLabelId.Location = new System.Drawing.Point(3, 17);
            this.tbLabelId.Name = "tbLabelId";
            this.tbLabelId.Size = new System.Drawing.Size(226, 21);
            this.tbLabelId.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbImgId);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(232, 46);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "图片编号";
            // 
            // tbImgId
            // 
            this.tbImgId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbImgId.Location = new System.Drawing.Point(3, 17);
            this.tbImgId.Name = "tbImgId";
            this.tbImgId.Size = new System.Drawing.Size(226, 21);
            this.tbImgId.TabIndex = 0;
            // 
            // tpLabelSelect
            // 
            this.tpLabelSelect.Controls.Add(this.lvLabels);
            this.tpLabelSelect.Controls.Add(this.panel2);
            this.tpLabelSelect.Location = new System.Drawing.Point(4, 22);
            this.tpLabelSelect.Name = "tpLabelSelect";
            this.tpLabelSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tpLabelSelect.Size = new System.Drawing.Size(238, 526);
            this.tpLabelSelect.TabIndex = 0;
            this.tpLabelSelect.Text = "标签筛选";
            this.tpLabelSelect.UseVisualStyleBackColor = true;
            // 
            // lvLabels
            // 
            this.lvLabels.CheckBoxes = true;
            this.lvLabels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLabels.HideSelection = false;
            this.lvLabels.Location = new System.Drawing.Point(3, 55);
            this.lvLabels.MultiSelect = false;
            this.lvLabels.Name = "lvLabels";
            this.lvLabels.Size = new System.Drawing.Size(232, 468);
            this.lvLabels.TabIndex = 1;
            this.lvLabels.UseCompatibleStateImageBehavior = false;
            this.lvLabels.View = System.Windows.Forms.View.Details;
            this.lvLabels.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvLabels_ItemCheck);
            this.lvLabels.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvLabels_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标签名";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标签颜色";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "填充";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 52);
            this.panel2.TabIndex = 0;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "Save";
            // 
            // timeAutoSave
            // 
            this.timeAutoSave.Enabled = true;
            this.timeAutoSave.Interval = 30000;
            this.timeAutoSave.Tick += new System.EventHandler(this.timeAutoSave_Tick);
            // 
            // 导出子分类对错集ToolStripMenuItem
            // 
            this.导出子分类对错集ToolStripMenuItem.Name = "导出子分类对错集ToolStripMenuItem";
            this.导出子分类对错集ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.导出子分类对错集ToolStripMenuItem.Text = "导出子分类对错集";
            this.导出子分类对错集ToolStripMenuItem.Click += new System.EventHandler(this.导出子分类对错集ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 552);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.msMain;
            this.Name = "MainForm";
            this.Text = "繁星标注";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpLabelInfo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tpLabelSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载Voc数据集ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 加载图片数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载VocXml标注ToolStripMenuItem;
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
        private UserForm.LabelImageUserForm liShow;
        private UserForm.LabelInfoUserForm liMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 保存数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载数据ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.Timer timeAutoSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbLabelId;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbImgId;
        private System.Windows.Forms.ToolStripMenuItem 导出子分类到ImageNet数据集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出子分类对错集ToolStripMenuItem;
    }
}

