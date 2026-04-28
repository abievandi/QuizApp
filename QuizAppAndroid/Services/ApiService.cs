using System.Net.Http.Json;

namespace QuizAppAndroid.Services;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5226/api/")
        };
    }

    public async Task<dynamic?> Login(string email, string password)
    {
        var response = await _client.PostAsJsonAsync("auth/login", new
        {
            Email = email,
            PasswordHash = password
        });

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<dynamic>();
    }

    public async Task<bool> Upgrade(int userId)
    {
        var response = await _client.PostAsync($"subscription/upgrade/{userId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<dynamic>?> GetAIQuiz(int userId)
    {
        return await _client.GetFromJsonAsync<List<dynamic>>(
            $"aiquiz/generate?userId={userId}");
    }

    public async Task<List<dynamic>?> GetLeaderboard()
    {
        return await _client.GetFromJsonAsync<List<dynamic>>("social/leaderboard");
    }
}