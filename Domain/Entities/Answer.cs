namespace QuestionaryApp.Domain.Entities
{
    public class Answer : BaseAuditableEntity
    {
        public string Text { get; set; }
        public bool isCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}