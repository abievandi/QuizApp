using System.Text;
using System.Text.Json;

namespace CodeLingoAPI.Services
{
    public class AIQuizService
    {
        private readonly HttpClient _client;
        private readonly string apiKey;

        public AIQuizService(IConfiguration config)
        {
            _client = new HttpClient();
            apiKey = config["GeminiApiKey"] ?? "";
        }

        public async Task<List<QuizQuestion>> GenerateQuizAsync(string topic, string difficulty = "medium")
        {
            string prompt = $@"Create 5 {difficulty} level multiple choice questions about {topic}.
Return ONLY JSON in this format:
[
  {{
    ""question"": ""..."",
    ""options"": [""A"", ""B"", ""C"", ""D""],
    ""correctAnswer"": ""A""
  }}
]";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            var parsed = JsonSerializer.Deserialize<JsonElement>(result);

            string aiReply = parsed
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";

            aiReply = aiReply.Replace("```json", "").Replace("```", "").Trim();

            var questions = JsonSerializer.Deserialize<List<QuizQuestion>>(aiReply);

            return questions ?? new List<QuizQuestion>();
        }
    }

    public class QuizQuestion
    {
        public string question { get; set; } = "";
        public List<string> options { get; set; } = new();
        public string correctAnswer { get; set; } = "";
    }
}