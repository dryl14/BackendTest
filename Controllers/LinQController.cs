using Microsoft.AspNetCore.Mvc;
using E_commerce_Product_Management.DTOs;
using E_commerce_Product_Management.Services;

namespace E_commerce_Product_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqQueriesController : ControllerBase
    {
        private readonly ILinqService _linqService;

        public LinqQueriesController(ILinqService reportService)
        {
            _linqService = reportService;
        }

        [HttpGet("GetAllProductsByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _linqService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpGet("OrdersPlacedWithinLastMonth")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersLastMonth()
        {
            var orders = await _linqService.GetOrdersLastMonthAsync();
            return Ok(orders);
        }

        [HttpGet("TotalPerProduct")]
        public async Task<ActionResult<IEnumerable<ProductSalesDto>>> GetTotalSalesByProduct()
        {
            var productSales = await _linqService.GetTotalSalesByProductAsync();
            return Ok(productSales);
        }

        [HttpGet("Top5HighestProductSales")]
        public async Task<ActionResult<IEnumerable<ProductSalesDto>>> GetTop5Products()
        {
            var top5Products = await _linqService.GetTop5ProductsAsync();
            return Ok(top5Products);
        }
    }
}