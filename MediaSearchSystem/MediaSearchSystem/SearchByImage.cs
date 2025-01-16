using OpenCvSharp;

namespace MediaSearchSystem
{
    public partial class SearchByImage : Form
    {
        private Dictionary<string, (Mat Histogram, Mat Edges, Mat ORBDescriptors)> imageIndex = new Dictionary<string, (Mat, Mat, Mat)>();

        private Mat inputImage;
        private Mat inputHist;
        private Mat inputEdges;
        private Mat inputORBDescriptors;

        public SearchByImage()
        {
            InitializeComponent();
        }

        private void ic_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        // Tiền xử lý ảnh: đọc ảnh, thay đổi kích cỡ, và lọc nhiễu Gaussian
        private Mat PreprocessImage(string imagePath)
        {
            Mat image = Cv2.ImRead(imagePath); // Đọc ảnh
            Mat resizedImage = new Mat();
            Cv2.Resize(image, resizedImage, new OpenCvSharp.Size(215, 215)); // Đưa ảnh về kích cỡ 215x215
            Mat smoothedImage = new Mat();
            Cv2.GaussianBlur(resizedImage, smoothedImage, new OpenCvSharp.Size(5, 5), 0); // Lọc nhiễu Gaussian
            return smoothedImage;
        }

        // Trích chọn đặc trưng: HSV Histogram, Canny edges, và ORB
        private class FeatureExtractor
        {
            public Mat ComputeHSVHistogram(Mat image)
            {
                Mat hsvImage = new Mat();
                Cv2.CvtColor(image, hsvImage, ColorConversionCodes.BGR2HSV);
                int[] histSize = { 50, 60 }; // Hue và Saturation bins
                Rangef[] ranges = { new Rangef(0, 180), new Rangef(0, 256) };
                Mat hist = new Mat();
                Cv2.CalcHist(new[] { hsvImage }, new[] { 0, 1 }, null, hist, 2, histSize, ranges);
                Cv2.Normalize(hist, hist);
                return hist;
            }

            public Mat DetectEdges(Mat image)
            {
                Mat edges = new Mat();
                Cv2.Canny(image, edges, 100, 200); // Phát hiện biên cạnh Canny
                return edges;
            }

            public KeyPoint[] ExtractORBFeatures(Mat image, out Mat descriptors)
            {
                var orb = OpenCvSharp.ORB.Create();
                // Phát hiện các điểm đặc trưng (keypoints)
                KeyPoint[] keypoints = orb.Detect(image);

                // Tính toán descriptors cho các keypoints
                descriptors = new Mat();
                orb.Compute(image, ref keypoints, descriptors); // Lưu ý tham số keypoints là ref

                return keypoints;
            }
        }

        // Xây dựng chỉ mục ảnh
        private void AddToIndex(string imagePath, Mat histogram, Mat edges, Mat descriptors)
        {
            imageIndex[imagePath] = (histogram, edges, descriptors);
        }

        // So khớp các đặc trưng của ảnh
        private class ImageMatcher
        {
            public double MatchHistogram(Mat hist1, Mat hist2)
            {
                return Cv2.CompareHist(hist1, hist2, HistCompMethods.Correl);
            }

            public int MatchORB(Mat desc1, Mat desc2)
            {
                if (desc1.Empty() || desc2.Empty()) return int.MaxValue;

                var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
                var matches = bf.Match(desc1, desc2);

                // Cộng dồn khoảng cách và chuyển đổi giá trị float thành int
                return matches.Sum(m => (int)m.Distance);
            }

            public double MatchEdges(Mat edges1, Mat edges2)
            {
                Mat diff = new Mat();
                Cv2.Absdiff(edges1, edges2, diff);
                return Cv2.CountNonZero(diff) / (double)(edges1.Rows * edges1.Cols);
            }

            public Dictionary<string, double> FindSimilarImages(Mat inputHist, Mat inputEdges, Mat inputORB,
                Dictionary<string, (Mat Histogram, Mat Edges, Mat ORBDescriptors)> index,
                double w1 = 0.4, double w2 = 0.3, double w3 = 0.3)
            {
                var results = new Dictionary<string, double>();

                foreach (var item in index)
                {
                    double histScore = MatchHistogram(inputHist, item.Value.Histogram);
                    double edgeScore = 1.0 - MatchEdges(inputEdges, item.Value.Edges);
                    int orbScore = MatchORB(inputORB, item.Value.ORBDescriptors);

                    double totalScore = w1 * histScore + w2 * edgeScore + w3 * (1.0 / (1 + orbScore));
                    results[item.Key] = totalScore;
                }
                // Sắp xếp và in ra danh sách kết quả
                //Console.WriteLine("Danh sách kết quả đo khoảng cách:");
                //foreach (var result in results.OrderByDescending(r => r.Value))
                //{
                //    Console.WriteLine($"File: {result.Key}, Score: {result.Value:F4}");
                //}


                return results.OrderByDescending(r => r.Value).ToDictionary(r => r.Key, r => r.Value);
            }
        }

        // tìm kiếm
        private void pbSearchImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string inputPath = openFileDialog.FileName;
                    pbSearchImage.Image = System.Drawing.Image.FromFile(inputPath);

                    // Tiền xử lý ảnh
                    inputImage = PreprocessImage(inputPath);

                    // Trích chọn đặc trưng
                    FeatureExtractor extractor = new FeatureExtractor();
                    inputHist = extractor.ComputeHSVHistogram(inputImage);
                    inputEdges = extractor.DetectEdges(inputImage);
                    extractor.ExtractORBFeatures(inputImage, out inputORBDescriptors);
                }
            }
        }

        // Tạo chỉ mục cho các ảnh trong thư mục dữ liệu (chỉ làm một lần)
        private void BuildIndex(string[] imagePaths)
        {
            FeatureExtractor extractor = new FeatureExtractor();
            foreach (var path in imagePaths)
            {
                Mat image = PreprocessImage(path);
                Mat hist = extractor.ComputeHSVHistogram(image);
                Mat edges = extractor.DetectEdges(image);
                Mat descriptors;
                extractor.ExtractORBFeatures(image, out descriptors);
                AddToIndex(path, hist, edges, descriptors);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string[] dataFolderPaths = System.IO.Directory.GetFiles(@"D:\Nam4\DPT\ck_dpt\Resources\ImageDatabase\archive\dataset\val\images", "*.*", System.IO.SearchOption.AllDirectories);
            BuildIndex(dataFolderPaths); // Xây dựng chỉ mục ảnh

            ImageMatcher matcher = new ImageMatcher();
            var results = matcher.FindSimilarImages(inputHist, inputEdges, inputORBDescriptors, imageIndex);

            flpResults.Controls.Clear();

            foreach (var result in results.Take(20))
            {
                string filePath = result.Key;
                double score = result.Value;

                Image img = System.Drawing.Image.FromFile(filePath);

                PictureBox pictureBox = new PictureBox()
                {
                    Image = img,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 150,
                    Height = 150
                };

                ////  Label hiển thị điểm số 
                //Label label3 = new Label()
                //{
                //    Name = "label3",
                //    Text = $"Độtươngtự: {score:F4}",
                //    TextAlign = ContentAlignment.MiddleCenter,
                //    AutoSize = false,
                //    Dock = DockStyle.Bottom,
                //    Height = 20
                //};

                Panel panel = new Panel()
                {
                    Width = 150,
                    Height = 170,
                    Margin = new Padding(5)
                };

                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label3);

                flpResults.Controls.Add(panel);
            }


        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            flpResults.Controls.Clear();

            if (pbSearchImage.Image != null)
            {
                pbSearchImage.Image.Dispose();
                pbSearchImage.Image = null;
            }

            inputImage = null;
            inputHist = null;
            inputEdges = null;
            inputORBDescriptors = null;
        }
        private void flpResults_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
