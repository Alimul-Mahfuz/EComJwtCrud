using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);
        public Task<User?> GetByUserNameAsync(string username);
        public Task AddUserAsync(User user);
    }
}
