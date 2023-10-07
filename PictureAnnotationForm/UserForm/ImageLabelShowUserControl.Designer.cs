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
            SuspendLayout();
            // 
            // ImageLabelShowUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(16F, 33F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            DoubleBuffered = true;
            Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "ImageLabelShowUserControl";
            Size = new System.Drawing.Size(0, 0);
            Paint += ImageLabelShowUserControl_Paint;
            MouseDown += ImageLabelShowUserControl_MouseDown;
            MouseEnter += ImageLabelShowUserControl_MouseEnter;
            MouseLeave += ImageLabelShowUserControl_MouseLeave;
            MouseMove += ImageLabelShowUserControl_MouseMove;
            MouseUp += ImageLabelShowUserControl_MouseUp;
            PreviewKeyDown += ImageLabelShowUserControl_PreviewKeyDown;
            ResumeLayout(false);
        }

        #endregion
    }
}
