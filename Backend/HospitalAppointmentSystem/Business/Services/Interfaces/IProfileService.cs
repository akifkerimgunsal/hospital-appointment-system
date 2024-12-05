using Business.Helpers;
using DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IProfileService
    {
        // Kullanıcı Profilini Görüntüleme
        Task<ServiceResponseHelper<UserProfileDTO>> GetUserProfile(Guid userId);

        // Kullanıcı Profilini Güncelleme
        Task<ServiceResponseHelper<UserProfileDTO>> UpdateUserProfile(Guid userId, UpdateUserProfileDTO userProfileDto);
    }
}
