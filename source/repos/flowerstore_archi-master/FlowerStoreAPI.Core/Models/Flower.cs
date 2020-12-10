using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Models
{
    public class Flower
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public double Price { get; set; }
    }
}