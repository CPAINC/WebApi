using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApiAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Autor> AutorsTable { get; set; }

        public DbSet<Book> BooksTable { get; set; }

        public DbSet<IAutor> IAutorsTable { get; set; }

        public DbSet<IBook> IBooksTable { get; set; }

        public DbSet<Genre> GenrecsTable { get; set; }

    }
}
