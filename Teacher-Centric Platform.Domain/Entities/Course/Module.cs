using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.Course
{
    public class Module : BaseAuditableEntity
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid CourseId { get; private set; }

        public Course Course { get; private set; }= null!;

        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();

        // Required for EF Core
        private Module() { }

        public Module(string name, string? description, int orderIndex, Guid courseId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Module name cannot be empty.");

            Name = name;
            OrderIndex = orderIndex;
            CourseId = courseId;
            Description = description;
        }

        public void AddLesson(Lesson lesson)
        {
            if (Lessons.Any(l => l.OrderIndex == lesson.OrderIndex))
                throw new DomainException("Lesson order index must be unique within the module.");

            Lessons.Add(lesson);
        }
    }
}
