using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController2 : ControllerBase
    {
        private readonly IWishlist _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistController2(IWishlist wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        // Get Wishlist by User ID
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(WishlistDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetWishlistAsync(int userId)
        {
            var wishlist = await _wishlistRepository.GetWishlistAsync(userId);

            if (wishlist == null)
                return NotFound("Wishlist not found for this user");

            var wishlistDto = _mapper.Map<WishlistDto>(wishlist);
            return Ok(wishlistDto);
        }

        // Add Item to Wishlist
        [HttpPost("add")]
        [ProducesResponseType(201, Type = typeof(WishlistItemDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistItemDto wishlistItemDto)
        {
            if (wishlistItemDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _wishlistRepository.WishlistExistsAsync(wishlistItemDto.WishlistID))
                return NotFound("Wishlist not found");

            var wishlistItem = await _wishlistRepository.AddWishlistItemAsync(
                wishlistItemDto.WishlistID,
                wishlistItemDto.ProductID);

            if (!await _wishlistRepository.SaveChangesAsync())
                return StatusCode(500, "Something went wrong while saving the wishlist item");

            var wishlistItemToReturn = _mapper.Map<WishlistItemDto>(wishlistItem);
            return CreatedAtAction(nameof(GetWishlistAsync), new { userId = wishlistItemToReturn.WishlistID }, wishlistItemToReturn);
        }

        // Optionally, you could add other endpoints here, like removing an item from the wishlist or listing all wishlist items
    }
}
