namespace QuestionaryApp.Gateway.Models
{
    public class QuestionModel
    {
        public Guid id { get; set; }
        public string title { get; set; } = String.Empty;
        public DateTime created_at { get; set; } 
    }
}