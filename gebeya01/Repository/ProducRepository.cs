using AutoMapper;
using gebeya01.Dto;
using gebeya01.Interfaces;
using gebeya01.Models;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Repository
{
    public class ProducRepository : IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProducRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        

        public Task<bool> CreateProductAsync(int categoryId, Product product)
        {
            var categoryEntity = _context.Categories.Where(a => a.CategoryID == categoryId).FirstOrDefault();
            _context.Add(product);
            return SaveAsync();

        }
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false; // Product not found
            }

            _context.Products.Remove(product);
            return await SaveAsync();
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

        //public async Task<bool> IsInWishList(int productId, Per Person)
        

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductID == id);
        }

        
        public async Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto)
        {
            
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                return null; // Product not found
            }

            // Map updated values from DTO to the existing entity
            existingProduct.Name = productDto.Name;
            existingProduct.CategoryID = productDto.CategoryID;
            existingProduct.Price = productDto.Price;
            // Update other fields as necessary

            _context.Products.Update(existingProduct);
            bool saved = await SaveAsync();

            if (!saved)
            {
                return null; // Update failed
            }

            // Map and return the updated product as DTO
            return _mapper.Map<ProductDto>(existingProduct);
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log exception or handle it accordingly
                return false;
            }
        }


    }

}
