using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet("/api/products")]
        public ActionResult GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = 
                products.Select(product => ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels);
        }

        [HttpGet("/api/products/{id}")]
        public ActionResult GetProductById(int id)
        {
            _logger.LogInformation("Getting product by Id");
            var product = _productService.GetProductById(id);
            var mappedProduct = ProductMapper.SerializeProductModel(product);

            return Ok(mappedProduct);
        }

        [HttpPatch("/api/products/{id}/archive")]
        public ActionResult ArchiveProductById(int id)
        {
            _logger.LogInformation($"Archiving product {id}.");
            var product = _productService.ArchiveProduct(id);
            if (!product.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(product);
        }
    }
}