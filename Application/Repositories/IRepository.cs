namespace QuestionaryApp.Application.Repositories
{
    public interface IRepository<T>
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(Guid id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
        Task<bool> Exists(Guid id);
    }
}