namespace Teacher_Centric_Platform.Domain.Entities
{
    public class Module : BaseAuditableEntity
    {
        public string Name { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid CourseId { get; private set; }

        private readonly List<Lesson> _lessons = new();
        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

        // Required for EF Core
        private Module() { }

        public Module(string name, int orderIndex, Guid courseId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Module name cannot be empty.");

            Name = name;
            OrderIndex = orderIndex;
            CourseId = courseId;
        }

        public void AddLesson(Lesson lesson)
        {
            if (_lessons.Any(l => l.OrderIndex == lesson.OrderIndex))
                throw new DomainException("Lesson order index must be unique within the module.");

            _lessons.Add(lesson);
        }
    }
}
