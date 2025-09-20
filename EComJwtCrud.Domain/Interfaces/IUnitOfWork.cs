using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User {  get; }
        ICategoryRepository Category {  get; }
        IProductRepository Product {  get; }
        Task<int> SaveTaskAsync();

    }
}
