using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface ICartItem
    {
        Task<ICollection<CartItem>> GetCartItemsByCartIdAsync(int cartItemId);
        Task<ICollection<CartItem>> GetCartItemsAsync();
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<bool> CartExists(int cartId);

    }
}
