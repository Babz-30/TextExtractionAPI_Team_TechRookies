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
                    "grayscale_binarization", 
                    "chainfilter",
                    "invert",
                    "hsi_adjustment",
                    "mirror"
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

                // Debug: Print preprocessed images
                Console.WriteLine("\n[DEBUG] Preprocessed Images:");
                foreach (var img in preprocessedImages)
                {
                    Console.WriteLine($"{img.Key} -> {img.Value}");
                }

                // Perform OCR text extraction on preprocessed images
                ocrTexts = OcrResults(preprocessedImages);

                // Debug: Print extracted OCR text
                Console.WriteLine("\n[DEBUG] OCR Results:");
                foreach (var result in ocrTexts)
                {
                    Console.WriteLine($"{result.Key}: {result.Value}");
                }

                // Generate embeddings
                Dictionary<string, List<double>> embeddings = new Dictionary<string, List<double>>();

                foreach (var item in ocrTexts)
                {
                    embeddings[item.Key] = await TextEmbedding.ComputeEmbedding(item.Value);
                }

                // Debug: Print embeddings before filtering
                Console.WriteLine("\n[DEBUG] Text Embeddings:");
                foreach (var emb in embeddings)
                {
                    Console.WriteLine($"{emb.Key} -> [{string.Join(", ", emb.Value)}]");
                }

                // Remove entries with all-zero embeddings
                var keysToRemove = embeddings
                    .Where(pair => pair.Value.All(value => value == 0.0))
                    .Select(pair => pair.Key)
                    .ToList();

                if (keysToRemove.Count > 0)
                {
                    Console.WriteLine("[WARNING] Some embeddings are all zero and might affect similarity computation.");
                }

                foreach (var key in keysToRemove)
                {
                    embeddings.Remove(key);
                }

                // Debug: Ensure embeddings exist before similarity computation
                if (embeddings.Count == 0)
                {
                    Console.WriteLine("[ERROR] No valid embeddings found! Cosine similarity matrix will not be generated.");
                }
                else
                {
                    Console.WriteLine($"\n[INFO] Generating Cosine Similarity Matrix at: {cosineSimilarityPath}");
                    TextSimilarity.GenerateCosineSimilarityMatrix(embeddings, cosineSimilarityPath);
                    Console.WriteLine("[SUCCESS] Cosine Similarity Matrix generated successfully!");
                }

                // Wait for user input before closing
                Console.WriteLine("\nPress Enter to exit...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
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
    }
}
