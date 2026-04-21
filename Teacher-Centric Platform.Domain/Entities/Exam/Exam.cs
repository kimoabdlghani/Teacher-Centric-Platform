using Teacher_Centric_Platform.Domain.Common;
namespace Teacher_Centric_Platform.Domain.Entities.Exam
{
    public class Exam : BaseAuditableEntity
    {
        
        public Guid CourseId { get; private set; }

        public Domain.Entities.Course.Course Course { get; private set; } = null!;

        public string Title { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public int DurationMinutes { get; private set; }

        public int MaxAttempts { get; private set; }

        public decimal PassingScore { get; private set; }

        public bool IsRandomized { get; private set; }

        public bool IsPublished { get; private set; }

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public ICollection<Question> Questions { get; private set; } = new List<Question>();

        public ICollection<ExamAttempt> Attempts { get; private set; } = new List<ExamAttempt>();

        private Exam()
        {
            
        }
        public bool CanStart(DateTime now)
        {
            if (!IsPublished) return false;
            if (StartDate.HasValue && now < StartDate) return false;
            if (EndDate.HasValue && now > EndDate) return false;
            return true;
        }

        public bool CanAttempt(int currentAttempts)
            => currentAttempts < MaxAttempts;

        public bool IsWithinDuration(DateTime startTime, DateTime now)
            => (now - startTime).TotalMinutes <= DurationMinutes;

        public bool IsPassed(decimal score)
            => score >= PassingScore;
    }
}
