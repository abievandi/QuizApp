using Quiz.Controllers;

namespace Quiz.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly ResultController _controller;
        private int _userId;

        public ProfilePage(AppDbContext context, int userId)
        {
            InitializeComponent();

            _controller = new ResultController(context);
            _userId = userId;

            LoadResults();
        }

        private void LoadResults()
        {
            var results = _controller.GetResultsByUser(_userId);
            ResultsList.ItemsSource = results;
        }
    }
}