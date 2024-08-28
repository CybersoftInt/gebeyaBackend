using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using gebeya01.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemController : ControllerBase
    {
        private readonly IWishlist _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistItemController(IWishlist wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistItemDto wishlistItemDto)
        {
            var wishlist = await _wishlistRepository.GetWishlistAsync(wishlistItemDto.WishlistID);
            if (wishlist == null)
                return NotFound("Wishlist not found");

            if (!await _wishlistRepository.WishlistExistsAsync(wishlistItemDto.WishlistID))
                return NotFound("Wishlist not found");

            var wishlistItem = await _wishlistRepository.AddWishlistItemAsync(
                wishlistItemDto.WishlistID,
                wishlistItemDto.ProductID);

            if (!await _wishlistRepository.SaveChangesAsync())
                return StatusCode(500, "Something went wrong while saving the wishlist item");

            var wishlistItemToReturn = _mapper.Map<WishlistItemDto>(wishlistItem);
            return CreatedAtAction(nameof(AddToWishlist), new { id = wishlistItemToReturn.WishlistItemID }, wishlistItemToReturn);
        }
    }
}
