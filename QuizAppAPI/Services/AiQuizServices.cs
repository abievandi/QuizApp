using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CodeLingoAPI.Services
{
    public class AIQuizService
    {
        private readonly string apiKey;

        public AIQuizService(IConfiguration config)
        {
            if (config["GeminiApiKey"] == null)
                {
                    apiKey = "";
                 }
            else
                {
                    apiKey = config["GeminiApiKey"];
                }
        private readonly HttpClient _client = new HttpClient();

        public async Task<List<QuizQuestion>> GenerateQuizAsync(string topic, string difficulty = "medium")
        {
            string prompt = $@"Create 5 {difficulty} level multiple choice questions about {topic} for a coding quiz app.
Return ONLY a JSON array, no extra text, no markdown, in this exact format:
[
  {{
    ""question"": ""What is...?"",
    ""options"": [""A) Option1"", ""B) Option2"", ""C) Option3"", ""D) Option4""],
    ""correctAnswer"": ""A) Option1""
  }}
]";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.0-flash:generateContent?key={apiKey}";
            var response = await _client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();

            var parsed = JsonSerializer.Deserialize<JsonElement>(result);
            string aiReply = parsed
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";

            aiReply = aiReply.Replace("```json", "").Replace("```", "").Trim();

            var questions = JsonSerializer.Deserialize<List<QuizQuestion>>(aiReply);
            if (questions == null)
            {
                return new List<QuizQuestion>();
            }
            return questions;
}
        }
    }

    public class QuizQuestion
    {
        public string question { get; set; } = "";
        public List<string> options { get; set; } = new();
        public string correctAnswer { get; set; } = "";
    }
}
