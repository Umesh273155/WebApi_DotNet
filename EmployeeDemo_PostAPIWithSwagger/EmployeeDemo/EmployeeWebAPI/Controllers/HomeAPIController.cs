using EmployeeDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAPIController : ControllerBase
    {
        private readonly ConnectionString _connectionString;

        public HomeAPIController(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        [HttpPost]
        [Route("EmployeeAddUpdate")]
        public async Task<IActionResult> AddUpdateEmployee(EmployeeRequest request)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString.GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("Proc_EmpAPITest", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpId", request.EmpId);
                        cmd.Parameters.AddWithValue("@EmpName", request.EmpName);
                        cmd.Parameters.AddWithValue("@Email", request.Email);
                        cmd.Parameters.AddWithValue("@Logic", request.EmpId > 0 ? "Update" : "Insert");

                        await cmd.ExecuteNonQueryAsync();

                        return Ok(new ResponseModel
                        {
                            HttpStatusCode = 200,
                            StatusCode = 1,
                            Message = "Success"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel
                {
                    HttpStatusCode = 500,
                    StatusCode = 1,
                    Message = "Internal Server Error"
                });
            }
        }
    }
}
