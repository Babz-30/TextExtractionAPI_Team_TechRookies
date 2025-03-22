using System;
using System.Drawing;
using System.Linq;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Provides functionality to binarize an image using Otsu's thresholding method.
    /// </summary>
    public class Binarization
    {
        public static string ApplyOtsuBinarization(string inputImagePath, string outputImagePath)
        {
            /// <summary>
            /// Applies Otsu's binarization to convert a grayscale image into a binary (black and white) image.
            /// </summary>
            /// <param name="inputImagePath">Path to the input image.</param>
            /// <param name="outputImagePath">Path where the binarized image will be saved.</param>
            /// <returns>Path to the output binarized image.</returns>
            try
            {
                using Bitmap inputImage = new(inputImagePath);
                int width = inputImage.Width;
                int height = inputImage.Height;
                using Bitmap binarizedImage = new(width, height);

                // Compute histogram
                int[] histogram = new int[256];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = inputImage.GetPixel(x, y);
                        int grayValue = (pixel.R + pixel.G + pixel.B) / 3;
                        histogram[grayValue]++;
                    }
                }

                // Compute Otsu's threshold
                int totalPixels = width * height;
                int sum = histogram.Select((t, i) => i * t).Sum();
                int sumB = 0, wB = 0, wF = 0;
                float maxVariance = 0;
                int threshold = 0;

                for (int t = 0; t < 256; t++)
                {
                    wB += histogram[t]; // Weight Background
                    if (wB == 0) continue;

                    wF = totalPixels - wB; // Weight Foreground
                    if (wF == 0) break;

                    sumB += t * histogram[t];
                    float mB = (float)sumB / wB;
                    float mF = (float)(sum - sumB) / wF;

                    // Compute Between-Class Variance
                    float varianceBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                    // Update the best threshold if a new max variance is found
                    if (varianceBetween > maxVariance)
                    {
                        maxVariance = varianceBetween;
                        threshold = t;
                    }
                }

                // Apply threshold
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = inputImage.GetPixel(x, y);
                        int grayValue = (pixel.R + pixel.G + pixel.B) / 3;
                        int binaryColor = grayValue > threshold ? 255 : 0;
                        binarizedImage.SetPixel(x, y, Color.FromArgb(binaryColor, binaryColor, binaryColor));
                    }
                }

                binarizedImage.Save(outputImagePath);
                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Otsu binarization: {ex.Message}");
                throw;
            }
        }
    }
}
