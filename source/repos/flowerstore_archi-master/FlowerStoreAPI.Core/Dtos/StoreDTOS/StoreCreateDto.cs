using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.StoreDTOS
{
    public class StoreCreateDto
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Region { get; set; }
    }
}