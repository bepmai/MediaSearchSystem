namespace MediaSearchSystem
{
    partial class SearchByImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchByImage));
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            ic_back = new PictureBox();
            label2 = new Label();
            pbSearchImage = new PictureBox();
            btnSearch = new Button();
            btnDelete = new Button();
            flpResults = new FlowLayoutPanel();
            label3 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSearchImage).BeginInit();
            flpResults.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(271, 33);
            label1.Name = "label1";
            label1.Size = new Size(366, 37);
            label1.TabIndex = 1;
            label1.Text = "Tìm kiếm ảnh bằng hình ảnh";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(ic_back);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(914, 112);
            panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(674, 11);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(113, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            //pictureBox1.Click += pictureBox1_Click;
            // 
            // ic_back
            // 
            ic_back.Image = (Image)resources.GetObject("ic_back.Image");
            ic_back.Location = new Point(127, 48);
            ic_back.Margin = new Padding(3, 4, 3, 4);
            ic_back.Name = "ic_back";
            ic_back.Size = new Size(26, 35);
            ic_back.SizeMode = PictureBoxSizeMode.Zoom;
            ic_back.TabIndex = 4;
            ic_back.TabStop = false;
            ic_back.Click += ic_back_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(324, 129);
            label2.Name = "label2";
            label2.Size = new Size(204, 20);
            label2.TabIndex = 6;
            label2.Text = "Chọn ảnh bạn muốn tìm kiếm";
            // 
            // pbSearchImage
            // 
            pbSearchImage.BackColor = SystemColors.GradientInactiveCaption;
            pbSearchImage.Location = new Point(271, 170);
            pbSearchImage.Name = "pbSearchImage";
            pbSearchImage.Size = new Size(299, 129);
            pbSearchImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbSearchImage.TabIndex = 7;
            pbSearchImage.TabStop = false;
            pbSearchImage.Click += pbSearchImage_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(630, 185);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(152, 46);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(630, 253);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(152, 46);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // 
            // flpResults
            // 
            flpResults.Controls.Add(label3);
            flpResults.Location = new Point(26, 316);
            flpResults.Name = "flpResults";
            flpResults.Size = new Size(863, 327);
            flpResults.TabIndex = 10;
            flpResults.Paint += flpResults_Paint;

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(0, 20);
            label3.TabIndex = 11;
            // 
            // SearchByImage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 655);
            Controls.Add(flpResults);
            Controls.Add(btnDelete);
            Controls.Add(btnSearch);
            Controls.Add(pbSearchImage);
            Controls.Add(label2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "SearchByImage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchByImage";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSearchImage).EndInit();
            flpResults.ResumeLayout(false);
            flpResults.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox ic_back;
        private Label label2;
        private PictureBox pbSearchImage;
        private Button btnSearch;
        private Button btnDelete;
        private FlowLayoutPanel flpResults;
        private Label label3;
    }
}