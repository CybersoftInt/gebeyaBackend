using gebeya01.Dto;
using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface IProduct
    {
        Task<ICollection<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> GetProductAsync(string name);
        Task<bool> ProductExists(int id);
        Task<bool> CreateProductAsync(int categoryId, Product product);
        Task<bool> SaveAsync();
        Task<bool> DeleteProductAsync(int productId);
        Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto);
    }
}
