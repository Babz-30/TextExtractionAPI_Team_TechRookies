using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    public static class Grayscale
    {
        public static string ConvertToGrayscale(string inputImagePath, string outputImagePath)
        {
            try
            {
                Bitmap inputImage = new(inputImagePath);
                Bitmap grayscale = new Bitmap(inputImage.Width, inputImage.Height);
                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        Color originalColor = inputImage.GetPixel(x, y);
                        int grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);
                        Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                        grayscale.SetPixel(x, y, grayColor);
                    }
                }

                grayscale.Save(outputImagePath);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image grayscale: {ex.Message}");
                throw;
            }
        }
    }
}
