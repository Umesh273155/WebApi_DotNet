namespace EmployeeDemo.Models
{
    public class ResponseModel
    {
        public int HttpStatusCode { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; } 
        public ResponseModel()
        {
            HttpStatusCode = 500;
            StatusCode = -1;
            Message = "Internal Server Error";
        }
    }
}
