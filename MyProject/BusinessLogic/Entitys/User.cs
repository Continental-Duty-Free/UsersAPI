using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.BusinessLogic.Entitys
{
    
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, Range(3, 30)]
        public string Username { get; set; }

        [Required, Range(3, 30)]
        public string Name { get; set; }

        [Required, Range(8, 30)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required, Range(2,15)]
        public string Phone { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
