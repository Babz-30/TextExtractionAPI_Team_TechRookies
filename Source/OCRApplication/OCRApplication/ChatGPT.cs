using System.Text;
using System.Text.Json;

namespace OCRApplication
{
    public class ChatGPT
    {
        // Path to save the extracted text
        private readonly string chatgptOutputFilePath = UtilityClass.ChatgptOutputPath("chatgpt_output.txt");

        public async void Task(string imagepath)
        {
            try
            {
                string base64Image = EncodeImage(imagepath);

                var client = new HttpClient();
                var config = Configuration.Config();
                client.DefaultRequestHeaders.Add("Authorization", config["authtoken"]);

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new { type = "text", text = "Flip and rotate the  vertical mirror image and extract the text from the without adding any extra information." },
                        new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                    }
                }
                }
                };

                string jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                // Extract only the "content" field
                string extractedContent = ExtractContent(responseString);

                UtilityClass.SaveToFile(chatgptOutputFilePath, extractedContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during image extraction from chatGPT: {ex.Message}");
                throw;
            }
        }

        static string EncodeImage(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        static string ExtractContent(string jsonResponse)
        {
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("choices", out JsonElement choices) && choices.GetArrayLength() > 0)
            {
                JsonElement firstChoice = choices[0];
                if (firstChoice.TryGetProperty("message", out JsonElement message) &&
                    message.TryGetProperty("content", out JsonElement content))
                {
                    string text = content.GetString().Trim();
                    int index = text.IndexOf("\n\n");
                    return index >= 0 ? text[(index + 2)..] : text;
                }
            }

            return "No content found in response.";
        }
    }
}