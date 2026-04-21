using QuizAppAPI.Models;

namespace QuizAppAPI.Data
{
    // Temporary in-memory data store.
    // Andera will replace this with a real SQLite/EF Core database.
    public static class InMemoryStore
    {
        public static List<User> Users { get; } = new()
        {
            new User { Id = 1, Username = "mohin", Email = "mohin@test.com", XP = 120, Level = 2, Streak = 3, SubscriptionType = "Free" },
            new User { Id = 2, Username = "abie",  Email = "abie@test.com",  XP = 350, Level = 4, Streak = 7, SubscriptionType = "Premium" }
        };

        public static List<Quiz> Quizzes { get; } = new()
        {
            new Quiz
            {
                Id = 1, Title = "Basic Math", Category = "Math", Difficulty = "Easy", IsPremium = false,
                Questions = new List<Question>
                {
                    new() { Id = 1, QuizId = 1, Text = "What is 5 + 7?",   OptionA = "10", OptionB = "11", OptionC = "12", OptionD = "13", CorrectOption = "C" },
                    new() { Id = 2, QuizId = 1, Text = "What is 9 x 3?",   OptionA = "27", OptionB = "24", OptionC = "29", OptionD = "21", CorrectOption = "A" },
                    new() { Id = 3, QuizId = 1, Text = "What is 100 / 4?", OptionA = "20", OptionB = "25", OptionC = "30", OptionD = "15", CorrectOption = "B" }
                }
            },
            new Quiz
            {
                Id = 2, Title = "General Science", Category = "Science", Difficulty = "Medium", IsPremium = false,
                Questions = new List<Question>
                {
                    new() { Id = 4, QuizId = 2, Text = "What planet is closest to the Sun?", OptionA = "Venus",  OptionB = "Mercury", OptionC = "Mars",   OptionD = "Earth",   CorrectOption = "B" },
                    new() { Id = 5, QuizId = 2, Text = "What is H2O commonly known as?",    OptionA = "Oxygen", OptionB = "Salt",    OptionC = "Water",  OptionD = "Hydrogen",CorrectOption = "C" },
                    new() { Id = 6, QuizId = 2, Text = "How many bones in the adult body?", OptionA = "206",    OptionB = "201",     OptionC = "210",    OptionD = "196",     CorrectOption = "A" }
                }
            },
            new Quiz
            {
                Id = 3, Title = "Advanced History", Category = "History", Difficulty = "Hard", IsPremium = true,
                Questions = new List<Question>
                {
                    new() { Id = 7, QuizId = 3, Text = "In which year did WWII end?",       OptionA = "1943", OptionB = "1945", OptionC = "1947", OptionD = "1950", CorrectOption = "B" },
                    new() { Id = 8, QuizId = 3, Text = "Who was the first US President?",   OptionA = "Lincoln", OptionB = "Jefferson", OptionC = "Washington", OptionD = "Adams", CorrectOption = "C" }
                }
            }
        };

        public static List<Result> Results { get; } = new();
    }
}