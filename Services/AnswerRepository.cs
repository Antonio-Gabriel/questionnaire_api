using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class AnswerRepository : IAnswerRepository
    {
        private DataContext _context;
        public AnswerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Answer entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Answer entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Answers.Where(x => x.Id == id).AnyAsync();
        }

        public async Task<Answer> Get(Guid id)
        {
            return await _context.Answers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Answer>> GetAll()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<bool> GetByTitle(string title)
        {
            return await _context.Answers.Where(x => x.Text.Trim().ToLower() == title)
                .AnyAsync();
        }

        public async Task<int> GetTotalAnswersByQuestion(Guid questionId)
        {
            return await _context.Answers.Where(x => x.Question.Id == questionId).CountAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Answer entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}