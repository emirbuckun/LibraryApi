using LibraryApp.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.DatabaseContext
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Student> Students { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> opt) : base(opt) { }
    }
}
