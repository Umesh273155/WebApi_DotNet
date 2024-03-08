namespace WebApplication1.Models
{
    public class ResponseModel
    {
        public int HttpStatusCode {  get; set; }    
        public int StatusCode {  get; set; }    
        public string Message { get; set; }
        public ResponseModel()
        {
            HttpStatusCode = 500;
            StatusCode = -1;
            Message ="Internal Server Error";
        }
    }
}
