namespace Lockall_Windows.Forms
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wifiRB = new System.Windows.Forms.RadioButton();
            this.bluetoothRB = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bluetoothRB);
            this.groupBox1.Controls.Add(this.wifiRB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection type";
            // 
            // wifiRB
            // 
            this.wifiRB.AutoSize = true;
            this.wifiRB.Location = new System.Drawing.Point(8, 19);
            this.wifiRB.Name = "wifiRB";
            this.wifiRB.Size = new System.Drawing.Size(46, 17);
            this.wifiRB.TabIndex = 1;
            this.wifiRB.TabStop = true;
            this.wifiRB.Text = "WiFi";
            this.wifiRB.UseVisualStyleBackColor = true;
            this.wifiRB.CheckedChanged += new System.EventHandler(this.wifiRB_CheckedChanged);
            // 
            // bluetoothRB
            // 
            this.bluetoothRB.AutoSize = true;
            this.bluetoothRB.Location = new System.Drawing.Point(8, 42);
            this.bluetoothRB.Name = "bluetoothRB";
            this.bluetoothRB.Size = new System.Drawing.Size(70, 17);
            this.bluetoothRB.TabIndex = 2;
            this.bluetoothRB.TabStop = true;
            this.bluetoothRB.Text = "Bluetooth";
            this.bluetoothRB.UseVisualStyleBackColor = true;
            this.bluetoothRB.CheckedChanged += new System.EventHandler(this.bluetoothRB_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 88);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton bluetoothRB;
        private System.Windows.Forms.RadioButton wifiRB;
    }
}