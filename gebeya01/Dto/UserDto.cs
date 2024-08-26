// src/Dto/UserDto.cs
using System.ComponentModel.DataAnnotations;

namespace gebeya01.Dto
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }
    }
}
