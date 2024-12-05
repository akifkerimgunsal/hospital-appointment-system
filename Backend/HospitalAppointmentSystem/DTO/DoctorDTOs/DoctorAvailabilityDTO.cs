namespace DTO.DoctorDTOs
{
    public class DoctorAvailabilityDTO
    {
        public DateTime Date { get; set; }
        public List<DoctorScheduleDTO> ScheduleAvailableTimes { get; set; }
    }
}
