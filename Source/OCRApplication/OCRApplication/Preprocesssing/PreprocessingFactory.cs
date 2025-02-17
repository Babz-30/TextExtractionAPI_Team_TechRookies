﻿

using OCRApplication.Helpers;

namespace OCRApplication.Preprocesssing
{
    public class PreprocessingFactory
    {
        // image preprocessing
        public Dictionary<string, string> PreprocessImage(string imagePath, string technique)
        {
            Dictionary<string, string> processedImages = new Dictionary<string, string>();

            string outputDir = UtilityClass.OutputImageDirectory();
            string variation; string outputImagePath;

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
                    variation = $"inverted";
                    outputImagePath = $"{outputDir}/{variation}.jpg";
                    var invert = new InvertImage();
                    invert.InvertingImage(imagePath, outputImagePath);
                    processedImages[variation] = outputImagePath;
                    break;
                default:
                    processedImages[technique] = imagePath; // No processing applied
                    break;
            }

            return processedImages; // Return dictionary with variation and processed image paths
        }
    }
}
