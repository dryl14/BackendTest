using System;
using System.Collections.Generic;
using System.Linq;
using E_commerce_Product_Management.Data;
using E_commerce_Product_Management.Models;
using Microsoft.EntityFrameworkCore;

public class DataSeed
{
    private readonly ECommerceContext _context; // Replace with your actual DbContext

    public DataSeed(ECommerceContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Categories
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Name = "Clothing", Description = "Apparel and fashion items" },
                new Category { Name = "Books", Description = "Physical and digital books" },
                new Category { Name = "Home & Garden", Description = "Items for home improvement and gardening" },
                new Category { Name = "Sports & Outdoors", Description = "Sporting goods and outdoor equipment" }
            };
            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }

        // Products
        if (!_context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Smartphone", Description = "Latest model smartphone", Price = 699.99m, StockQuantity = 50 },
                new Product { Name = "T-shirt", Description = "Cotton crew neck t-shirt", Price = 19.99m, StockQuantity = 100 },
                new Product { Name = "Novel", Description = "Bestselling fiction novel", Price = 14.99m, StockQuantity = 75 },
                new Product { Name = "Garden Hose", Description = "50ft expandable garden hose", Price = 29.99m, StockQuantity = 30 },
                new Product { Name = "Tennis Racket", Description = "Professional grade tennis racket", Price = 89.99m, StockQuantity = 20 }
            };
            _context.Products.AddRange(products);
            _context.SaveChanges();

            // Assign categories to products
            var allCategories = _context.Categories.ToList();
            var allProducts = _context.Products.ToList();
            for (int i = 0; i < allProducts.Count; i++)
            {
                allProducts[i].Categories.Add(allCategories[i]);
            }
            _context.SaveChanges();
        }

        // Orders
        if (!_context.Orders.Any())
        {
            var orders = new List<Order>
            {
                new Order { CustomerName = "John Doe", OrderDate = DateTime.Now.AddDays(-5) },
                new Order { CustomerName = "Jane Smith", OrderDate = DateTime.Now.AddDays(-4) },
                new Order { CustomerName = "Bob Johnson", OrderDate = DateTime.Now.AddDays(-3) },
                new Order { CustomerName = "Alice Brown", OrderDate = DateTime.Now.AddDays(-2) },
                new Order { CustomerName = "Charlie Davis", OrderDate = DateTime.Now.AddDays(-1) }
            };
            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }

        // OrderItems
        if (!_context.OrderItems.Any())
        {
            var orders = _context.Orders.ToList();
            var products = _context.Products.ToList();
            var random = new Random();

            foreach (var order in orders)
            {
                var orderItems = new List<OrderItem>
                {
                    new OrderItem { Order = order, Product = products[random.Next(products.Count)], Quantity = random.Next(1, 5), UnitPrice = products[random.Next(products.Count)].Price }
                };
                _context.OrderItems.AddRange(orderItems);
            }
            _context.SaveChanges();
        }
    }
}