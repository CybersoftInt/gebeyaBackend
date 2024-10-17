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
        private readonly IPerson _personRepository;

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
                UserID = wishlist.Person.UserID, // Accessing UserID through Person
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
                    UserID = wishlist.Person.UserID, // Accessing UserID through Person
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
            var person = await _personRepository.GetPersonAsync(wishlistDto.UserID);
            if (person == null)
            {
                return NotFound("User not found");
            }

            var wishlist = new Wishlist
            {
                Person = person, // Set the Person navigation property directly
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

            // Validate that the user exists
            var person = await _personRepository.GetPersonAsync(wishlistDto.UserID);
            if (person == null)
            {
                return NotFound("User not found");
            }

            existingWishlist.Person = person; // Update the Person navigation property
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
            // Retrieve the wishlist items for the given wishlistId
            var items = await _wishlistItemRepository.GetItemsByWishlistIdAsync(wishlistId);

            if (items == null || !items.Any())
            {
                // Return NotFound if no items are found for the given wishlistId
                return NotFound("No items found for this wishlist.");
            }

            // Map the WishlistItem entities to DTOs
            var itemDtos = items.Select(item => new WishlistItemDto
            {
                WishlistItemID = item.WishlistItemID,
                WishlistID = item.WishlistID,
                ProductID = item.ProductID,
                AddedDate = item.AddedDate
            }).ToList();

            // Return the list of WishlistItem DTOs
            return Ok(itemDtos);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWishlistsByUserId(int userId)
        {
            var wishlists = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            if (wishlists == null || !wishlists.Any())
            {
                return NotFound();
            }

            var wishlistDtos = wishlists.Select(w => new WishlistDto
            {
                WishlistID = w.WishlistID,
                UserID = w.Person?.UserID ?? 0, // Use null-conditional operator
                Name = w.Name,
                CreatedDate = w.CreatedDate
            }).ToList();

            return Ok(wishlistDtos);
        }
    }
}
