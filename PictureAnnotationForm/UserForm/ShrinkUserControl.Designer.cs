namespace PictureAnnotationForm.UserForm
{
    partial class ShrinkUserControl
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
            MainPanel = new System.Windows.Forms.Panel();
            button1 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.BackColor = System.Drawing.SystemColors.Control;
            MainPanel.Location = new System.Drawing.Point(3, 32);
            MainPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new System.Drawing.Size(246, 194);
            MainPanel.TabIndex = 3;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Top;
            button1.Location = new System.Drawing.Point(3, 3);
            button1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(246, 29);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ShrinkUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(MainPanel);
            Controls.Add(button1);
            Name = "ShrinkUserControl";
            Padding = new System.Windows.Forms.Padding(3);
            Size = new System.Drawing.Size(252, 229);
            VisibleChanged += ShrinkUserControl_VisibleChanged;
            Paint += ShrinkUserControl_Paint;
            MouseLeave += ShrinkUserControl_MouseLeave;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Panel MainPanel;
    }
}
