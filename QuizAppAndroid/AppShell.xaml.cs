
using CommunityToolkit.Maui.Alerts;

namespace QuizAppAndroid
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(Views.HomePage), typeof(Views.HomePage));
            Routing.RegisterRoute(nameof(Views.QuizPage), typeof(Views.QuizPage));
            Routing.RegisterRoute(nameof(Views.ResultsPage), typeof(Views.ResultsPage));
            Routing.RegisterRoute(nameof(Views.ProfilePage), typeof(Views.ProfilePage));
            Routing.RegisterRoute(nameof(Views.SocialPage), typeof(Views.SocialPage));
            Routing.RegisterRoute(nameof(Views.SubscriptionPage), typeof(Views.SubscriptionPage));
        }

        public static async Task DisplayToastAsync(string message)
        {
            if (OperatingSystem.IsWindows())
                return;

            var toast = Toast.Make(message, textSize: 18);
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            await toast.Show(cts.Token);
        }
    }
}