using System;
using System.Drawing; // For Bitmap
using System.IO;

namespace OCRApplication
{
    internal class MirrorImage : IPreprocessing
    {
        private readonly string inputImagePath;
        private readonly string mirrorType;
        private readonly string mirrorImagePath;

        public MirrorImage(string inputImagePath, string mirrorType)
        {
            this.inputImagePath = inputImagePath;
            this.mirrorType = mirrorType;

            // Generate output path based on mirror type
            this.mirrorImagePath = UtilityClass.ImagePath($"{mirrorType.ToLower()}_flipped_image.jpg");
        }

        public string Process()
        {
            try
            {
                // Load the input image
                using Bitmap inputImage = new(inputImagePath);
                Bitmap mirroredImage;

                // Apply the specified mirror type
                if (mirrorType.Equals("Horizontal", StringComparison.OrdinalIgnoreCase))
                {
                    mirroredImage = ImageProcessing.MirrorImageHorizontal(inputImage);
                }
                else if (mirrorType.Equals("Vertical", StringComparison.OrdinalIgnoreCase))
                {
                    mirroredImage = ImageProcessing.MirrorImageVertical(inputImage);
                }
                else
                {
                    throw new ArgumentException("Invalid mirror type. Use 'Horizontal' or 'Vertical'.");
                }

                // Save the mirrored image
                mirroredImage.Save(mirrorImagePath);

                Console.WriteLine($"Mirrored image saved to: {mirrorImagePath}");
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
        public static Bitmap MirrorImageHorizontal(Bitmap inputImage)
        {
            Bitmap mirroredImage = new(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX); // Perform horizontal flip
            return mirroredImage;
        }

        // Method to mirror an image vertically
        public static Bitmap MirrorImageVertical(Bitmap inputImage)
        {
            Bitmap mirroredImage = new(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY); // Perform vertical flip
            return mirroredImage;
        }
    }
}
