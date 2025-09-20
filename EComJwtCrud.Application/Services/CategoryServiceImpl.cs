using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Application.CustomException;
using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.Services
{
    
    public class CategoryServiceImpl : ICategoryService
    {
        protected readonly ICategoryRepository repository;
        protected readonly IUnitOfWork unitOfWork;
        public CategoryServiceImpl(IUnitOfWork unitOfWork, ICategoryRepository repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }
        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category()
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
            };
            await unitOfWork.Category.AddCategoryAsync(category);
            await unitOfWork.SaveTaskAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category= await repository.GetCategoroyWithProductAsync(id);
            if (category != null && !category.Products.Any())
            {
                await unitOfWork.Category.DeleteCategoryAsync(id);
                await unitOfWork.SaveTaskAsync();
            }
            else 
            {
                throw new ProductLinkBreakException("Category is associated with one or more product",409);
            }
        }

        public async Task<IEnumerable<CategoryListReponse>> GetAllAsync()
        {
           var categories= await repository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryListReponse
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<Category?> GetCategoryById(int Id)
        {
            return await repository.FindByIdAsync(Id);
        }

        public async Task<IEnumerable<CategoryListReponse>> GetCategoryWithProductCount()
        {
            var categories = await repository.GetAllCategoriesAsync();
            return categories.Select(categories => new CategoryListReponse {
                Name = categories.Name,
                Id = categories.Id,
                ProductCount = categories.Products.Count()

            }).ToList();
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var category=await repository.FindByIdAsync(updateCategoryDto.Id);
            if (category != null) { 
                category.Name = updateCategoryDto.Name;
                if (updateCategoryDto.Description != null)
                {
                    category.Description = updateCategoryDto.Description;
                }
                unitOfWork.Category.UpdateCategoryAsync(category);
                await unitOfWork.SaveTaskAsync();
                return category;
            }
            else
            {
                throw new Exception("Category Doesn't Exist");
            }

        }
    }
}
