using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    // Domain/Entities/ExamAttempt.cs
    public class ExamAttempt : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid AssignmentId { get; private set; }

        public Domain.Entities.Assignment.Assignment Assignment { get; private set; } = null!;
        public DateTime StartTime { get; private set; } = DateTime.Now;
        public DateTime? EndTime { get; private set; }
        public double? FinalScore { get; private set; }
        public int AttemptNumber { get; private set; }

        public ICollection<ExamAttemptAnswer> Answers { get; private set; } = new List<ExamAttemptAnswer>();


        public void Submit(double finalScore)
        {
            if (EndTime.HasValue) throw new DomainException("Attempt already submitted.");
            EndTime = DateTime.UtcNow;
            FinalScore = finalScore;
        }
    }
}
