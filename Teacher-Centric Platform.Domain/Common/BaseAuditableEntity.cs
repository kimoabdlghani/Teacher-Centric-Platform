namespace Teacher_Centric_Platform.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string? CreatedBy { get; private set; }
        public DateTime? LastModifiedAt { get; private set; }
        public string? LastModifiedBy { get; private set; }

        public void UpdateAuditInfo(string? user)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = user;
        }
    }
}
