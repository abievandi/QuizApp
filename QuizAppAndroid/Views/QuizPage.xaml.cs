
using QuizAppAndroid.Services;

namespace QuizAppAndroid.Views;

public partial class QuizPage : ContentPage
{
    private readonly ApiService _api = new();
    private List<dynamic> _questions = new();
    private int _index = 0;

    public QuizPage()
    {
        InitializeComponent();
        LoadQuiz();
    }

    private async void LoadQuiz()
    {
        _questions = await _api.GetAIQuiz(1);

        if (_questions == null || _questions.Count == 0)
        {
            QuestionLabel.Text = "No quiz questions found.";
            return;
        }

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        var q = _questions[_index];
        QuestionLabel.Text = q.question;
    }

    private async void OnAnswerClicked(object sender, EventArgs e)
    {
        _index++;

        if (_index >= _questions.Count)
        {
            await Shell.Current.GoToAsync(nameof(ResultsPage));
            return;
        }

        ShowQuestion();
    }
}