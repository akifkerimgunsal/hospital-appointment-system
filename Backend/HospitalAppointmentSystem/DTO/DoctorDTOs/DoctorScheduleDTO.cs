﻿namespace DTO.DoctorDTOs
{
    public class DoctorScheduleDTO
    {
        public Guid DoctorScheduleId { get; set; }
        public DateTime WorkDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
