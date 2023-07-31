using LibraryApp.Api.Entity;
using BDTO = LibraryApp.Api.DTO.BaseDto;

namespace LibraryApp.Api.DTO.AuthorDto
{
    public class EditAuthorDto : BDTO.BaseDto
    {
        public string Name { get; set; }

        public EditAuthorDto()
        {
            Name = string.Empty;
        }

        public Author ToAuthor()
        {
            return new Author()
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
