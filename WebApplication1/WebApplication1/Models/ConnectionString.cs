namespace WebApplication1.Models
{
    public class ConnectionString
    {
        private readonly IConfiguration _configuration;

        public ConnectionString(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GetConnectionString()
        {
            var connstring = _configuration.GetConnectionString("EmployeeConnString") ??string.Empty;
            return connstring;  
        }
    }
}
