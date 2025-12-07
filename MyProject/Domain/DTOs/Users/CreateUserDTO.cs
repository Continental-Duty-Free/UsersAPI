using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.DTOs.Users
{
    public class CreateUserDTO 
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
