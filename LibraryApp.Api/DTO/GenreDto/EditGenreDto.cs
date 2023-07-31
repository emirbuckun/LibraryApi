using LibraryApp.Api.Entity;
using BDTO = LibraryApp.Api.DTO.BaseDto;

namespace LibraryApp.Api.DTO.GenreDto
{
    public class EditGenreDto : BDTO.BaseDto
    {
        public string Name { get; set; }

        public EditGenreDto()
        {
            Name = string.Empty;
        }

        public Genre ToGenre()
        {
            return new Genre()
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
