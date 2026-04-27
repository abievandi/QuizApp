

using CodeLingoAPI.Models;

namespace CodeLingoAPI.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            // If quizzes already exist, stop — don't add duplicates
            if (db.Quizzes.Any()) return;

            
            //  SUBJECT 1: ENGLISH
           
            var englishQuiz = new Quiz
            {
                Title = "English: Grammar Basics",
                Category = "English",
                Difficulty = "Easy",
                IsAIGenerated = false,
                IsPremium = false,    // free for everyone
                TimeLimitSeconds = 120,      // 2 minutes
                Questions = new List<Question>
                {
                    new Question {
                        QuestionText  = "Which of these is a noun?",
                        OptionA = "Run",
                        OptionB = "Happy",
                        OptionC = "London",
                        OptionD = "Quickly",
                        CorrectOption = "C",   // London is a noun
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What punctuation ends a question?",
                        OptionA = "Full stop (.)",
                        OptionB = "Question mark (?)",
                        OptionC = "Comma (,)",
                        OptionD = "Exclamation mark (!)",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Which sentence is written in past tense?",
                        OptionA = "She runs to school.",
                        OptionB = "She will run to school.",
                        OptionC = "She ran to school.",
                        OptionD = "She is running to school.",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What does an adjective describe?",
                        OptionA = "A verb",
                        OptionB = "A noun",
                        OptionC = "An adverb",
                        OptionD = "A sentence",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Choose the correct spelling:",
                        OptionA = "Recieve",
                        OptionB = "Receive",
                        OptionC = "Recive",
                        OptionD = "Receeve",
                        CorrectOption = "B",
                        Points        = 10
                    }
                }
            };

            
            //  SUBJECT 2: GEOGRAPHY
          
            var geogQuiz = new Quiz
            {
                Title = "Geography: World Capitals",
                Category = "Geography",
                Difficulty = "Medium",
                IsAIGenerated = false,
                IsPremium = false,
                TimeLimitSeconds = 120,
                Questions = new List<Question>
                {
                    new Question {
                        QuestionText  = "What is the capital city of France?",
                        OptionA = "Lyon",
                        OptionB = "Marseille",
                        OptionC = "Paris",
                        OptionD = "Nice",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Which is the longest river in the world?",
                        OptionA = "Amazon",
                        OptionB = "Nile",
                        OptionC = "Mississippi",
                        OptionD = "Yangtze",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Which continent is the Sahara Desert in?",
                        OptionA = "Asia",
                        OptionB = "South America",
                        OptionC = "Australia",
                        OptionD = "Africa",
                        CorrectOption = "D",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What is the capital of Japan?",
                        OptionA = "Beijing",
                        OptionB = "Seoul",
                        OptionC = "Tokyo",
                        OptionD = "Osaka",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "How many continents are there on Earth?",
                        OptionA = "5",
                        OptionB = "6",
                        OptionC = "7",
                        OptionD = "8",
                        CorrectOption = "C",
                        Points        = 10
                    }
                }
            };

            //  SUBJECT 3: MATHS
           
            var mathsQuiz = new Quiz
            {
                Title = "Maths: Number Skills",
                Category = "Maths",
                Difficulty = "Easy",
                IsAIGenerated = false,
                IsPremium = false,
                TimeLimitSeconds = 90,   // 90 seconds for maths
                Questions = new List<Question>
                {
                    new Question {
                        QuestionText  = "What is 12 x 8?",
                        OptionA = "86",
                        OptionB = "96",
                        OptionC = "106",
                        OptionD = "76",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What is 25% of 200?",
                        OptionA = "25",
                        OptionB = "75",
                        OptionC = "50",
                        OptionD = "100",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What is the value of pi (to 2 decimal places)?",
                        OptionA = "3.12",
                        OptionB = "3.41",
                        OptionC = "3.14",
                        OptionD = "3.16",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Simplify the fraction 18/24:",
                        OptionA = "2/3",
                        OptionB = "3/4",
                        OptionC = "4/5",
                        OptionD = "5/6",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What is the square root of 144?",
                        OptionA = "11",
                        OptionB = "13",
                        OptionC = "12",
                        OptionD = "14",
                        CorrectOption = "C",
                        Points        = 10
                    }
                }
            };

            
            //  SUBJECT 4: SCIENCE
           
            var scienceQuiz = new Quiz
            {
                Title = "Science: Biology Basics",
                Category = "Science",
                Difficulty = "Medium",
                IsAIGenerated = false,
                IsPremium = false,
                TimeLimitSeconds = 120,
                Questions = new List<Question>
                {
                    new Question {
                        QuestionText  = "What is the powerhouse of the cell?",
                        OptionA = "Nucleus",
                        OptionB = "Ribosome",
                        OptionC = "Mitochondria",
                        OptionD = "Cell wall",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What gas do plants produce during photosynthesis?",
                        OptionA = "Carbon dioxide",
                        OptionB = "Nitrogen",
                        OptionC = "Hydrogen",
                        OptionD = "Oxygen",
                        CorrectOption = "D",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "What is the chemical symbol for water?",
                        OptionA = "O2",
                        OptionB = "HO",
                        OptionC = "H2O",
                        OptionD = "H2O2",
                        CorrectOption = "C",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "How many bones are in the adult human body?",
                        OptionA = "196",
                        OptionB = "206",
                        OptionC = "216",
                        OptionD = "226",
                        CorrectOption = "B",
                        Points        = 10
                    },
                    new Question {
                        QuestionText  = "Which planet is known as the Red Planet?",
                        OptionA = "Venus",
                        OptionB = "Jupiter",
                        OptionC = "Saturn",
                        OptionD = "Mars",
                        CorrectOption = "D",
                        Points        = 10
                    }
                }
            };

            // Save all 4 quizzes to the database in one go
            db.Quizzes.AddRange(englishQuiz, geogQuiz, mathsQuiz, scienceQuiz);
            db.SaveChanges();
            // SaveChanges() actually writes to the .db file — without this, nothing is saved!
        }
    }
}
