using System.Collections.Generic;
using System.Threading.Tasks;
using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IWishlist
    {
        Task<Wishlist> GetWishlistAsync(int userId);
        Task<WishlistItem> AddWishlistItemAsync(int wishlistId, int productId);
        Task<bool> WishlistExistsAsync(int wishlistId);
        Task<bool> SaveChangesAsync();
    }
}
