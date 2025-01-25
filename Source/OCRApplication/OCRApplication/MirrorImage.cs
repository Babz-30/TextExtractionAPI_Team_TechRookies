using System.Drawing; // For Bitmap

namespace OCRApplication
{
    internal class MirrorImage(string inputImagePath) : IPreprocessing
    {
        // Path to save the mirrored image
        private readonly string mirrorImagePath = UtilityClass.ImagePath("flipped_image.jpg");
        private readonly string inputImagePath = inputImagePath;

        public string Process()
        {
            try
            {
                // Load the input image
                using Bitmap inputImage = new(inputImagePath);
                // Apply mirroring
                Bitmap mirroredImage = ImageProcessing.MirrorImage(inputImage);

                // Save the mirrored image
                mirroredImage.Save(mirrorImagePath);

                return mirrorImagePath;
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
            Bitmap mirroredImage = new(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX); // Perform horizontal flip
            return mirroredImage;
        }
    }
}
