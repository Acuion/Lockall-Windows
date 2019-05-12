namespace Lockall_Windows.Forms
{
    partial class PasswordGetterForm
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
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarPasslen = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPasslen = new System.Windows.Forms.Label();
            this.checkBoxUppercase = new System.Windows.Forms.CheckBox();
            this.checkBoxLowercase = new System.Windows.Forms.CheckBox();
            this.checkBoxDigits = new System.Windows.Forms.CheckBox();
            this.checkBoxSymbols = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonVis = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPasslen)).BeginInit();
            this.SuspendLayout();
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(12, 12);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '•';
            this.passwordBox.Size = new System.Drawing.Size(243, 20);
            this.passwordBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGenerate);
            this.groupBox1.Controls.Add(this.checkBoxSymbols);
            this.groupBox1.Controls.Add(this.checkBoxDigits);
            this.groupBox1.Controls.Add(this.checkBoxLowercase);
            this.groupBox1.Controls.Add(this.checkBoxUppercase);
            this.groupBox1.Controls.Add(this.labelPasslen);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBarPasslen);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 155);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate";
            // 
            // trackBarPasslen
            // 
            this.trackBarPasslen.Location = new System.Drawing.Point(6, 35);
            this.trackBarPasslen.Maximum = 64;
            this.trackBarPasslen.Minimum = 6;
            this.trackBarPasslen.Name = "trackBarPasslen";
            this.trackBarPasslen.Size = new System.Drawing.Size(303, 45);
            this.trackBarPasslen.TabIndex = 2;
            this.trackBarPasslen.Value = 12;
            this.trackBarPasslen.Scroll += new System.EventHandler(this.trackBarPasslen_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Length:";
            // 
            // labelPasslen
            // 
            this.labelPasslen.AutoSize = true;
            this.labelPasslen.Location = new System.Drawing.Point(55, 19);
            this.labelPasslen.Name = "labelPasslen";
            this.labelPasslen.Size = new System.Drawing.Size(19, 13);
            this.labelPasslen.TabIndex = 4;
            this.labelPasslen.Text = "12";
            // 
            // checkBoxUppercase
            // 
            this.checkBoxUppercase.AutoSize = true;
            this.checkBoxUppercase.Checked = true;
            this.checkBoxUppercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUppercase.Location = new System.Drawing.Point(9, 74);
            this.checkBoxUppercase.Name = "checkBoxUppercase";
            this.checkBoxUppercase.Size = new System.Drawing.Size(125, 17);
            this.checkBoxUppercase.TabIndex = 5;
            this.checkBoxUppercase.Text = "Uppercase letters (A)";
            this.checkBoxUppercase.UseVisualStyleBackColor = true;
            // 
            // checkBoxLowercase
            // 
            this.checkBoxLowercase.AutoSize = true;
            this.checkBoxLowercase.Checked = true;
            this.checkBoxLowercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLowercase.Location = new System.Drawing.Point(9, 97);
            this.checkBoxLowercase.Name = "checkBoxLowercase";
            this.checkBoxLowercase.Size = new System.Drawing.Size(124, 17);
            this.checkBoxLowercase.TabIndex = 6;
            this.checkBoxLowercase.Text = "Lowercase letters (a)";
            this.checkBoxLowercase.UseVisualStyleBackColor = true;
            // 
            // checkBoxDigits
            // 
            this.checkBoxDigits.AutoSize = true;
            this.checkBoxDigits.Checked = true;
            this.checkBoxDigits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDigits.Location = new System.Drawing.Point(212, 74);
            this.checkBoxDigits.Name = "checkBoxDigits";
            this.checkBoxDigits.Size = new System.Drawing.Size(67, 17);
            this.checkBoxDigits.TabIndex = 7;
            this.checkBoxDigits.Text = "Digits (4)";
            this.checkBoxDigits.UseVisualStyleBackColor = true;
            // 
            // checkBoxSymbols
            // 
            this.checkBoxSymbols.AutoSize = true;
            this.checkBoxSymbols.Location = new System.Drawing.Point(212, 97);
            this.checkBoxSymbols.Name = "checkBoxSymbols";
            this.checkBoxSymbols.Size = new System.Drawing.Size(85, 17);
            this.checkBoxSymbols.TabIndex = 8;
            this.checkBoxSymbols.Text = "Symbols (@)";
            this.checkBoxSymbols.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(9, 120);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(300, 23);
            this.buttonGenerate.TabIndex = 9;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(297, 12);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(30, 20);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonVis
            // 
            this.buttonVis.Location = new System.Drawing.Point(261, 12);
            this.buttonVis.Name = "buttonVis";
            this.buttonVis.Size = new System.Drawing.Size(30, 20);
            this.buttonVis.TabIndex = 3;
            this.buttonVis.Text = "Vis";
            this.buttonVis.UseVisualStyleBackColor = true;
            this.buttonVis.Click += new System.EventHandler(this.buttonVis_Click);
            // 
            // PasswordGetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 204);
            this.Controls.Add(this.buttonVis);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.passwordBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordGetterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PasswordGetterForm";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPasslen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelPasslen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarPasslen;
        private System.Windows.Forms.CheckBox checkBoxLowercase;
        private System.Windows.Forms.CheckBox checkBoxUppercase;
        private System.Windows.Forms.CheckBox checkBoxSymbols;
        private System.Windows.Forms.CheckBox checkBoxDigits;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonVis;
    }
}