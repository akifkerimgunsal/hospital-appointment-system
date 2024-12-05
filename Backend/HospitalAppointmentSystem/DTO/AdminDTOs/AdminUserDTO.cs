using DTO.UserDTOs;
using Entities.Enum;

namespace DTO.AdminDTOs
{
    public class AdminUserDTO
    {
        public UserProfileDTO UserProfile { get; set; }
        public bool IsActive { get; set; }
    }
}
