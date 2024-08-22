using gebeya01.Models;

namespace gebeya01.Interfaces
{
    public interface ICategory
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int categoryID);
        Task<ICollection<Product>> GetProductByCategory(int categoryId);
        //Task<Product> GetProductAsync(string name);
        Task<bool> CategoryExists(int id);
    }
}
