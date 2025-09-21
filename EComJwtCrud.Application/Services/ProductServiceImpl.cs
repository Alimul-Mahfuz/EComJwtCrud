using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceImpl(IUnitOfWork unitOfWork, IProductRepository productInterface,ICategoryRepository categoryRepository) 
        { 
            _unitOfWork = unitOfWork;
            _productRepository = productInterface;
            _categoryRepository = categoryRepository;
        }


        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            var category= await _categoryRepository.FindByIdAsync(createProductDto.CategoryId);
            if (category == null) 
            {
                throw new Exception("Invalid category Id");
            }
            else
            {
                var product = new Product
                {
                    Name = createProductDto.Name,
                    Description = createProductDto.Description,
                    Stock = createProductDto.Stock,
                    Price = createProductDto.Price,
                    CategoryId = createProductDto.CategoryId,
                };

                await _unitOfWork.Product.AddProductAsync(product);
                await _unitOfWork.SaveTaskAsync();
            }

        }

        public async Task DeleteProductById(int Id)
        {
            var product=await _productRepository.GetProductByIdAsync(Id);
            if(product == null)
            {
                throw new Exception("Unknow product Id");
            }
            await _unitOfWork.Product.DeleteProductById(product.Id);
            await _unitOfWork.SaveTaskAsync();
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            var products=await _productRepository.GetAllProductsAsync();
            if(products == null)
            {
                throw new Exception("Product list is empty");
            }
            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Category=new CategoryProduct
                {
                    Id = p.CategoryId,
                    Name = p.Name,
                }
            }).ToList();
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts(
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1,
            int limit = 10)
        {
            var query = _productRepository.GetAllQueryableProducts();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            query = query.Skip((page - 1) * limit).Take(limit);

            var products = await query
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Stock = p.Stock,
                    Category = new CategoryProduct
                    {
                        Id = p.CategoryId,
                        Name = p.Category.Name 
                    }
                })
                .ToListAsync();

            if (!products.Any())
                throw new Exception("Product list is empty");

            return products;
        }


        public async Task<ProductResponse> GetProductById(int Id)
        {
            var product= await _productRepository.GetProductByIdAsync(Id);
            if (product == null) {

                throw new Exception("Product not found");
            }
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Category = new CategoryProduct
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                }
            };
        }

        public async Task UpdateProduct(UpdateProductDto updateProductDto)
        {
            var categoryId = updateProductDto.CategoryId;
            var category=await _categoryRepository.FindByIdAsync(categoryId);
            if (category == null) {
                throw new Exception("Invalid category Id");
            }
            var product=await _productRepository.GetProductByIdAsync(updateProductDto.Id);
            if (product == null) 
            {
                throw new Exception("Product not found");
            }
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;
            product.CategoryId = categoryId;

             _unitOfWork.Product.UpdateProduct(product);
             await _unitOfWork.SaveTaskAsync();
        }


    }
}
