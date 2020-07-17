namespace PictureAnnotation
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbMian = new System.Windows.Forms.PictureBox();
            this.palFoot = new System.Windows.Forms.Panel();
            this.lvMain = new System.Windows.Forms.ListView();
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.palNext = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.palLast = new System.Windows.Forms.Panel();
            this.btnLast = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvLables = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.fbdOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).BeginInit();
            this.palFoot.SuspendLayout();
            this.palNext.SuspendLayout();
            this.palLast.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1250, 25);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            this.msMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.msMain_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem6});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem5.Text = "加载Voc数据集";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem3.Text = "加载图片数据集";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem2.Text = "加载Voc-XML标注";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem4.Text = "导出Voc数据集";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem6.Text = "导出EasyData数据集";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pbMian);
            this.splitContainer1.Panel1.Controls.Add(this.palFoot);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcMain);
            this.splitContainer1.Size = new System.Drawing.Size(1250, 527);
            this.splitContainer1.SplitterDistance = 1000;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // pbMian
            // 
            this.pbMian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMian.Location = new System.Drawing.Point(0, 0);
            this.pbMian.Name = "pbMian";
            this.pbMian.Size = new System.Drawing.Size(1000, 407);
            this.pbMian.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMian.TabIndex = 1;
            this.pbMian.TabStop = false;
            this.pbMian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseDown);
            this.pbMian.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseMove);
            this.pbMian.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseUp);
            // 
            // palFoot
            // 
            this.palFoot.Controls.Add(this.lvMain);
            this.palFoot.Controls.Add(this.palNext);
            this.palFoot.Controls.Add(this.palLast);
            this.palFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.palFoot.Location = new System.Drawing.Point(0, 407);
            this.palFoot.Name = "palFoot";
            this.palFoot.Size = new System.Drawing.Size(1000, 120);
            this.palFoot.TabIndex = 0;
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
            this.lvMain.ItemActivate += new System.EventHandler(this.lvMain_ItemActivate);
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            this.lvMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseClick);
            // 
            // ilMain
            // 
            this.ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMain.Images.SetKeyName(0, "000fb092e2f84211a854d62aaff65736.jpg");
            // 
            // palNext
            // 
            this.palNext.Controls.Add(this.btnNext);
            this.palNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.palNext.Location = new System.Drawing.Point(955, 0);
            this.palNext.Name = "palNext";
            this.palNext.Size = new System.Drawing.Size(45, 120);
            this.palNext.TabIndex = 1;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNext.Font = new System.Drawing.Font("Microsoft YaHei UI", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 120);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // palLast
            // 
            this.palLast.Controls.Add(this.btnLast);
            this.palLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.palLast.Location = new System.Drawing.Point(0, 0);
            this.palLast.Name = "palLast";
            this.palLast.Size = new System.Drawing.Size(45, 120);
            this.palLast.TabIndex = 0;
            // 
            // btnLast
            // 
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLast.Font = new System.Drawing.Font("Microsoft YaHei UI", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLast.Location = new System.Drawing.Point(0, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 120);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "<";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(246, 527);
            this.tcMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvLables);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(238, 497);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "标签筛选";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvLables
            // 
            this.lvLables.CheckBoxes = true;
            this.lvLables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvLables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLables.FullRowSelect = true;
            this.lvLables.HideSelection = false;
            this.lvLables.Location = new System.Drawing.Point(3, 81);
            this.lvLables.MultiSelect = false;
            this.lvLables.Name = "lvLables";
            this.lvLables.Size = new System.Drawing.Size(232, 413);
            this.lvLables.TabIndex = 1;
            this.lvLables.UseCompatibleStateImageBehavior = false;
            this.lvLables.View = System.Windows.Forms.View.Details;
            this.lvLables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvLables_ItemCheck);
            this.lvLables.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvLables_MouseClick);
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
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 78);
            this.panel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(238, 497);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图片设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 552);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.msMain);
            this.Name = "MainForm";
            this.Text = "标注工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).EndInit();
            this.palFoot.ResumeLayout(false);
            this.palNext.ResumeLayout(false);
            this.palLast.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pbMian;
        private System.Windows.Forms.Panel palFoot;
        private System.Windows.Forms.Panel palLast;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.Panel palNext;
        private System.Windows.Forms.ImageList ilMain;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.FolderBrowserDialog fbdOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvLables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

