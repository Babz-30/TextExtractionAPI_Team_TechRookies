using SixLabors.ImageSharp; // Install SixLabors.ImageSharp
using SixLabors.ImageSharp.Processing;

namespace OCRApplication
{
    public class RotateImage : IPreprocessing
    {
        // Save corrected image
        string correctedImagePath = @"path_to_image\corrected_image.jpg";

        private string inputImagePath;

        private int skewAngle;

        public RotateImage(string inputImagePath, int skewAngle)
        {
            this.inputImagePath = inputImagePath;
            this.skewAngle = skewAngle;
        }

        public string Process()
        {

            // Step 1: Load and Correct the Image Orientation
            using (var image = Image.Load(inputImagePath)) // Load the image
            {
                // Rotate the image to correct orientation (adjust the angle as needed)
                image.Mutate(x => x.Rotate(skewAngle)); // Example: Rotate 90 degrees clockwise

                // Save the corrected image
                image.Save(correctedImagePath);

                return correctedImagePath;
            }
        }
    }
}
