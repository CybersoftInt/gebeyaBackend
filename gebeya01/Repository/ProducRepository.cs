﻿using AutoMapper;
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

        

        public async Task<bool> CreateProductAsync(int categoryId, Product product)
        {
            var categoryEntity = await _context.Categories
                .Where(a => a.CategoryID == categoryId)
                .FirstOrDefaultAsync();
           
            if (categoryEntity == null)
            {
                // Category not found
                return false;
            }
            // Assign the category to the product if needed (e.g., if there is a navigation property)
            product.Category = categoryEntity;

            // Add the product to the context
            _context.Products.Add(product);

            // Save changes asynchronously

            return await SaveAsync();

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
            existingProduct.Description = productDto.Description;
            existingProduct.ImageURL = productDto.ImageURL;
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
        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Enumerable.Empty<ProductDto>();
            }

            return await _context.Products
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .Take(7)
                .Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    Color = p.Color,
                    Size = p.Size,
                    Price = p.Price,
                    Brand = p.Brand,
                   
                })
                .ToListAsync();
        }


    }

}
