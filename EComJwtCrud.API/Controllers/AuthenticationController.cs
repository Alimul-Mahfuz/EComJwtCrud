using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Application.Services;
using EComJwtCrud.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EComJwtCrud.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthenticationController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ApiResponse<Object>> Register(RegistrationDto registrationDto)
        {
            try
            {
                await _authenticationService.RegistrationAsync(registrationDto.Username, registrationDto.Password,registrationDto.Email);
                return ApiResponse<Object>.SuccessResponse(null,message:"Registration Successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<Object>.FailResponse(ex.Message,ex.Message);

            }
        }
        [HttpPost("Login")]
        public async Task<ApiResponse<Object>> Login(LoginDto loginDto)
        {
            try
            {
                LoginResponseDto tokenData = await _authenticationService.LoginAsync(loginDto.Username, loginDto.Password);

                return ApiResponse<Object>.SuccessResponse(
                    data: tokenData,
                    message: "Login successful"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(
                    error: ex.Message,
                    message: "Login failed"
                );
            }
        }

    }
}
