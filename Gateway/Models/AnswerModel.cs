namespace QuestionaryApp.Gateway.Models
{
    public class AnswerModel
    {
        public Guid id { get; set; }
        public Guid question_id { get; set; }
        public string text { get; set; } = String.Empty;
        public bool is_correct { get; set; }
        public DateTime created_at { get; set; }
    }
}