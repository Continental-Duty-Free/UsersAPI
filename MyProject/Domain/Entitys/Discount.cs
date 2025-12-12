using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.Entitys
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Porcentage { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly ValidFrom { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly ValidUntil { get; set; }
    }
}
