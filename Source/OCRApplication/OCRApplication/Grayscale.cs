using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication
{
    public static class Grayscale
    {
        public static Bitmap ConvertToGrayscale(Bitmap inputImage)
        {
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

            // Save grayscale image
            string outputPath = "grayscale_output.png";
            grayscale.Save(outputPath, ImageFormat.Png);
            Console.WriteLine($"Grayscale image saved at: {outputPath}");

            return grayscale;
        }
    }
}
