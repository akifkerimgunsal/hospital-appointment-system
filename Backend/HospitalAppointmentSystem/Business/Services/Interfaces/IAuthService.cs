using Business.Helpers;
using DTO.UserDTOs;
using Entities.Concrete;

namespace Business.Services.Interfaces
{
    public interface IAuthService
    {
        // Kullanıcı Kaydı
        Task<ServiceResponseHelper<UserProfileDTO>> RegisterUser(RegisterUserDTO registerDto);

        // Kullanıcı Girişi
        Task<ServiceResponseHelper<User>> LoginUser(string email, string password);
    }
}
