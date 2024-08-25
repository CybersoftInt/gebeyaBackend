using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class CartRepository : ICartItem
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository( ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> CartExists(int cartItemId)
        {
            return _context.CartItems.AnyAsync(c => c.CartItemID == cartItemId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }

        public async Task<ICollection<CartItem>> GetCartItemsAsync()
        {
            var cartItems = await _context.CartItems.ToListAsync();
            return (cartItems);
        }

        public async Task<ICollection<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems
                                 .Where(ci => ci.ShoppingCartID == cartId)
                                 .ToListAsync();
        }

    }
}
