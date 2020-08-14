namespace PictureAnnotationForm.UserForm
{
    partial class ImageLabelShowUserControl
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
            this.SuspendLayout();
            // 
            // ImageLabelShowUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ImageLabelShowUserControl";
            this.Size = new System.Drawing.Size(0, 0);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageLabelShowUserControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageLabelShowUserControl_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ImageLabelShowUserControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ImageLabelShowUserControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageLabelShowUserControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageLabelShowUserControl_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ImageLabelShowUserControl_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
