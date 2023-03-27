using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IProductService
    {
        public Task <Product> CreateProduct(ProductCreateDto productdto);
    }
}
