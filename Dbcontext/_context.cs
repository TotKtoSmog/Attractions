using Microsoft.EntityFrameworkCore;

namespace Attractions.Dbcontext
{
    public class _context : DbContext
    {
        public _context(DbContextOptions<_context> options)
        : base(options)
        {
        }
        _context()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

