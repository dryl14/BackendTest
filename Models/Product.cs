using E_commerce_Product_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_Product_Management.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Product()
        {
            Categories = [];
        }
    }
}