using BEFE_B.Models;
using Microsoft.EntityFrameworkCore;

namespace BEFE_B.Data
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(t => t.Author)
                .WithMany(t => t.Books)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
