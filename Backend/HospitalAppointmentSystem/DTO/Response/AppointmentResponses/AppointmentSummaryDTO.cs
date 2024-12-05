using Entities.Enum;

namespace DTO.AppointmentDTOs
{
    public class AppointmentSummaryDTO
    {
        public DateTime AppointmentDate { get; set; }
        public string DoctorName { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }

    }
}
