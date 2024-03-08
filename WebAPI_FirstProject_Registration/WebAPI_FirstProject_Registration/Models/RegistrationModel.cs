namespace WebAPI_FirstProject_Registration.Models
{
    public class RegistrationModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;    
        public  bool IsActive { get; set; }
    }
    public class LoginModel
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;   

    }
}
