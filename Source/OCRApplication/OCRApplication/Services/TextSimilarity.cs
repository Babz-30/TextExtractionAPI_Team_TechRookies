namespace OCRApplication.Services
{
    public static class TextSimilarity
    {
        /// <summary>
        /// Computes the cosine similarity between two numeric vectors.
        /// Cosine similarity measures the angle between two vectors in a multi-dimensional space.
        /// </summary>
        /// <param name="vectorA">First vector of numerical values.</param>
        /// <param name="vectorB">Second vector of numerical values.</param>
        /// <returns>Cosine similarity score ranging from -1 (opposite) to 1 (identical).</returns>
        public static double ComputeCosineSimilarity(List<double> vectorA, List<double> vectorB)
        {
            double dotProduct = 0, magA = 0, magB = 0;

            // Ensure both vectors have the same length before computation
            if (vectorA.Count != vectorB.Count)
                throw new ArgumentException("Vectors must have the same dimensions.");

            // Compute dot product and magnitudes of the vectors
            for (int i = 0; i < vectorA.Count; i++)
            {
                dotProduct += vectorA[i] * vectorB[i]; // Summing element-wise products
                magA += Math.Pow(vectorA[i], 2); // Squaring elements for magnitude A
                magB += Math.Pow(vectorB[i], 2); // Squaring elements for magnitude B
            }

            // Compute cosine similarity (avoid division by zero)
            return (magA == 0 || magB == 0) ? 0 : dotProduct / (Math.Sqrt(magA) * Math.Sqrt(magB));
        }
    }
}
