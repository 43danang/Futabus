namespace futabus
{
    partial class Form_datve
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
            this.button1 = new System.Windows.Forms.Button();
            this.label_idchuyendi = new System.Windows.Forms.Label();
            this.label_diemdi = new System.Windows.Forms.Label();
            this.label_diemden = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(150, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_idchuyendi
            // 
            this.label_idchuyendi.AutoSize = true;
            this.label_idchuyendi.Location = new System.Drawing.Point(182, 195);
            this.label_idchuyendi.Name = "label_idchuyendi";
            this.label_idchuyendi.Size = new System.Drawing.Size(44, 16);
            this.label_idchuyendi.TabIndex = 1;
            this.label_idchuyendi.Text = "label1";
            // 
            // label_diemdi
            // 
            this.label_diemdi.AutoSize = true;
            this.label_diemdi.Location = new System.Drawing.Point(386, 192);
            this.label_diemdi.Name = "label_diemdi";
            this.label_diemdi.Size = new System.Drawing.Size(85, 16);
            this.label_diemdi.TabIndex = 2;
            this.label_diemdi.Text = "label_diemdi";
            this.label_diemdi.Click += new System.EventHandler(this.label_diemdi_Click);
            // 
            // label_diemden
            // 
            this.label_diemden.AutoSize = true;
            this.label_diemden.Location = new System.Drawing.Point(453, 280);
            this.label_diemden.Name = "label_diemden";
            this.label_diemden.Size = new System.Drawing.Size(97, 16);
            this.label_diemden.TabIndex = 3;
            this.label_diemden.Text = "label_diemden";
            // 
            // Form_datve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_diemden);
            this.Controls.Add(this.label_diemdi);
            this.Controls.Add(this.label_idchuyendi);
            this.Controls.Add(this.button1);
            this.Name = "Form_datve";
            this.Text = "Form_datve";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_idchuyendi;
        private System.Windows.Forms.Label label_diemdi;
        private System.Windows.Forms.Label label_diemden;
    }
}