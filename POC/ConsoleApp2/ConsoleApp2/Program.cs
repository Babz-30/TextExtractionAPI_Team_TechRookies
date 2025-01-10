using System;
using Tesseract;
using System.Drawing;

namespace OCRApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the tessdata folder
            string tessDataPath = @"D:\OCR-SwEngg\tessdata";

            // Path to your image file
            string imagePath = @"D:\Cultural Diversity-Akanksha\pics\1690377485631.png";

            try
            {
                // Initialize the Tesseract engine
                using (var ocrEngine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    Console.WriteLine("Tesseract engine initialized.");

                    // Load the image file
                    using (var image = Pix.LoadFromFile(imagePath))
                    {
                        Console.WriteLine("Image loaded successfully.");

                        // Perform OCR on the image
                        using (var page = ocrEngine.Process(image))
                        {
                            // Extract and display text
                            string extractedText = page.GetText();
                            Console.WriteLine("Extracted Text:");
                            Console.WriteLine(extractedText);

                            // Display OCR confidence
                            float confidence = page.GetMeanConfidence();
                            Console.WriteLine($"Confidence: {confidence * 100:F2}%");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
