using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace gebeya01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistItemController : ControllerBase
    {
        private readonly IWishlistItem _wishlistItemRepository;
        private readonly IMapper _mapper;
        private readonly IPerson _personRepository; // Added for user validation (if needed)

        public WishlistItemController(IWishlistItem wishlistItemRepository, IMapper mapper, IPerson personRepository)
        {
            _wishlistItemRepository = wishlistItemRepository;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistItemDto wishlistItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Optionally validate that the user exists (if required)
            var personExists = await _personRepository.PersonExistsAsync(wishlistItemDto.UserID);
            if (!personExists)
            {
                return NotFound("User not found");
            }

            var wishlistItem = _mapper.Map<WishlistItem>(wishlistItemDto);

            await _wishlistItemRepository.AddWishlistItemAsync(wishlistItem);

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
