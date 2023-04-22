namespace QuestionaryApp.Application.Repositories
{
    public interface IQuestionnaireRepository : IRepository<Questionnaire>
    {
        Task<ICollection<Question>> GetQuestionnaireQuestions(Guid questionnaireId);
        Task<bool> GetByTitle(string title);
    }
}