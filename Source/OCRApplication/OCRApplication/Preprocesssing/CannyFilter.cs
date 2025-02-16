using Emgu.CV;
using Emgu.CV.CvEnum;

namespace OCRApplication.Preprocesssing
{
    public class CannyFilter
    {
        public string ApplyCannyEdgeDetection(string imageImagePath, string outputImagePath, int threshold1 = 100, int threshold2 = 200)
        {
            try
            {
              
                // Load the image in grayscale
                Mat image = CvInvoke.Imread(imageImagePath, ImreadModes.Grayscale);

                // Apply Gaussian blur to reduce noise
                Mat blurred = new Mat();
                CvInvoke.GaussianBlur(image, blurred, new System.Drawing.Size(5, 5), 1.5);

                // Apply Gaussian Blur to smooth out noise
                using var edges = new Mat();
                CvInvoke.Canny(blurred, edges, threshold1, threshold2);

                // Save the preprocessed image for OCR
                CvInvoke.Imwrite(outputImagePath, edges);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image cannyfilter: {ex.Message}");
                throw;
            }
        }
    }
}
