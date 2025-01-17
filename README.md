# Search Capabilities and Package Installation

This document explains the search capabilities and the corresponding packages required for implementing search functionality by sketch, sound, and image.

---

## Search by Sketch
To enable search by sketch functionality, use the following package:

```powershell
Install-Package Emgu.CV
```

Emgu.CV is a cross-platform .NET wrapper for OpenCV that supports real-time computer vision operations.

---

## Search by Sound
To implement search by sound, you will need the following tools and packages:

1. **NAudio**: A powerful .NET library for audio manipulation.

   ```powershell
   Install-Package NAudio
   ```

2. **Vosk**: An open-source speech recognition toolkit.

   ```powershell
   Install-Package Vosk
   ```

3. **FFmpeg**: A multimedia framework for handling audio and video files. Ensure FFmpeg is installed and properly configured in your system's PATH.

   - [FFmpeg Download](https://ffmpeg.org/download.html)

---

## Search by Image
To implement search by image functionality, use the following packages:

1. **OpenCvSharp4**: A .NET wrapper for OpenCV.

   ```powershell
   Install-Package OpenCvSharp4
   ```

2. **OpenCvSharp4.Windows**: Windows-specific bindings for OpenCvSharp4.

   ```powershell
   Install-Package OpenCvSharp4.Windows
   ```

3. **Modify Image Data Source Path**: Update the image data source path in your code. For example:

   ```csharp
   // Change the image data source path at line 173
   string dataSourcePath = "<new_image_data_source_path>";
   ```

   Replace `<new_image_data_source_path>` with the actual path to your image dataset.

---