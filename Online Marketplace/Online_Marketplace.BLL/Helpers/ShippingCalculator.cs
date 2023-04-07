using Online_Marketplace.DAL.Enums;

namespace Online_Marketplace.BLL.Helpers
{
    public class ShippingCalculator
    {
        public static async Task<(decimal shippingCost, DateTime estimatedDeliveryDate)> CalculateShippingCostAsync(ShippingMethod shippingMethod)
        {
            decimal shippingCost = 0;
            DateTime estimatedDeliveryDate = DateTime.Now;

            switch (shippingMethod)
            {
                case ShippingMethod.Express:
                    shippingCost = 1.5m;
                    estimatedDeliveryDate = DateTime.Now.AddDays(2);
                    break;
                case ShippingMethod.NextDay:
                    shippingCost += 2.0m;
                    estimatedDeliveryDate = DateTime.Now.AddDays(1);
                    break;
                default:
                    estimatedDeliveryDate = DateTime.Now.AddDays(5);
                    break;
            }

            return (shippingCost, estimatedDeliveryDate);
        }
    }

}
