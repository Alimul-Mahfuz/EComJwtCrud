using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using EComJwtCrud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EComJwtCrud.Infrastructure.Repositories
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p=>p.Category)
                .ToListAsync();
        }


        public async Task<Product?> GetProductByIdAsync(int Id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == Id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<bool> DeleteProductById(int Id)
        {
            var affectedRow=await _context.Products.Where(p=>p.Id==Id).ExecuteDeleteAsync();
            return affectedRow > 0;
        }

        public IQueryable<Product> GetAllQueryableProducts()
        {
            return _context.Products.AsQueryable();
        }
    }
}
