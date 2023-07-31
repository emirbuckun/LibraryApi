using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.AuthorDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public AuthorController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryContext.Authors.ToListAsync();

            if (result == null)
                return NotFound();

            var authorInfoList = new List<AuthorInfoDto>();
            result.ForEach(author => authorInfoList.Add(new AuthorInfoDto(author)));
            return Ok(authorInfoList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _libraryContext.Authors.Include(author => author.Books).SingleOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(new AuthorInfoDto(result));
            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewAuthorDto request)
        {
            var item = _libraryContext.Authors.Add(request.ToAuthor());
            await _libraryContext.SaveChangesAsync();
            return Ok(new AuthorInfoDto(item.Entity));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditAuthorDto request)
        {
            var item = await _libraryContext.Authors.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (item != null)
            {
                item.Name = request.Name;
                var result = await _libraryContext.SaveChangesAsync();
                return Ok(item);
            }
            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _libraryContext.Authors.SingleOrDefaultAsync(x => x.Id == id);
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
