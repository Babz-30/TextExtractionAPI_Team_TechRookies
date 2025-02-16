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
    }
}
