using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocessing
{
    internal class HSIAdjustment
    {
        /// <summary>
        /// Modifies the hue, saturation, and intensity (HSI) of an image.
        /// </summary>
        /// <param name="inputImagePath">File path of the source image.</param>
        /// <param name="outputImagePath">File path where the processed image is saved.</param>
        /// <param name="hueShift">Hue adjustment in degrees.</param>
        /// <param name="saturationFactor">Multiplier for saturation adjustment.</param>
        /// <param name="intensityFactor">Multiplier for intensity adjustment.</param>
        /// <returns>File path of the saved processed image.</returns>
        public string AdjustHSI(string inputImagePath, string outputImagePath, double hueShift, double saturationFactor, double intensityFactor)
        {
            using Bitmap bitmap = new(inputImagePath);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // Retrieve original pixel color
                    Color originalColor = bitmap.GetPixel(x, y);

                    // Convert RGB color to HSI
                    (double h, double s, double i) = RgbToHsi(originalColor);

                    // Apply transformations to H, S, and I values
                    h = (h + hueShift) % 360; // Normalize hue within [0, 360]
                    s = Math.Clamp(s * saturationFactor, 0, 1); // Keep saturation in [0, 1]
                    i = Math.Clamp(i * intensityFactor, 0, 1); // Keep intensity in [0, 1]

                    // Convert modified HSI back to RGB and update pixel
                    Color newColor = HsiToRgb(h, s, i);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            // Save the updated image
            bitmap.Save(outputImagePath, ImageFormat.Png);
            return outputImagePath;
        }

        /// <summary>
        /// Converts an RGB color to its HSI (Hue, Saturation, Intensity) representation.
        /// </summary>
        /// <param name="color">Input color in RGB format.</param>
        /// <returns>Tuple containing (hue, saturation, intensity).</returns>
        private (double, double, double) RgbToHsi(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            // Compute intensity as the average of RGB components
            double intensity = (r + g + b) / 3.0;
            double minRGB = Math.Min(r, Math.Min(g, b));
            double saturation = (intensity > 0) ? 1 - (minRGB / intensity) : 0;

            double hue = 0;
            if (saturation > 0)
            {
                double numerator = 0.5 * ((r - g) + (r - b));
                double denominator = Math.Sqrt((r - g) * (r - g) + (r - b) * (g - b));
                hue = Math.Acos(numerator / (denominator + 1e-10)) * (180 / Math.PI);
                if (b > g) hue = 360 - hue; // Adjust hue for correct quadrant
            }

            return (hue, saturation, intensity);
        }

        /// <summary>
        /// Converts HSI (Hue, Saturation, Intensity) values back to an RGB color.
        /// </summary>
        /// <param name="h">Hue angle in degrees (0-360).</param>
        /// <param name="s">Saturation level (0-1).</param>
        /// <param name="i">Intensity level (0-1).</param>
        /// <returns>Equivalent RGB color.</returns>
        private Color HsiToRgb(double h, double s, double i)
        {
            double r, g, b;
            double radH = h * (Math.PI / 180); // Convert degrees to radians

            if (h < 120) // First sector (Red-Green)
            {
                r = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                b = i * (1 - s);
                g = 3 * i - (r + b);
            }
            else if (h < 240) // Second sector (Green-Blue)
            {
                h -= 120;
                radH = h * (Math.PI / 180);
                g = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                r = i * (1 - s);
                b = 3 * i - (r + g);
            }
            else // Third sector (Blue-Red)
            {
                h -= 240;
                radH = h * (Math.PI / 180);
                b = i * (1 + s * Math.Cos(radH) / Math.Cos(Math.PI / 3 - radH));
                g = i * (1 - s);
                r = 3 * i - (g + b);
            }

            // Convert values to valid RGB range and return the color object
            return Color.FromArgb(
                Math.Clamp((int)(r * 255), 0, 255),
                Math.Clamp((int)(g * 255), 0, 255),
                Math.Clamp((int)(b * 255), 0, 255));
        }
    }
}
