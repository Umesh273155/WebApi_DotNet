using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using WebAPI_FirstProject_Registration.Models;

namespace WebAPI_FirstProject_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
       private readonly IConfiguration _configuration;   
       public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public string Registration([FromBody]RegistrationModel registrationModel)
        {
            string constr = _configuration.GetConnectionString("ConnUser") ?? string.Empty;

            SqlConnection conn= new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Proc_tblResistration", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", registrationModel.studentId);
            cmd.Parameters.AddWithValue("@StudentName",registrationModel.studentName);
            cmd.Parameters.AddWithValue("@Email", registrationModel.email);
            cmd.Parameters.AddWithValue("@Password", registrationModel.password);
            cmd.Parameters.AddWithValue("@IsActive", registrationModel.IsActive);
            cmd.Parameters.AddWithValue("@Logic", "Insert");
            int retval= cmd.ExecuteNonQuery();
            conn.Close();
            if (retval> 0 ) {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }  
           
        }

        [HttpPost]
        [Route("login")]
        public string Login([FromBody] LoginModel loginModel)
        {

            string constr = _configuration.GetConnectionString("ConnUser") ?? string.Empty;
            SqlConnection conn= new SqlConnection(constr);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Proc_tblResistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", loginModel.email);
                cmd.Parameters.AddWithValue ("@Password", loginModel.password);  
                cmd.Parameters.AddWithValue("@Logic", "GetUser");
                DataSet ds = new DataSet(constr);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return "Data Found";
                    }
                    else
                    {
                        return "Data not Found";
                    }
                    
                }
                else
                {
                    return "Invalid User";
                }
            }
            catch(Exception ex) { 
                return ex.Message.ToString();
            }    
            finally { 
                conn.Close();
                
            }
            // return "UserLogged";
        }
      
    }
}
