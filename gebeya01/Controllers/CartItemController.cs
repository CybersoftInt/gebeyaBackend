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
    public class CartItemController : Controller
    {
        private readonly ICartItem _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemController(ICartItem cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CartItem>))]
        public async Task<ICollection<CartItemsDto>> GetCartItemsAsync()
        {
            var cartItems = await _cartItemRepository.GetCartItemsAsync();
            var cartItemDtos = _mapper.Map<ICollection<CartItemsDto>>(cartItems);

            return cartItemDtos;
        }

        [HttpGet("cartitems/{cartId}")]
        public async Task<IActionResult> GetCartItemsByCartId(int cartId)
        {
            var cartItems = await _cartItemRepository.GetCartItemsByCartIdAsync(cartId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cartItems);
        }
        [HttpGet("cartitem/{cartItemId}")]
        public async Task<IActionResult> GetCartItemById(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return cartItem == null ? NotFound() : Ok(cartItem);
        }
    }
}
