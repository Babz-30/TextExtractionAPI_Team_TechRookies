using System;
using System.Drawing;
using System.Drawing.Imaging;

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
            this.mirrorImagePath = UtilityClass.OutputImagePath($"{mirrorType.ToLower()}_mirrored_image.png"); // Changed to .png
        }

        public string Process()
        {
            try
            {
                // Load input image (manually dispose later)
                Bitmap inputImage = new(inputImagePath);

                // Apply preprocessing (Grayscale + Binarization)
                Bitmap preprocessedImage = ApplyPreprocessing(inputImage);
                inputImage.Dispose(); // Dispose after preprocessing

                // Apply mirroring
                Bitmap mirroredImage = mirrorType.Equals("Horizontal", StringComparison.OrdinalIgnoreCase)
                    ? ImageProcessing.MirrorImageHorizontal(preprocessedImage)
                    : ImageProcessing.MirrorImageVertical(preprocessedImage);

                preprocessedImage.Dispose(); // Dispose preprocessed image after use

                // Save the mirrored image
                mirroredImage.Save(mirrorImagePath, ImageFormat.Png); // Changed to .png
                Console.WriteLine($"Mirrored image saved to: {mirrorImagePath}");

                mirroredImage.Dispose(); // Dispose mirrored image after saving

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
            Bitmap grayscaleImage = Grayscale.ConvertToGrayscale(inputImage);
            Bitmap binarizedImage = Binarization.ApplyBinarization(grayscaleImage);
            grayscaleImage.Dispose(); // Free memory after processing
            return binarizedImage;
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

        // Method to mirror an image vertically
        public static Bitmap MirrorImageVertical(Bitmap inputImage)
        {
            Bitmap mirroredImage = (Bitmap)inputImage.Clone(); // Clone to avoid modifying the original
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return mirroredImage;
        }
    }
}
