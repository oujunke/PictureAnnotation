namespace PictureAnnotationForm.UserForm
{
    partial class LabelImageUserControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.pbMian = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.Location = new System.Drawing.Point(483, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 317);
            this.btnNext.TabIndex = 3;
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
            this.btnLast.Size = new System.Drawing.Size(45, 317);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "<";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // pbMian
            // 
            this.pbMian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMian.Location = new System.Drawing.Point(45, 0);
            this.pbMian.Margin = new System.Windows.Forms.Padding(0);
            this.pbMian.Name = "pbMian";
            this.pbMian.Size = new System.Drawing.Size(438, 317);
            this.pbMian.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbMian.TabIndex = 4;
            this.pbMian.TabStop = false;
            this.pbMian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseDown);
            this.pbMian.MouseLeave += new System.EventHandler(this.pbMian_MouseLeave);
            this.pbMian.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseMove);
            this.pbMian.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMian_MouseUp);
            // 
            // LabelImageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMian);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.DoubleBuffered = true;
            this.Name = "LabelImageUserControl";
            this.Size = new System.Drawing.Size(528, 317);
            ((System.ComponentModel.ISupportInitialize)(this.pbMian)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.PictureBox pbMian;
    }
}
