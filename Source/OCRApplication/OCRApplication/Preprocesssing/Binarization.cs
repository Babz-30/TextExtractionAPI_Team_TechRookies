using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    public static class Binarization
    {
        public static string ApplyBinarization(string inputImagePath, string outputImagePath, int threshold = 128)
        {
            try
            {
                Bitmap inputImage = new(inputImagePath);
                Bitmap binarized = new Bitmap(inputImage.Width, inputImage.Height);
                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        Color pixel = inputImage.GetPixel(x, y);
                        int grayValue = (pixel.R + pixel.G + pixel.B) / 3;  // Convert to grayscale
                        int binaryColor = grayValue > threshold ? 255 : 0;
                        binarized.SetPixel(x, y, Color.FromArgb(binaryColor, binaryColor, binaryColor));
                    }
                }

                // Save binarized image
                binarized.Save(outputImagePath);

                return outputImagePath;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image binarization: {ex.Message}");
                throw;
            }
        }
    }
}
