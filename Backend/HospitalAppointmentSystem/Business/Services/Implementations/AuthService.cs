using AutoMapper;
using Business.Exceptions;
using Business.Helpers;
using Business.Services.Interfaces;
using Business.Validation;
using DataAccess.UnitOfWork;
using DTO.UserDTOs;
using Entities.Concrete;
using Entities.Enum;

namespace Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserValidator _userValidator;

        public AuthService(IMapper mapper, IUnitOfWork unitOfWork, UserValidator userValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userValidator = userValidator;
        }

        public async Task<ServiceResponseHelper<UserProfileDTO>> RegisterUser(RegisterUserDTO registerDto)
        {
            var validationResult = await _userValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.First().ErrorMessage);
            }

            var existingUser = await _unitOfWork.Users.GetUserByIdentityAsync(registerDto.IdentityNumber);
            if (existingUser != null)
                throw new ValidationException("A user with this identity number already exists.");

            existingUser = (await _unitOfWork.Users.FindAsync(u => u.Email == registerDto.Email)).FirstOrDefault();
            if (existingUser != null)
                throw new ValidationException("A user with this email address already exists.");

            existingUser = (await _unitOfWork.Users.FindAsync(u => u.PhoneNumber == registerDto.PhoneNumber)).FirstOrDefault();
            if (existingUser != null)
                throw new ValidationException("A user with this email phone number already exists.");

            var user = _mapper.Map<User>(registerDto);
            user.Password = HashingHelper.HashPassword(registerDto.Password);

            if (user.UserRole == UserRole.Patient || user.UserRole == UserRole.Admin)
                user.DoctorSpecialty = 0;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var userDto = _mapper.Map<UserProfileDTO>(user);
            return ServiceResponseHelper<UserProfileDTO>.SuccessResponse(userDto, "User registered successfully.");
        }

        public async Task<ServiceResponseHelper<User>> LoginUser(string emailOrPhone, string password)
        {
            var user = (await _unitOfWork.Users.FindAsync(u => u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone && !u.IsDeleted)).FirstOrDefault();
            if (user == null || !HashingHelper.VerifyPassword(user.Password, password))
                {
                    ServiceResponseHelper<UserProfileDTO>.ErrorResponse("Invalid credentials or user is deleted.");
                }

            bool isPasswordValid = HashingHelper.VerifyPassword(user.Password, password);
            if (!isPasswordValid)
                throw new ValidationException("Incorrect password.");

            return ServiceResponseHelper<User>.SuccessResponse(user, "User logged in successfully.");
        }



    }
}
