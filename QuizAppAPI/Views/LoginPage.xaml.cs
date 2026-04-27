using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CodeLingoAPI.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            // Basic validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorLabel.Text = "Please enter email and password";
                ErrorLabel.IsVisible = true;
                return;
            }

            try
            {
                var client = new HttpClient();

                var loginData = new
                {
                    email = email,
                    password = password
                };

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "http://localhost:5000/api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Success", "Login successful!", "OK");

                    // Navigate to Quiz page
                    await Navigation.PushAsync(new QuizPage());
                }
                else
                {
                    ErrorLabel.Text = "Invalid email or password";
                    ErrorLabel.IsVisible = true;
                }
            }
            catch
            {
                ErrorLabel.Text = "Cannot connect to server";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}