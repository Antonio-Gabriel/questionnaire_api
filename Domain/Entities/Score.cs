namespace QuestionaryApp.Domain.Entities
{
    public class Score : BaseAuditableEntity
    {
        public int Correct { get; set; } = 0;
        public int Wrong { get; set; } = 0;
        public Guid UserId { get; set; }
        public User User { get; set; }   
    }
}