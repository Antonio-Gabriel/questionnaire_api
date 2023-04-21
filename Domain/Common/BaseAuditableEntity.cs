namespace QuestionaryApp.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
    }
}