using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Core.Entity.Order_Aggregatoin
{
    public class DeliveryMethod:BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string discription, string delivaryTime, decimal cost)
        {
            ShortName = shortName;
            Description = discription;
            DeliveryTime = delivaryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
