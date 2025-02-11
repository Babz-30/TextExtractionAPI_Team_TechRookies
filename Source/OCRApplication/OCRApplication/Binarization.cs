using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication
{
    public static class Binarization
    {
        public static Bitmap ApplyBinarization(Bitmap inputImage, int threshold = 128)
        {
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
            string outputPath = "binarized_output.png";
            binarized.Save(outputPath, ImageFormat.Png);
            Console.WriteLine($"Binarized image saved at: {outputPath}");

            return binarized;
        }
    }
}
