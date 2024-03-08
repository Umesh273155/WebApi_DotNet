using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration _configuration;
        public ConnectionString _connectionString;

        public HomeController(IConfiguration configuration,ConnectionString connectionString)
        {
            _configuration = configuration;
            _connectionString = connectionString;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteEmployee(int Id)
        {
            int retval = 0;
            ResponseModel response = new ResponseModel();
            using (var conn = new SqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_Test", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);                    
                    cmd.Parameters.AddWithValue("@Logic","Delete");

                    retval = cmd.ExecuteNonQuery();
                    if (retval > 0)
                    {
                        response = new ResponseModel()
                        {
                            HttpStatusCode = 200,
                            StatusCode = 1,
                            Message = "Success"
                        };
                    }
                    else
                    {
                        response = new ResponseModel()
                        {
                            HttpStatusCode = 500,
                            StatusCode = -1,
                            Message = "Failed"
                        };
                    }
                }

                catch (Exception ex)
                {
                    response = new ResponseModel()
                    {
                        HttpStatusCode = 500,
                        StatusCode = -1,
                        Message = "Failed"
                    };
                }
            }
            return Json(response);

        }


        [HttpPost]
        public IActionResult GetEmployee()
        {
            List<EmployeeResponse> list = new List<EmployeeResponse>();
            using (var conn = new SqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_Test", conn);
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@Logic", "Get");                   
                    DataSet ds= new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    {
                        foreach(DataRow item in ds.Tables[0].Rows)
                        {
                            EmployeeResponse employeeResponse = new EmployeeResponse()
                            {
                                Id = Convert.ToInt32(item["Id"].ToString()),
                                Name = item["Name"].ToString() ?? string.Empty,
                                IsActive = Convert.ToBoolean(item["IsActive"].ToString())
                            };

                            list.Add(employeeResponse); 
                        }
                    }
                }

                catch (Exception ex)
                {                    
                }
            }
            return Json(list);

        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult EditEmployee(int Id)
        {
            EmployeeResponse response  = new EmployeeResponse();
            using (var conn = new SqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_Test", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id",Id);
                    cmd.Parameters.AddWithValue("@Logic", "Edit");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        response = new EmployeeResponse()
                            {
                                Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString()),
                                Name = ds.Tables[0].Rows[0]["Name"].ToString()??string.Empty,
                               
                            };                                                
                    }
                }

                catch (Exception ex)
                {
                }
            }
            return Json(response);
        }

        [HttpPost]
        public IActionResult AddUpdateEmployee([FromBody] EmployeeRequest request)
        {
            int retval = 0;
            ResponseModel response = new ResponseModel();
            using(var conn= new SqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("Proc_Test", conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", request.Id);  
                    cmd.Parameters.AddWithValue("@Name", request.Name);  
                    cmd.Parameters.AddWithValue("@Logic", request.Id>0? "Update":"Insert"); 

                    retval= cmd.ExecuteNonQuery(); 
                    if(retval> 0) {
                        response = new ResponseModel()
                        {
                            HttpStatusCode=200,
                            StatusCode=1,
                            Message="Success"
                        };
                    }
                    else
                    {
                        response = new ResponseModel()
                        {
                            HttpStatusCode = 500,
                            StatusCode = -1,
                            Message = "Failed"
                        };
                    }
                }

                catch (Exception ex)
                {
                    response = new ResponseModel()
                    {
                        HttpStatusCode = 500,
                        StatusCode = -1,
                        Message = "Failed"
                    };
                }
            }
            return Json(response);
        }        
    }
}
