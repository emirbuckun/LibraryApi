using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.GenreDto
{
    public class GenreInfoDto : EditGenreDto
    {
        public GenreInfoDto(Genre genre)
        {
            Id = genre.Id;
            Name = genre.Name;
        }
    }
}
