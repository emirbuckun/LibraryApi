using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.BookDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public BookController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("byName")]
        public async Task<IActionResult> GetBooksByName(string bookName)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Where(x => x.Title.Contains(bookName)).ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("byAuthor")]
        public async Task<IActionResult> GetBooksByAuthor(int id)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Where(x => x.AuthorId == id).ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("byPublisher")]
        public async Task<IActionResult> GetBooksByPublisher(int id)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Where(x => x.PublisherId == id).ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("byGenre")]
        public async Task<IActionResult> GetBooksByGenre(int id)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Where(x => x.GenreId == id).ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("byStudent")]
        public async Task<IActionResult> GetBooksByStudent(int id)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Where(x => x.StudentId == id).ToListAsync();

            if (result == null)
                return NotFound();

            var bookDetailList = new List<BookDetailDto>();
            result.ForEach(book => bookDetailList.Add(new BookDetailDto(book)));
            return Ok(bookDetailList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                SingleOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(new BookDetailDto(result));

            return NotFound();
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewBookDto request)
        {
            var item = _libraryContext.Books.Add(request.ToBook());
            await _libraryContext.SaveChangesAsync();
            var newEntity = _libraryContext.Books.
                Include(book => book.Author).
                Include(book => book.Genre).
                Include(book => book.Publisher).
                Single(x => x.Id == item.Entity.Id);
            return Ok(new BookDetailDto(newEntity));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditBookDto request)
        {
            var item = await _libraryContext.Books.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (item != null)
            {
                item.Title = request.Title;
                item.PublishDate = request.PublishDate;
                var result = await _libraryContext.SaveChangesAsync();
                return Ok(new BookDetailDto(item));
            }
            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _libraryContext.Books.SingleOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _libraryContext.Remove(item);
                await _libraryContext.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}
