﻿using NAudio.Dsp;
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
using System.Text.Json;
using MathNet.Filtering.IIR;
using MathNet.Filtering;
using System.Collections;

namespace MediaSearchSystem
{
    public partial class SearchBySound : Form
    {
        private WaveIn waveIn;
        private WaveFileWriter writer;
        private string outputFilePath = "recordedAudio.wav";
        private string processedFilePath = "processedAudio.wav";
        private string solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        public SearchBySound()
        {
            InitializeComponent();

            // Tạo CircularPictureBox
            CircularPictureBox micPictureBox = new CircularPictureBox
            {
                Width = 125,
                Height = 125,
                Image = Properties.Resources.ic_microphone, // Đường dẫn hình ảnh
                SizeMode = PictureBoxSizeMode.CenterImage, // Co giãn hình ảnh phù hợp
                BackColor = Color.White
            };

            // Đặt vị trí và thêm vào form
            micPictureBox.Location = new Point(15, 140); // Tuỳ chỉnh vị trí
            micPictureBox.MouseDown += (s, e) => button1_MouseDown(s, e);
            micPictureBox.MouseUp += (s, e) => button1_MouseUp(s, e);
            micPictureBox.MouseEnter += (s, e) => micPictureBox.BackColor = Color.Teal;
            micPictureBox.MouseLeave += (s, e) => micPictureBox.BackColor = Color.White;
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

        private void ConvertToPcm16kHz(string inputFilePath, string outputFilePath)
        {
            using (var reader = new AudioFileReader(inputFilePath))
            using (var resampler = new MediaFoundationResampler(reader, new WaveFormat(16000, 16, 1)))
            {
                resampler.ResamplerQuality = 60; // Chất lượng cao
                WaveFileWriter.CreateWaveFile(outputFilePath, resampler);
            }
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
                waveIn.WaveFormat = new WaveFormat(16000, 16, 1); // 16000kHz, Mono

                waveIn.DataAvailable += WaveIn_DataAvailable;
                waveIn.RecordingStopped += WaveIn_RecordingStopped;

                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

                waveIn.StartRecording();
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
                this.Enabled = false;

                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.Dispose();
                    waveIn = null;

                    writer?.Dispose();
                    writer = null;

                    ShowTemporaryToolTip(this, "Dừng thu âm!", 3000);

                    //if (File.Exists(outputFilePath))
                    //{
                    //    ConvertToPcm16kHz(outputFilePath, processedFilePath);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Không tìm thấy file thu âm!");
                    //    return;
                    //}

                    string modelPath = "vosk-model-en-us-0.22";
                    string query = await ConvertSpeechToTextAsync(processedFilePath, modelPath);

                    if (!string.IsNullOrEmpty(query))
                    {
                        textBox1.Text = query;

                        string imageDirectory = Path.Combine(solutionDirectory, "Resources", "ImageEnglishDatabase");
                        string[] imagePaths = Directory.GetFiles(imageDirectory, "*.*")
                            .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                           file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                            .ToArray();

                        if (imagePaths.Length == 0)
                        {
                            MessageBox.Show("Thư mục không chứa ảnh nào hợp lệ!");
                            return;
                        }

                        var images = LoadImageMetadata(imageDirectory);
                        var bestMatches = FindBestMatches(query, images);

                        listView1.Items.Clear();
                        imageList1.Images.Clear();

                        foreach (var match in bestMatches)
                        {
                            try
                            {
                                Image image = Image.FromFile(match.FilePath);

                                imageList1.Images.Add(image);

                                var item = new ListViewItem
                                {
                                    Text = Path.GetFileName(match.SimilarityScore+ "%"),
                                    ImageIndex = imageList1.Images.Count - 1
                                };

                                listView1.Items.Add(item);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Không thể tải ảnh từ {match.FilePath}: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            finally
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                this.Enabled = true;
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

                    string para = $"-i {audioFilePath} -af afftdn {audioFilePath}";
                    RunExe("ffmpeg.exe", para);

                    //para = $"-i {audioFilePath} -af lowpass=f=500 {audioFilePath}";
                    //RunExe("ffmpeg.exe", para);

                    //para = $"-i {audioFilePath} -af highpass=f=300 {audioFilePath}";
                    //RunExe("ffmpeg.exe", para);

                    para = $"-i {audioFilePath} -af loudnorm {audioFilePath}";
                    RunExe("ffmpeg.exe", para);

                    //para = $"-i {audioFilePath} -af aecho=0.8:0.88:60:0.4 {audioFilePath}";
                    //RunExe("ffmpeg.exe", para);

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

                        // Nhận kết quả cuối cùng và trích xuất giá trị từ JSON
                        string jsonResult = recognizer.FinalResult().Trim();

                        var resultObject = JsonDocument.Parse(jsonResult);
                        return resultObject.RootElement.GetProperty("text").GetString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                    return null;
                }
            });
        }

        static List<ImageMetadata> LoadImageMetadata(string imageDirectory)
        {
            var metadataList = new List<ImageMetadata>();

            byte[] fileContent = Properties.Resources.ImageEnglishCaptions;

            // Nếu cần chuyển đổi mảng byte sang chuỗi (UTF-8)
            string fileAsString = System.Text.Encoding.UTF8.GetString(fileContent);

            // Đọc từng dòng
            using (StringReader reader = new StringReader(fileAsString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        metadataList.Add(new ImageMetadata
                        {
                            FilePath = imageDirectory + "\\" + parts[0].Trim(),
                            Description = parts[1].Trim()
                        });
                    }
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
                    SimilarityScore = calSimilar2AmTiet(query, image.Description)
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

        public static double calSimilar2AmTiet(string str1, string str2)
        {
            ArrayList fealst = new ArrayList();
            double[] feaVector = SimilarCls.getFeaVector2Amtiet(str1, ref fealst);

            ArrayList fealst2 = new ArrayList();
            double[] feaVector2 = SimilarCls.getFeaVector2Amtiet(str2, ref fealst2);

            ArrayList allFealst = SimilarCls.unifyFealst(fealst, fealst2);

            return SimilarCls.calSimilarCosinelAllFea(allFealst, fealst, fealst2, feaVector, feaVector2) * 100;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string query = textBox1.Text;

                string imageDirectory = Path.Combine(solutionDirectory, "Resources", "ImageEnglishDatabase");
                string[] imagePaths = Directory.GetFiles(imageDirectory, "*.*")
                    .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (imagePaths.Length == 0)
                {
                    MessageBox.Show("Thư mục không chứa ảnh nào hợp lệ!");
                    return;
                }

                var images = LoadImageMetadata(imageDirectory);
                var bestMatches = FindBestMatches(query, images);

                listView1.Items.Clear();
                imageList1.Images.Clear();

                foreach (var match in bestMatches)
                {
                    try
                    {
                        Image image = Image.FromFile(match.FilePath);

                        imageList1.Images.Add(image);

                        var item = new ListViewItem
                        {
                            Text = Path.GetFileName(match.SimilarityScore + "%"),
                            ImageIndex = imageList1.Images.Count - 1
                        };

                        listView1.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Không thể tải ảnh từ {match.FilePath}: {ex.Message}");
                    }
                }
            }
        }
    }
}
