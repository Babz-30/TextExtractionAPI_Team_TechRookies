﻿using OCRApplication.Helpers;
using Tesseract;

namespace OCRApplication.Services
{
    /// <summary>
    /// Provides functionality to extract text from an image using OCR (Optical Character Recognition).
    /// </summary>
    internal class TextExtraction
    {

        // Path to the trained tessdata folder
        readonly string TrainedDataPath = UtilityClass.TrainedDataPath();

        /// <summary>
        /// Extracts text from images using tesseract api
        /// </summary>
        /// <param name="imagePath">Path to input image.</param>
        /// <param name="technique">Preprocessing technique.</param>
        /// <param name="language">Language used for text extraction default english</param>
        /// <returns></returns>
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

        /// <summary>
        /// Combines text extracted from all the preprocessed images
        /// </summary>
        /// <param name="preprocessedImages">Preprocessed Images</param>
        /// <returns>Combined dictionary of Text extracted from all preprocessed images</returns>
        public static Dictionary<string, string> GetTexts(Dictionary<string, string> preprocessedImages)
        {
            Dictionary<string, string> ocrResults = [];
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
