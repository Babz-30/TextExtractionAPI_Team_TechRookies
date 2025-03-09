using OCRApplication.Helpers;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OCRApplication.Preprocesssing
{
    internal class MirrorImage : IPreprocessing
    {
        private readonly string inputImagePath;
        private readonly string mirrorImagePath;

        public MirrorImage(string inputImagePath)
        {
            this.inputImagePath = inputImagePath;
            mirrorImagePath = UtilityClass.OutputImagePath("horizontal_mirrored_image.png"); // Changed to .png
        }

        public string Process()
        {
            try
            {
                // Load input image
                using Bitmap inputImage = new(inputImagePath);

                // Apply preprocessing (if needed)
                using Bitmap preprocessedImage = ApplyPreprocessing(inputImage);

                // Apply only horizontal mirroring
                using Bitmap mirroredImage = ImageProcessing.MirrorImageHorizontal(preprocessedImage);

                // Save the mirrored image
                mirroredImage.Save(mirrorImagePath, ImageFormat.Png);
                Console.WriteLine($"Mirrored image saved to: {mirrorImagePath}");

                return mirrorImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image processing: {ex.Message}");
                throw;
            }
        }

        public Bitmap ApplyPreprocessing(Bitmap inputImage)
        {
            return inputImage; // No preprocessing applied
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
