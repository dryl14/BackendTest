using Microsoft.EntityFrameworkCore;
using E_commerce_Product_Management.Data;
using E_commerce_Product_Management.DTOs;
using E_commerce_Product_Management.Models;

namespace E_commerce_Product_Management.Services
{
    public class LinqService(ECommerceContext context) : ILinqService
    {
        private readonly ECommerceContext _context = context;

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryIds = p.Categories.Select(c => c.Id).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersLastMonthAsync()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            return await _context.Orders
                .Where(o => o.OrderDate >= lastMonth)
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

        public async Task<IEnumerable<ProductSalesDto>> GetTotalSalesByProductAsync()
        {
            return await _context.Products
                .GroupJoin(
                    _context.OrderItems,
                    p => p.Id,
                    oi => oi.ProductId,
                    (p, orderItems) => new { Product = p, OrderItems = orderItems }
                )
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Product.Id,
                    ProductName = g.Product.Name,
                    TotalSales = g.OrderItems.Sum(oi => (decimal?)oi.Quantity * oi.UnitPrice) ?? 0
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ProductSalesDto>> GetTop5ProductsAsync()
        {
            return await _context.Products
                .GroupJoin(
                    _context.OrderItems,
                    p => p.Id,
                    oi => oi.ProductId,
                    (p, orderItems) => new { Product = p, OrderItems = orderItems }
                )
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Product.Id,
                    ProductName = g.Product.Name,
                    TotalSales = g.OrderItems.Sum(oi => (decimal?)oi.Quantity * oi.UnitPrice) ?? 0
                })
                .OrderByDescending(ps => ps.TotalSales)
                .Take(5)
                .ToListAsync();
        }

    }

    public class ProductSalesDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalSales { get; set; }
    }
}