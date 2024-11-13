using E_commerce_Product_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_Product_Management.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}