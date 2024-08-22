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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(productDtos);

        }
        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            if (!await _productRepository.ProductExists(productId))
                return NotFound();

            var product = await _productRepository.GetProductAsync(productId);
            var productDtos = _mapper.Map<ProductDto>(product);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDtos);
        }


    }
}