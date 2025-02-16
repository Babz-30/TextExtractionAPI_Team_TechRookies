using Emgu.CV;

namespace OCRApplication.Preprocesssing
{
    public class DenoiseImage
    {

        public static string DenoisingImage(string inputImagePath, string outputImagePath)
        {
            try
            {
                // Load the image in grayscale
                Mat image = CvInvoke.Imread(inputImagePath);

                // Apply Gaussian Blur to smooth out noise
                Mat blurred = new();
                CvInvoke.GaussianBlur(image, blurred, new System.Drawing.Size(5, 5), 0);

                // Save the preprocessed image for OCR
                CvInvoke.Imwrite(outputImagePath, blurred);

                return outputImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image denoising: {ex.Message}");
                throw;
            }
        }
    }
}
