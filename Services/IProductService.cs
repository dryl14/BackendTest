using E_commerce_Product_Management.DTOs;

namespace E_commerce_Product_Management.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}