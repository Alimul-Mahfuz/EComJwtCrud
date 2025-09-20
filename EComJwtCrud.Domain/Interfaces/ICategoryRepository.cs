using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category?> FindByIdAsync(int id);
        public Task AddCategoryAsync(Category category);
        public void UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();

        public Task<Category?> GetCategoroyWithProductAsync(int Id);

    }
}
