using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.LibraryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public LibraryController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        [Route("borrowBook")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto request)
        {
            int studentId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var book = await _libraryContext.Books.SingleOrDefaultAsync(x => x.Id == request.BookId);

            if (book == null)
                return NotFound(new LibraryResponseDto(string.Format("Book with Id : {0} is not found", request.BookId)));

            if (book.StudentId.HasValue)
                return Ok(new LibraryResponseDto(string.Format("{0} is already been borrowed", book.Title)));

            var student = await _libraryContext.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
                return NotFound(new LibraryResponseDto(string.Format("Student with Id : {0} is not found", studentId)));

            book.StudentId = studentId;
            await _libraryContext.SaveChangesAsync();
            return Ok(new LibraryResponseDto(string.Format("{0} is reserved by {1}", book.Title, student.Name)));
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        [Route("returnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto request)
        {
            int studentId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var book = await _libraryContext.Books.SingleOrDefaultAsync(x => x.Id == request.BookId);

            if (book == null)
                return NotFound(new LibraryResponseDto(string.Format("Book with Id : {0} is not found", request.BookId)));

            if (!book.StudentId.HasValue)
                return Ok(new LibraryResponseDto(string.Format("{0} is not been borrowed", book.Title)));

            var student = await _libraryContext.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
                return NotFound(new LibraryResponseDto(string.Format("Student with Id : {0} is not found", studentId)));

            if (book.StudentId != studentId)
                return BadRequest(new LibraryResponseDto(string.Format("{0} has been borrowed by another student", book.Title)));

            book.StudentId = null;
            await _libraryContext.SaveChangesAsync();
            return Ok(new LibraryResponseDto(string.Format("{0} is returned by {1}", book.Title, student.Name)));
        }
    }
}
