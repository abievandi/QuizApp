using CommunityToolkit.Mvvm.Input;
using QuizAppAndroid.Models;

namespace QuizAppAndroid.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}