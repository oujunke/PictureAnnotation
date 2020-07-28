namespace PictureAnnotationForm.UserForm
{
    partial class LabelInfoUserForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSubName = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudY1 = new System.Windows.Forms.NumericUpDown();
            this.nudX1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nudY2 = new System.Windows.Forms.NumericUpDown();
            this.nudX2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudDown = new System.Windows.Forms.NumericUpDown();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbSubName);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(181, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签名称";
            // 
            // tbSubName
            // 
            this.tbSubName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbSubName.Location = new System.Drawing.Point(6, 54);
            this.tbSubName.Name = "tbSubName";
            this.tbSubName.Size = new System.Drawing.Size(169, 21);
            this.tbSubName.TabIndex = 1;
            this.tbSubName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbSubName.Leave += new System.EventHandler(this.tbName_Leave);
            this.tbSubName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbName_PreviewKeyDown);
            // 
            // tbName
            // 
            this.tbName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbName.Location = new System.Drawing.Point(6, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(169, 21);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Leave += new System.EventHandler(this.tbName_Leave);
            this.tbName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbName_PreviewKeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudY1);
            this.groupBox2.Controls.Add(this.nudX1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 53);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标签左上角坐标";
            // 
            // nudY1
            // 
            this.nudY1.Location = new System.Drawing.Point(92, 26);
            this.nudY1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudY1.Name = "nudY1";
            this.nudY1.Size = new System.Drawing.Size(82, 21);
            this.nudY1.TabIndex = 3;
            this.nudY1.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nudX1
            // 
            this.nudX1.Location = new System.Drawing.Point(4, 26);
            this.nudX1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudX1.Name = "nudX1";
            this.nudX1.Size = new System.Drawing.Size(82, 21);
            this.nudX1.TabIndex = 2;
            this.nudX1.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudY2);
            this.groupBox3.Controls.Add(this.nudX2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(181, 57);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "标签右下角坐标";
            // 
            // nudY2
            // 
            this.nudY2.Location = new System.Drawing.Point(91, 20);
            this.nudY2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudY2.Name = "nudY2";
            this.nudY2.Size = new System.Drawing.Size(82, 21);
            this.nudY2.TabIndex = 5;
            this.nudY2.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // nudX2
            // 
            this.nudX2.Location = new System.Drawing.Point(3, 20);
            this.nudX2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudX2.Name = "nudX2";
            this.nudX2.Size = new System.Drawing.Size(82, 21);
            this.nudX2.TabIndex = 4;
            this.nudX2.ValueChanged += new System.EventHandler(this.nud_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nudDown);
            this.groupBox4.Controls.Add(this.nudTop);
            this.groupBox4.Controls.Add(this.nudRight);
            this.groupBox4.Controls.Add(this.nudLeft);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 191);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(181, 90);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "标签坐标位移";
            // 
            // nudDown
            // 
            this.nudDown.Location = new System.Drawing.Point(91, 51);
            this.nudDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudDown.Name = "nudDown";
            this.nudDown.Size = new System.Drawing.Size(82, 21);
            this.nudDown.TabIndex = 7;
            this.nudDown.ValueChanged += new System.EventHandler(this.nudDown_ValueChanged);
            // 
            // nudTop
            // 
            this.nudTop.Location = new System.Drawing.Point(3, 51);
            this.nudTop.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(82, 21);
            this.nudTop.TabIndex = 6;
            this.nudTop.ValueChanged += new System.EventHandler(this.nudTop_ValueChanged);
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(91, 20);
            this.nudRight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(82, 21);
            this.nudRight.TabIndex = 5;
            this.nudRight.ValueChanged += new System.EventHandler(this.nudRight_ValueChanged);
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(3, 20);
            this.nudLeft.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(82, 21);
            this.nudLeft.TabIndex = 4;
            this.nudLeft.ValueChanged += new System.EventHandler(this.nudLeft_ValueChanged);
            // 
            // LabelInfoUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LabelInfoUserForm";
            this.Size = new System.Drawing.Size(181, 263);
            this.Resize += new System.EventHandler(this.LabelInfoUserForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nudY1;
        private System.Windows.Forms.NumericUpDown nudX1;
        private System.Windows.Forms.NumericUpDown nudY2;
        private System.Windows.Forms.NumericUpDown nudX2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudDown;
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.TextBox tbSubName;
    }
}
