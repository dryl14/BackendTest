using Microsoft.EntityFrameworkCore;
using E_commerce_Product_Management.Data;
using E_commerce_Product_Management.DTOs;
using E_commerce_Product_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Product_Management.Services
{
    public class OrderService(ECommerceContext context) : IOrderService
    {
        private readonly ECommerceContext _context = context;

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                OrderDate = orderDto.OrderDate,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = _context.Products.Find(oi.ProductId).Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderDto.Id = order.Id;
            return orderDto;
        }

        public async Task UpdateOrderAsync(int id, OrderDto orderDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            order.CustomerName = orderDto.CustomerName;
            order.OrderDate = orderDto.OrderDate;

            // Remove existing order items
            _context.OrderItems.RemoveRange(order.OrderItems);

            // Add new order items
            order.OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = _context.Products.Find(oi.ProductId).Price
            }).ToList();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<OrderDto>> GetOrderAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}