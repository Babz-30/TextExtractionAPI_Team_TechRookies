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

                string cosineSimilarityPath = UtilityClass.CosineSimilarityDirectory("CosineSimilarityMatrix.csv");

                List<string> techniques = new List<string>
                {
                    "rotation",
                    "cannyfilter",
                    "chainfilter",
                    "invert",
                    "hsi_adjustment",
                    "denoise",
                    "mirror_horizontal"
                };

                PreprocessingFactory preprocessingFactory = new PreprocessingFactory();
                Dictionary<string, string> preprocessedImages = new Dictionary<string, string>();
                Dictionary<string, string> ocrTexts = new Dictionary<string, string>();

                // Apply preprocessing techniques
                foreach (var technique in techniques)
                {
                    Dictionary<string, string> processedImages = preprocessingFactory.PreprocessImage(inputImagePath, technique);

                    foreach (var img in processedImages)
                    {
                        preprocessedImages[img.Key] = img.Value; // Store all preprocessed variations
                    }
                }

                // Perform OCR text extraction on preprocessed images
                ocrTexts = OcrResults(preprocessedImages);

                // Generate embeddings
                Dictionary<string, List<double>> embeddings = new Dictionary<string, List<double>>();

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

                PrintResults(cosineSimilarityPath);

                // Compute Similarity between text embeddings
                TextSimilarity.GenerateCosineSimilarityMatrix(embeddings, cosineSimilarityPath);

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
            TextExtraction textExtraction = new TextExtraction();
            string extractedText;

            foreach (var technique in preprocessedImages)
            {
                extractedText = textExtraction.ExtractText(technique.Value, technique.Key);
                ocrResults[technique.Key] = extractedText;
            }

            return ocrResults;
        }

        static void PrintResults(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var results = new List<(string Technique, double Mean)>();

            foreach (var line in lines.Skip(1)) // Skip header
            {
                var parts = line.Split(',');
                string technique = parts[0];
                var values = parts.Skip(1).Select(s => double.Parse(s)).ToList();
                double mean = values.Average();
                results.Add((technique, mean));
            }

            var top5 = results.OrderByDescending(r => r.Mean).Take(5);
            Console.WriteLine("===========================================================================");
            Console.WriteLine("Top 5 Techniques with Highest Mean Cosine similarity:");
            foreach (var item in top5)
            {
                Console.WriteLine($"{item.Technique}: {item.Mean:F4}");
            }
            Console.WriteLine("===========================================================================");
        }
    }
}