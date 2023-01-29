using WebApi.Entity;

namespace WebApiAPI
{
    public class Seed
    {
        private readonly DataContext _context;
        private string[] lines = System.IO.File.ReadAllLines(@"Resourses/Geners.txt");

        public Seed(DataContext context)
        {
            _context = context;
            SeedDataContext();
        }

        public void SeedDataContext()
        {
            if (!_context.GenrecsTable.Any())
            {
                List<Genre> genre = new List<Genre>();
                foreach (var line in lines)
                {
                    genre.Add(new Genre() { NameGenre = $"{line}" });
                }
                _context.GenrecsTable.AddRange(genre);
                _context.SaveChanges();
            }
        }
    }
}
