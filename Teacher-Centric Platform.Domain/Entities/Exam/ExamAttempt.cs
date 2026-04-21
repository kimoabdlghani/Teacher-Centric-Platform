using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    // Domain/Entities/ExamAttempt.cs
    public class ExamAttempt : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid ExamId { get; private set; }

        public Exam Exam { get; private set; } = null!;
        public DateTime StartedAt { get; private set; } 
        public DateTime? SubmittedAt { get; private set; }
        public decimal? FinalScore { get; private set; }
        public int AttemptNumber { get; private set; }

        public ICollection<ExamAttemptAnswer> Answers { get; private set; } = new List<ExamAttemptAnswer>();

        private ExamAttempt() { }

        public void Start(DateTime now)
        {
            if (!Exam.CanStart(now))
                throw new Exception("Exam not available");

            StartedAt = now;
        }
        public void Submit(DateTime now, decimal score)
        {
            if (SubmittedAt.HasValue)
                throw new Exception("Already submitted");

            if (!Exam.IsWithinDuration(StartedAt, now))
                throw new Exception("Time expired");

            SubmittedAt = now;
            FinalScore = score;
        }

        public bool IsPassed()
        {
            if (!FinalScore.HasValue)
                throw new Exception("Not graded yet");

            return Exam.IsPassed(FinalScore.Value);
        }
    }
}
