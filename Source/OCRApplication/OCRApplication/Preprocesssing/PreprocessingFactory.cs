﻿using OCRApplication.Helpers;
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
        public Dictionary<string, string> PreprocessImage(string imagePath, string technique)
        {
            Dictionary<string, string> processedImages = new Dictionary<string, string>();

            // Define output directory for processed images
            string outputDir = UtilityClass.OutputImageDirectory();
            string variation;
            string outputImagePath;

            // Define parameter sets for different preprocessing techniques
            float[] rotateAngles = { -45.0f, -30.0f, -20.0f, 20.0f, 30.0f, 45.0f };
            int[] thresholds = { 50, 150, 200 };
            int[] targerDPIs = { 50, 100, 200, 300 };
            double[] satFactors = { 2.0, 1.5, 0.5 };
            double[] intensityFactors = { 2.0, 1.5, 0.5 };

            switch (technique)
            {
                case "rotation":
                    // Apply rotation to the image for different angles and resize variations
                    RotateImage rm = new RotateImage();
                    ResizeImage rm2 = new ResizeImage();

                    foreach (float angle in rotateAngles)
                    {
                        variation = $"rotated_{angle}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        var rotatedImg = rm.ApplyRotation(imagePath, outputImagePath, angle);

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
                    var cannyFilter = new CannyFilter();
                    var invertImage = new InvertImage();

                    foreach (int threshold in thresholds)
                    {
                        variation = $"cannyfilter_{threshold}";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        var cannyImage = cannyFilter.ApplyCannyEdgeDetection(imagePath, outputImagePath, threshold);

                        variation = $"cannyfilter_{threshold}_invert";
                        outputImagePath = $"{outputDir}/{variation}.jpg";
                        invertImage.InvertingImage(cannyImage, outputImagePath);
                        processedImages[variation] = outputImagePath;
                    }
                    break;

                case "binarization":
                    // Apply Otsu binarization
                    var binarization = new Binarization();
                    variation = "binarized_otsu";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    binarization.ApplyOtsuBinarization(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "grayscale":
                    // Convert image to grayscale
                    variation = "grayscale";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var grayscale = new Grayscale();
                    grayscale.ConvertToGrayscale(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;

                case "chainfilter":
                    // Apply a sequence of filters: grayscale -> binarization -> resize
                    var gray = new Grayscale();
                    variation = "grayscale";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var grayImg = gray.ConvertToGrayscale(imagePath, outputImagePath);

                    var bin = new Binarization();
                    variation = "grayscale_binarize";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var binImg = bin.ApplyOtsuBinarization(grayImg, outputImagePath);

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
                    HistogramAdjustment ha = new HistogramAdjustment();

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

                case "mirror":
                    // Mirror the image horizontally
                    variation = "mirror";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    MirrorImage mirror = new MirrorImage(imagePath);
                    string mirroredPath = mirror.Process();
                    processedImages[variation] = mirroredPath;
                    break;

                default:
                    // If no valid technique is provided, return original image
                    processedImages[technique] = imagePath;
                    break;
            }

            return processedImages; // Return dictionary containing variations and processed image paths
        }
    }
}
