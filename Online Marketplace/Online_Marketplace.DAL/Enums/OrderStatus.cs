using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.DAL.Enums
{
    public enum OrderStatus
    {
        PendingPayment,
        Paid,
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
