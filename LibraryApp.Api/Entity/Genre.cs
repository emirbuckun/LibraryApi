namespace LibraryApp.Api.Entity
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Name = string.Empty;
            Books = new List<Book>();
        }
    }
}
