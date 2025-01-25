using SixLabors.ImageSharp; // Install SixLabors.ImageSharp
using SixLabors.ImageSharp.Processing;

namespace OCRApplication
{
    public class RotateImage(string inputImagePath, int skewAngle) : IPreprocessing
    {
        // Save corrected image
        readonly string correctedImagePath = UtilityClass.OutputImagePath("corrected_image.jpg");

        readonly private string inputImagePath = inputImagePath;

        readonly private int skewAngle = skewAngle;

        public string Process()
        {

            // Load the Image
            using var image = Image.Load(inputImagePath);

            // Rotate the image to correct orientation (adjust the angle as needed)
            image.Mutate(x => x.Rotate(skewAngle)); // Example: Rotate 90 degrees clockwise

            // Save the corrected image
            image.Save(correctedImagePath);

            return correctedImagePath;
        }
    }
}
