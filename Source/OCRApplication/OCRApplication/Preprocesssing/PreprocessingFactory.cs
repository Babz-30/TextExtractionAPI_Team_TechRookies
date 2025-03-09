using OCRApplication.Helpers;
namespace OCRApplication.Preprocesssing
{
    /// <summary>
    /// Preprocessing Factory that generates Dictionary of preprocessed images.
    /// </summary>
    public class PreprocessingFactory
    {

        // Image preprocessing
        /// <summary>
        /// Preprocesses image based on technique selected for different variation within the technique.
        /// </summary>
        /// <param name="imagePath">Path to input image.</param>
        /// <param name="technique">Preprocessing technique.</param>
        /// <returns>Dictionary of preprocessing technique (with variations) as key and value as the processed image path.</returns>

        public Dictionary<string, string> PreprocessImage(string imagePath, string technique)
        {
            Dictionary<string, string> processedImages = new Dictionary<string, string>();

            string outputDir = UtilityClass.OutputImageDirectory();
            string variation;
            string outputImagePath;

            switch (technique)
            {
                case "rotation":
                    RotateImage rm = new RotateImage();
                    float[] rotateAngles = { 00.0f, 20.0f, 30.0f, 45.0f, 90.0f };

                    foreach (float angle in rotateAngles)
                    {
                        variation = $"rotated_{angle}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        rm.ApplyRotation(imagePath, outputImagePath, angle);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "cannyfilter":
                    var cannyFilter = new CannyFilter();
                    var invertImage = new InvertImage();

                    int[] thresholds = { 10, 30, 50, 100, 150 };

                    foreach (int threshold in thresholds)
                    {
                        variation = $"cannyfilter_{threshold}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        var cannyImage = cannyFilter.ApplyCannyEdgeDetection(imagePath, outputImagePath, threshold);
                        invertImage.InvertingImage(imagePath, outputImagePath);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "resize":
                    var resize = new ResizeImage();
                    int[] targerDPIs = { 50, 100, 200, 300 };
                    foreach (int targerDPI in targerDPIs)
                    {
                        variation = $"resized_{targerDPI}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        resize.ResizingImage(imagePath, outputImagePath, targerDPI);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "invert":
                    variation = "inverted";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var invert = new InvertImage();
                    invert.InvertingImage(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "binarization":
                    var binarization = new Binarization();
                    variation = "binarized_otsu";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    binarization.ApplyOtsuBinarization(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "grayscale":
                    variation = "grayscale";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var grayscale = new Grayscale();
                    grayscale.ConvertToGrayscale(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "hsi_adjustment":
                    HistogramAdjustment ha = new HistogramAdjustment();
                    double[] satFactors = { 2.0, 1.5, 0.5 };
                    double[] intensityFactors = { 2.0, 1.5, 0.5 };

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
                    variation = "mirror_horizontal";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    MirrorImage mirror = new MirrorImage(imagePath);
                    string mirroredPath = mirror.Process();
                    processedImages[variation] = mirroredPath;
                    break;

                default:
                    processedImages[technique] = imagePath; // No processing applied
                    break;
            }

            return processedImages; // Return dictionary with variation and processed image paths
        }
    }
}
