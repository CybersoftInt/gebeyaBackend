using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IWishlistItem
    {
        Task<WishlistItem> GetWishlistItemByIdAsync(int id);
        Task<IEnumerable<WishlistItem>> GetItemsByWishlistIdAsync(int wishlistId);
        Task AddWishlistItemAsync(WishlistItem wishlistItem);
        Task UpdateWishlistItemAsync(WishlistItem wishlistItem);
        Task DeleteWishlistItemAsync(int id);
    }
}
