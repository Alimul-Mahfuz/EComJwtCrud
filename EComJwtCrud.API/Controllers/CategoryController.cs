using EComJwtCrud.API.Filters;
using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Application.CustomException;
using EComJwtCrud.Application.Services;
using EComJwtCrud.Domain.Common;
using EComJwtCrud.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EComJwtCrud.API.Controllers
{
    [ApiController]
    [JwtAuthorizationFilter]
    [Route("api/[controller]")]
    //[JwtAuthorizationFilter]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("Create")]
        public async Task<ApiResponse<Object>> Create(CreateCategoryDto createCategoryDto)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(createCategoryDto);
                return ApiResponse<Object>.SuccessResponse(null, "Category created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message, "Failed To Create Category");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse<IEnumerable<CategoryListReponse>>> GetAll()
        {
            try
            {
                var category = await _categoryService.GetAllAsync();
                return ApiResponse<IEnumerable<CategoryListReponse>>.SuccessResponse(category, "Category List");
            }
            catch (Exception ex) {
                return ApiResponse<IEnumerable<CategoryListReponse>>.FailResponse(ex.Message, "Faied to get category data");
            }
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ApiResponse<Category>> GetById(int Id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(Id);
                if (category == null)
                {
                    return ApiResponse<Category>.FailResponse("Category Not Found", "", 404);
                }
                else
                {
                    return ApiResponse<Category>.SuccessResponse(category, "");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<Category>.FailResponse(ex.Message, "Failed to get category data");
            }
        }

        [HttpPatch("UpdateCategory")]
        public async Task<ApiResponse<Category>>UpdateById(UpdateCategoryDto categoryDto)
        {
            try
            {
                var category = await _categoryService.UpdateCategory(categoryDto);
                return ApiResponse<Category>.SuccessResponse(category, "Category Updated Successfully");
            }
            catch (Exception ex) 
            {

                return ApiResponse<Category>.FailResponse(ex.Message, "Failed to update");
            }
        }


        [HttpDelete("Delete/{Id}")]
        public async Task<ApiResponse<Object>> Delete(int Id)
        {
            try
            {
                await _categoryService.DeleteCategory(Id);
                return ApiResponse<Object>.SuccessResponse(null, "Category Successfully Deleted");
            }
            catch(ProductLinkBreakException pex)
            {
                return ApiResponse<Object>.FailResponse(pex.Message, "Something went wrong", pex.StatusCode);
            }
            catch (Exception ex) {
                return ApiResponse<Object>.FailResponse(ex.Message, "Something went wrong");
            
            }
        }

        [HttpGet("CategoryWiseProductCount")]
        public async Task<ApiResponse<IEnumerable<CategoryListReponse>>> GetCategoryWiseProductCount()
        {
            try
            {
                var categoryList = await _categoryService.GetCategoryWithProductCount();
                return ApiResponse<IEnumerable<CategoryListReponse>>.SuccessResponse(categoryList, "Category List with product coutn");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<CategoryListReponse>>.FailResponse(ex.Message, "Something went wrong");
            }
        }
    }
}
