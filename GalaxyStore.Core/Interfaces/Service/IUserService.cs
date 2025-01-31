using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(string userId);
        Task<ServiceResponse<string>> DeleteUserAsync(string userId);
        Task<ServiceResponse<string>> EditUserRoleAsync(EditUserRoleDto dto);
        Task<ServiceResponse<string>> UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto);
    }
}
