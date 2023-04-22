namespace QuestionaryApp.Application.Dtos.Response
{
    public class QuestionnaireResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}