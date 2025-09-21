using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductByIdAsync(int Id);

        public IQueryable<Product> GetAllQueryableProducts();

        public Task AddProductAsync(Product product);
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<bool> DeleteProductById(int Id);

        public void UpdateProduct(Product product);
    }
}
