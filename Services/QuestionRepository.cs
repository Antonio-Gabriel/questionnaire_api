using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private DataContext _context;
        public QuestionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Question entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Question entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Questions.AnyAsync(x => x.Id == id);
        }

        public async Task<Question> Get(Guid id)
        {
            return await _context.Questions.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Question>> GetAll()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<ICollection<Answer>> GetAnswersOfQuestion(Guid questionId)
        {
            return await _context.Answers.Where(x => x.Question.Id == questionId).ToListAsync();
        }

        public async Task<Question> GetQuestionByTitle(string title)
        {
            return await _context.Questions.Where(
                x => x.Title.Trim().ToLower() == title.Trim().ToLower()
                ).FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalQuestionsByQuestionnaire(Guid questionnaireId)
        {
            return await _context.Questions.Where(x => x.Questionnaire.Id == questionnaireId)
                .CountAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Question entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}