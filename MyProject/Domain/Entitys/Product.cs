using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersAPI.Domain.Entitys
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Customer> CustomersWhoFavourited { get; set; }

        [Required, StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public float UnitPrice { get; set; }

        public bool Discontinued { get; set; }

        [Required, StringLength(20, MinimumLength = 5)]
        public string Brand { get; set; }
    }
}
