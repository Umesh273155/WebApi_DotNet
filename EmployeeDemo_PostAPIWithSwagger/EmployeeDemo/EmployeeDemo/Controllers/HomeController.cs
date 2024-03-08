using BusinessLayer.Services;
using EmployeeDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionString _connstring;
       
        public HomeController( ConnectionString connstring ,IConfiguration configuration)
        {           
            _connstring = connstring;
            _configuration = configuration;
        }

        [HttpGet]   
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateEmployee([FromBody] EmployeeRequest request)
        {
             ResponseModel response = new ResponseModel();
            var apiClient = new ApiClient<ResponseModel>();
            string APIsBaseUrl = _configuration.GetSection("APIBaseUrl:BaseUrl").Value ?? string.Empty;
            string url = APIsBaseUrl + APIs.AddUpdateEmployee;
            
            var apiResponse = await apiClient.PostAsync(url, request);
            if (apiResponse == null)
            {
                response.HttpStatusCode = 401;
                response.StatusCode = -1;
                response.Message = "bad Request";
                return Json(response);
            }
            else
            {
                return Json(apiResponse);
            }
        }
    }
}
