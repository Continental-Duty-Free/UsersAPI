using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.DTOs.Customers
{
    public class CreateCustomerDTO
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }

        public string Email { get; set; }
    }
}
