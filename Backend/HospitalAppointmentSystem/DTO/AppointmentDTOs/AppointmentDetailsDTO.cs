using DTO.MedicalReportDTOs;
using DTO.UserDTOs;
using Entities.Enum;

namespace DTO.AppointmentDTOs
{
    public class AppointmentDetailsDTO
    {
        public UserProfileDTO Doctor { get; set; }
        public UserProfileDTO Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public DoctorSpecialty DoctorSpecialty { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public List<MedicalReportDetailsDTO>? MedicalReports { get; set; }

    }
}
