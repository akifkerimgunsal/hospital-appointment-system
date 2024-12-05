namespace DTO.FeedbackDTOs
{
    public class FeedbackResponseDTO
    {
        public string FeedbackId { get; set; } 
        public string AdminResponse { get; set; }
        public bool IsResolved { get; set; }
        public DateTime ResponseDate { get; set; } = DateTime.Now; // Yanıtın verildiği tarih
    }
}
