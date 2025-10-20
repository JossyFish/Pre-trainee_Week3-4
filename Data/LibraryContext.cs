using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using WK_34.Entities;

namespace WK_34.Data
{
    public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
    {
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BookEntity> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
        }
    }
}
