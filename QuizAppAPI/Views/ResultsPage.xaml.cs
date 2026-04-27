using Quiz.Controllers;

namespace Quiz.Views
{
    public partial class ResultsPage : ContentPage
    {
        private readonly ResultController _controller;
        private int _score;
        private int _userId;
        private int _quizItemId;

        public ResultsPage(AppDbContext context, int userId, int quizItemId, int correct, int total)
        {
            InitializeComponent();

            _controller = new ResultController(context);

            _userId = userId;
            _quizItemId = quizItemId;

            _score = _controller.CalculateScore(correct, total);

            ScoreLabel.Text = _score + "%";
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _controller.SaveResult(_userId, _quizItemId, _score);

            DisplayAlert("Success", "Result Saved Successfully!", "OK");
        }
    }
}