using Microsoft.Extensions.Configuration;
using OCRApplication.Helpers;
namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Preprocessing Factory that generates Dictionary of preprocessed images.
    /// </summary>
    public class PreprocessingFactory
    {
        /// <summary>
        /// Preprocesses image based on technique selected for different variation within the technique.
        /// </summary>
        /// <param name="imagePath">Path to input image.</param>
        /// <param name="technique">Preprocessing technique.</param>
        /// <returns>Dictionary of preprocessing technique (with variations) as key and value as the processed image path.</returns>
        public static Dictionary<string, string> PreprocessImage(string imagePath, string technique)
        {
            Dictionary<string, string> processedImages = [];

            // Define output directory for processed images
            string outputDir = UtilityClass.OutputImageDirectory();
            string variation;
            string outputImagePath;

            var config = Configuration.Config();

            // Define parameter sets for different preprocessing techniques
            float[] rotateAngles = GetConfigurationArray<float>(config, "PreprocessingSettings:RotateAngles");
            int[] thresholds = GetConfigurationArray<int>(config, "PreprocessingSettings:Thresholds");
            int[] targerDPIs = GetConfigurationArray<int>(config, "PreprocessingSettings:TargetDPIs");
            double[] satFactors = GetConfigurationArray<double>(config, "PreprocessingSettings:SatFactors");
            double[] intensityFactors = GetConfigurationArray<double>(config, "PreprocessingSettings:IntensityFactors");

            switch (technique)
            {
                case "rotation":
                    // Apply rotation to the image for different angles and resize variations
                    ResizeImage rm2 = new();

                    foreach (float angle in rotateAngles)
                    {
                        variation = $"rotated_{angle}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        var rotatedImg = RotateImage.ApplyRotation(imagePath, outputImagePath, angle);

                        foreach (int targerDPI in targerDPIs)
                        {
                            variation = $"rotated_{angle}_resized_{targerDPI}";
                            outputImagePath = $"{outputDir}/{variation}.jpg";
                            rm2.ResizingImage(rotatedImg, outputImagePath, targerDPI);
                            processedImages[variation] = outputImagePath;
                        }
                    }
                    break;

                case "cannyfilter":
                    // Apply Canny edge detection and invert the resulting image
                    var invertImage = new InvertImage();

                    foreach (int threshold in thresholds)
                    {
                        variation = $"cannyfilter_{threshold}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        var cannyImage = CannyFilter.ApplyCannyEdgeDetection(imagePath, outputImagePath, threshold);

                        variation = $"cannyfilter_{threshold}_invert";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        invertImage.InvertingImage(cannyImage, outputImagePath);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "denoise":
                    // Convert image to grayscale
                    variation = "grayscale";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var imgGray = Grayscale.ConvertToGrayscale(imagePath, outputImagePath);

                    // Denoise image
                    variation = "grayscale_binarized";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    Binarization.ApplyOtsuBinarization(imgGray, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "chainfilter":
                    // Apply a sequence of filters: grayscale -> binarization -> resize
                    variation = "grayscale";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var grayImg = Grayscale.ConvertToGrayscale(imagePath, outputImagePath);

                    variation = "grayscale_binarize";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var binImg = Binarization.ApplyOtsuBinarization(grayImg, outputImagePath);

                    var resize = new ResizeImage();
                    foreach (int targerDPI in targerDPIs)
                    {
                        variation = $"grayscale_binarize_resized_{targerDPI}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        resize.ResizingImage(binImg, outputImagePath, targerDPI);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "invert":
                    // Invert the image
                    variation = "inverted";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var invert = new InvertImage();
                    invert.InvertingImage(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "hsi_adjustment":
                    // Apply Histogram Adjustment with different saturation and intensity factors
                    HistogramAdjustment ha = new();

                    foreach (double sat in satFactors)
                    {
                        foreach (double intensity in intensityFactors)
                        {
                            variation = $"hsi_s{sat}_i{intensity}";
                            outputImagePath = $"{outputDir}/{variation}.jpg";
                            ha.ApplyHistogramAdjustment(imagePath, outputImagePath, sat, intensity);
                            processedImages[variation] = outputImagePath;
                        }
                    }
                    break;

                case "mirror_horizontal":
                    // Mirror the image horizontally
                    variation = "mirror";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    MirrorImage.Process(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                default:
                    // If no valid technique is provided, return original image
                    processedImages[technique] = imagePath;
                    break;
            }

            return processedImages; // Return dictionary containing variations and processed image paths
        }

        // Helper function to safely get configuration arrays
        private static T[] GetConfigurationArray<T>(IConfiguration config, string key)
        {
            var array = config.GetSection(key).Get<T[]>();
            return array ?? [];  // Return an empty array if the section is missing or null
        }
    }
}
