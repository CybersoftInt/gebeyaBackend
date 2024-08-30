using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gebeya01.Repositories
{
    public class WishlistItemRepository : IWishlistItem
    {
        private readonly ApplicationDbContext _context;

        public WishlistItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WishlistItem> GetWishlistItemByIdAsync(int id)
        {
            return await _context.WishlistItems
                .Include(wi => wi.Product)
                .FirstOrDefaultAsync(wi => wi.WishlistItemID == id);
        }

        public async Task<IEnumerable<WishlistItem>> GetItemsByWishlistIdAsync(int wishlistId)
        {
            return await _context.WishlistItems
                .Include(wi => wi.Product)
                .Where(wi => wi.WishlistID == wishlistId)
                .ToListAsync();
        }

        public async Task AddWishlistItemAsync(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWishlistItemAsync(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Update(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWishlistItemAsync(int id)
        {
            var item = await GetWishlistItemByIdAsync(id);
            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
