using OCRApplication.Helpers;
using OCRApplication.Preprocesssing;
using OCRApplication.Services;

namespace OCRApplication
{
    class Program
    {
        static readonly Dictionary<string, string> ocrResults = [];

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Computing the best pre-processing technique to extract text from image by tesseract OCR.");
                // Path to your image file
                string inputImagePath = UtilityClass.InputImagePath("image1.jpg");

                string cosineSimilarityPath = UtilityClass.CosineSimilarityDirectory("CosineSimilarityMatrix.csv");

                List<string> techniques =
                [
                    "rotation",
                    "cannyfilter",
                    "chainfilter",
                    "invert",
                    "hsi_adjustment",
                    "denoise",
                    "mirror_horizontal"
                ];

                Dictionary<string, string> preprocessedImages = [];
                Dictionary<string, string> ocrTexts = [];

                // Apply preprocessing techniques
                foreach (var technique in techniques)
                {
                    Dictionary<string, string> processedImages = PreprocessingFactory.PreprocessImage(inputImagePath, technique);

                    foreach (var img in processedImages)
                    {
                        preprocessedImages[img.Key] = img.Value; // Store all preprocessed variations
                    }
                }

                // Perform OCR text extraction on preprocessed images
                ocrTexts = OcrResults(preprocessedImages);

                // Generate embeddings
                Dictionary<string, List<double>> embeddings = [];

                Console.WriteLine("Computing Embeddings for extracted text and then calculating cosine similarity between preprocessing techniques...");

                foreach (var item in ocrTexts)
                {
                    embeddings[item.Key] = await TextEmbedding.ComputeEmbedding(item.Value);
                }

                // Remove entries with List<double> where all values are 0
                var keysToRemove = embeddings
                    .Where(pair => pair.Value.All(value => value == 0.0))
                    .Select(pair => pair.Key)
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    embeddings.Remove(key);
                }

                // Compute Similarity between text embeddings
                TextSimilarity.GenerateCosineSimilarityMatrix(embeddings, cosineSimilarityPath);

                Results.PrintResults(cosineSimilarityPath);

                // Wait for user to press Enter before closing
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                UtilityClass.DeleteAllFiles();
            }
        }

        static Dictionary<string, string> OcrResults(Dictionary<string, string> preprocessedImages)
        {
            TextExtraction textExtraction = new();
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