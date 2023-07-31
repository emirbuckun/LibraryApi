using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.GenreDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public GenreController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryContext.Genres.ToListAsync();

            if (result == null)
                return NotFound();

            var genreInfoList = new List<GenreInfoDto>();
            result.ForEach(genre => genreInfoList.Add(new GenreInfoDto(genre)));

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _libraryContext.Genres.SingleOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(new GenreInfoDto(result));

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewGenreDto request)
        {
            var item = _libraryContext.Genres.Add(request.ToGenre());
            await _libraryContext.SaveChangesAsync();
            return Ok(new GenreInfoDto(item.Entity));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditGenreDto request)
        {
            var item = await _libraryContext.Genres.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (item != null)
            {
                item.Name = request.Name;
                var result = await _libraryContext.SaveChangesAsync();
                return Ok(new GenreInfoDto(item));
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _libraryContext.Genres.SingleOrDefaultAsync(x => x.Id == id);

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
