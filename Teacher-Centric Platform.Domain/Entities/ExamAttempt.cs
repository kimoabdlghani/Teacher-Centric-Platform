namespace Teacher_Centric_Platform.Domain.Entities
{
    // Domain/Entities/ExamAttempt.cs
    public class ExamAttempt : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid AssignmentId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public double Score { get; private set; }
        public int AttemptNumber { get; private set; }

        public ExamAttempt(Guid studentId, Guid assignmentId, int attemptNumber)
        {
            StudentId = studentId;
            AssignmentId = assignmentId;
            AttemptNumber = attemptNumber;
            StartTime = DateTime.UtcNow;
        }

        public void Submit(double finalScore)
        {
            if (EndTime.HasValue) throw new DomainException("Attempt already submitted.");
            EndTime = DateTime.UtcNow;
            Score = finalScore;
        }
    }
}
