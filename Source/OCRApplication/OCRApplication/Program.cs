using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OCRApplication.Helpers;
using OCRApplication.Preprocessing;
using OCRApplication.Preprocesssing;
using OCRApplication.Services;

namespace OCRApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Path to your image file
                string inputImagePath = UtilityClass.InputImagePath("image1.jpg");
                string preprocessedImagePath = inputImagePath;

                List<string> techniques = new List<string> { "rotation", "cannyfilter", "resize", "invert", "hsi_adjustment" };

                PreprocessingFactory preprocessingFactory = new PreprocessingFactory();
                HSIAdjustment hsiAdjustment = new HSIAdjustment();

                Dictionary<string, string> ocrTexts = new Dictionary<string, string>();
                Dictionary<string, string> preprocessedImages = new Dictionary<string, string>();

                // Apply preprocessing techniques
                foreach (var technique in techniques)
                {
                    if (technique == "hsi_adjustment")
                    {
                        string hsiOutputPath = "hsi_output.png";
                        preprocessedImagePath = hsiAdjustment.AdjustHSI(inputImagePath, hsiOutputPath, 30.0, 1.2, 1.1);
                        preprocessedImages[technique] = hsiOutputPath;
                    }
                    else
                    {
                        preprocessedImages = preprocessingFactory.PreprocessImage(inputImagePath, technique);
                    }

                    // Perform OCR on preprocessed images
                    ocrTexts = OcrResults(preprocessedImages);
                }

                // Generate embeddings and compute similarity
                Dictionary<string, List<double>> embeddings = new Dictionary<string, List<double>>();
                foreach (var item in ocrTexts)
                {
                    embeddings[item.Key] = await TextEmbedding.ComputeEmbedding(item.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static Dictionary<string, string> OcrResults(Dictionary<string, string> preprocessedImages)
        {
            TextExtraction textExtraction = new TextExtraction();
            Dictionary<string, string> ocrResults = new Dictionary<string, string>();
            string extractedText;

            foreach (var technique in preprocessedImages)
            {
                extractedText = textExtraction.ExtractText(technique.Value, technique.Key);
                ocrResults[technique.Key] = extractedText;
            }

            return ocrResults;
        }
    }
}
