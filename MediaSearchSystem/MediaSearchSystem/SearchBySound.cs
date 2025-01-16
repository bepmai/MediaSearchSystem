using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Vosk;

namespace MediaSearchSystem
{
    public partial class SearchBySound : Form
    {
        private WaveIn waveIn;
        private WaveFileWriter writer;
        private string outputFilePath = "recordedAudio.wav";
        private string processedFilePath = "processedAudio.wav";
        public SearchBySound()
        {
            InitializeComponent();

            // Tạo CircularPictureBox
            CircularPictureBox micPictureBox = new CircularPictureBox
            {
                Width = 100,
                Height = 100,
                Image = Image.FromFile("path_to_mic_image.png"), // Đường dẫn hình ảnh
                SizeMode = PictureBoxSizeMode.StretchImage, // Co giãn hình ảnh phù hợp
                BackColor = Color.White
            };

            // Đặt vị trí và thêm vào form
            micPictureBox.Location = new Point(70, 200); // Tuỳ chỉnh vị trí
            this.Controls.Add(micPictureBox);
        }

        private void ic_back_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.ShowDialog();

            this.Close();
        }
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (writer != null)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
        }

        private void ProcessAudio(string inputPath, string outputPath)
        {
            using (var reader = new AudioFileReader(inputPath))
            using (var writer = new WaveFileWriter(outputPath, reader.WaveFormat))
            {
                var buffer = new float[reader.WaveFormat.SampleRate]; // 1 giây dữ liệu
                int samplesRead;

                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Loại bỏ tiếng ồn bằng High-pass filter
                    var filteredBuffer = ApplyHighPassFilter(buffer.Take(samplesRead).ToArray(), reader.WaveFormat.SampleRate);

                    // Ghi vào file mới
                    writer.WriteSamples(filteredBuffer, 0, filteredBuffer.Length);
                }
            }
        }

        private float[] ApplyHighPassFilter(float[] buffer, int sampleRate)
        {
            var filter = BiQuadFilter.HighPassFilter(sampleRate, 300, 1); // Cắt tần số dưới 300Hz
            return buffer.Select(sample => filter.Transform(sample)).ToArray();
        }

        private async void ShowTemporaryToolTip(Control control, string message, int duration)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.Show(message, control, control.Width / 2, control.Height / 2, duration);
            await Task.Delay(duration); // Chờ thời gian hiển thị
            toolTip.Dispose(); // Xóa toolTip
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                waveIn = new WaveIn();
                waveIn.WaveFormat = new WaveFormat(16000, 1); // 16000kHz, Mono

                waveIn.DataAvailable += WaveIn_DataAvailable;
                waveIn.RecordingStopped += WaveIn_RecordingStopped;

                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

                waveIn.StartRecording();
                //MessageBox.Show("Bắt đầu thu âm!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thu âm: {ex.Message}");
            }
        }

        private async void button1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.Dispose();
                    waveIn = null;

                    writer?.Dispose();
                    writer = null;

                    ShowTemporaryToolTip(this, "Dừng thu âm!", 3000);

                    if (File.Exists(outputFilePath))
                    {
                        // Chỉ xử lý sau khi đảm bảo tài nguyên đã được giải phóng
                        ProcessAudio(outputFilePath, processedFilePath);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file thu âm!");
                    }

                    string modelPath = "vosk-model-en-us-0.22";
                    string dictionaryPath = "frequency_dictionary_en_82_765.txt";
                    int maxEditDistance = 2;

                    string query = await ConvertSpeechToTextAsync(outputFilePath, modelPath);

                    if (!string.IsNullOrEmpty(query))
                    {
                        //textBox1.Text = query;

                        var images = LoadImageMetadata("captions.txt");

                        var bestMatches = FindBestMatches(query, images);

                        foreach (var match in bestMatches)
                        {
                            //textBox2.Text = textBox2.Text + $"File: {match.FilePath}, Độ tương tự: {match.SimilarityScore}\n";
                            //Console.WriteLine($"File: {match.FilePath}, Độ tương tự: {match.SimilarityScore}\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        static string ConvertSpeechToText(string audioFilePath, string modelPath)
        {
            try
            {
                // Kiểm tra tính hợp lệ của file âm thanh và thư mục mô hình
                if (!File.Exists(audioFilePath))
                {
                    Console.WriteLine("File âm thanh không tồn tại.");
                    return null;
                }

                if (!Directory.Exists(modelPath))
                {
                    Console.WriteLine("Thư mục mô hình không tồn tại.");
                    return null;
                }

                string para = $"-i {audioFilePath} -ar 16000 -ac 1 {audioFilePath}";
                RunExe("ffmpeg.exe", para);

                // Tải mô hình Vosk
                var model = new Model(modelPath);

                // Sử dụng FileStream để đọc file âm thanh
                using (var recognizer = new VoskRecognizer(model, 16000))
                using (var audioStream = new FileStream(audioFilePath, FileMode.Open))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    // Đọc tệp từng phần và xử lý bằng recognizer
                    while ((bytesRead = audioStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        recognizer.AcceptWaveform(buffer, bytesRead);
                    }

                    // Trả về kết quả cuối cùng
                    string result = recognizer.FinalResult().Trim();
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ghi log
                Console.WriteLine($"Lỗi khi chuyển đổi: {ex.Message}");
                return null;
            }
        }

        private async Task<string> ConvertSpeechToTextAsync(string audioFilePath, string modelPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Kiểm tra tính hợp lệ của file âm thanh và thư mục mô hình
                    if (!File.Exists(audioFilePath))
                    {
                        Console.WriteLine("File âm thanh không tồn tại.");
                        return null;
                    }

                    if (!Directory.Exists(modelPath))
                    {
                        Console.WriteLine("Thư mục mô hình không tồn tại.");
                        return null;
                    }

                    string para = $"-i {audioFilePath} -ar 16000 -ac 1 {audioFilePath}";
                    RunExe("ffmpeg.exe", para);

                    var model = new Model(modelPath);
                    using (var recognizer = new VoskRecognizer(model, 16000))
                    using (var audioStream = new FileStream(audioFilePath, FileMode.Open))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        while ((bytesRead = audioStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            recognizer.AcceptWaveform(buffer, bytesRead);
                        }

                        return recognizer.FinalResult().Trim();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                    return null;
                }
            });
        }

        static List<ImageMetadata> LoadImageMetadata(string txtFilePath)
        {
            var lines = File.ReadLines(txtFilePath);
            var metadataList = new List<ImageMetadata>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    metadataList.Add(new ImageMetadata
                    {
                        FilePath = "Images/" + parts[0].Trim(),
                        Description = parts[1].Trim()
                    });
                }
            }

            return metadataList;
        }

        static List<ImageMetadata> FindBestMatches(string query, List<ImageMetadata> images)
        {
            return images
                .Select(image => new
                {
                    image.FilePath,
                    image.Description,
                    SimilarityScore = CalculateSimilarity(query, image.Description)
                })
                .Where(match => match.SimilarityScore > 0.1) // Chỉ lấy những mục có SimilarityScore > 0.1
                .GroupBy(match => match.FilePath) // Nhóm theo FilePath
                .Select(group => group.OrderByDescending(match => match.SimilarityScore).First()) // Lấy mục có SimilarityScore cao nhất trong nhóm
                .OrderByDescending(match => match.SimilarityScore) // Sắp xếp theo SimilarityScore giảm dần
                .Take(20) // Giới hạn số lượng kết quả trả về là 20
                .Select(match => new ImageMetadata
                {
                    FilePath = match.FilePath,
                    Description = match.Description,
                    SimilarityScore = match.SimilarityScore
                })
                .ToList();
        }

        private static void RunExe(string v, string para)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = false;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = para;
            processStartInfo.FileName = v;
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
        }

        static double CalculateSimilarity(string text1, string text2)
        {
            var words1 = text1.Split(' ').ToList();
            var words2 = text2.Split(' ').ToList();

            var intersection = words1.Intersect(words2).Count();
            var cosineSimilarity = (double)intersection / (Math.Sqrt(words1.Count) * Math.Sqrt(words2.Count));
            return cosineSimilarity;
        }
    }
}
