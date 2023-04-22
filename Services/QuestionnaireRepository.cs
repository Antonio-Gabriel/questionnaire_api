using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private DataContext _context;
        public QuestionnaireRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Questionnaire entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Questionnaire entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Questionnaires.AnyAsync(x => x.Id == id);
        }

        public async Task<Questionnaire> Get(Guid id)
        {
            return await _context.Questionnaires.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Questionnaire>> GetAll()
        {
            return await _context.Questionnaires.ToListAsync();
        }

        public async Task<bool> GetByTitle(string title)
        {
            return await _context.Questionnaires.Where(
                x => x.Title.Trim().ToLower() == title.Trim().ToLower()
                ).AnyAsync();
        }

        public async Task<ICollection<Question>> GetQuestionnaireQuestions(Guid questionnaireId)
        {
            return await _context.Questions
                .Where(x => x.Questionnaire.Id == questionnaireId).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Questionnaire entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}