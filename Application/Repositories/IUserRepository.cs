namespace QuestionaryApp.Application.Repositories
{
    public interface IUserRepository : IRepository<Score>
    {
        Task<User> GetUserByEmail(string email);
        Task<Score> GetUserScore(Guid userId);
    }
}