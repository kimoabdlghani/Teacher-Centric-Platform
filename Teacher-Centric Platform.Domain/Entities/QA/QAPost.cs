using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.QA
{
    public class QAPost: BaseAuditableEntity
    {

        public Guid ThreadId { get; private set; }

        public QAThread Thread { get; private set; } = null!;

        public Guid AuthorId { get; private set; }

        public string Content { get; private set; }

        public Guid? ParentPostId { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private QAPost() { }

        internal QAPost(Guid threadId, Guid authorId, string content, Guid? parentPostId)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content required");

            ThreadId = threadId;
            AuthorId = authorId;
            Content = content;
            ParentPostId = parentPostId;
        }

        public void Edit(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Content required");

            Content = newContent;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
