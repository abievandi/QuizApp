using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using CodeLingoAPI.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeLingoAPI.Views
{
    public partial class QuizPage : ContentPage
    {
        int currentIndex = 0;
        int score = 0;

        List<Question> questions;
        string selectedAnswer;

        int timeLeft = 10;
        System.Timers.Timer timer;

        public QuizPage()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimerTick;
            _ = LoadQuestionsFromAPI();
        }

        void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    QuestionId = 1,
                    QuestionText = "What is the chemical symbol for Water?",
                    OptionA = "WA",
                    OptionB = "H2O",
                    OptionC = "HO",
                    OptionD = "W",
                    CorrectOption = "B"
                },
                new Question
                {
                    QuestionId = 2,
                    QuestionText = "What planet is closest to the Sun?",
                    OptionA = "Earth",
                    OptionB = "Venus",
                    OptionC = "Mercury",
                    OptionD = "Mars",
                    CorrectOption = "C"
                },
                new Question
                {
                    QuestionId = 3,
                    QuestionText = "What gas do plants absorb?",
                    OptionA = "Oxygen",
                    OptionB = "Nitrogen",
                    OptionC = "Hydrogen",
                    OptionD = "Carbon Dioxide",
                    CorrectOption = "D"
                }
            };

            DisplayQuestion();
        }

        async Task LoadQuestionsFromAPI()
        {
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync("http://localhost:5000/api/quiz/questions");
                questions = JsonSerializer.Deserialize<List<Question>>(json);
                DisplayQuestion();
            }
            catch
            {
                await DisplayAlert("Notice", "Using local questions", "OK");
                LoadQuestions();
            }
        }

        void DisplayQuestion()
        {
            var q = questions[currentIndex];

            QuestionNumberLabel.Text = $"Question {currentIndex + 1}/{questions.Count}";
            QuestionLabel.Text = q.QuestionText;

            OptionA.Text = $"A. {q.OptionA}";
            OptionB.Text = $"B. {q.OptionB}";
            OptionC.Text = $"C. {q.OptionC}";
            OptionD.Text = $"D. {q.OptionD}";

            OptionA.IsEnabled = true;
            OptionB.IsEnabled = true;
            OptionC.IsEnabled = true;
            OptionD.IsEnabled = true;

            OptionA.BackgroundColor = Colors.LightGray;
            OptionB.BackgroundColor = Colors.LightGray;
            OptionC.BackgroundColor = Colors.LightGray;
            OptionD.BackgroundColor = Colors.LightGray;

            timeLeft = 10;
            TimerLabel.Text = $"Time: {timeLeft}";
            timer.Start();

            QuizProgress.Progress = (double)(currentIndex + 1) / questions.Count;
        }

        void OnOptionSelected(object sender, EventArgs e)
        {
            var btn = sender as Button;
            selectedAnswer = btn.Text.Substring(0, 1);

            OptionA.BackgroundColor = Colors.LightGray;
            OptionB.BackgroundColor = Colors.LightGray;
            OptionC.BackgroundColor = Colors.LightGray;
            OptionD.BackgroundColor = Colors.LightGray;

            btn.BackgroundColor = Colors.LightBlue;

            OptionA.IsEnabled = false;
            OptionB.IsEnabled = false;
            OptionC.IsEnabled = false;
            OptionD.IsEnabled = false;
        }

        async void OnNextClicked(object sender, EventArgs e)
        {
            timer.Stop();

            if (selectedAnswer == questions[currentIndex].CorrectOption)
                score += questions[currentIndex].Points;

            currentIndex++;

            if (currentIndex < questions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                await DisplayAlert("Quiz Finished", $"Your Score: {score}", "OK");
            }
        }

        void OnTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeLeft--;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = $"Time: {timeLeft}";
            });

            if (timeLeft <= 0)
            {
                timer.Stop();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnNextClicked(null, null);
                });
            }
        }
    }
}