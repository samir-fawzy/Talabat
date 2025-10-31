using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatProject.Core.Entity
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string CustomerIntentId { get; set; }
        public string ShippingCost { get; set; }
        public string ClientSecret { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
