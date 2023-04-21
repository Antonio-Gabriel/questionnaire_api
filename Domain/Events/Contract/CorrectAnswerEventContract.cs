using QuestionaryApp.Domain.Entities;

namespace QuestionaryApp.Domain.Events.Contract
{
    public struct CorrectAnswerEventContract
    {
        public Guid QuestionnaireId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public Score CurrentScore { get; set; }
    }
}