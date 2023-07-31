using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.AuthorDto
{
    public class NewAuthorDto
    {
        public string Name { get; set; }

        public NewAuthorDto()
        {
            Name = string.Empty;
        }

        public Author ToAuthor()
        {
            return new Author()
            {
                Id = 0,
                Name = Name,
            };
        }
    }
}
