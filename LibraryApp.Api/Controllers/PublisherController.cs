using LibraryApp.Api.Auth;
using LibraryApp.Api.DatabaseContext;
using LibraryApp.Api.DTO.PublisherDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public PublisherController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryContext.Publishers.ToListAsync();

            if (result == null)
                return NotFound();

            var publisherInfoList = new List<PublisherInfoDto>();
            result.ForEach(publisher => publisherInfoList.Add(new PublisherInfoDto(publisher)));

            return Ok(publisherInfoList);
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _libraryContext.Publishers.SingleOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(new PublisherInfoDto(result));

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewPublisherDto request)
        {
            var item = _libraryContext.Publishers.Add(request.ToPublisher());
            await _libraryContext.SaveChangesAsync();
            return Ok(new PublisherInfoDto(item.Entity));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditPublisherDto request)
        {
            var item = await _libraryContext.Publishers.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (item != null)
            {
                item.Name = request.Name;
                var result = await _libraryContext.SaveChangesAsync();
                return Ok(new PublisherInfoDto(item));
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _libraryContext.Publishers.SingleOrDefaultAsync(x => x.Id == id);

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
