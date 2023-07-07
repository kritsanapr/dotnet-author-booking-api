using Microsoft.EntityFrameworkCore;
using rest_api_template.Models;

namespace rest_api_template.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Author> Authors { get;}
        public DbSet<Book> Books { get;}

        protected override void OnModelCreating(ModelBuilder builder) {
             // Define relationship between books and authors
            builder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books);

            // Seed database with authors and books for demo
            new DbInitializer(builder).Seed();
        }

    }
}