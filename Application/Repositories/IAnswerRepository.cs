namespace QuestionaryApp.Application.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<bool> GetByTitle(string title);
        Task<int> GetTotalAnswersByQuestion(Guid questionId);
    }
}