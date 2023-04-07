using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.BLL.Extension
{
   public class OrderReferenceGenerator
    {

        public static string GenerateOrderReference()
        {
            var date = DateTime.UtcNow;
            var reference = $"ORDER-{date.Year}-{date.Month}-{date.Day}-{Guid.NewGuid()}";
            return reference;
        }
    }
}
