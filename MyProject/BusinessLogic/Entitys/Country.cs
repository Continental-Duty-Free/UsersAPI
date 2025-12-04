using System.ComponentModel.DataAnnotations;

namespace MyProject.BusinessLogic.Entitys
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
