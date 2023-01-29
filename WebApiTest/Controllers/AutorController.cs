using Microsoft.AspNetCore.Mvc;
using WebApi.Entity;

namespace WebApiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly DataContext _context;

        public AutorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return Ok(await _context.AutorsTable.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Autor>>> AddHero(Autor hero)
        {
            foreach (var b in hero.Books)
            {
                if (!_context.BooksTable.Any(x => x.Id == b.Id))
                {

                    Book book = new Book { Id = b.Id, DateOfPublication = b.DateOfPublication, EditorialOffice = b.EditorialOffice, Genre = b.Genre, Name = b.Name};
                    book.Autors.Add(new IAutor { Id = hero.Id, Name = hero.Name, DateOfBirth = hero.DateOfBirth });

                    _context.BooksTable.Add(book);
                    await _context.SaveChangesAsync();
                }
                else if (_context.BooksTable.Any(x => x.Id == b.Id))
                {
                    Book book = _context.BooksTable.Where(x => x.Id == b.Id).FirstOrDefault();
                    if (book.Autors != null)
                    {
                        // book.Autors.Add(new IAutor { Id = hero.Id, Name = hero.Name, DateOfBirth = hero.DateOfBirth }); // Невозможно вставить явное значение для столбца идентификаторов в таблице "IAutor", когда параметр IDENTITY_INSERT имеет значение OFF.
                    }
                    await _context.SaveChangesAsync();
                }
            }

            _context.AutorsTable.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.AutorsTable.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Autor>>> UpdateHero(Autor request, int AutorId)
        {
            var dbHero = await _context.AutorsTable.FindAsync(AutorId);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            dbHero.Name = request.Name;
            dbHero.DateOfBirth = request.DateOfBirth;
            dbHero.Books = request.Books;
            await _context.SaveChangesAsync();
            return Ok(await _context.AutorsTable.ToListAsync());
        }

        [HttpDelete("{id}")] // Конфликт инструкции DELETE с ограничением REFERENCE "FK_IBooksTable_AutorsTable_AutorId". Конфликт произошел в базе данных "Test"
        public async Task<ActionResult<List<Autor>>> Delete(int id)
        {
            var dbHero = await _context.AutorsTable.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            _context.AutorsTable.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.AutorsTable.ToListAsync());
        }
    }
}

