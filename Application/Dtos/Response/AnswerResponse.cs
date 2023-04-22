namespace QuestionaryApp.Application.Dtos.Response
{
    public class AnswerResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool isCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}