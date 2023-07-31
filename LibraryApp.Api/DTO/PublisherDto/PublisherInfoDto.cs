using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.PublisherDto
{
    public class PublisherInfoDto : EditPublisherDto
    {
        public PublisherInfoDto(Publisher publisher)
        {
            Id = publisher.Id;
            Name = publisher.Name;
        }
    }
}
