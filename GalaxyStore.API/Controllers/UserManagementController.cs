using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsersAsync();
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("EditUserRole/{userId}")]
        public async Task<IActionResult> EditUserRole(EditUserRoleDto editUserRoleDto)
        {
            var result = await _userService.EditUserRoleAsync(editUserRoleDto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("UpdateProfile/{userId}")]
        public async Task<IActionResult> UpdateProfile(string userId, [FromBody] UpdateProfileDto updateProfileDto)
        {
            var result = await _userService.UpdateProfileAsync(userId, updateProfileDto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
