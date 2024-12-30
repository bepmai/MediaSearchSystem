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
            button1 = new Button();
            pictureBoxSketch = new PictureBox();
            flowLayoutPanelResults = new FlowLayoutPanel();
            label1 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            ic_back = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSketch).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ic_back).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(379, 128);
            button1.Name = "button1";
            button1.Size = new Size(120, 51);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBoxSketch
            // 
            pictureBoxSketch.Location = new Point(12, 128);
            pictureBoxSketch.Name = "pictureBoxSketch";
            pictureBoxSketch.Size = new Size(361, 351);
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
            flowLayoutPanelResults.Location = new Point(520, 128);
            flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            flowLayoutPanelResults.Size = new Size(268, 351);
            flowLayoutPanelResults.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(266, 27);
            label1.Name = "label1";
            label1.Size = new Size(291, 30);
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
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 84);
            panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(590, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(99, 69);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // ic_back
            // 
            ic_back.Image = (Image)resources.GetObject("ic_back.Image");
            ic_back.Location = new Point(111, 36);
            ic_back.Name = "ic_back";
            ic_back.Size = new Size(23, 26);
            ic_back.SizeMode = PictureBoxSizeMode.Zoom;
            ic_back.TabIndex = 4;
            ic_back.TabStop = false;
            ic_back.Click += ic_back_Click;
            // 
            // SearchBySketch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 491);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanelResults);
            Controls.Add(pictureBoxSketch);
            Controls.Add(button1);
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

        private Button button1;
        private PictureBox pictureBoxSketch;
        private FlowLayoutPanel flowLayoutPanelResults;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox ic_back;
    }
}