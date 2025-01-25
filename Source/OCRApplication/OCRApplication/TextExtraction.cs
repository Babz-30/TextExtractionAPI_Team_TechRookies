using Tesseract;

namespace OCRApplication
{
    internal class TextExtraction
    {
        // Path to the trained tessdata folder
        readonly string tessDataPath = UtilityClass.TessDataPath();
        public void ExtractText(string imagePath, string language)
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
                string extractedText = page.GetText();
                Display(extractedText);


                // Display OCR confidence
                float confidence = page.GetMeanConfidence();
                Console.WriteLine($"Confidence: {confidence * 100:F2}%");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void Display(String extractedText)
        {
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(extractedText);
        }
    }
}
