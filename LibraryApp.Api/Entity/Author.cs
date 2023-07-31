namespace LibraryApp.Api.Entity
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book>? Books { get; set; }

        public Author() { Name = string.Empty; }
        public Author(string name, ICollection<Book>? books)
        {
            Name = name;
            Books = books;
        }
    }
}
