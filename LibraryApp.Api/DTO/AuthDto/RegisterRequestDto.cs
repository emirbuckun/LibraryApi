﻿namespace LibraryApp.Api.DTO.AuthDto
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegisterRequestDto(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
