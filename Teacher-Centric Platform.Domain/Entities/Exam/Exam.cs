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
    }
}
