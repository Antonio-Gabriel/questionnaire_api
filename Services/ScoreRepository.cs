using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class ScoreRepository : IScoreRepository
    {
        private DataContext _context;
        public ScoreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Score entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Score entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Scores.AnyAsync(x => x.Id == id);
        }

        public async Task<Score> Get(Guid id)
        {
            return await _context.Scores.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Score>> GetAll()
        {
            return await _context.Scores.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Score entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}