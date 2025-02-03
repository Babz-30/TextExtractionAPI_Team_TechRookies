using Emgu.CV;
using Emgu.CV.CvEnum;

namespace OCRApplication
{
    internal class DenoiseImage(string inputImagePath) : IPreprocessing
    {
        // Save corrected image
        readonly string denoisedImagePath = UtilityClass.OutputImagePath("denoised_image.jpg");

        readonly private string inputImagePath = inputImagePath;
        public string Process()
        {
            try
            {
                // Load the image in grayscale
                Mat image = CvInvoke.Imread(inputImagePath, ImreadModes.Grayscale);

                // Apply Gaussian Blur to smooth out noise
                Mat blurred = new();
                CvInvoke.GaussianBlur(image, blurred, new System.Drawing.Size(5, 5), 0);

                // Save the preprocessed image for OCR
                CvInvoke.Imwrite(denoisedImagePath, blurred);

                return denoisedImagePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image denoising: {ex.Message}");
                throw;
            }
        }
    }
}
