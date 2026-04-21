using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Answer;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    public class ExamAttemptAnswer : AnswerBase
    {
        public Guid ExamAttemptId { get; private set; }
        public ExamAttempt ExamAttempt { get; private set; } = null!;
        private ExamAttemptAnswer()
        {
           
        }

    }
}
