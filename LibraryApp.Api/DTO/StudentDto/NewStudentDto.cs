using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.StudentDto
{
    public class NewStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public NewStudentDto()
        {
            Name = string.Empty;
            Email = string.Empty;
        }

        public Student ToStudent()
        {
            return new Student()
            {
                Id = 0,
                Name = this.Name,
                Email = this.Email
            };
        }
    }
}