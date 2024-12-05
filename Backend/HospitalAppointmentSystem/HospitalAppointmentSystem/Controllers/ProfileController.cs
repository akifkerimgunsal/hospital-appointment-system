using Microsoft.AspNetCore.Mvc;
using DTO.UserDTOs;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // Kullanıcı Profilini Görüntüleme
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(Guid userId)
        {
            var response = await _profileService.GetUserProfile(userId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Kullanıcı Profilini Güncelleme
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfile(Guid userId, [FromBody] UpdateUserProfileDTO userProfileDto)
        {
            var response = await _profileService.UpdateUserProfile(userId, userProfileDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Şifre Değiştirme
        [HttpPut("{userId}/change-password")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] UpdateUserProfileDTO changePasswordDto)
        {
            var response = await _profileService.UpdateUserProfile(userId, changePasswordDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
