using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(User entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(User entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<User> Get(Guid id)
        {
            return await _context.Users.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Score> GetUserScore(Guid userId)
        {
            return await _context.Users.Where(x => x.Id == userId).Select(x => x.Score)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(User entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}