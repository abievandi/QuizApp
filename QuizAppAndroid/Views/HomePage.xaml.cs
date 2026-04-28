namespace QuizAppAndroid.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnStartQuiz(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(QuizPage));
    }

    private async void OnLeaderboard(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SocialPage));
    }

    private async void OnProfile(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}