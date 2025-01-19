using System;
using Tesseract;
using System.Drawing;

namespace OCRApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the trained tessdata folder
            string tessDataPath = @"path_to_tessdata";

            // Path to your image file
            string imagePath = @"path_to_image\image4.jpg";

            // Path to your mirrored image file
            string mirroredImagePath = @"path_to_image\mirror_image1.png";

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
    }

