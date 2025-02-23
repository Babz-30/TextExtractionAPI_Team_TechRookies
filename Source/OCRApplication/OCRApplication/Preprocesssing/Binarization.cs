using System;
using System.Drawing;

namespace OCRApplication.Preprocessing
{
    public static class Binarization
    {
        public static string ApplyBinarization(string inputImagePath, string outputImagePath, int threshold = 128)
        {
            try
            {
                // Load the input image from the file path
                Bitmap inputImage = new(inputImagePath);
                Bitmap binarized = new Bitmap(inputImage.Width, inputImage.Height);

                // Iterate through each pixel in the image
                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        // Retrieve the original pixel color
                        Color pixel = inputImage.GetPixel(x, y);

                        // Convert the pixel to grayscale using the average method
                        int grayValue = (pixel.R + pixel.G + pixel.B) / 3;

                        // Apply thresholding: Assign black (0) if below threshold, white (255) otherwise
                        int binaryColor = grayValue > threshold ? 255 : 0;

                        // Set the new binarized pixel color
                        binarized.SetPixel(x, y, Color.FromArgb(binaryColor, binaryColor, binaryColor));
                    }
                }

                // Save the binarized image to the specified output path
                binarized.Save(outputImagePath);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                // Log and rethrow the exception in case of an error
                Console.WriteLine($"Error during image binarization: {ex.Message}");
                throw;
            }
        }
    }
}
