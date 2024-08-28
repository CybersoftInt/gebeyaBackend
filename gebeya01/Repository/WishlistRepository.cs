using System.Threading.Tasks;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class WishlistRepository : IWishlist
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wishlist> GetWishlistAsync(int userId)
        {
            return await _context.Wishlists
                .Include(w => w.WishlistItems)
                .FirstOrDefaultAsync(w => w.UserID == userId);
        }

        public async Task<WishlistItem> AddWishlistItemAsync(int wishlistId, int productId)
        {
            var wishlistItem = new WishlistItem
            {
                WishlistID = wishlistId,
                ProductID = productId,
                AddedDate = DateTime.UtcNow
            };

            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return wishlistItem;
        }

        public async Task<bool> WishlistExistsAsync(int wishlistId)
        {
            return await _context.Wishlists.AnyAsync(w => w.WishlistID == wishlistId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
