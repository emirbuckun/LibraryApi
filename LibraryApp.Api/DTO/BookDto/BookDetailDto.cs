using LibraryApp.Api.Entity;
using BDTO = LibraryApp.Api.DTO.BaseDto;

namespace LibraryApp.Api.DTO.BookDto
{
    public class BookDetailDto : BDTO.BaseDto
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        //public BookStatus Status { get; set; }
        public bool IsAvailable { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

        public BookDetailDto(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            PublishDate = book.PublishDate;
            Author = book.Author.Name;
            Genre = book.Genre.Name;
            Publisher = book.Publisher.Name;
            IsAvailable = !book.StudentId.HasValue;
            //this.Status = !book.StudentId.HasValue ? BookStatus.Available : BookStatus.Borrowed;
        }
    }
}