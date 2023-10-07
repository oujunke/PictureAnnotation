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
            btnNext = new System.Windows.Forms.Button();
            btnLast = new System.Windows.Forms.Button();
            pbMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbMain).BeginInit();
            SuspendLayout();
            // 
            // btnNext
            // 
            btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            btnNext.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            btnNext.Location = new System.Drawing.Point(724, 0);
            btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(68, 528);
            btnNext.TabIndex = 3;
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
            btnLast.Size = new System.Drawing.Size(68, 528);
            btnLast.TabIndex = 2;
            btnLast.Text = "<";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += btnLast_Click;
            // 
            // pbMain
            // 
            pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pbMain.Location = new System.Drawing.Point(68, 0);
            pbMain.Margin = new System.Windows.Forms.Padding(0);
            pbMain.Name = "pbMain";
            pbMain.Size = new System.Drawing.Size(656, 528);
            pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pbMain.TabIndex = 4;
            pbMain.TabStop = false;
            pbMain.SizeChanged += pbMian_SizeChanged;
            pbMain.MouseDown += pbMian_MouseDown;
            pbMain.MouseEnter += pbMian_MouseEnter;
            pbMain.MouseLeave += pbMian_MouseLeave;
            pbMain.MouseMove += pbMian_MouseMove;
            pbMain.MouseUp += pbMian_MouseUp;
            // 
            // LabelImageUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pbMain);
            Controls.Add(btnNext);
            Controls.Add(btnLast);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "LabelImageUserControl";
            Size = new System.Drawing.Size(792, 528);
            ((System.ComponentModel.ISupportInitialize)pbMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.PictureBox pbMain;
    }
}
