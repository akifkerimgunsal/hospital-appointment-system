using DTO.AppointmentDTOs;

namespace DTO.MedicalReportDTOs
{
    public class MedicalReportSummaryDTO
    {
        public Guid ReportId { get; set; }
        public AppointmentDetailsDTO AppointmentDetails { get; set; }
    }
}
