namespace QuizAppAPI.DTOs
{
    public class QuizSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public int QuestionCount { get; set; }
        public bool IsPremium { get; set; }
    }

    // Question sent to client WITHOUT the correct answer
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
    }

    public class QuizWithQuestionsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; } = string.Empty; // "A", "B", "C", "D"
    }

    public class SubmitQuizDto
    {
        public int UserId { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class QuestionFeedbackDto
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; } = string.Empty;
        public string CorrectOption { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class QuizResultDto
    {
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int XPEarned { get; set; }
        public List<QuestionFeedbackDto> Feedback { get; set; } = new();
    }
}