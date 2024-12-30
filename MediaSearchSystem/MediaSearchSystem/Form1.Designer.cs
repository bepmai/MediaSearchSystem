namespace MediaSearchSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            label1 = new Label();
            byText = new PictureBox();
            bySound = new PictureBox();
            byImg = new PictureBox();
            bySketch = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)byText).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bySound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)byImg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bySketch).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 84);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(197, 26);
            label1.Name = "label1";
            label1.Size = new Size(388, 30);
            label1.TabIndex = 1;
            label1.Text = "Hệ thống tìm kiếm ảnh đa phương tiện";
            // 
            // byText
            // 
            byText.Image = (Image)resources.GetObject("byText.Image");
            byText.Location = new Point(140, 123);
            byText.Name = "byText";
            byText.Size = new Size(172, 125);
            byText.SizeMode = PictureBoxSizeMode.Zoom;
            byText.TabIndex = 1;
            byText.TabStop = false;
            byText.Click += byText_Click;
            // 
            // bySound
            // 
            bySound.Image = (Image)resources.GetObject("bySound.Image");
            bySound.Location = new Point(484, 123);
            bySound.Name = "bySound";
            bySound.Size = new Size(172, 125);
            bySound.SizeMode = PictureBoxSizeMode.Zoom;
            bySound.TabIndex = 2;
            bySound.TabStop = false;
            bySound.Click += bySound_Click;
            // 
            // byImg
            // 
            byImg.Image = (Image)resources.GetObject("byImg.Image");
            byImg.Location = new Point(140, 299);
            byImg.Name = "byImg";
            byImg.Size = new Size(172, 125);
            byImg.SizeMode = PictureBoxSizeMode.Zoom;
            byImg.TabIndex = 3;
            byImg.TabStop = false;
            byImg.Click += byImg_Click;
            // 
            // bySketch
            // 
            bySketch.Image = Properties.Resources.graphic_tablet;
            bySketch.Location = new Point(484, 299);
            bySketch.Name = "bySketch";
            bySketch.Size = new Size(172, 125);
            bySketch.SizeMode = PictureBoxSizeMode.Zoom;
            bySketch.TabIndex = 4;
            bySketch.TabStop = false;
            bySketch.Click += bySketch_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.ForestGreen;
            label2.Location = new Point(140, 251);
            label2.Name = "label2";
            label2.Size = new Size(172, 21);
            label2.TabIndex = 5;
            label2.Text = "Tìm kiếm bằng văn bản";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.ForestGreen;
            label3.Location = new Point(484, 251);
            label3.Name = "label3";
            label3.Size = new Size(183, 21);
            label3.TabIndex = 6;
            label3.Text = "Tìm kiếm bằng âm thanh";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.ForestGreen;
            label4.Location = new Point(140, 427);
            label4.Name = "label4";
            label4.Size = new Size(178, 21);
            label4.TabIndex = 7;
            label4.Text = "Tìm kiếm bằng hình ảnh";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.ForestGreen;
            label5.Location = new Point(484, 427);
            label5.Name = "label5";
            label5.Size = new Size(180, 21);
            label5.TabIndex = 8;
            label5.Text = "Tìm kiếm bằng phác họa";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 491);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(bySketch);
            Controls.Add(byImg);
            Controls.Add(bySound);
            Controls.Add(byText);
            Controls.Add(panel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)byText).EndInit();
            ((System.ComponentModel.ISupportInitialize)bySound).EndInit();
            ((System.ComponentModel.ISupportInitialize)byImg).EndInit();
            ((System.ComponentModel.ISupportInitialize)bySketch).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private PictureBox byText;
        private PictureBox bySound;
        private PictureBox byImg;
        private PictureBox bySketch;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
