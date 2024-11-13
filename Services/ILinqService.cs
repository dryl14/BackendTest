using E_commerce_Product_Management.DTOs;

namespace E_commerce_Product_Management.Services
{
    public interface ILinqService
    {
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<OrderDto>> GetOrdersLastMonthAsync();
        Task<IEnumerable<ProductSalesDto>> GetTotalSalesByProductAsync();
        Task<IEnumerable<ProductSalesDto>> GetTop5ProductsAsync();
    }
}