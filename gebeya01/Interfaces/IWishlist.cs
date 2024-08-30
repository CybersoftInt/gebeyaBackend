using System.Collections.Generic;
using System.Threading.Tasks;
using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IWishlist
    {
        Task<Wishlist> GetWishlistByIdAsync(int id);
        Task<IEnumerable<Wishlist>> GetAllWishlistsAsync();
        Task AddWishlistAsync(Wishlist wishlist);
        Task UpdateWishlistAsync(Wishlist wishlist);
        Task DeleteWishlistAsync(int id);
    }
}
