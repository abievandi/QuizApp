using QuizAppAndroid.Models;
using QuizAppAndroid.PageModels;

namespace QuizAppAndroid.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}