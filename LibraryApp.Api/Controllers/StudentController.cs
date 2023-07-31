using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public StudentController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryContext.Students.ToListAsync();

            if (result == null)
                return NotFound();

            var studentInfoList = new List<StudentInfoDto>();
            result.ForEach(student => studentInfoList.Add(new StudentInfoDto(student)));

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _libraryContext.Students.SingleOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(new StudentInfoDto(result));

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("getBorrower")]
        public async Task<IActionResult> GetStudentByBookId(int bookId)
        {
            var result = await _libraryContext.Books.Include(book => book.Student).SingleOrDefaultAsync(x => x.Id == bookId);

            if (result == null)
                return NotFound();

            return Ok(new StudentInfoDto(result.Student));
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] NewStudentDto request)
        //{
        //    var item = _libraryContext.Students.Add(request.ToStudent());
        //    await _libraryContext.SaveChangesAsync();

        //    return Ok(new StudentInfoDto(item.Entity));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] EditStudentDto request)
        //{
        //    var item = await _libraryContext.Students.SingleOrDefaultAsync(x => x.Id == request.Id);

        //    if (item != null)
        //    {
        //        item.Name = request.Name;
        //        var result = await _libraryContext.SaveChangesAsync();
        //        return Ok(new StudentInfoDto(item));
        //    }

        //    return NotFound();
        //}

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _libraryContext.Students.SingleOrDefaultAsync(x => x.Id == id);

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
