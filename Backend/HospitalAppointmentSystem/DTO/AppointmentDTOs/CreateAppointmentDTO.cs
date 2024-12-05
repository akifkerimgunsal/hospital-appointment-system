using Entities.Enum;

namespace DTO.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
