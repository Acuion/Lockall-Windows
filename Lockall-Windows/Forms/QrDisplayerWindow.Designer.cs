namespace Lockall_Windows.Forms
{
    partial class QrDisplayerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageQr = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageQr)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageQr
            // 
            this.ImageQr.Location = new System.Drawing.Point(12, 12);
            this.ImageQr.Name = "ImageQr";
            this.ImageQr.Size = new System.Drawing.Size(265, 251);
            this.ImageQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageQr.TabIndex = 0;
            this.ImageQr.TabStop = false;
            // 
            // QrDisplayerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 274);
            this.Controls.Add(this.ImageQr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QrDisplayerWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QrDisplayerWindow";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.ImageQr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageQr;
    }
}