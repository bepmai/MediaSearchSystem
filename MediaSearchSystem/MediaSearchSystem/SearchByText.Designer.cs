namespace MediaSearchSystem
{
    partial class SearchByText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchByText));
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            ic_back = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(238, 27);
            label1.Name = "label1";
            label1.Size = new Size(279, 30);
            label1.TabIndex = 1;
            label1.Text = "Tìm kiếm ảnh bằng văn bản";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(ic_back);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 84);
            panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(535, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(99, 69);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // ic_back
            // 
            ic_back.Image = (Image)resources.GetObject("ic_back.Image");
            ic_back.Location = new Point(56, 31);
            ic_back.Name = "ic_back";
            ic_back.Size = new Size(23, 26);
            ic_back.SizeMode = PictureBoxSizeMode.Zoom;
            ic_back.TabIndex = 2;
            ic_back.TabStop = false;
            ic_back.Click += ic_back_Click_1;
            // 
            // SearchByText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 491);
            Controls.Add(panel1);
            Name = "SearchByText";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchByText";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox ic_back;
    }
}