using BEFE_B.Models;
using Microsoft.EntityFrameworkCore;

namespace BEFE_B.Data
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }
    }
}
