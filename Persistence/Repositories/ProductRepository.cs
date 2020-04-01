using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }
        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Find(int id)
        {
          var Product = await _context.Products.SingleOrDefaultAsync(a => a.Id == id);         
            return Product;
        }
        public async Task<Product> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(a => a.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> Exists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

        public Task Save()
        {
           return _context.SaveChangesAsync();
        }
    }
}