using E_commerce_Product_Management.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Product_Management.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(int id, CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<ActionResult<CategoryDto>> GetCategoryAsync(int id);
    }
}