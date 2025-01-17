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
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnTextSearch = new Button();
            txtContentSearch = new TextBox();
            btnChoseImage = new Button();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackgroundImage = (Image)resources.GetObject("flowLayoutPanel1.BackgroundImage");
            flowLayoutPanel1.Location = new Point(12, 104);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(517, 346);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnTextSearch
            // 
            btnTextSearch.Location = new Point(535, 161);
            btnTextSearch.Name = "btnTextSearch";
            btnTextSearch.Size = new Size(75, 23);
            btnTextSearch.TabIndex = 3;
            btnTextSearch.Text = "Tìm kiếm";
            btnTextSearch.UseVisualStyleBackColor = true;
            btnTextSearch.Click += btnTextSearch_Click;
            // 
            // txtContentSearch
            // 
            txtContentSearch.Location = new Point(535, 104);
            txtContentSearch.Name = "txtContentSearch";
            txtContentSearch.PlaceholderText = "Hãy nhập nội dung ảnh bạn muốn tìm";
            txtContentSearch.Size = new Size(250, 23);
            txtContentSearch.TabIndex = 4;
            txtContentSearch.TextChanged += txtContentSearch_TextChanged;
            // 
            // btnChoseImage
            // 
            btnChoseImage.Location = new Point(535, 427);
            btnChoseImage.Name = "btnChoseImage";
            btnChoseImage.Size = new Size(75, 23);
            btnChoseImage.TabIndex = 5;
            btnChoseImage.Text = "Chọn ảnh";
            btnChoseImage.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(535, 190);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(253, 221);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // SearchByText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 467);
            Controls.Add(pictureBox2);
            Controls.Add(btnChoseImage);
            Controls.Add(txtContentSearch);
            Controls.Add(btnTextSearch);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel1);
            Name = "SearchByText";
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox ic_back;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnTextSearch;
        private TextBox txtContentSearch;
        private Button btnChoseImage;
        private PictureBox pictureBox2;
    }
}