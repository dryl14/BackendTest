using E_commerce_Product_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_Product_Management.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = [];
        }
    }
}