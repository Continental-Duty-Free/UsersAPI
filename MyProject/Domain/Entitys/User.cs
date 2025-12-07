using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersAPI.Domain.Entitys
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username should be between 2 and 20 characters long")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 30 characters long")]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 30 characters long")]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Email should be between 8 and 30 characters long")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
