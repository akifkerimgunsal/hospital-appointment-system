using DTO.UserDTOs;
using Entities.Enum;

namespace DTO.FeedbackDTOs
{
    public class FeedbackDetailsDTO
    {
        public Guid FeedbackId { get; set; }
        public string FeedbackContent { get; set; } // Kullanıcı geri bildirimi
        public bool IsResolved { get; set; } // Geri bildirimin çözülüp çözülmediği
        public string? AdminResponse { get; set; } // Admin yanıtı, eğer varsa
        public DateTime? AdminResponseDate { get; set; } // Yanıt tarihi, eğer varsa

        public UserProfileDTO User {  get; set; }
        
    }
}
