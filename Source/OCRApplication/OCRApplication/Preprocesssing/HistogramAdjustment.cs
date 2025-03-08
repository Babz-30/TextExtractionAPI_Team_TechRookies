using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocesssing
{
    public class HistogramAdjustment
    {
        public string ApplyHistogramAdjustment(string inputImagePath, string outputImagePath, double saturationFactor, double intensityFactor)
        {
            try
            {
                using Bitmap inputImage = new(inputImagePath);
                int width = inputImage.Width;
                int height = inputImage.Height;
                using Bitmap adjustedImage = new(width, height);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = inputImage.GetPixel(x, y);
                        float hue = pixel.GetHue();
                        float saturation = pixel.GetSaturation() * (float)saturationFactor;
                        float intensity = pixel.GetBrightness() * (float)intensityFactor;

                        // Clamp values between 0 and 1
                        saturation = Math.Max(0, Math.Min(1, saturation));
                        intensity = Math.Max(0, Math.Min(1, intensity));

                        // Convert HSI back to RGB
                        Color adjustedPixel = HSIToRGB(hue, saturation, intensity);
                        adjustedImage.SetPixel(x, y, adjustedPixel);
                    }
                }

                adjustedImage.Save(outputImagePath, ImageFormat.Jpeg);
                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during HSI adjustment: {ex.Message}");
                throw;
            }
        }

        private Color HSIToRGB(float hue, float saturation, float intensity)
        {
            int r, g, b;
            if (saturation == 0)
            {
                r = g = b = (int)(intensity * 255);
            }
            else
            {
                float h = hue / 60f;
                int i = (int)Math.Floor(h);
                float f = h - i;
                float p = intensity * (1 - saturation);
                float q = intensity * (1 - saturation * f);
                float t = intensity * (1 - saturation * (1 - f));

                switch (i)
                {
                    case 0: r = (int)(intensity * 255); g = (int)(t * 255); b = (int)(p * 255); break;
                    case 1: r = (int)(q * 255); g = (int)(intensity * 255); b = (int)(p * 255); break;
                    case 2: r = (int)(p * 255); g = (int)(intensity * 255); b = (int)(t * 255); break;
                    case 3: r = (int)(p * 255); g = (int)(q * 255); b = (int)(intensity * 255); break;
                    case 4: r = (int)(t * 255); g = (int)(p * 255); b = (int)(intensity * 255); break;
                    default: r = (int)(intensity * 255); g = (int)(p * 255); b = (int)(q * 255); break;
                }
            }
            return Color.FromArgb(r, g, b);
        }
    }
}
