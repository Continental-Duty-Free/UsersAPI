using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.DTOs.Users
{
    public class LoginRequestDTO
    {
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
