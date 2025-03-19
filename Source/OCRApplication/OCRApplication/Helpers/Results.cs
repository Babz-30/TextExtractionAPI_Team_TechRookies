using CsvHelper;
using System.Globalization;

namespace OCRApplication.Helpers
{
    public class Results
    {
        public static string PrintResults(string filePath)
        {
            try
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

                Console.WriteLine("\n================================================================================================");
                Console.WriteLine("Top 5 Techniques with Highest Mean Cosine similarity:");
                foreach (var (Technique, Mean) in top5)
                {
                    Console.WriteLine($"{Technique}: {Mean:F4}");
                }
                Console.WriteLine("================================================================================================");

                // Read CSV file to fetch dictionary accuracy
                string filePathConf = UtilityClass.TesseractOutputPath("ExtractedTextMeanConfidence.csv");

                using var reader = new StreamReader(filePathConf);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<OCRAnalysis>().ToList();

                // Merge top 5 results with dictionary accuracy from the CSV
                var top5WithAccuracy = top5
                    .Join(records,
                          result => result.Technique,
                          record => record.Technique,
                          (result, record) => new
                          {
                              result.Technique,
                              result.Mean,
                              record.DictionaryAccuracy,
                              record.MeanConfidence
                          })
                    .OrderByDescending(r => r.Mean)
                    .ThenByDescending(r => r.DictionaryAccuracy)    // Sort first by Dictionary Accuracy
                    .ThenByDescending(r => r.MeanConfidence)       // Then by Mean Confidence
                    .Take(5).ToList();

                // Best technique
                var bestTechnique = top5WithAccuracy.First();

                if (bestTechnique.DictionaryAccuracy == 0)
                {
                    Console.WriteLine("The above Techniques have 0 Dictionary Accuracy, hence eliminating false similarity.");
                    top5WithAccuracy = records.Where(r => r.DictionaryAccuracy > 0).Join(results,
                          result => result.Technique,
                          record => record.Technique,
                          (result, record) => new
                          {
                              result.Technique,
                              record.Mean,
                              result.DictionaryAccuracy,
                              result.MeanConfidence
                          }).OrderByDescending(r => r.DictionaryAccuracy).ThenByDescending(r => r.MeanConfidence).Take(5).ToList();
                    if (top5WithAccuracy.Count != 0)
                    {
                        bestTechnique = top5WithAccuracy.First();
                    }
                }

                // Display results
                Console.WriteLine("Top 5 Techniques (then sorted by Dictionary Accuracy and then by Mean Confidence):");
                foreach (var item in top5WithAccuracy)
                {
                    Console.WriteLine($"Technique: {item.Technique}, Dictionary Accuracy: {item.DictionaryAccuracy}, Mean Confidence: {item.MeanConfidence}");
                }

                Console.WriteLine("================================================================================================");
                Console.WriteLine($"Best Technique: {bestTechnique.Technique} \nMean Cosine Similarity: {bestTechnique.Mean} \nDictionary Accuracy: {bestTechnique.DictionaryAccuracy} \nMean Confidence: {bestTechnique.MeanConfidence}");
                return bestTechnique.Technique;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the CSV file: " + ex.Message);
            }
            return "";
        }
    }

    class OCRAnalysis
    {
        public required string Technique { get; set; }          // Name of preprocessing technique
        public double DictionaryAccuracy { get; set; } // Accuracy percentage from dictionary
        public double MeanConfidence { get; set; }     // Mean confidence percentage
    }
}
