using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OCRApplication.Helpers;
using OCRApplication.Preprocesssing;
using OCRApplication.Services;

namespace OCRApplication
{
    class Program
    {
        static Dictionary<string, string> ocrResults = new Dictionary<string, string>();

        static async Task Main(string[] args)
        {
            try
            {
                // Path to your image file
                string inputImagePath = UtilityClass.InputImagePath("image1.jpg");
                string preprocessedImagePath = inputImagePath;

                string cosineSimilarityPath = UtilityClass.CosineSimilarityDirectory("CosineSimilarityMatrix.csv");

                // ✅ Added "binarization", "grayscale", and "mirror_horizontal"
                List<string> techniques = new List<string>
                {
                    "rotation",
                    "cannyfilter",
                    "resize",
                    "invert",
                    "hsi_adjustment",
                    "binarization",
                    "grayscale",
                    "mirror_horizontal" // ✅ Added horizontal mirroring
                };

                PreprocessingFactory preprocessingFactory = new PreprocessingFactory();
              //  hsi_adjustment hsiAdjustment = new HSIAdjustment();
                Dictionary<string, string> ocrTexts = new Dictionary<string, string>();
                Dictionary<string, string> preprocessedImages = new Dictionary<string, string>();

                // Apply preprocessing techniques
                foreach (var technique in techniques)
                {
                    Dictionary<string, string> processedImages = preprocessingFactory.PreprocessImage(inputImagePath, technique);

                    foreach (var img in processedImages)
                    {
                        preprocessedImages[img.Key] = img.Value; // ✅ Store all preprocessed variations
                    }
                }

                // Perform OCR on preprocessed images
                ocrTexts = OcrResults(preprocessedImages);

                // Generate embeddings and compute similarity
                Dictionary<string, List<double>> embeddings = new Dictionary<string, List<double>>();
                foreach (var item in ocrTexts)
                {
                    embeddings[item.Key] = await TextEmbedding.ComputeEmbedding(item.Value);
                }

                foreach (var item in embeddings)
                {
                    Console.WriteLine($"{item.Key} - {string.Join(", ", item.Value)} ");
                }

                // ✅ Ensures binarization, grayscale & mirror_horizontal results are included in the output file
                TextSimilarity.GenerateCosineSimilarityMatrix(embeddings, cosineSimilarityPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static Dictionary<string, string> OcrResults(Dictionary<string, string> preprocessedImages)
        {
            TextExtraction textExtraction = new TextExtraction();
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
