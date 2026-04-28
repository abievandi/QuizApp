
using QuizAppAndroid.Services;

namespace QuizAppAndroid.Views;

public partial class SubscriptionPage : ContentPage
{
    private readonly ApiService _api = new();

    public SubscriptionPage()
    {
        InitializeComponent();
    }

    private async void OnUpgradeClicked(object sender, EventArgs e)
    {
        var success = await _api.Upgrade(1);

        if (success)
            await DisplayAlert("Success", "Upgraded!", "OK");
    }
}