using System.ComponentModel.DataAnnotations;
using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.DTOs.Users
{
    public class CreateUserDTO 
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
