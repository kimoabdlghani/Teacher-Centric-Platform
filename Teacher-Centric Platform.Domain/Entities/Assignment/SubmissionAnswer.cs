using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Exam;

namespace Teacher_Centric_Platform.Domain.Entities.Assignment
{
    public class SubmissionAnswer : BaseAuditableEntity 
    {
        public Guid SubmissionId { get; private set; }

        public AssignmentSubmission Submission { get; private set; } = null!;
        public Guid QuestionId { get; private set; }

        public Question Question { get; private set; } = null!;

        public Guid? SelectedOptionId { get; private set; }

        public QuestionOption? SelectedOption { get; private set; }
        //        SelectedOptionId Guid?	for MCQ
        public string? TextAnswer { get; private set; }

        //TextAnswer  string?	for Written

        public bool? IsCorrect { get; private set; }


        //IsCorrect   bool? auto-set for MCQ

        public decimal? Score { get; private set; }
        //Score   decimal? auto for MCQ, teacher for Written

        public string? TeacherFeedback { get; private set; }
    }




}
