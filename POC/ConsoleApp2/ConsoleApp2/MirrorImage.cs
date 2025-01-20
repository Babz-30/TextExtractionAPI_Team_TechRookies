using System;
using System.Drawing; // For Bitmap
using Tesseract; // Assuming it’s used elsewhere in your project

namespace OCRApplication
{
    internal class MirrorImage : IPreprocessing
    {
        // Path to save the mirrored image
        private readonly string mirrorImagePath = @"path_to_image\flipped_image.jpg";
        private readonly string inputImagePath;

        public MirrorImage(string inputImagePath)
        {
            this.inputImagePath = inputImagePath;
        }

        public string Process()
        {
            try
            {
                // Load the input image
                using (Bitmap inputImage = new Bitmap(inputImagePath))
                {
                    // Apply mirroring
                    Bitmap mirroredImage = ImageProcessing.MirrorImage(inputImage);

                    // Save the mirrored image
                    mirroredImage.Save(mirrorImagePath);

                    return mirrorImagePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image mirroring: {ex.Message}");
                throw;
            }
        }
    }

    public static class ImageProcessing
    {
        // Method to mirror an image horizontally
        public static Bitmap MirrorImage(Bitmap inputImage)
        {
            Bitmap mirroredImage = new Bitmap(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX); // Perform horizontal flip
            return mirroredImage;
        }
    }
}
