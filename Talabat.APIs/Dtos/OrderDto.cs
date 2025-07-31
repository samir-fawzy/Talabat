using TalabatProject.Core.Entity.Order_Aggregatoin;

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; }

    }
}
