using System.Drawing;

namespace OCRApplication
{
    internal class ResizeImage(string inputImagePath) : IPreprocessing
    {
        // Path to save the resized image
        private readonly string resizeImagePath = UtilityClass.ImagePath("resized_image.jpg");
        private readonly string inputImagePath = inputImagePath;
        public string Process()
        {
            try
            {
                ResizeAndSetDPI(inputImagePath, resizeImagePath, 300);
                Console.WriteLine("Image resized and DPI set to at least 300.");
                return resizeImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image resizing: {ex.Message}");
                throw;
            }
        
        }

        static void ResizeAndSetDPI(string inputPath, string outputPath, int targetDPI)
        {
            using (Bitmap original = new Bitmap(inputPath))
            {
                int newWidth = (int)(original.Width * (targetDPI / (float)original.HorizontalResolution));
                int newHeight = (int)(original.Height * (targetDPI / (float)original.VerticalResolution));

                using (Bitmap resized = new Bitmap(newWidth, newHeight))
                {
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
    }
}
