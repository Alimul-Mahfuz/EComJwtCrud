using EComJwtCrud.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EComJwtCrud.API.Filters
{
    public class JwtAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new JsonResult(ApiResponse<object>.FailResponse("Token is missing", "Unauthorized", 401))
                { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            try
            {
                var jwtSettings = config.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claimsIdentity = new System.Security.Claims.ClaimsIdentity(jwtToken.Claims, "jwt");
                context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    statusCode = 401,
                    message = "Invalid token",
                    data = (object?)null,
                    errors = new[] { ex.Message }
                })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
