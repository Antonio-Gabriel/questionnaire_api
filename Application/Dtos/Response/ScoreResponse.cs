namespace QuestionaryApp.Application.Dtos.Response
{
    public class ScoreResponse
    {
        public Guid Id { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}