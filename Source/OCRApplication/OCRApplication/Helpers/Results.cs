using CsvHelper;
using System.Globalization;

namespace OCRApplication.Helpers
{
    public class Results
    {
        public static void PrintResults(string filePath)
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

            Console.WriteLine("================================================================================================");
            Console.WriteLine("Top 5 Techniques with Highest Mean Cosine similarity:");
            foreach (var item in top5)
            {
                Console.WriteLine($"{item.Technique}: {item.Mean:F4}");
            }
            Console.WriteLine("================================================================================================");

            // Read CSV file to fetch dictionary accuracy
            string filePathConf = UtilityClass.TesseractOutputPath("ExtractedTextMeanConfidence.csv");

            try
            {
                using (var reader = new StreamReader(filePathConf))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<OCRAnalysis>().ToList();

                    // Merge top 5 results with dictionary accuracy from the CSV
                    var top5WithAccuracy = top5
                        //.OrderByDescending(r => r.Mean)
                        .Join(records,
                              result => result.Technique,
                              record => record.Technique,
                              (result, record) => new
                              {
                                  result.Technique,
                                  Mean = result.Mean,
                                  DictionaryAccuracy = record.DictionaryAccuracy,
                                  MeanConfidence = record.MeanConfidence
                              })
                        .OrderByDescending(r => r.Mean)
                        .ThenByDescending(r => r.DictionaryAccuracy)    // Sort first by Dictionary Accuracy
                        .ThenByDescending(r => r.MeanConfidence)       // Then by Mean Confidence
                        .ToList();

                    // Best technique
                    var bestTechnique = top5WithAccuracy.First();


                    // Display results
                    Console.WriteLine("Top 5 Techniques (then sorted by Dictionary Accuracy and then by Mean Confidence):");
                    foreach (var item in top5WithAccuracy)
                    {
                        Console.WriteLine($"Technique: {item.Technique}, Dictionary Accuracy: {item.DictionaryAccuracy}%, Mean Confidence: {item.MeanConfidence}%");
                    }

                    Console.WriteLine("================================================================================================");
                    Console.WriteLine($"Best Technique:{bestTechnique.Technique} \nMean Cosine Similarity: {bestTechnique.Mean} \nDictionary Accuracy: {bestTechnique.DictionaryAccuracy}% \nMean Confidence: {bestTechnique.MeanConfidence}%");
                    Console.WriteLine("================================================================================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the CSV file: " + ex.Message);
            }
        }
    }

    class OCRAnalysis
    {
        public string Technique { get; set; }          // Name of preprocessing technique
        public double DictionaryAccuracy { get; set; } // Accuracy percentage from dictionary
        public double MeanConfidence { get; set; }     // Mean confidence percentage
    }
}
