using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TalabatProject.Core.Entity.Order_Aggregatoin
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Recive")]
        PaymentRecive,
        [EnumMember(Value = "Paument Faild")]
        PaumentFaild
    }
}
