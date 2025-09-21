using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Application.Services;
using EComJwtCrud.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EComJwtCrud.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ApiResponse<Object>> Create(CreateProductDto createProductDto)
        {
            try
            {
                await _productService.CreateProduct(createProductDto);
                return ApiResponse<Object>.SuccessResponse(null,"Product Created Successfully");
            }
            catch (Exception ex) 
            {
                return ApiResponse<Object>.FailResponse(ex.Message, "Failed to create product");
            }
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ApiResponse<ProductResponse>> GetById(int Id)
        {
            try
            {
                var product=await _productService.GetProductById(Id);
                return ApiResponse<ProductResponse>.SuccessResponse(product,"Product Info");
            }
            catch (Exception ex) 
            {
                return ApiResponse<ProductResponse>.FailResponse(ex.Message, "Something went wrong");
            }
        }
        [HttpGet("GetAll")]
        public async Task<ApiResponse<IEnumerable<ProductResponse>>> GetProductList(
            [FromQuery] int? categoryId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var productList = await _productService.GetAllProducts(categoryId, minPrice, maxPrice, page, limit);

                return ApiResponse<IEnumerable<ProductResponse>>.SuccessResponse(productList, "Product List");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ProductResponse>>.FailResponse(ex.Message, "Something went wrong");
            }
        }


        [HttpPatch("Update")]
        public async Task<ApiResponse<Object>> UpdateProduct(UpdateProductDto updateProductDto)
        {
            try
            {
                await _productService.UpdateProduct(updateProductDto);
                return ApiResponse<Object>.SuccessResponse(null, "Product updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<Object>.FailResponse(ex.Message, "Something went wrong");
            }
        }
        [HttpDelete("/{Id}")]
        public async Task<ApiResponse<Object>> DeleteById(int Id)
        {
            try
            {
                await _productService.DeleteProductById(Id);
                return ApiResponse<Object>.SuccessResponse(null, "Product delted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<Object>.FailResponse(ex.Message, "Something went wrong");
            }
        }
    }
}
