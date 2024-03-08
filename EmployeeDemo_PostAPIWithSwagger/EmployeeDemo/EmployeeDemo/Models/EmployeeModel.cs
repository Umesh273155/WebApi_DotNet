namespace EmployeeDemo.Models
{
    public class EmployeeRequest
    {
        public int EmpId { get; set; }
        public string  EmpName { get; set; }=string.Empty;
        public string  Email { get; set; }=string.Empty;
    }
    public class EmployeeResponse
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Boolean IsActive {  get; set; }  
    }
}
