using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Course;

namespace Teacher_Centric_Platform.Domain.Entities.QA
{
    public class QAThread : BaseAuditableEntity
    {
        

        public Guid LessonId { get; private set; }

        public Lesson Lesson { get; private set; } = null!;

        public Guid AuthorId { get; private set; }

        public string Title { get; private set; }


        public  ICollection<QAPost> Posts {  get; private set; } = new List<QAPost>();


        private QAThread() { }

        public QAThread(Guid lessonId, Guid authorId, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title required");

            LessonId = lessonId;
            AuthorId = authorId;
            Title = title;
            CreatedAt = DateTime.UtcNow;
        }

        // 👇 إضافة رد جديد
        public QAPost AddPost(Guid authorId, string content, Guid? parentPostId = null)
        {
            var post = new QAPost(Id, authorId, content, parentPostId);

            // Validation: parent لازم يكون موجود
            if (parentPostId != null && !Posts.Any(p => p.Id == parentPostId))
                throw new Exception("Parent post not found");

            Posts.Add(post);

            return post;
        }
    }
}
