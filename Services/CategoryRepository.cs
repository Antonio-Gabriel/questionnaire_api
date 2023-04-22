using Microsoft.EntityFrameworkCore;

namespace QuestionaryApp.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Category entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Category entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Update(Category entity)
        {
            _context.Update(entity);
            return await Save();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Categories
                .Where(x => x.Id == id)
                .AnyAsync();
        }

        public async Task<Category> Get(Guid id)
        {
            return await _context.Categories.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> CategoryAlreadyExists(string name)
        {
            return await _context.Categories
                .AnyAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}