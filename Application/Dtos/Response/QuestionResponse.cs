namespace QuestionaryApp.Application.Dtos.Response
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid QuestionnaireId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}