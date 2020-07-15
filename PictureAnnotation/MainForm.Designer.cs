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
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbMian = new System.Windows.Forms.PictureBox();
            this.palFoot = new System.Windows.Forms.Panel();
            this.lvMain = new System.Windows.Forms.ListView();
            this.palNext = new System.Windows.Forms.Panel();
            this.palLast = new System.Windows.Forms.Panel();
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).BeginInit();
            this.palFoot.SuspendLayout();
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
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem4});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem5.Text = "加载Voc数据集";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem3.Text = "加载图片数据集";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem2.Text = "加载Voc-XML标注";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem4.Text = "导出Voc模型";
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
            this.pbMian.TabIndex = 1;
            this.pbMian.TabStop = false;
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
            this.lvMain.Location = new System.Drawing.Point(45, 0);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Scrollable = false;
            this.lvMain.Size = new System.Drawing.Size(910, 120);
            this.lvMain.TabIndex = 2;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            // 
            // palNext
            // 
            this.palNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.palNext.Location = new System.Drawing.Point(955, 0);
            this.palNext.Name = "palNext";
            this.palNext.Size = new System.Drawing.Size(45, 120);
            this.palNext.TabIndex = 1;
            // 
            // palLast
            // 
            this.palLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.palLast.Location = new System.Drawing.Point(0, 0);
            this.palLast.Name = "palLast";
            this.palLast.Size = new System.Drawing.Size(45, 120);
            this.palLast.TabIndex = 0;
            // 
            // ilMain
            // 
            this.ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilMain.ImageSize = new System.Drawing.Size(16, 16);
            this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).EndInit();
            this.palFoot.ResumeLayout(false);
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
    }
}

