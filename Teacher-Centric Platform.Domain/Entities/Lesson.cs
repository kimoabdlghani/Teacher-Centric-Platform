using Teacher_Centric_Platform.Domain.Enums;

namespace Teacher_Centric_Platform.Domain.Entities
{
    public class Lesson : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public LessonType Type { get; private set; }
        public string MediaUrl { get; private set; }
        public int OrderIndex { get; private set; }
        public Guid? PrerequisiteId { get; private set; } // التحسين: الدرس السابق المطلوب
        public bool IsPreview { get; private set; } // التحسين: هل متاح للمعاينة مجاناً؟

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
