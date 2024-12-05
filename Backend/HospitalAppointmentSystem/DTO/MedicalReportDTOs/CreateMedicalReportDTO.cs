namespace DTO.MedicalReportDTOs
{
    public class CreateMedicalReportDTO
    {
        public Guid AppointmentId { get; set; }
        public string MedicalReportContent { get; set; } // Raporun içeriği
    }
}
