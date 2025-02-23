using System;
using System.Drawing;

namespace OCRApplication.Preprocessing
{
    public static class Grayscale
    {
        public static string ConvertToGrayscale(string inputImagePath, string outputImagePath)
        {
            try
            {
                // Load the input image from the given file path
                Bitmap inputImage = new(inputImagePath);
                Bitmap grayscale = new Bitmap(inputImage.Width, inputImage.Height);

                // Iterate over each pixel in the image
                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        // Get the original color of the current pixel
                        Color originalColor = inputImage.GetPixel(x, y);

                        // Compute grayscale value using the weighted sum method
                        int grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);

                        // Create a grayscale color using the computed intensity
                        Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                        // Set the new grayscale pixel in the output image
                        grayscale.SetPixel(x, y, grayColor);
                    }
                }

                // Save the grayscale image to the specified output path
                grayscale.Save(outputImagePath);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                // Log error details and rethrow the exception
                Console.WriteLine($"Error during image grayscale conversion: {ex.Message}");
                throw;
            }
        }
    }
}
