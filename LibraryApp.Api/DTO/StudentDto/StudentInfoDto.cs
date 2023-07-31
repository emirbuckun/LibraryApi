using LibraryApp.Api.Entity;

namespace LibraryApp.Api.DTO.StudentDto
{
    public class StudentInfoDto : EditStudentDto
    {
        public StudentInfoDto(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Email = student.Email;
        }
    }
}
