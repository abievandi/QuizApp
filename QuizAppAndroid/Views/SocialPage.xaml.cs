
using QuizAppAndroid.Services;

namespace QuizAppAndroid.Views;

public partial class SocialPage : ContentPage
{
    private readonly ApiService _api = new();

    public SocialPage()
    {
        InitializeComponent();
        LoadLeaderboard();
    }

    private async void LoadLeaderboard()
    {
        var data = await _api.GetLeaderboard();
        LeaderboardList.ItemsSource = data;
    }
}