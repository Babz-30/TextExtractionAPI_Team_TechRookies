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

        private (double, double, double) RgbToHsi(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double intensity = (r + g + b) / 3.0;
            double minRGB = Math.Min(r, Math.Min(g, b));
            double saturation = (intensity == 0) ? 0 : 1 - (minRGB / intensity);

            double hue = 0;
            if (saturation != 0)
            {
                double numerator = 0.5 * ((r - g) + (r - b));
                double denominator = Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b));
                hue = Math.Acos(numerator / (denominator + 1e-10)) * (180 / Math.PI);
                if (b > g) hue = 360 - hue;
            }

            return (hue, saturation, intensity);
        }

        private Color HsiToRgb(double h, double s, double i)
        {
            double r, g, b;
            double radH = h * (Math.PI / 180);

            if (h < 120)
            {
                r = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                b = i * (1 - s);
                g = 3 * i - (r + b);
            }
            else if (h < 240)
            {
                h -= 120;
                radH = h * (Math.PI / 180);
                g = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                r = i * (1 - s);
                b = 3 * i - (r + g);
            }
            else
            {
                h -= 240;
                radH = h * (Math.PI / 180);
                b = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                g = i * (1 - s);
                r = 3 * i - (g + b);
            }

            return Color.FromArgb(
                Math.Clamp((int)(r * 255), 0, 255),
                Math.Clamp((int)(g * 255), 0, 255),
                Math.Clamp((int)(b * 255), 0, 255));
        }
    }
}
