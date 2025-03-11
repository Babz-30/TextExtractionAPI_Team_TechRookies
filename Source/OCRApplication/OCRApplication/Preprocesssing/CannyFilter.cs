using Emgu.CV;
using Emgu.CV.CvEnum;

namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Character Edge detecting filter 
    /// </summary>
    public class CannyFilter
    {
        /// <summary>
        /// Converts image to grayscale, reduces noise and apply canny edge detection filter to the image.
        /// </summary>
        /// <param name="imageImagePath">Path to input image.</param>
        /// <param name="outputImagePath">Path to output image.</param>
        /// <param name="threshold1">Threshold1 for edge detection.</param>
        /// <param name="threshold2">Threshold2 for edge detection.</param>
        /// <returns>Path to output processed image.</returns>
        public string ApplyCannyEdgeDetection(string imageImagePath, string outputImagePath, int threshold1, int threshold2 = 255)
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
