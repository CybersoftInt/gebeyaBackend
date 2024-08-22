using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IProduct
    {
        Task<ICollection<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> GetProductAsync(string name);
        Task<bool> ProductExists(int id);
    }
}
