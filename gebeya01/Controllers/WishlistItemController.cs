using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace gebeya01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistItemController : ControllerBase
    {
        private readonly IWishlistItem _wishlistItemRepository;
        private readonly IWishlist _wishlistRepository;
        private readonly IProduct _productRepository;
        private readonly IMapper _mapper;

        public WishlistItemController(IWishlistItem wishlistItemRepository, IWishlist wishlistRepository, IProduct productRepository,IMapper mapper)
        {
            _wishlistItemRepository = wishlistItemRepository;
            _wishlistRepository = wishlistRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistItemDto wishlistItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validate that the wishlist exists
            var wishlistExists = await _wishlistRepository.GetWishlistByIdAsync(wishlistItemDto.WishlistID);
            if (wishlistExists == null)
            {
                return NotFound("Wishlist not found");
            }

            // If needed, validate the product exists (optional)
             var productExists = await _productRepository.GetProductAsync(wishlistItemDto.ProductID);
             if (productExists == null)
             {
                 return NotFound("Product not found");
             }

            var wishlistItem = _mapper.Map<WishlistItem>(wishlistItemDto);

            try
            {
                await _wishlistItemRepository.AddWishlistItemAsync(wishlistItem);
            }
            catch (DbUpdateException ex)
            {

                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while saving the wishlist item.");
            }

            var wishlistItemToReturn = _mapper.Map<WishlistItemDto>(wishlistItem);

            return CreatedAtAction(nameof(GetWishlistItem), new { id = wishlistItemToReturn.WishlistItemID }, wishlistItemToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            var wishlistItem = await _wishlistItemRepository.GetWishlistItemByIdAsync(id);
            if (wishlistItem == null)
                return NotFound();

            var wishlistItemDto = _mapper.Map<WishlistItemDto>(wishlistItem);

            return Ok(wishlistItemDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem(int id)
        {
            var wishlistItem = await _wishlistItemRepository.GetWishlistItemByIdAsync(id);
            if (wishlistItem == null)
                return NotFound();

            await _wishlistItemRepository.DeleteWishlistItemAsync(id);

            return NoContent();
        }
    }
}
