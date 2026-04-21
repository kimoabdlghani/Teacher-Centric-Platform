using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Exam;
using Teacher_Centric_Platform.Domain.Entities.Answer;

namespace Teacher_Centric_Platform.Domain.Entities.Assignment
{
    public class SubmissionAnswer : AnswerBase
    {
        public Guid SubmissionId { get; private set; }
        public AssignmentSubmission Submission { get; private set; } = null!;

        public string? TeacherFeedback { get; private set; }

        private SubmissionAnswer() { }

        public void SetScore(decimal score)
        {
            Score = score;
        }

        public void AddFeedback(string feedback)
        {
            TeacherFeedback = feedback;
        }
    }



}
