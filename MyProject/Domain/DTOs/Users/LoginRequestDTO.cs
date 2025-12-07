using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.DTOs.Users
{
    public class LoginRequestDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 30 characters long")]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Email should be between 8 and 30 characters long")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
