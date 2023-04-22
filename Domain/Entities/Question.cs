namespace QuestionaryApp.Domain.Entities
{
    public class Question : BaseAuditableEntity
    {
        public string Title { get; set; }
        public Guid QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }       
        public ICollection<Answer> Answers { get; set; }
    }
}