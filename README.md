# Search Capabilities and Package Installation

Tài liệu này giải thích các khả năng tìm kiếm và các gói tương ứng cần thiết để triển khai chức năng tìm kiếm bằng bản phác thảo, âm thanh và hình ảnh.

---

## Search by Sketch
Để bật tìm kiếm theo chức năng phác thảo, hãy sử dụng gói sau:

```powershell
Install-Package Emgu.CV
```

---

## Search by Sound
Để triển khai tìm kiếm bằng âm thanh, bạn sẽ cần các công cụ và gói sau:

1. **NAudio**: Thư viện .NET mạnh mẽ để thao tác âm thanh.

   ```powershell
   Install-Package NAudio
   ```

2. **Vosk**: Bộ công cụ nhận dạng giọng nói nguồn mở.

   ```powershell
   Install-Package Vosk
   ```

3. **FFmpeg**: Một khung đa phương tiện để xử lý các tập tin âm thanh và video. Đảm bảo FFmpeg được cài đặt và cấu hình đúng cách trong PATH của hệ thống của bạn.

   - [FFmpeg Download](https://ffmpeg.org/download.html)

Cách cài đặt:

1. Tải Vosk Model (vosk-model-en-us-0.22) trên drive nhóm về MediaSearchSystem\MediaSearchSystem\MediaSearchSystem\bin\Debug\net8.0-windows

   - [Vosk Model Download](https://drive.google.com/drive/folders/1v1DTGt0KOXyxh6Wgaq5cG7jZJhXkpTgG)
  
2. Tải FFmpeg (ffmpeg.exe) trên drive nhóm về MediaSearchSystem\MediaSearchSystem\MediaSearchSystem\bin\Debug\net8.0-windows

   - [FFmpeg Download](https://drive.google.com/drive/folders/1v1DTGt0KOXyxh6Wgaq5cG7jZJhXkpTgG)
  
3. Khởi chạy

---

## Search by Image
Để triển khai chức năng tìm kiếm theo hình ảnh, hãy sử dụng các gói sau:

1. **OpenCvSharp4**: Trình bao bọc .NET cho OpenCV.

   ```powershell
   Install-Package OpenCvSharp4
   ```

2. **OpenCvSharp4.Windows**: Các ràng buộc dành riêng cho Windows cho OpenCvSharp4.

   ```powershell
   Install-Package OpenCvSharp4.Windows
   ```

3. **Modify Image Data Source Path**: Cập nhật đường dẫn nguồn dữ liệu hình ảnh trong mã của bạn. Ví dụ:

   ```csharp
   // Change the image data source path at line 173
   string dataSourcePath = "<new_image_data_source_path>";
   ```

   Replace `<new_image_data_source_path>` with the actual path to your image dataset.

---
