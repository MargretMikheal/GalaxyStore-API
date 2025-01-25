
using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.Identity;

namespace GalaxyStore.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> CreateAccountAsync(CreateAccountDto createAccountDto);
        Task<string> GenerateTokenAsync(LoginDto userDto);
    }
}
