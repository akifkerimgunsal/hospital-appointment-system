namespace Business.Helpers
{
    public class ServiceResponseHelper<T>
    {
        public T Data { get; set; } // İşlem sonucu döndürülen veri
        public bool Success { get; set; } 
        public string Message { get; set; } 

        public static ServiceResponseHelper<T> SuccessResponse(T data, string message = "")
        {
            return new ServiceResponseHelper<T> { Data = data, Success = true, Message = message };
        }

        public static ServiceResponseHelper<T> ErrorResponse(string message)
        {
            return new ServiceResponseHelper<T> { Success = false, Message = message };
        }
    }
}
