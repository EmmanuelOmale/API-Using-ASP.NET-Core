namespace MyWebApi.Models
{
    public  class ResponseDto
    {
        public static string Status { get; set; }
        public static string Message { get; set; }
        public ResponseDto(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
