namespace LibraryApp.Api.Entity
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Name = string.Empty;
            Books = new List<Book>();
        }
    }
}
