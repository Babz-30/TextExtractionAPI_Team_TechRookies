using Newtonsoft.Json;
using OCRApplication.Helpers;
using System.Text;

namespace OCRApplication.Services
{
    /// <summary>
    /// OpenAI used to calculate text embeddings.
    /// </summary>
    public static class TextEmbedding
    {
        /// <summary>
        /// Call OpenAI API to get text embedding of the input text.
        /// </summary>
        /// <param name="text">Text extracted from processed image.</param>
        /// <returns>List of double of the received text embeddings.</returns>
        public static async Task<List<double>> ComputeEmbedding(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<double>(new double[1536]);

            var config = Configuration.Config();
            string? url = config["API_URL"]; // API URL

            var requestBody = new
            {
                model = config["EMBEDDING_MODEL"],
                input = text,
            };

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", config["authtoken"]);

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, mediaType: config["MEDIA_TYPE"]);

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                string? responseContent = await response.Content.ReadAsStringAsync();

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
