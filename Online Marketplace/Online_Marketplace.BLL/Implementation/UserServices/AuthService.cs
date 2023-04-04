using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Online_Marketplace.BLL.Interface.IUserServices;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared;
using Online_Marketplace.Shared.DTOs;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Marketplace.BLL.Implementation.UserServices
{
    public sealed class AuthService : IAuthService
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;


        public AuthService(ILoggerManager logger, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }



        public async Task<ServiceResponse<string>> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            try
            {
                _logger.LogInfo("Validates user and logs them in");

                _user = await _userManager.FindByNameAsync(userForAuth.UserName);

                var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
                if (!result)
                {
                    _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");

                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "Login failed. Wrong username or password."
                    };
                }
                return new ServiceResponse<string>
                {
                    Success = true,
                    Message = "Login successful. Wrong username or password."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(ValidateUser)} service method {ex}");
                throw;
            }
        }

        public async Task<string> CreateToken()
        {
            try
            {
                _logger.LogInfo("Creates the JWT token");

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims();


                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateToken)} service method {ex}");
                throw;
            }

        }

        private static SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        }

        private async Task<List<Claim>> GetClaims()
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }


    }

}