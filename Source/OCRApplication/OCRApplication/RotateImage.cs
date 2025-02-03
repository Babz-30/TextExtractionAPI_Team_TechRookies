using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SixLabors.ImageSharp; // Install SixLabors.ImageSharp
using SixLabors.ImageSharp.Processing;

namespace OCRApplication
{
    public class RotateImage(string inputImagePath, int skewAngle = 0) : IPreprocessing
    {
        // Save corrected image
        readonly string correctedImagePath = UtilityClass.OutputImagePath("corrected_image.jpg");

        readonly private string inputImagePath = inputImagePath;

        private float skewAngle = skewAngle;

        public string Process()
        {
            try
            {
                //Autodetect skew
                skewAngle = (float)GetSkewAngle(inputImagePath);

                // Load the Image
                using var image = Image.Load(inputImagePath);
                Console.WriteLine($"Detected Skew Angle: {skewAngle} degrees");

                // Rotate the image to correct orientation (adjust the angle as needed)
                image.Mutate(x => x.Rotate(-skewAngle)); // Example: Rotate 90 degrees clockwise

                // Save the corrected image
                image.Save(correctedImagePath);

                return correctedImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image resizing: {ex.Message}");
                throw;
            }
        }

        static double GetSkewAngle(string imagePath)
        {
            using (Mat image = CvInvoke.Imread(imagePath, ImreadModes.Grayscale))
            {
                Mat edges = new Mat();
                CvInvoke.Canny(image, edges, 50, 150);

                LineSegment2D[] lines = CvInvoke.HoughLinesP(edges, 1, Math.PI / 180, 100, 100, 10);

                if (lines.Length == 0)
                    return 0; // No lines detected, assume no skew

                double[] angles = lines.Select(line => Math.Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X) * 180 / Math.PI).ToArray();
                return angles.Median(); // Return the median angle for better accuracy
            }
        }
    }

    public static class Extensions
    {
        public static double Median(this double[] source)
        {
            var sortedList = source.OrderBy(n => n).ToArray();
            int count = sortedList.Length;
            return count % 2 == 0 ? (sortedList[count / 2 - 1] + sortedList[count / 2]) / 2.0 : sortedList[count / 2];
        }
    }
}
