namespace QuestionaryApp.Domain.Entities
{
    public class Questionnaire : BaseAuditableEntity
    {
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}