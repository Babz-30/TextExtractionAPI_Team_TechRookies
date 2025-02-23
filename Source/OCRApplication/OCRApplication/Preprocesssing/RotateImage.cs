using OCRApplication.Helpers;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Rotates image based on rotation angle
    /// </summary>
    public class RotateImage
    {
        /// <summary>
        /// Rotates image based on rotation angle.
        /// </summary>
        /// <param name="inputPath">Path to input image.</param>
        /// <param name="outputImagePath">Path to output image.</param>
        /// <param name="angle">Angle in degree to rotate image.</param>
        /// <returns>Path to output processed image.</returns>
        public string ApplyRotation(string inputPath, string outputImagePath, float angle)
        {
            try
            {
                Image image = Image.FromFile(inputPath);
                if (image == null)
                    throw new ArgumentNullException(nameof(image));

                // Calculate the new bounding box size after rotation
                float radian = angle * (float)(Math.PI / 180);
                double cos = Math.Abs(Math.Cos(radian));
                double sin = Math.Abs(Math.Sin(radian));
                int newWidth = (int)(image.Width * cos + image.Height * sin);
                int newHeight = (int)(image.Width * sin + image.Height * cos);

                // Create a new blank bitmap with the calculated dimensions
                Bitmap rotatedImage = new Bitmap(newWidth, newHeight);
                rotatedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (Graphics g = Graphics.FromImage(rotatedImage))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // Set rotation point to center of image
                    g.TranslateTransform(newWidth / 2f, newHeight / 2f);
                    g.RotateTransform(angle);
                    g.TranslateTransform(-image.Width / 2f, -image.Height / 2f);

                    // Draw the original image onto the rotated image
                    g.DrawImage(image, new Point(0, 0));
                }

                rotatedImage.Save(outputImagePath);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image rotation: {ex.Message}");
                throw;
            }
        }
    }
}
