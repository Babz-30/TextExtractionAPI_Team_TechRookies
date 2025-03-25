using OCRApplication.Helpers;
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
                Console.WriteLine("Computing the best pre-processing technique to extract text from image by tesseract OCR.");

                // Path to your input image file
                var config = Configuration.Config();
                var imageName = UtilityClass.GetRequiredConfigValue(config, "InputImage");
                string inputImagePath = UtilityClass.InputImagePath(imageName);

                // Path to output cosine similarity matrix file
                string cosineSimilarityPath = UtilityClass.GetRequiredConfigValue(config,"CosineSimilarityFileName");

                //Preprocessing Techniques
                List<string> techniques = UtilityClass.GetConfigurationList<string>(config, "Techniques");

                // Apply preprocessing techniques
                Dictionary<string, string> preprocessedImages = PreprocessingFactory.ApplyPreprocessing(inputImagePath, techniques);

                // Perform OCR text extraction on preprocessed images
                Dictionary<string, string>  ocrTexts = TextExtraction.GetTexts(preprocessedImages);

                Console.WriteLine("Computing Embeddings for extracted text and then calculating cosine similarity between preprocessing techniques...");

                // Generate embeddings of the extracted texts
                Dictionary<string, List<double>> textEmbeddings = await TextEmbedding.GetTextEmbeddingsAsync(ocrTexts);

                // Compute Cosine Similarity between text embeddings
                TextSimilarity.GenerateCosineSimilarityMatrix(textEmbeddings, cosineSimilarityPath);

                // Data mining to determine the best preprocessing technique
                Results.PrintResults(cosineSimilarityPath, ocrTexts);
                
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
    }        
}