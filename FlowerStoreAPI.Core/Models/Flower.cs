using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Models
{
    // Includes all parameters that are available for the flower model.
    public class Flower
    {
        //tells the database that the Id is used as the primary key
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