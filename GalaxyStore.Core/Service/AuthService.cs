using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.AuthDtos;
using GalaxyStore.Domain.Helper;
using GalaxyStore.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GalaxyStore.Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<ServiceResponse<string>> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(createAccountDto);

            if (!Validator.TryValidateObject(createAccountDto, validationContext, validationResults, true))
            {
                var validationErrors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"errors: {validationErrors}"
                };
            }

            if (string.IsNullOrEmpty(createAccountDto.Password))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Password cannot be null or empty."
                };
            }

            if (await _userManager.FindByEmailAsync(createAccountDto.Email) != null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Email is already registered!"
                };
            }

            if (await _userManager.FindByNameAsync(createAccountDto.Username) != null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Username is already registered!"
                };
            }

            var user = new ApplicationUser
            {
                Email = createAccountDto.Email,
                UserName = createAccountDto.Username,
                EmployeeId = createAccountDto.EmployeeId,
                Name = createAccountDto.Name,
                Gander = createAccountDto.Gender
            };

            var result = await _userManager.CreateAsync(user, createAccountDto.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, createAccountDto.Role.ToString());
                if (roleResult.Succeeded)
                {
                    return new ServiceResponse<string>
                    {
                        Success = true,
                        Data = "User account created successfully."
                    };
                }

                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Failed to assign Role: {roleErrors}"
                };
            }

            var creationErrors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new ServiceResponse<string>
            {
                Success = false,
                Message = $"Failed to create account: {creationErrors}"
            };
        }

        public async Task<string> GenerateTokenAsync(LoginDto loginDto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loginDto);

            if (!Validator.TryValidateObject(loginDto, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException($"Errors : {errors}");
            }

            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name),
                new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).FirstOrDefault())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
