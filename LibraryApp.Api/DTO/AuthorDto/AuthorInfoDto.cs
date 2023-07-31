using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.AuthorDto
{
    public class AuthorInfoDto : EditAuthorDto
    {
        public AuthorInfoDto(Author author)
        {
            Id = author.Id;
            Name = author.Name;
        }
    }
}
