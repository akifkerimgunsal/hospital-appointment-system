using DTO.DoctorDTOs;
using Entities.Enum;

namespace DTO.UserDTOs
{
    public class LoginUserDTO
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }

}
