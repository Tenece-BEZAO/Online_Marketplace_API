using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IProductService
    {

        public Task<Product> CreateProduct(ProductCreateDto productDto);
        public Task<List<ProductCreateDto>> GetProducts(ProductSearchDto searchDto);
        public Task<List<ProductCreateDto>> ViewProducts();
        public Task<bool> AddToCartAsync(int productId, int quantity);

        public Task<string> UpdateProduct(int productId, ProductCreateDto productDto);
        public Task<string> DeleteProduct(int productId);

        public Task<List<ProductCreateDto>> GetSellerProducts();
    }
}
