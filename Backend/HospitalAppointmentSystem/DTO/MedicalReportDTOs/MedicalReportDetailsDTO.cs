using DTO.AppointmentDTOs;

namespace DTO.MedicalReportDTOs
{
    public class MedicalReportDetailsDTO
    {
        public Guid ReportId { get; set; }
        public AppointmentDetailsDTO AppointmentDetails { get; set; }
        public string MedicalReportContent { get; set; } // Rapor içeriği
        public DateTime ReportDate { get; set; } // Raporun tarihi
    }
}
