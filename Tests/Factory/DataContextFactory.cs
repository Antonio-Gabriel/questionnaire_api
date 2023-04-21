using Microsoft.EntityFrameworkCore;

namespace Tests.Factory
{
    public abstract class DataContextFactory
    {
        public static DataContext create()
        {
            DbContextOptions<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(Globals.MyConnection)
                .Options;

            DataContext _context = new DataContext(dbOptions);

            return _context;
        }
    }
}