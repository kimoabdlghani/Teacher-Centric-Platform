using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Assignment
{
    public class AssignmentSubmission : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid AssignmentId { get; private set; }

        public Assignment Assignment { get; private set; } = null!;

        public DateTime StartedAt { get; private set; }
        public DateTime? SubmittedAt { get; private set; }

        public decimal Score { get; private set; }
        public SubmissionStatus Status { get; private set; }

        public ICollection<SubmissionAnswer> Answers { get; private set; } = new List<SubmissionAnswer>();

        private AssignmentSubmission() { }

        public void Start()
        {
            StartedAt = DateTime.UtcNow;
            Status = SubmissionStatus.InProgress;
        }

        public void Submit(DateTime now)
        {
            if (!Assignment.IsWithinTime(StartedAt, now))
                throw new Exception("Submission time expired");

            SubmittedAt = now;
            Status = SubmissionStatus.Submitted;
        }

        public void ApplyGrading(decimal score)
        {
            Score = score;
            Status = Assignment.Evaluate(score);
        }
    }




}
