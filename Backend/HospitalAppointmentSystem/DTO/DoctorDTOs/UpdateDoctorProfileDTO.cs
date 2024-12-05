using Entities.Enum;

namespace DTO.DoctorDTOs
{
    public class UpdateDoctorProfileDTO
    {
        public DoctorSpecialty DoctorSpecialty { get; set; }
        public List<DoctorAvailabilityDTO> Availability { get; set; }
    }
}
