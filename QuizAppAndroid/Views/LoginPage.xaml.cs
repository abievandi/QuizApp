

using QuizAppAndroid.Services;

namespace QuizAppAndroid.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _api = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var result = await _api.Login(EmailEntry.Text, PasswordEntry.Text);

        if (result == null)
        {
            ErrorLabel.Text = "Login failed";
            ErrorLabel.IsVisible = true;
            return;
        }

        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}