using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayStack.Net;


namespace Online_Marketplace.BLL.Interface.IMarketServices
{
    public interface IPaymentService
    {
        public Task<bool> VerifyPaymentAndUpdateOrderStatus(string referenceCode);

    }
}
