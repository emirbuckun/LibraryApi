namespace LibraryApp.Api.Entity
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int? StudentId { get; set; }

        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
        public Publisher? Publisher { get; set; }
        public Student? Student { get; set; }

        public Book()
        {
            Title = string.Empty;
        }
    }
}
