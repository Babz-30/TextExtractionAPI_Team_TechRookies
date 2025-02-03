using System;
using System.Drawing;
using System.Drawing.Imaging;
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
            this.mirrorImagePath = UtilityClass.OutputImagePath($"{mirrorType.ToLower()}_mirrored_image.jpg");
        }

        public string Process()
        {
            try
            {
                // Load the input image
                using Bitmap inputImage = new(inputImagePath);

                // Apply preprocessing (Grayscale + Binarization)
                Bitmap preprocessedImage = ApplyPreprocessing(inputImage);

                Bitmap mirroredImage;
                if (mirrorType.Equals("Horizontal", StringComparison.OrdinalIgnoreCase))
                {
                    mirroredImage = ImageProcessing.MirrorImageHorizontal(preprocessedImage);
                }
                else if (mirrorType.Equals("Vertical", StringComparison.OrdinalIgnoreCase))
                {
                    mirroredImage = ImageProcessing.MirrorImageVertical(preprocessedImage);
                }
                else
                {
                    throw new ArgumentException("Invalid mirror type. Use 'Horizontal' or 'Vertical'.");
                }

                // Save the mirrored image
                mirroredImage.Save(mirrorImagePath, ImageFormat.Jpeg);

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
            Bitmap grayscaleImage = Grayscale.ConvertToGrayscale(inputImage);
            Bitmap binarizedImage = Binarization.ApplyBinarization(grayscaleImage);
            return binarizedImage;
        }
    }

    public static class ImageProcessing
    {
        // Method to mirror an image horizontally
        public static Bitmap MirrorImageHorizontal(Bitmap inputImage)
        {
            Bitmap mirroredImage = new(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return mirroredImage;
        }

        // Method to mirror an image vertically
        public static Bitmap MirrorImageVertical(Bitmap inputImage)
        {
            Bitmap mirroredImage = new(inputImage);
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return mirroredImage;
        }
    }
}
