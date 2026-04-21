using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Course;
using Teacher_Centric_Platform.Domain.Entities.Exam;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Assignment
{
    // Domain/Entities/Assignment.cs
    public class Assignment : BaseAuditableEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public int PassingScore { get; private set; }
        public int DurationInMinutes { get; private set; }
        public int MaxAttempts { get; private set; }

        public AssignmentType Type { get; private set; }

        public Guid LessonId { get; private set; }
        public Lesson Lesson { get; private set; } = null!;

        public ICollection<Question> Questions { get; private set; } = new List<Question>();
        public ICollection<AssignmentSubmission> Submissions { get; private set; } = new List<AssignmentSubmission>();

        private Assignment() { }

        // 🧠 Business Rules
        public bool CanSubmit(int currentAttempts)
            => currentAttempts < MaxAttempts;

        public SubmissionStatus Evaluate(decimal score)
            => score >= PassingScore
                ? SubmissionStatus.Passed
                : SubmissionStatus.Failed;

        public bool IsWithinTime(DateTime startedAt, DateTime now)
            => (now - startedAt).TotalMinutes <= DurationInMinutes;
    }


}




