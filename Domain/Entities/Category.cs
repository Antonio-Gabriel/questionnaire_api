namespace QuestionaryApp.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }      
        public Questionnaire Questionnaire { get; set; }
    }
}