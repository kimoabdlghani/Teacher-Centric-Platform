using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities
{
    public class Question : BaseAuditableEntity
    {
        public string Text { get; private set; }
        public QuestionType Type { get; private set; }
        public double Marks { get; private set; }
        public string CorrectAnswer { get; private set; } // For auto-grading
        public Guid AssignmentId { get; private set; }

        private Question() { }

        public Question(string text, QuestionType type, double marks, string correctAnswer, Guid assignmentId)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException("Question text is required.");

            if (marks <= 0)
                throw new DomainException("Question marks must be greater than zero.");

            Text = text;
            Type = type;
            Marks = marks;
            CorrectAnswer = correctAnswer;
            AssignmentId = assignmentId;
        }

        public void UpdateQuestion(string text, double marks, string correctAnswer)
        {
            // Logic: You can't update a question if students already started the exam
            // This check would usually happen in the Application Layer or by passing a flag
            Text = text;
            Marks = marks;
            CorrectAnswer = correctAnswer;
        }
    }
}
