using Newtonsoft.Json;
using OCRApplication.Helpers;
using System.Text;

namespace OCRApplication.Services
{
    public static class TextEmbedding
    {
        // Call OpenAI API to get text embedding
        public static async Task<List<double>> ComputeEmbedding(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<double>(new double[1536]);


            var config = Configuration.Config();
            string url = "https://api.openai.com/v1/embeddings"; // Example API

            var requestBody = new
            {
                model = "text-embedding-ada-002",
                input = text,
            };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", config["authtoken"]);

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var embeddingArray = jsonResponse["data"][0]["embedding"].ToObject<List<double>>();

                return embeddingArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching embedding: {ex.Message}");
                return new List<double>(new double[1536]); // Return a zero-vector in case of failure
            }
        }

    }
}
