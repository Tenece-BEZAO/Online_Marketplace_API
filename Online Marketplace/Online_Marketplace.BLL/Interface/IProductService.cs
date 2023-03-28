using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IProductService
    {

        public Task<Product> CreateProduct(ProductCreateDto productDto);
        public Task<List<ProductCreateDto>> GetProducts(ProductSearchDto searchDto);
        public Task<List<ProductCreateDto>> ViewProducts();
    }
}
