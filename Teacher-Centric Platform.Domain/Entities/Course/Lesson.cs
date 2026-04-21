using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities.Course
{
    public class Lesson : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public LessonType Type { get; private set; }
        public string MediaUrl { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid? PrerequisiteId { get; private set; }
        public bool IsPreview { get; private set; } = false;

        public Guid ModuleId { get; private set; }

        public Module Module { get; private set; }= null!;

        public string? YoutubeVideoId { get; private set; } // For YouTube videos, we can store the video ID for easier embedding

        public string? SourceUrl { get; private set; } // For coding lessons, a link to PDF or similar

        public string? TextContent { get; private set; } // For text-based lessons

        public int? DurationInMinutes { get; private set; } // Optional duration for the lesson

        public ICollection<Domain.Entities.Assignment.Assignment> Assignments { get; private set; } = new List<Domain.Entities.Assignment.Assignment>();


        public Lesson(string title, LessonType type, string mediaUrl, int orderIndex, Guid? prerequisiteId = null)
        {
            Title = title;
            Type = type;
            MediaUrl = mediaUrl;
            OrderIndex = orderIndex;
            PrerequisiteId = prerequisiteId;
        }
    }
}
