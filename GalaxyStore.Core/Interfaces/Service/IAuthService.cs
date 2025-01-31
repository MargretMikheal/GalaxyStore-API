using GalaxyStore.Domain.DTOs.AuthDtos;
using GalaxyStore.Domain.Identity;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> CreateAccountAsync(CreateAccountDto createAccountDto);
        Task<string> GenerateTokenAsync(LoginDto userDto);
    }
}
