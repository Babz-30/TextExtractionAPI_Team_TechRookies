using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OCRApplication
{
    public static class TextSimilarity
    {
        public static double ComputeCosineSimilarity(string text1, string text2)
        {
            if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(text2))
                return 0.0;

            Dictionary<string, int> freq1 = GetWordFrequencies(text1);
            Dictionary<string, int> freq2 = GetWordFrequencies(text2);

            HashSet<string> uniqueWords = new HashSet<string>(freq1.Keys);
            uniqueWords.UnionWith(freq2.Keys);

            double dotProduct = 0, magnitude1 = 0, magnitude2 = 0;

            foreach (var word in uniqueWords)
            {
                int count1 = freq1.ContainsKey(word) ? freq1[word] : 0;
                int count2 = freq2.ContainsKey(word) ? freq2[word] : 0;

                dotProduct += count1 * count2;
                magnitude1 += Math.Pow(count1, 2);
                magnitude2 += Math.Pow(count2, 2);
            }

            magnitude1 = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);

            return magnitude1 == 0 || magnitude2 == 0 ? 0 : dotProduct / (magnitude1 * magnitude2);
        }

        private static Dictionary<string, int> GetWordFrequencies(string text)
        {
            Dictionary<string, int> wordFrequencies = new();

            // Remove special characters and convert to lowercase
            text = text.ToLower().Trim('"');

            string[] words = Regex.Split(text, "\\W+");

            foreach (string word in words)
            {
                if (string.IsNullOrWhiteSpace(word)) continue;

                if (wordFrequencies.ContainsKey(word))
                    wordFrequencies[word]++;
                else
                    wordFrequencies[word] = 1;
            }
            return wordFrequencies;
        }
    }
}
