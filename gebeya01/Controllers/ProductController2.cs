using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Mvc;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController2 : Controller
    {
        private readonly IProduct _productRepository;
        private readonly IMapper _mapper;

        public ProductController2(IProduct productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(productDtos);

        }
        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            if (!await _productRepository.ProductExists(productId))
                return NotFound();

            var product = await _productRepository.GetProductAsync(productId);
            var productDtos = _mapper.Map<ProductDto>(product);

            return Ok(productDtos);
        }
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromQuery] int categoryId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
            {
                return BadRequest("Product cannot be null.");
            }

            if (categoryId <= 0)
            {
                return BadRequest("Invalid category ID.");
            }

         try
            {
                // Map DTO to entity
                var productEntity = _mapper.Map<Product>(productCreate);
                productEntity.CategoryID = categoryId; // Set the category ID

                await _productRepository.CreateProductAsync(categoryId,productEntity);

                bool saved = await _productRepository.SaveAsync();
                
                if (!saved)
                {
                    return StatusCode(500, "A problem occurred while handling(saving) your request.");
                }

                // Map the created product entity back to DTO
                var createdProductDto = _mapper.Map<ProductDto>(productEntity);

                // Return the created product with a location header
                return CreatedAtAction(
                    nameof(GetProductAsync), 
                    new { productId = createdProductDto.ProductID }, 
                    createdProductDto
                    );
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError(ex, "An error occurred while creating the product.");
                return StatusCode(500, "A problem occurred while handling your request.");
            }
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int productId, ProductDto productDto)
        {
            if (productDto == null || productId != productDto.ProductID)
            {
                return BadRequest("Product data is invalid.");
            }

            var updatedProductDto = await _productRepository.UpdateProductAsync(productId, productDto);
            if (updatedProductDto == null)
            {
                return NotFound(); // Product not found 
            }

            return Ok(updatedProductDto); // Return the updated product DTO
        }




        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                var deleted = await _productRepository.DeleteProductAsync(productId);

                if (!deleted)
                {
                    return NotFound(); // Product not found
                }

                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError(ex, "An error occurred while deleting the product.");
                return StatusCode(500, "A problem occurred while handling your request.");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var products = await _productRepository.SearchProductsAsync(query);
            return Ok(products);
        }






    }
}