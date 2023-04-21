namespace QuestionaryApp.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> CategoryAlreadyExists(string name);
    }
}