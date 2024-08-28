using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class CategoryRepository : ICategory
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryID == id);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryID)
        {
            var category = await _context.Categories.FindAsync(categoryID);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int categoryID)
        {
            return await _context.Categories.FindAsync(categoryID);
        }

        public async Task<ICollection<Product>> GetProductByCategory(int categoryId)
        {
            return await _context.Products
                          .Where(p => p.CategoryID == categoryId).ToListAsync();
        }
    }
}
