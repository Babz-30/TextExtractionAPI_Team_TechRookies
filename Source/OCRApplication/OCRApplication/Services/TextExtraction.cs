using OCRApplication.Helpers;
using OCRApplication.Preprocesssing;
using System.ComponentModel.DataAnnotations;
using Tesseract;

namespace OCRApplication.Services
{
    internal class TextExtraction
    {
        // Path to the trained tessdata folder
        readonly string TrainedDataPath = UtilityClass.TrainedDataPath();
        
        public string ExtractText(string imagePath, string technique, string language = "eng")
        {
            try
            {
                // Initialize the Tesseract engine
                using var ocrEngine = new TesseractEngine(TrainedDataPath, language, EngineMode.Default);
                Console.WriteLine("Tesseract engine initialized.");

                // Load the image file
                using var image = Pix.LoadFromFile(imagePath);

                // Perform OCR on the image
                using var page = ocrEngine.Process(image);
                // Extract and display text
                string extractedText = page.GetText().Trim();

                string tesseractOutputFilePath = UtilityClass.TesseractOutputPath($"tesseract_output_{technique}.txt");

                UtilityClass.SaveToFile(tesseractOutputFilePath, extractedText);

                TextAnalysis.SaveConfidence(technique, page);
                
                return extractedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during text extraction by tesseract: {ex.Message}");
                throw;
            }
        }
    }
}
