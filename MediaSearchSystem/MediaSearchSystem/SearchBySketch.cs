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
using Emgu.CV.Features2D;
using System.Diagnostics;

namespace MediaSearchSystem
{
    public partial class SearchBySketch : Form
    {
        private bool isDrawing = false;
        private Point previousPoint;
        private string solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        private List<List<Point>> drawnLines = new List<List<Point>>(); // Lưu danh sách các nét vẽ
        private List<Point> currentLine = null; // Lưu tạm nét đang vẽ

        public SearchBySketch()
        {
            InitializeComponent();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Lưu ảnh phác thảo vào tệp tạm
                Bitmap sketch = new Bitmap(pictureBoxSketch.Image);
                Bitmap background = new Bitmap(sketch.Width, sketch.Height);
                using (Graphics g = Graphics.FromImage(background))
                {
                    g.Clear(Color.White);
                    g.DrawImage(sketch, 0, 0);
                }
                string sketchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sketch.jpg");
                background.Save(sketchPath);

                string imageDirectory = Path.Combine(solutionDirectory, "Resources", "ImageDatabase");
                string[] imagePaths = Directory.GetFiles(imageDirectory, "*.*")
                    .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (imagePaths.Length == 0)
                {
                    MessageBox.Show("Thư mục không chứa ảnh nào hợp lệ!");
                    return;
                }

                var results = new List<(string ImagePath, double Similarity)>();

                // Áp dụng tiền xử lý cho ảnh phác thảo
                using (Image<Gray, byte> sketchImage = new Image<Gray, byte>(sketchPath))
                {
                    Image<Gray, byte> processedSketch = PreprocessImage(sketchImage);
                    var orb = new ORB();
                    VectorOfKeyPoint keypointsSketch = new VectorOfKeyPoint();
                    Mat descriptorsSketch = new Mat();
                    orb.DetectAndCompute(processedSketch, null, keypointsSketch, descriptorsSketch, false);

                    // Nếu không phát hiện được đặc trưng trong ảnh phác thảo, thoát khỏi hàm
                    if (descriptorsSketch.IsEmpty || keypointsSketch.Size == 0)
                    {
                        MessageBox.Show("Không phát hiện được đặc trưng từ ảnh phác thảo.");
                        return;
                    }

                    foreach (string imagePath in imagePaths)
                    {
                        using (Image<Gray, byte> dbImage = new Image<Gray, byte>(imagePath))
                        {
                            // Resize ảnh cơ sở dữ liệu để khớp với ảnh phác thảo
                            Image<Gray, byte> resizedDbImage = dbImage.Resize(processedSketch.Width, processedSketch.Height, Inter.Linear);
                            Image<Gray, byte> processedDbImage = PreprocessImage(resizedDbImage);

                            // Tiền xử lý ảnh trong cơ sở dữ liệu
                            VectorOfKeyPoint keypointsDb = new VectorOfKeyPoint();
                            Mat descriptorsDb = new Mat();
                            orb.DetectAndCompute(processedDbImage, null, keypointsDb, descriptorsDb, false);

                            // Nếu không tìm thấy đặc trưng trong ảnh cơ sở dữ liệu, bỏ qua ảnh này
                            if (descriptorsDb.IsEmpty || keypointsDb.Size == 0)
                            {
                                continue;
                            }

                            // So sánh đặc trưng giữa ảnh phác thảo và ảnh cơ sở dữ liệu
                            var bfMatcher = new BFMatcher(DistanceType.Hamming, crossCheck: true);
                            VectorOfDMatch matches = new VectorOfDMatch();
                            bfMatcher.Match(descriptorsSketch, descriptorsDb, matches);

                            // Tính tổng khoảng cách và tính độ tương đồng
                            double totalDistance = 0.0;
                            for (int i = 0; i < matches.Size; i++)
                            {
                                var match = matches[i];
                                totalDistance += match.Distance;
                            }

                            // Đảm bảo tránh chia cho 0 trong trường hợp không có sự khớp nào
                            double similarity = matches.Size > 0 ? 1.0 / (1.0 + totalDistance / matches.Size) : 0.0;
                            results.Add((imagePath, similarity));
                        }
                    }
                }

                // Lọc và sắp xếp kết quả theo độ tương đồng
                const double threshold = 0.03; // Ngưỡng tương đồng
                results = results.Where(r => r.Similarity >= threshold)
                                 .OrderByDescending(r => r.Similarity)
                                 .ToList();

                // Hiển thị kết quả
                DisplayResults(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                Debug.WriteLine(ex.Message);
            }
        }
        private Image<Gray, byte> PreprocessImage(Image<Gray, byte> inputImage)
        {
            int standardSize = 200; // Kích thước chuẩn
            Image<Gray, byte> resizedImage = inputImage.Resize(standardSize, standardSize, Inter.Cubic);

            // Cân bằng histogram
            CvInvoke.EqualizeHist(resizedImage, resizedImage);

            // Làm mờ để giảm nhiễu
            CvInvoke.GaussianBlur(resizedImage, resizedImage, new Size(3, 3), 1.0);

            // Phát hiện biên (Edge Detection)
            double otsuThreshold = CvInvoke.Threshold(resizedImage, new Mat(), 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            double lowerThreshold = otsuThreshold * 0.5;
            double upperThreshold = otsuThreshold * 1.5;

            Image<Gray, byte> edgeImage = new Image<Gray, byte>(resizedImage.Size);
            CvInvoke.Canny(resizedImage, edgeImage, lowerThreshold, upperThreshold);

            return edgeImage;
        }

        private void DisplayResults(List<(string ImagePath, double Similarity)> results)
        {
            flowLayoutPanelResults.Controls.Clear(); // Xóa kết quả cũ

            if (results.Any())
            {
                foreach (var result in results)
                {
                    // Tạo PictureBox để hiển thị ảnh
                    PictureBox pb = new PictureBox
                    {
                        Image = Image.FromFile(result.ImagePath),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = 150,
                        Height = 150
                    };

                    // Thêm tooltip hiển thị độ tương đồng
                    ToolTip tooltip = new ToolTip();
                    tooltip.SetToolTip(pb, $"Độ tương đồng: {result.Similarity:P2}");

                    flowLayoutPanelResults.Controls.Add(pb);
                }

                MessageBox.Show($"Đã tìm thấy {results.Count} ảnh phù hợp với độ tương đồng cao.");
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh nào phù hợp.");
            }
        }

        private void pictureBoxSketch_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            currentLine = new List<Point> { e.Location }; // Tạo nét mới với điểm bắt đầu
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
                currentLine.Add(e.Location);
                pictureBoxSketch.Invalidate();
            }
        }

        private void pictureBoxSketch_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            if (currentLine != null && currentLine.Count > 1)
            {
                drawnLines.Add(currentLine); // Lưu nét vừa vẽ vào danh sách
            }
            currentLine = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flowLayoutPanelResults.Controls.Clear();
            pictureBoxSketch.Image = new Bitmap(pictureBoxSketch.Width, pictureBoxSketch.Height);

            using (Graphics g = Graphics.FromImage(pictureBoxSketch.Image))
            {
                g.Clear(Color.White);
            }
            drawnLines.Clear();
            pictureBoxSketch.Invalidate();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (drawnLines.Count > 0)
            {
                // Xóa nét cuối cùng
                drawnLines.RemoveAt(drawnLines.Count - 1);

                // Tạo lại hình ảnh từ các nét còn lại
                pictureBoxSketch.Image = new Bitmap(pictureBoxSketch.Width, pictureBoxSketch.Height);

                using (Graphics g = Graphics.FromImage(pictureBoxSketch.Image))
                {
                    g.Clear(Color.White); // Làm sạch hình nền
                    foreach (var line in drawnLines)
                    {
                        for (int i = 1; i < line.Count; i++)
                        {
                            g.DrawLine(Pens.Black, line[i - 1], line[i]); // Vẽ lại từng nét
                        }
                    }
                }

                pictureBoxSketch.Invalidate();
            }
            else
            {
                MessageBox.Show("Không còn nét nào để xóa!");
            }
        }
    }
}
