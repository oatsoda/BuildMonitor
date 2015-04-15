using System.Drawing;

namespace BuildMonitor.UI
{
    internal static class BitmapExtensions
    {
        private static readonly byte[] s_Pngiconheader = { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static Icon ToPngIcon(this Image img, int size = 16)
        {
            using (var bmp = new Bitmap(img, new Size(size, size)))
            {
                byte[] png;
                using (var fs = new System.IO.MemoryStream())
                {
                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    fs.Position = 0;
                    png = fs.ToArray();
                }

                using (var fs = new System.IO.MemoryStream())
                {
                    if (size >= 256) size = 0;
                    s_Pngiconheader[6] = (byte)size;
                    s_Pngiconheader[7] = (byte)size;
                    s_Pngiconheader[14] = (byte)(png.Length & 255);
                    s_Pngiconheader[15] = (byte)(png.Length / 256);
                    s_Pngiconheader[18] = (byte)(s_Pngiconheader.Length);

                    fs.Write(s_Pngiconheader, 0, s_Pngiconheader.Length);
                    fs.Write(png, 0, png.Length);
                    fs.Position = 0;
                    return new Icon(fs);
                }
            }
        }
    }
}