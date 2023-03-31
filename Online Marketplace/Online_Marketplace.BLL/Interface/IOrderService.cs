using Online_Marketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.BLL.Interface
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrderHistoryAsync();

  
    }
}
