using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.Services
{
    public interface ICategoryService
    {
        public Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        public Task<IEnumerable<CategoryListReponse>> GetAllAsync();

        public Task<Category?> GetCategoryById(int id);

        public Task<Category> UpdateCategory(UpdateCategoryDto updateCategoryDto);

        public Task DeleteCategory(int id);

        public Task<IEnumerable<CategoryListReponse>> GetCategoryWithProductCount();
    }
}
