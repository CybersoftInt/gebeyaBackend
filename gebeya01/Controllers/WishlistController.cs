using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gebeya01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlist _wishlistRepository;
        private readonly IWishlistItem _wishlistItemRepository;
        private readonly IPerson _personRepository; // Updated to use IPerson

        public WishlistController(IWishlist wishlistRepository, IWishlistItem wishlistItemRepository, IPerson personRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
            _personRepository = personRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistById(int id)
        {
            var wishlist = await _wishlistRepository.GetWishlistByIdAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            var wishlistDto = new WishlistDto
            {
                WishlistID = wishlist.WishlistID,
                UserID = wishlist.UserID,
                Name = wishlist.Name,
                CreatedDate = wishlist.CreatedDate
            };

            return Ok(wishlistDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWishlists()
        {
            var wishlists = await _wishlistRepository.GetAllWishlistsAsync();
            var wishlistDtos = new List<WishlistDto>();

            foreach (var wishlist in wishlists)
            {
                wishlistDtos.Add(new WishlistDto
                {
                    WishlistID = wishlist.WishlistID,
                    UserID = wishlist.UserID,
                    Name = wishlist.Name,
                    CreatedDate = wishlist.CreatedDate
                });
            }

            return Ok(wishlistDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishlist([FromBody] WishlistDto wishlistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the user exists
            var userExists = await _personRepository.PersonExistsAsync(wishlistDto.UserID);
            if (!userExists)
            {
                return NotFound("User not found");
            }

            var wishlist = new Wishlist
            {
                UserID = wishlistDto.UserID,
                Name = wishlistDto.Name,
                CreatedDate = wishlistDto.CreatedDate
            };

            await _wishlistRepository.AddWishlistAsync(wishlist);

            return CreatedAtAction(nameof(GetWishlistById), new { id = wishlist.WishlistID }, wishlistDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWishlist(int id, [FromBody] WishlistDto wishlistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingWishlist = await _wishlistRepository.GetWishlistByIdAsync(id);
            if (existingWishlist == null)
            {
                return NotFound();
            }

            existingWishlist.UserID = wishlistDto.UserID;
            existingWishlist.Name = wishlistDto.Name;
            existingWishlist.CreatedDate = wishlistDto.CreatedDate;

            await _wishlistRepository.UpdateWishlistAsync(existingWishlist);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            var existingWishlist = await _wishlistRepository.GetWishlistByIdAsync(id);
            if (existingWishlist == null)
            {
                return NotFound();
            }

            await _wishlistRepository.DeleteWishlistAsync(id);

            return NoContent();
        }

        [HttpGet("{wishlistId}/items")]
        public async Task<IActionResult> GetItemsByWishlistId(int wishlistId)
        {
            var items = await _wishlistItemRepository.GetItemsByWishlistIdAsync(wishlistId);
            var itemDtos = new List<WishlistItemDto>();

            foreach (var item in items)
            {
                itemDtos.Add(new WishlistItemDto
                {
                    WishlistItemID = item.WishlistItemID,
                    WishlistID = item.WishlistID,
                    ProductID = item.ProductID,
                    AddedDate = item.AddedDate
                });
            }

            return Ok(itemDtos);
        }
    }
}
