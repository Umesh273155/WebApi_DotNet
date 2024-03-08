namespace WebApplication1.Models
{
    public class EmployeeRequest
    {
        public int Id { get; set; } 
        public string Name { get; set; }=string.Empty;
    }
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Boolean IsActive { get; set; } 
    }
}
