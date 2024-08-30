// Repositories/WishlistRepository.cs
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gebeya01.Repositories
{
    public class WishlistRepository : IWishlist
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wishlist> GetWishlistByIdAsync(int id)
        {
            return await _context.Wishlists.Include(w => w.WishlistItems)
                                          .FirstOrDefaultAsync(w => w.WishlistID == id);
        }

        public async Task<IEnumerable<Wishlist>> GetAllWishlistsAsync()
        {
            return await _context.Wishlists.Include(w => w.WishlistItems).ToListAsync();
        }

        public async Task AddWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlists.Update(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWishlistAsync(int id)
        {
            var wishlist = await GetWishlistByIdAsync(id);
            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}