using OCRApplication.Helpers;
using Tesseract;

namespace OCRApplication.Services
{
    public class TextAnalysis
    {
        static readonly string tesseractConfidenceOutputFilePath = UtilityClass.TesseractOutputPath("ExtractedTextMeanConfidence.csv");

        public static void SaveConfidence(string technique, Page page)
        { 
            // Check if the file exists
            bool fileExists = File.Exists(tesseractConfidenceOutputFilePath);

            List<float> confidences = [];
            int totalWords = 0, correctWords = 0;

            try
            {
                string dictionaryPath = UtilityClass.DictionaryPath();
                // Load English dictionary from file
                HashSet<string> dictionary = LoadDictionary(dictionaryPath);


                using (var iter = page.GetIterator())
                {
                    iter.Begin();

                    do
                    {
                        if (iter.IsAtBeginningOf(PageIteratorLevel.Word))
                        {
                            string? word = iter.GetText(PageIteratorLevel.Word)?.Trim();
                            float confidence = iter.GetConfidence(PageIteratorLevel.Word);

                            if (!string.IsNullOrEmpty(word) && confidence > 0)
                            {
                                totalWords++;
                                confidences.Add(confidence);

                                // Check if the word exists in dictionary
                                if (dictionary.Contains(word.ToLower()))
                                {
                                    correctWords++;
                                }
                            }
                        }
                    } while (iter.Next(PageIteratorLevel.Word));
                }

                // Calculate metrics
                float meanConfidence = totalWords > 0 ? (float)confidences.Average() /100 : 0;
                float dictionaryAccuracy = totalWords > 0 ? (float)correctWords / totalWords : 0;

                // If the file doesn't exist, write the headers
                if (!fileExists)
                {
                    string headers = "Technique,TotalWords,MeanConfidence,DictionaryAccuracy";
                    File.WriteAllText(tesseractConfidenceOutputFilePath, headers + Environment.NewLine);
                }

                // Create a line with the values (CSV format)
                string newLine = $"{technique},{totalWords},{meanConfidence:F4},{dictionaryAccuracy:F4}";

                // Append the new line to the file
                File.AppendAllText(tesseractConfidenceOutputFilePath, newLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Text Analysis: " + ex.Message);
            }
        }

        // Load dictionary words from a file into a HashSet
        static HashSet<string> LoadDictionary(string filePath)
        {
            HashSet<string> words = [];

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    words.Add(line.Trim().ToLower());
                }
            }
            else
            {
                Console.WriteLine("Dictionary file not found! Using an empty dictionary.");
            }

            return words;
        }
    }
}
