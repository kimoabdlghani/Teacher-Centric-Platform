using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities
{
    // Domain/Entities/Course.cs
    public class Course : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public CourseStatus Status { get; private set; }
        public Guid TeacherId { get; private set; }

        private readonly List<Module> _modules = new();
        public IReadOnlyCollection<Module> Modules => _modules.AsReadOnly();

        // Constructor ensures the Entity is never in an invalid state
        public Course(string title, string description, decimal price, Guid teacherId)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty");
            if (price < 0) throw new ArgumentException("Price cannot be negative");

            Title = title;
            Description = description;
            Price = price;
            TeacherId = teacherId;
            Status = CourseStatus.Draft;
        }

        public void Publish()
        {
            if (_modules.Count == 0) throw new InvalidOperationException("Cannot publish a course without modules");
            Status = CourseStatus.Published;
        }

        public void AddModule(string name, int orderIndex)
        {
            if (_modules.Any(m => m.OrderIndex == orderIndex))
                throw new InvalidOperationException("Module order must be unique");

            _modules.Add(new Module(name, orderIndex, Id));
        }
    }
}
