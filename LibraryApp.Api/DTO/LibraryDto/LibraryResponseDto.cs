namespace LibraryApp.Api.DTO.LibraryDto
{
    public class LibraryResponseDto
    {
        public LibraryResponseDto(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
