using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class ProducRepository : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProducRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> GetProductAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync((p => p.Name == name));
        }

        public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductID == id);
        }
    }
}
