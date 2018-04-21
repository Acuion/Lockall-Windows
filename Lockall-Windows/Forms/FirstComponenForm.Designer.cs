namespace Lockall_Windows
{
    partial class FirstComponentForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstComponentForm));
            this.secw1Text = new System.Windows.Forms.TextBox();
            this.secw2Text = new System.Windows.Forms.TextBox();
            this.secw3Text = new System.Windows.Forms.TextBox();
            this.secw4Text = new System.Windows.Forms.TextBox();
            this.secw5Text = new System.Windows.Forms.TextBox();
            this.secw6Text = new System.Windows.Forms.TextBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // secw1Text
            // 
            this.secw1Text.Location = new System.Drawing.Point(12, 12);
            this.secw1Text.Name = "secw1Text";
            this.secw1Text.Size = new System.Drawing.Size(130, 20);
            this.secw1Text.TabIndex = 0;
            // 
            // secw2Text
            // 
            this.secw2Text.Location = new System.Drawing.Point(148, 12);
            this.secw2Text.Name = "secw2Text";
            this.secw2Text.Size = new System.Drawing.Size(130, 20);
            this.secw2Text.TabIndex = 1;
            // 
            // secw3Text
            // 
            this.secw3Text.Location = new System.Drawing.Point(284, 12);
            this.secw3Text.Name = "secw3Text";
            this.secw3Text.Size = new System.Drawing.Size(130, 20);
            this.secw3Text.TabIndex = 2;
            // 
            // secw4Text
            // 
            this.secw4Text.Location = new System.Drawing.Point(12, 38);
            this.secw4Text.Name = "secw4Text";
            this.secw4Text.Size = new System.Drawing.Size(130, 20);
            this.secw4Text.TabIndex = 3;
            // 
            // secw5Text
            // 
            this.secw5Text.Location = new System.Drawing.Point(148, 38);
            this.secw5Text.Name = "secw5Text";
            this.secw5Text.Size = new System.Drawing.Size(130, 20);
            this.secw5Text.TabIndex = 4;
            // 
            // secw6Text
            // 
            this.secw6Text.Location = new System.Drawing.Point(284, 38);
            this.secw6Text.Name = "secw6Text";
            this.secw6Text.Size = new System.Drawing.Size(130, 20);
            this.secw6Text.TabIndex = 5;
            // 
            // trayIcon
            // 
            this.trayIcon.Visible = true;
            // 
            // FirstComponentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 69);
            this.Controls.Add(this.secw6Text);
            this.Controls.Add(this.secw5Text);
            this.Controls.Add(this.secw1Text);
            this.Controls.Add(this.secw4Text);
            this.Controls.Add(this.secw2Text);
            this.Controls.Add(this.secw3Text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FirstComponentForm";
            this.Text = "Key base";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FirstComponentForm_FormClosing);
            this.Shown += new System.EventHandler(this.FirstComponentForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox secw1Text;
        private System.Windows.Forms.TextBox secw2Text;
        private System.Windows.Forms.TextBox secw3Text;
        private System.Windows.Forms.TextBox secw4Text;
        private System.Windows.Forms.TextBox secw5Text;
        private System.Windows.Forms.TextBox secw6Text;
        private System.Windows.Forms.NotifyIcon trayIcon;
    }
}

