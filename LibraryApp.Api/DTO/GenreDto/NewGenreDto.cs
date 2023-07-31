using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.GenreDto
{
    public class NewGenreDto
    {
        public string Name { get; set; }

        public NewGenreDto()
        {
            Name = string.Empty;
        }

        public Genre ToGenre()
        {
            return new Genre()
            {
                Id = 0,
                Name = this.Name,
            };
        }
    }
}
