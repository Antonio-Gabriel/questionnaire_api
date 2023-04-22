namespace QuestionaryApp.Application.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<Question> GetQuestionByTitle(string title);
        Task<ICollection<Answer>> GetAnswersOfQuestion(Guid questionId);
        Task<int> GetTotalQuestionsByQuestionnaire(Guid questionnaireId);
    }
}