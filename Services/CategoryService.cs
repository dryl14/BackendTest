using Microsoft.EntityFrameworkCore;
using E_commerce_Product_Management.Data;
using E_commerce_Product_Management.DTOs;
using E_commerce_Product_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Product_Management.Services
{
    public class CategoryService(ECommerceContext context) : ICategoryService
    {
        private readonly ECommerceContext _context = context;

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            categoryDto.Id = category.Id;
            return categoryDto;
        }

        public async Task UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}