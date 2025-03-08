using System;
using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    public class Grayscale
    {
        public string ConvertToGrayscale(string inputImagePath, string outputImagePath)
        {
            try
            {
                using Bitmap inputImage = new(inputImagePath);
                using Bitmap grayscale = new(inputImage.Width, inputImage.Height);

                // Iterate through each pixel of the image
                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        // Get the original pixel color
                        Color originalColor = inputImage.GetPixel(x, y);

                        // Convert to grayscale using weighted sum
                        int grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);

                        // Set the grayscale color
                        Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                        grayscale.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed grayscale image
                grayscale.Save(outputImagePath);
                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image grayscale conversion: {ex.Message}");
                throw;
            }
        }
    }
}
