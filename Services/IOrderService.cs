using E_commerce_Product_Management.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Product_Management.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task UpdateOrderAsync(int id, OrderDto orderDto);
        Task DeleteOrderAsync(int id);
        Task<ActionResult<OrderDto>> GetOrderAsync(int id);
    }
}