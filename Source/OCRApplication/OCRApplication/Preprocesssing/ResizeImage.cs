using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    internal class ResizeImage
    {
        public string ResizingImage(string inputImagePath, string outputImagePath, int targetDPI)
        {
            try
            {
                ResizeAndSetDPI(inputImagePath, outputImagePath, targetDPI);
                Console.WriteLine("Image resized and DPI set to at least 300.");
                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image resizing: {ex.Message}");
                throw;
            }

        }

        static void ResizeAndSetDPI(string inputPath, string outputPath, int targetDPI)
        {
            using Bitmap original = new(inputPath);
            int newWidth = (int)(original.Width * (targetDPI / (float)original.HorizontalResolution));
            int newHeight = (int)(original.Height * (targetDPI / (float)original.VerticalResolution));

            using Bitmap resized = new(newWidth, newHeight);
            resized.SetResolution(targetDPI, targetDPI);
            using (Graphics graphics = Graphics.FromImage(resized))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(original, 0, 0, newWidth, newHeight);
            }

            resized.Save(outputPath);
        }
    }
}
