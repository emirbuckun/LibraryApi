namespace LibraryApp.Api.Entity
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Book>? Books { get; set; }

        public Student()
        {
            Name = string.Empty;
            Email = string.Empty;
            Books = new List<Book>();
        }
    }
}
