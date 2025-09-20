using EComJwtCrud.Domain.Interfaces;
using EComJwtCrud.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IUserRepository User { get; }
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }
        


        public UnitOfWork(ApplicationDbContext context, 
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository
            
            )
        {
            _context = context;
            User = userRepository;
            Category = categoryRepository;
            Product = productRepository;
        }

        public async Task<int> SaveTaskAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
