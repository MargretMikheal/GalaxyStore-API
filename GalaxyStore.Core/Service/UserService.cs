using GalaxyStore.Core;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.Helper;
using GalaxyStore.Domain.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = user.Adapt<UserDto>();
                userDto.Roles = roles.ToList();
                userDtos.Add(userDto);
            }

            return new ServiceResponse<List<UserDto>>
            {
                Success = true,
                Data = userDtos
            };
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = user.Adapt<UserDto>();
            userDto.Roles = roles.ToList();

            return new ServiceResponse<UserDto>
            {
                Success = true,
                Data = userDto
            };
        }

        public async Task<ServiceResponse<string>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Failed to delete user: {errors}"
                };
            }

            return new ServiceResponse<string>
            {
                Success = true,
                Data = "User deleted successfully."
            };
        }

        public async Task<ServiceResponse<string>> EditUserRoleAsync(EditUserRoleDto dto)
        {
            // Validate the DTO
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto);
            if (!Validator.TryValidateObject(dto, validationContext, validationResults, true))
            {
                var validationErrors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Validation errors: {validationErrors}"
                };
            }

            // Check if the user exists
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            // Validate roles
            foreach (var role in dto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = $"Role '{role}' does not exist."
                    };
                }
            }

            // Remove existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Failed to remove existing roles: {errors}"
                };
            }

            // Add the new roles
            var addResult = await _userManager.AddToRolesAsync(user, dto.Roles.Select(r => r.ToString()));
            if (!addResult.Succeeded)
            {
                var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Failed to add roles: {errors}"
                };
            }

            return new ServiceResponse<string>
            {
                Success = true,
                Data = "User roles updated successfully."
            };
        }


        public async Task<ServiceResponse<string>> UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(updateProfileDto);
            if (!Validator.TryValidateObject(updateProfileDto, context, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Validation failed: {errors}"
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            user = updateProfileDto.Adapt<ApplicationUser>();

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Failed to update profile: {errors}"
                };
            }

            return new ServiceResponse<string>
            {
                Success = true,
                Data = "Profile updated successfully."
            };
        }

    }
}
