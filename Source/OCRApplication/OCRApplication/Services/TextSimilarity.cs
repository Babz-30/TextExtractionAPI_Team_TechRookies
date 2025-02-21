namespace OCRApplication.Services
{
    public static class TextSimilarity
    {
        public static double ComputeCosineSimilarity(List<double> vectorA, List<double> vectorB)
        {
            double dotProduct = 0, magA = 0, magB = 0;

            for (int i = 0; i < vectorA.Count; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magA += Math.Pow(vectorA[i], 2);
                magB += Math.Pow(vectorB[i], 2);
            }

            return dotProduct / (Math.Sqrt(magA) * Math.Sqrt(magB));
        }

        // Function to generate the cosine similarity matrix and save it to a CSV
        public static void GenerateCosineSimilarityMatrix(Dictionary<string, List<double>> preprocessingEmbeddings, string filePath)
        {
            // Get the list of preprocessing techniques (keys)
            var techniques = preprocessingEmbeddings.Keys.ToList();

            // Initialize the matrix
            var matrix = new double[techniques.Count, techniques.Count];

            // Calculate cosine similarity for each pair
            for (int i = 0; i < techniques.Count; i++)
            {
                for (int j = 0; j < techniques.Count; j++)
                {
                    matrix[i, j] = ComputeCosineSimilarity(preprocessingEmbeddings[techniques[i]], preprocessingEmbeddings[techniques[j]]);
                }
            }

            // Write the matrix to a CSV file
            using (var writer = new StreamWriter(filePath))
            {
                // Write the header row
                writer.WriteLine(string.Join(",", new[] { "" }.Concat(techniques)));

                // Write each row
                for (int i = 0; i < techniques.Count; i++)
                {
                    var row = new List<string> { techniques[i] };
                    row.AddRange(matrix.GetRow(i).Select(value => value.ToString("F4"))); // Format the value with 4 decimal places
                    writer.WriteLine(string.Join(",", row));
                }
            }

            Console.WriteLine($"Cosine similarity matrix has been saved to: {filePath}");
        }
    }

    public static class MatrixExtensions
    {
        // Helper extension to get a row from a 2D array
        public static IEnumerable<double> GetRow(this double[,] matrix, int rowIndex)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                yield return matrix[rowIndex, i];
            }
        }
    }
}
