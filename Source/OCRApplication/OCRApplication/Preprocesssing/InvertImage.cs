using System.Drawing;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Inverts image words from light to dark vice versa
    /// </summary>
    internal class InvertImage
    {
        /// <summary>
        /// Takes input image with dark background and light words and converts to light background and dark words.
        /// </summary>
        /// <param name="inputImagePath">Path to input image.</param>
        /// <param name="outputImagePath">Path to output image.</param>
        /// <returns>Path to output processed image.</returns>
        public string InvertingImage(string inputImagePath, string outputImagePath)
        {
            // Load the image
            using Bitmap bitmap = new(inputImagePath);

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
            bitmap.Save(outputImagePath);

            return outputImagePath;
        }
    }
}
