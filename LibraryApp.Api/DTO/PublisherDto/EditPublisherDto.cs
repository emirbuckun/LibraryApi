using LibraryApp.Api.Entity;
using BDTO = LibraryApp.Api.DTO.BaseDto;

namespace LibraryApp.Api.DTO.PublisherDto
{
    public class EditPublisherDto : BDTO.BaseDto
    {
        public string Name { get; set; }

        public EditPublisherDto()
        {
            Name = string.Empty;
        }

        public Publisher ToPublisher()
        {
            return new Publisher()
            {
                Id = this.Id,
                Name = Name,
            };
        }
    }
}
