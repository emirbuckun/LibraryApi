using LibraryApp.Api.Entity;
using BDTO = LibraryApp.Api.DTO.BaseDto;

namespace LibraryApp.Api.DTO.StudentDto
{
    public class EditStudentDto : BDTO.BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public EditStudentDto()
        {
            Name = string.Empty;
            Email = string.Empty;
        }

        public Student ToStudent()
        {
            return new Student()
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email
            };
        }
    }
}