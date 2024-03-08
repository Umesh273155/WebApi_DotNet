namespace EmployeeDemo.Models
{
    public class ConnectionString
    {
        private readonly IConfiguration _configuration;

        public ConnectionString(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  string GetConnectionString()
        {
            var connstring = _configuration.GetConnectionString("EmployeeConnString");
            return connstring;
        }
    }
}
