using Entities.Enum;

namespace DTO.AppointmentDTOs
{
    public class UpdateAppointmentDTO
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
