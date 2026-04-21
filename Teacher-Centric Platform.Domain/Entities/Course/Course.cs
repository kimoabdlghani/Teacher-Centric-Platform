using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Course
{
    // Domain/Entities/Course.cs
    public class Course : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string? ThumbnailUrl { get; private set; }
        public decimal Price { get; private set; }
        public CourseStatus Status { get; private set; }
 
        public  ICollection<Module> Modules { get; private set; } = new List<Module>();

        public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

        public ICollection<Domain.Entities.Exam.Exam> Exams { get; private set; } = new List<Domain.Entities.Exam.Exam>();

        // Constructor ensures the Entity is never in an invalid state
        public Course(string title, string description, string? thumbnailUrl, decimal price)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty");
            if (price < 0) throw new ArgumentException("Price cannot be negative");

            Title = title;
            Description = description;
            Price = price;
            Status = CourseStatus.Draft;
            ThumbnailUrl = thumbnailUrl;
        }

        public void Publish()
        {
            if (Modules.Count == 0) throw new InvalidOperationException("Cannot publish a course without modules");
            Status = CourseStatus.Published;
        }

        public void AddModule(string name, string? description, int orderIndex )
        {
            if (Modules.Any(m => m.OrderIndex == orderIndex))
                throw new InvalidOperationException("Module order must be unique");

            Modules.Add(new Module(name,description, orderIndex, Id));
        }
    }
}
