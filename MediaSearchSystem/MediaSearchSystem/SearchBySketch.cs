using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaSearchSystem
{
    public partial class SearchBySketch : Form
    {
        private bool isDrawing = false;
        private Point previousPoint;
        public SearchBySketch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap sketch = new Bitmap(pictureBoxSketch.Image);
            string sketchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sketch.png");
            sketch.Save(sketchPath);

            string[] imagePaths = Directory.GetFiles(@"E:\HTTT1\nam4\ki1\gd2\DPT\BC\ImageDatabase", "*.png");
            var results = new List<(string ImagePath, double Similarity)>();

            // Tải ảnh phác thảo và áp dụng tiền xử lý
            using (Image<Gray, byte> sketchImage = new Image<Gray, byte>(sketchPath))
            {
                // Tiền xử lý ảnh phác thảo
                Image<Gray, byte> processedSketch = PreprocessImage(sketchImage);

                foreach (string imagePath in imagePaths)
                {
                    using (Image<Gray, byte> dbImage = new Image<Gray, byte>(imagePath))
                    {
                        // Resize ảnh cơ sở dữ liệu
                        Image<Gray, byte> resizedDbImage = dbImage.Resize(processedSketch.Width, processedSketch.Height, Emgu.CV.CvEnum.Inter.Linear);

                        // Tiền xử lý ảnh cơ sở dữ liệu
                        Image<Gray, byte> processedDbImage = PreprocessImage(resizedDbImage);

                        // Tính độ tương đồng bằng MatchTemplate
                        Mat result = new Mat();
                        CvInvoke.MatchTemplate(processedDbImage, processedSketch, result, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                        double[] minValues, maxValues;
                        Point[] minLocations, maxLocations;
                        result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                        double similarity = maxValues[0]; // Độ tương đồng cao nhất
                        results.Add((imagePath, similarity));
                    }
                }
            }

            // Sắp xếp danh sách kết quả theo độ tương đồng giảm dần
            results = results.OrderByDescending(r => r.Similarity).ToList();

            // Hiển thị kết quả
            if (results.Any())
            {
                flowLayoutPanelResults.Controls.Clear(); // Xóa các kết quả trước đó

                foreach (var result in results)
                {
                    // Tạo một PictureBox để hiển thị ảnh
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(result.ImagePath);
                    pb.SizeMode = PictureBoxSizeMode.Zoom; // Để ảnh vừa với khung
                    pb.Width = 150; // Chiều rộng của khung ảnh
                    pb.Height = 150; // Chiều cao của khung ảnh

                    // Thêm tooltip để hiển thị độ tương đồng
                    ToolTip tooltip = new ToolTip();
                    tooltip.SetToolTip(pb, $"Độ tương đồng: {result.Similarity:P2}");

                    // Thêm PictureBox vào FlowLayoutPanel
                    flowLayoutPanelResults.Controls.Add(pb);
                }

                MessageBox.Show($"Đã tìm thấy {results.Count} ảnh, sắp xếp theo độ tương đồng.");
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh nào phù hợp.");
            }
        }

        // Hàm tiền xử lý ảnh (giữ nguyên như trước)
        private Image<Gray, byte> PreprocessImage(Image<Gray, byte> inputImage)
        {
            // 1. Cân bằng độ sáng
            Image<Gray, byte> equalizedImage = new Image<Gray, byte>(inputImage.Size);
            CvInvoke.EqualizeHist(inputImage, equalizedImage);

            // 2. Làm mờ để giảm nhiễu
            CvInvoke.GaussianBlur(equalizedImage, equalizedImage, new Size(5, 5), 1.5);

            // 3. Phát hiện biên (Edge Detection) bằng Sobel hoặc Canny
            Image<Gray, byte> edgeImage = new Image<Gray, byte>(equalizedImage.Size);
            CvInvoke.Canny(equalizedImage, edgeImage, 100, 200);

            return edgeImage; // Trả về ảnh đã qua tiền xử lý
        }

        private void ClearSketch()
        {
            pictureBoxSketch.Image = new Bitmap(pictureBoxSketch.Width, pictureBoxSketch.Height);
        }

        private void pictureBoxSketch_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            previousPoint = e.Location;
        }

        private void pictureBoxSketch_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(pictureBoxSketch.Image))
                {
                    g.DrawLine(Pens.Black, previousPoint, e.Location);
                }
                previousPoint = e.Location;
                pictureBoxSketch.Invalidate();
            }

        }

        private void pictureBoxSketch_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void SearchBySketch_Load(object sender, EventArgs e)
        {
            pictureBoxSketch.Image = new Bitmap(pictureBoxSketch.Width, pictureBoxSketch.Height);
        }

        private void ic_back_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }
    }
}
