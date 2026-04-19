using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities
{
    // Domain/Entities/Assignment.cs
    public class Assignment : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public int PassingScore { get; private set; }
        public int DurationInMinutes { get; private set; } // التحسين: وقت الامتحان
        public int MaxAttempts { get; private set; } // التحسين: عدد المحاولات
        public Guid LessonId { get; private set; }

        private readonly List<Question> _questions = new();
        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        public void AddQuestion(string text, QuestionType type, double marks, string correctAnswer)
        {
            _questions.Add(new Question(text, type, marks, correctAnswer, Id));
        }
    }
}
