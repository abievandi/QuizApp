
using QuizAppAndroid.Services;

namespace QuizAppAndroid.Views;

public partial class ResultsPage : ContentPage
{
    private readonly ApiService _api = new();

    public ResultsPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Saved", "Result saved!", "OK");
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}