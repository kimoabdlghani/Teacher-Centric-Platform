using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    public partial class Question : BaseAuditableEntity
    {
        public string Text { get; private set; }
        public QuestionType Type { get; private set; }
        public string CorrectAnswer { get; private set; } // For auto-grading
        public Guid? AssignmentId { get; private set; }

        public Domain.Entities.Assignment.Assignment? Assignment { get; private set; } 
        public decimal MaxScore { get; private set; } // For storing the score 
        public Guid? ExamId { get; private set; }

        public Exam? Exam { get; private set; }
        public ICollection<QuestionOption> Options { get; private set; } = new List<QuestionOption>();

        private Question() { }

        public Question(string text, QuestionType type, decimal maxScore, string correctAnswer, Guid assignmentId)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new DomainException("Question text is required.");

            if (maxScore <= 0)
                throw new DomainException("Question marks must be greater than zero.");

            Text = text;
            Type = type;
            MaxScore = maxScore;
            CorrectAnswer = correctAnswer;
            AssignmentId = assignmentId;
        }

    }
}
