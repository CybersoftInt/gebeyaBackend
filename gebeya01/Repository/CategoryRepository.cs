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
        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryID == id);
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
