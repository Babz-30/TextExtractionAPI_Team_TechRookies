using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Resizes image for better resolution.
    /// </summary>
    public class ResizeImage
    {
        /// <summary>
        /// Changes the resolution of image based on passed DPI
        /// </summary>
        /// <param name="inputImagePath">Path to input image.</param>
        /// <param name="outputImagePath">Path to output image.</param>
        /// <param name="targetDPI">Target resolution in DPI.</param>
        /// <returns>Path to output processed image.</returns>
        public static string ResizingImage(string inputImagePath, string outputImagePath, int targetDPI)
        {
            try
            {
                ResizeAndSetDPI(inputImagePath, outputImagePath, targetDPI);
                
                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image resizing: {ex.Message}");
                throw;
            }

        }

        // Complete implementation for improving resolution.
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
