namespace MediaSearchSystem
{
    partial class SearchBySketch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchBySketch));
            btnSearch = new Button();
            pictureBoxSketch = new PictureBox();
            flowLayoutPanelResults = new FlowLayoutPanel();
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            ic_back = new PictureBox();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSketch).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).BeginInit();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(442, 171);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(137, 68);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // pictureBoxSketch
            // 
            pictureBoxSketch.BackColor = SystemColors.ButtonHighlight;
            pictureBoxSketch.Location = new Point(14, 171);
            pictureBoxSketch.Margin = new Padding(3, 4, 3, 4);
            pictureBoxSketch.Name = "pictureBoxSketch";
            pictureBoxSketch.Size = new Size(413, 468);
            pictureBoxSketch.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSketch.TabIndex = 1;
            pictureBoxSketch.TabStop = false;
            pictureBoxSketch.MouseDown += pictureBoxSketch_MouseDown;
            pictureBoxSketch.MouseMove += pictureBoxSketch_MouseMove;
            pictureBoxSketch.MouseUp += pictureBoxSketch_MouseUp;
            // 
            // flowLayoutPanelResults
            // 
            flowLayoutPanelResults.AutoScroll = true;
            flowLayoutPanelResults.BackColor = SystemColors.ButtonHighlight;
            flowLayoutPanelResults.Location = new Point(594, 171);
            flowLayoutPanelResults.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            flowLayoutPanelResults.Size = new Size(306, 468);
            flowLayoutPanelResults.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(304, 36);
            label1.Name = "label1";
            label1.Size = new Size(370, 37);
            label1.TabIndex = 1;
            label1.Text = "Tìm kiếm ảnh bằng phác họa";
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
            panel1.TabIndex = 4;
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
            // btnClear
            // 
            btnClear.Location = new Point(442, 270);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(137, 68);
            btnClear.TabIndex = 5;
            btnClear.Text = "Xóa";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // SearchBySketch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 655);
            Controls.Add(btnClear);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanelResults);
            Controls.Add(pictureBoxSketch);
            Controls.Add(btnSearch);
            Margin = new Padding(3, 4, 3, 4);
            Name = "SearchBySketch";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchBySketch";
            Load += SearchBySketch_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxSketch).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSearch;
        private PictureBox pictureBoxSketch;
        private FlowLayoutPanel flowLayoutPanelResults;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox ic_back;
        private Button btnClear;
    }
}