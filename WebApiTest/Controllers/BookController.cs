using Microsoft.AspNetCore.Mvc;
using WebApi.Entity;

namespace WebApiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return Ok(await _context.BooksTable.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddHero(Book hero)
        {
            foreach (var a in hero.Autors)
            {
                if (!_context.AutorsTable.Any(x => x.Id == a.Id))
                {
                    Autor av = new Autor { Id = a.Id, DateOfBirth = a.DateOfBirth, Name = a.Name};
                    av.Books.Add(new IBook { Id = hero.Id, Name = hero.Name, DateOfPublication = hero.DateOfPublication, EditorialOffice = hero.EditorialOffice, Genre = hero.Genre });

                    _context.AutorsTable.Add(av);
                    await _context.SaveChangesAsync();
                }
                else if (_context.AutorsTable.Any(x => x.Id == a.Id))
                {
                    Autor av = _context.AutorsTable.Where(x => x.Id == a.Id).FirstOrDefault();
                    if (av.Books != null)
                    {
                        // av.Books.Add(new IBook { Id = hero.Id, Name = hero.Name, DateOfPublication = hero.DateOfPublication, EditorialOffice = hero.EditorialOffice, Genre = hero.Genre }); // Невозможно вставить явное значение для столбца идентификаторов в таблице "IAutor", когда параметр IDENTITY_INSERT имеет значение OFF.
                    }
                    await _context.SaveChangesAsync();
                }
            }

            _context.BooksTable.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.BooksTable.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateHero(Book request, int BookId)
        {
            var dbHero = await _context.BooksTable.FindAsync(BookId);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            dbHero.Name = request.Name;
            dbHero.DateOfPublication = request.DateOfPublication;
            dbHero.EditorialOffice = request.EditorialOffice;
            dbHero.Autors = request.Autors;
            dbHero.Genre = request.Genre;
            await _context.SaveChangesAsync();
            return Ok(await _context.BooksTable.ToListAsync());
        }

        [HttpDelete("{id}")] // Конфликт инструкции DELETE с ограничением REFERENCE "FK_IBooksTable_AutorsTable_AutorId". Конфликт произошел в базе данных "Test"
        public async Task<ActionResult<List<Book>>> Delete(int id)
        {
           
            var dbHero = await _context.BooksTable.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            _context.BooksTable.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.BooksTable.ToListAsync());
        }

    }
}
