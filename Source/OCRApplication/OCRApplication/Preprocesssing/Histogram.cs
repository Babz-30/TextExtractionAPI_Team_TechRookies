using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocessing
{
    internal class HSIAdjustment
    {
        public string AdjustHSI(string inputImagePath, string outputImagePath, double hueShift, double saturationFactor, double intensityFactor)
        {
            using Bitmap bitmap = new(inputImagePath);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color originalColor = bitmap.GetPixel(x, y);
                    (double h, double s, double i) = RgbToHsi(originalColor);

                    h = (h + hueShift) % 360; // Adjust Hue
                    s = Math.Min(Math.Max(s * saturationFactor, 0), 1); // Adjust Saturation
                    i = Math.Min(Math.Max(i * intensityFactor, 0), 1); // Adjust Intensity

                    Color newColor = HsiToRgb(h, s, i);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            bitmap.Save(outputImagePath, ImageFormat.Png);
            return outputImagePath;
        }

       
    }
}
