using OCRApplication.Helpers;
using OCRApplication.Preprocesssing;
using System.ComponentModel.DataAnnotations;
using Tesseract;

namespace OCRApplication.Services
{
    internal class TextExtraction
    {
        // Path to the trained tessdata folder
        readonly string tessDataPath = UtilityClass.TessDataPath();
        string tesseractConfidenceOutputFilePath = UtilityClass.TesseractOutputPath("ExtractedTextMeanConfidence.csv");
        public string ExtractText(string imagePath, string technique, string language = "eng")
        {
            try
            {
                // Initialize the Tesseract engine
                using var ocrEngine = new TesseractEngine(tessDataPath, language, EngineMode.Default);
                Console.WriteLine("Tesseract engine initialized.");

                // Load the image file
                using var image = Pix.LoadFromFile(imagePath);
                Console.WriteLine("Image loaded successfully.");

                // Perform OCR on the image
                using var page = ocrEngine.Process(image);
                // Extract and display text
                string extractedText = page.GetText().Trim();

                string tesseractOutputFilePath = UtilityClass.TesseractOutputPath($"tesseract_output_{technique}.txt");

                UtilityClass.SaveToFile(tesseractOutputFilePath, extractedText);

                // Save OCR confidence
                float confidence = page.GetMeanConfidence();

                SaveConfidence(technique, confidence);
                
                return extractedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during text extraction by tesseract: {ex.Message}");
                throw;
            }
        }

        private void SaveConfidence(string technique, float confidence)
        {
            // Check if the file exists
            bool fileExists = File.Exists(tesseractConfidenceOutputFilePath);

            // If the file doesn't exist, write the headers
            if (!fileExists)
            {
                string headers = "Technique,PageMeanCharWordRecognition";
                File.WriteAllText(tesseractConfidenceOutputFilePath, headers + Environment.NewLine);
            }
            
            // Create a line with the values (CSV format)
            string newLine = $"{technique},{confidence}";

            // Append the new line to the file
            File.AppendAllText(tesseractConfidenceOutputFilePath, newLine + Environment.NewLine);

            Console.WriteLine("Data added successfully.");
        }
    }
}
