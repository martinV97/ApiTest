using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Product object with id</returns>
        [HttpPost]
        [Route("createProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var result = await _productService.CreateProduct(product);
            return Ok(result);
        }

        /// <summary>
        /// Get a lis of products by category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Product list</returns>
        [HttpGet]
        [Route("getProductsByCategoryId")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var result = await _productService.GetAllProductsByCategoryId(id);
            return Ok(result);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product object</returns>
        [HttpGet]
        [Route("getProductById")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Updated product</returns>
        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            var result = await _productService.UpdateProductById(product);
            return Ok(result);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Product object</param>
        /// <returns>Deleted product object</returns>
        [HttpDelete]
        [Route("deleteProductById")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var result = await _productService.DeleteProductById(id);
            return Ok(result);
        }
    }
}
