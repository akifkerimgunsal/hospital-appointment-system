using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using DataAccess.UnitOfWork;
using DTO.UserDTOs;
using Entities.Concrete;

namespace Business.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Kullanıcı Profilini Görüntüleme
        public async Task<ServiceResponseHelper<UserProfileDTO>> GetUserProfile(Guid userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            var userDto = _mapper.Map<UserProfileDTO>(user);
            return ServiceResponseHelper<UserProfileDTO>.SuccessResponse(userDto, "User profile retrieved successfully.");
        }

        public async Task<ServiceResponseHelper<UserProfileDTO>> UpdateUserProfile(Guid userId, UpdateUserProfileDTO userProfileDto)
        {
            // Kullanıcının varlığını doğrula
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User", userId);

            // Kullanıcı profil bilgilerini güncelle
            _mapper.Map(userProfileDto, user);

            user.Password = HashingHelper.HashPassword(userProfileDto.Password);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            // Güncellenmiş kullanıcı bilgilerini DTO'ya dönüştür
            var updatedUserDto = _mapper.Map<UserProfileDTO>(user);
            return ServiceResponseHelper<UserProfileDTO>.SuccessResponse(updatedUserDto, "User profile updated successfully.");
        }
    }
}
