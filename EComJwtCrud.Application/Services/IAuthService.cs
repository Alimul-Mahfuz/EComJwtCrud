using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.Services
{
    public interface IAuthService
    {
        Task RegistrationAsync (string username, string password, string email);
        Task<LoginResponseDto>  LoginAsync(string username, string password);

        string GenerateJwtToken(User user);


    }
}
