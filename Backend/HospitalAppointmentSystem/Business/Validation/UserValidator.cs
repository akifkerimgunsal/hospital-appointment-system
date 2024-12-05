using DTO.UserDTOs;
using FluentValidation;
using Entities.Enum;

namespace Business.Validation
{
    public class UserValidator : AbstractValidator<RegisterUserDTO>
    {
        public UserValidator()
        {
            // Eğer UserRole Doctor ise DoctorSpecialty alanı boş olmamalı
            RuleFor(user => user.DoctorSpecialty)
                .NotEmpty()
                .When(user => user.UserRole == UserRole.Doctor)
                .WithMessage("Doctor specialty is required for Doctor role.");

            // Diğer doğrulama kuralları buraya eklenebilir
        }
    }
}
