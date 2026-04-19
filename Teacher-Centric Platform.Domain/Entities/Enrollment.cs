using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities
{
    // Domain/Entities/Enrollment.cs
    public class Enrollment : BaseAuditableEntity
    {
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public EnrollmentStatus Status { get; private set; }
        public double ProgressPercentage { get; private set; }

        public Enrollment(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
            Status = EnrollmentStatus.Active;
            ProgressPercentage = 0;
        }

        public void UpdateProgress(int totalLessons, int completedLessons)
        {
            if (totalLessons <= 0) return;

            // Logical check: Progress = (Completed / Total) * 100
            ProgressPercentage = Math.Round(((double)completedLessons / totalLessons) * 100, 2);

            if (ProgressPercentage >= 100)
            {
                Status = EnrollmentStatus.Completed;
                // AddDomainEvent(new CourseCompletedEvent(this));
            }
        }
    }
}
