using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;


namespace EComJwtCrud.Application.Services
{
    public interface IProductService
    {
        public Task CreateProduct(CreateProductDto createProductDto);
        public Task<ProductResponse> GetProductById(int Id);
        public Task UpdateProduct(UpdateProductDto updateProductDto);
        public Task DeleteProductById(int Id);
        public Task<IEnumerable<ProductResponse>> GetAllProducts();

    }
}
