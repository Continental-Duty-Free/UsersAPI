using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using UsersAPI.Domain.EntitysExceptions;

namespace UsersAPI.Domain.Entitys
{
    public class Person
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 40 characters long")]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Lastname should be between 3 and 40 characters long")]
        public string LastName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Password should between 8 and 60 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Person(string name,string lastName, string password)
        {
            Name = name;
            LastName = lastName;
            Password = password;
            Validate();
        }

        public void Validate()
        {
            ValidateFullName();
            ValidatePassword();
        }

        public void ValidateFullName()
        {
            if (string.IsNullOrEmpty(Name))
                throw new UserException("Name cant be null");
            if (Name.Length < 3 || Name.Length > 40)
                throw new UserException("Name should be between 3 and 40 characters long");

            if (string.IsNullOrEmpty(LastName))
                throw new UserException("LastName cant be null");
            if(LastName.Length < 3 || LastName.Length > 40)
                throw new UserException("LastName cant be null");
        }

        public void ValidatePassword()
        {
            if (string.IsNullOrEmpty(Password))
                throw new UserException("Password needs to have a value");

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,60}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(Password))
                throw new UserException("Password should contain At least one lower case letter");
            if (!hasUpperChar.IsMatch(Password))
                throw new UserException("Password should contain At least one upper case letter");
            if (!hasMiniMaxChars.IsMatch(Password))
                throw new UserException("Password should not be less than or greater than 12 characters");
            if (!hasNumber.IsMatch(Password))
                throw new UserException("Password should contain At least one numeric value");
            if (!hasSymbols.IsMatch(Password))
                throw new UserException("Password should contain At least one special case characters");
        }
    }
}
