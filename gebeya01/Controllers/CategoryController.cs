using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using gebeya01.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategory categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categoryies = await _categoryRepository.GetCategoriesAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categoryies);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categoryDtos);

        }
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoryAsync(int categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            var categoryDtos = _mapper.Map<CategoryDto>(category);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categoryDtos);
        }
        [HttpGet("product/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            var products = await  _categoryRepository.GetProductByCategory(categoryId);
            var catagoryDto = _mapper.Map<List<ProductDto>>(products);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(catagoryDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryDto);

            var addedCategory = await _categoryRepository.AddCategoryAsync(category);

            if (addedCategory == null)
                return BadRequest("Category could not be added");

            var addedCategoryDto = _mapper.Map<CategoryDto>(addedCategory);

            return CreatedAtAction(nameof(GetCategoryAsync), new { categoryId = addedCategoryDto.CategoryID }, addedCategoryDto);
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var result = await _categoryRepository.DeleteCategoryAsync(categoryId);

            if (!result)
                return BadRequest("Category could not be deleted");

            return NoContent();
        }
    }
}
