namespace QuestionaryApp.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<Score> GetUserScore(Guid userId);
    }
}