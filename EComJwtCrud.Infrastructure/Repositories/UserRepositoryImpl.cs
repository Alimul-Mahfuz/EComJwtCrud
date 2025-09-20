using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using EComJwtCrud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Infrastructure.Repositories
{
    public class UserRepositoryImpl : IUserRepository
    {
        protected ApplicationDbContext _context;

        public UserRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Username == username);
        }
    }
}
