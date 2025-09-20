using EComJwtCrud.Application.DTOs;
using EComJwtCrud.Domain.Entities;
using EComJwtCrud.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthServiceImpl(IUserRepository userRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task RegistrationAsync(string username, string password,string email)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(username);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            (var hash, var salt) = PasswordHasher(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt,
                Email = email
            };

            await _unitOfWork.User.AddUserAsync(newUser); 
            await _unitOfWork.SaveTaskAsync();
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
                throw new Exception("Invalid username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new Exception("Invalid username or password");

            var token= GenerateJwtToken(user);
            return new LoginResponseDto
            {
                Username = username,
                Token = token,
            };
        }


        public string GenerateJwtToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private (byte[] hash, byte[] salt) PasswordHasher(string password)
        {
            using var hmac = new HMACSHA512(); // generates unique salt
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var salt = hmac.Key;
            return (hash, salt);
        }

    }
}
