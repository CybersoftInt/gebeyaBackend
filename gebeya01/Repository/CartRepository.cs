using AutoMapper;
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
            return await _context.CartItems
                                 .Include(ci => ci.ShoppingCart)
                                 .Include(ci => ci.Product)
                                 .FirstOrDefaultAsync(ci => ci.CartItemID == cartItemId);
        }

        public async Task<ICollection<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async  Task<ICollection<CartItem>> GetCartItemsByCartIdAsync(int cartItemId)
        {
            return await _context.CartItems
                                 .Where(ci => ci.CartItemID == cartItemId)
                                 .Include(ci => ci.ShoppingCart)
                                 .Include(ci => ci.Product)
                                 .ToListAsync();
        }
    }
}
