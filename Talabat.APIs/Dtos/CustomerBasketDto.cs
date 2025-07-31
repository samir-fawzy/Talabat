using System.ComponentModel.DataAnnotations;
using TalabatProject.Core.Entity;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto(string id)
        {
            Id = id;
        }
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
