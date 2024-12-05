﻿using Entities.Enum;

namespace DTO.UserDTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public UserRole UserRole { get; set; }
        public DoctorSpecialty? Specialty { get; set; }
    }
}
