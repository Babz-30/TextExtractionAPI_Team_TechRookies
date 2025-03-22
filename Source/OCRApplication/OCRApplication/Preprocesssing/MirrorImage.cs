using OCRApplication.Helpers;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Provides functionality to create a horizontally mirrored version of an image.
    /// </summary>
    public class MirrorImage
    {
        /// <summary>
        /// Processes the image by applying a horizontal mirroring transformation.
        /// </summary>
        /// <param name="inputImagePath">Path to the input image.</param>
        /// <param name="outputImagePath">Path where the mirrored image will be saved.</param>
        /// <returns>Path to the output mirrored image.</returns>
        public static string Process(string inputImagePath, string outputImagePath)
        {
            try
            {
                // Load input image
                using Bitmap inputImage = new(inputImagePath);

                // Apply only horizontal mirroring
                using Bitmap mirroredImage = ImageProcessing.MirrorImageHorizontal(inputImage);

                // Save the mirrored image
                mirroredImage.Save(outputImagePath);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during mirror image processing: {ex.Message}");
                throw;
            }
        }
    }

    public static class ImageProcessing
    {
        // Method to mirror an image horizontally
        public static Bitmap MirrorImageHorizontal(Bitmap inputImage)
        {
            Bitmap mirroredImage = (Bitmap)inputImage.Clone(); // Clone to avoid modifying the original
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return mirroredImage;
        }
    }
}
