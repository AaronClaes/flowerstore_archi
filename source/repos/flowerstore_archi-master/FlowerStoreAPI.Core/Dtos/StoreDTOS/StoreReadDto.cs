using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.StoreDTOS
{
    public class StoreReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adres { get; set; }

        public string Region { get; set; }
    }
}