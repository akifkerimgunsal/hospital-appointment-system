namespace DTO.LiveSupportDTOs
{
    public class LiveSupportDTO
    {
        public DateTime StartTime { get; set; } 
        public string MessageContent { get; set; } 
        public Guid UserId { get; set; } 
        public string UserName { get; set; } 
    }
}
