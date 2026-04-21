using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Assignment
{
    public class AssignmentSubmission : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid AssignmentId { get; private set; }

        public Assignment assignment { get; private set; } = null!;
        public DateTime SubmittedAt { get; private set; } = DateTime.UtcNow;
        public double Score { get; private set; }
        public SubmissionStatus Status { get; private set; }
        // For simplicity, we store answers as a JSON string. In a real app, this might be a separate table.
        public ICollection<SubmissionAnswer> Answers { get; private set; } = new List<SubmissionAnswer>();

        public void Grade(double score)
        {
            Score = score;
            Status = score >= 50 ? SubmissionStatus.Passed : SubmissionStatus.Failed; // Example logic
        }
    }




}
