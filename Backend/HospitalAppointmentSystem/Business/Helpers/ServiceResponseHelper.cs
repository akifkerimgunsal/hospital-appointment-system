namespace Business.Helpers
{
    public class ServiceResponseHelper<T>
    {
        public T Data { get; set; } // İşlem sonucu döndürülen veri
        public bool Success { get; set; } // İşlemin başarı durumu
        public string Message { get; set; } // İşlem ile ilgili açıklama veya hata mesajı

        // Başarılı bir yanıt döndürmek için
        public static ServiceResponseHelper<T> SuccessResponse(T data, string message = "")
        {
            return new ServiceResponseHelper<T> { Data = data, Success = true, Message = message };
        }

        // Başarısız bir yanıt döndürmek için
        public static ServiceResponseHelper<T> ErrorResponse(string message)
        {
            return new ServiceResponseHelper<T> { Success = false, Message = message };
        }
    }
}
