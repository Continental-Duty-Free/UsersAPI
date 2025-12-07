using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersAPI.Domain.Entitys
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Email should be between 8 and 60 characters long")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public Person? Person { get; set; }
    }
}
