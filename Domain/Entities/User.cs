namespace QuestionaryApp.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CodeName { get; set; }
        public string Password { get; set; }        
        public Score Score { get; set; }
    }
}