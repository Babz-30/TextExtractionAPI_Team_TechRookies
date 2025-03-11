using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Inverts image words from light to dark and vice versa.
    /// </summary>
    public class InvertImage
    {
        /// <summary>
        /// Takes an input image with a dark background and light words and converts it to a light background with dark words.
        /// </summary>
        /// <param name="inputImagePath">Path to the input image.</param>
        /// <param name="outputImagePath">Path to the output image.</param>
        /// <returns>Path to the output processed image.</returns>
        public string InvertingImage(string inputImagePath, string outputImagePath)
        {
            try
            {
                // Load the image
                using (Bitmap originalBitmap = new Bitmap(inputImagePath))
                {
                    // Ensure the image is in a non-indexed format
                    using (Bitmap bitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format32bppArgb))
                    {
                        // Copy original image to the new format
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.DrawImage(originalBitmap, 0, 0);
                        }

                        // Process each pixel
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                // Get the pixel color
                                Color originalColor = bitmap.GetPixel(x, y);

                                // Invert the color
                                Color invertedColor = Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);

                                // Set the new color
                                bitmap.SetPixel(x, y, invertedColor);
                            }
                        }

                        // Save the modified image
                        bitmap.Save(outputImagePath, ImageFormat.Png);
                    }
                }

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image invert: {ex.Message}");
                throw;
            }
        }
    }
}
