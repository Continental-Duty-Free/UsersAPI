using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.Entitys
{
    public class Employee : User
    {
        public double Salary { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly HireDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly? TerminationDate { get; set; }
    }
}
