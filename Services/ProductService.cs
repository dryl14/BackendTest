using Microsoft.EntityFrameworkCore;
using E_commerce_Product_Management.Data;
using E_commerce_Product_Management.DTOs;
using E_commerce_Product_Management.Models;

namespace E_commerce_Product_Management.Services
{
    public class ProductService(ECommerceContext context) : IProductService
    {
        private readonly ECommerceContext _context = context;

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Categories)
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

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryIds = product.Categories.Select(c => c.Id).ToList()
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Categories = await _context.Categories
                    .Where(c => productDto.CategoryIds.Contains(c.Id))
                    .ToListAsync()
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDto.Id = product.Id;
            return productDto;
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;

            product.Categories.Clear();
            var categories = await _context.Categories
                .Where(c => productDto.CategoryIds.Contains(c.Id))
                .ToListAsync();
            product.Categories = categories;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}