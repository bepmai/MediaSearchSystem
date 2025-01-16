using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSearchSystem
{
    internal class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            // Vẽ hình tròn
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Tạo hình tròn từ client area
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, Width - 1, Height - 1);
                this.Region = new Region(gp);

                // Vẽ ảnh bên trong hình tròn
                base.OnPaint(pe);

                // Vẽ đường viền (tuỳ chọn)
                using (Pen pen = new Pen(Color.Gray, 2)) // Màu và độ dày viền
                {
                    g.DrawEllipse(pen, 0, 0, Width - 1, Height - 1);
                }
            }
        }

    }
}
