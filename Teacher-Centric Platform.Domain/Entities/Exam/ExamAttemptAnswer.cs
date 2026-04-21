using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    public class ExamAttemptAnswer : BaseAuditableEntity
    {
        public Guid ExamAttemptId { get; private set; }

        public ExamAttempt ExamAttempt { get; private set; } = null!;
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;
        public Guid? SelectedOptionId { get; private set; }
        public QuestionOption? SelectedOption { get; private set; }

        public string? TextAnswer { get; private set; }
        public ExamAttemptAnswer()
        {
           
        }

    }
}
