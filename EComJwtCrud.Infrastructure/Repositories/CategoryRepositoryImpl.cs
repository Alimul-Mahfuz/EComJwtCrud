using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using EComJwtCrud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Infrastructure.Repositories
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        
        public CategoryRepositoryImpl(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var affectedRows = await _dbContext.Categories
                    .Where(c => c.Id == id)
                    .ExecuteDeleteAsync();
            return affectedRows > 0;
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
           return await _dbContext.Categories.Include(c=>c.Products).ToListAsync();
        }

        public async Task<Category?> GetCategoroyWithProductAsync(int id)
        {
            return await _dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void UpdateCategoryAsync(Category category)
        {
             _dbContext.Categories.Update(category);

        }
    }
}
