using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;


namespace EComJwtCrud.Application.Services
{
    public interface IAuthService
    {
        Task RegistrationAsync (string username, string password, string email);
        Task<LoginResponseDto>  LoginAsync(string username, string password);

        string GenerateJwtToken(User user);



    }
}
