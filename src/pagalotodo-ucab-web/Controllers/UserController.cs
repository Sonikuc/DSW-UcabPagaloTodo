using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UCABPagaloTodoWeb.Models;
using System.Net.Http;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Responses;
using System.Text;

namespace UCABPagaloTodoWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private HttpClient _httpClient;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> UpdateUserView()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var apiUrl = "https://localhost:44339/api/userquery/byusername?username=";
            var url = apiUrl + userName;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<OneUserResponse>(responseContent);
                
                UpdateUserViewModel model = new UpdateUserViewModel
                {
                    Name = loginResponse.Name,
                    Lastname = loginResponse.Lastname,
                    UserName = loginResponse.Username,
                    Email = loginResponse.Email,
                    PhoneNumber = loginResponse.PhoneNumber
                };
                return View(model);
            }

            return RedirectToAction("AccessDeniedView", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string UserName, string _email, string PhoneNumber, string Name, string Lastname)
        {
            var apiUrl = "https://localhost:44339/api/userupdate/updateuser";
            var requestBody = new { name = Name, lastname = Lastname, username = UserName, email = _email, phoneNumber = PhoneNumber };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<Guid>(responseContent);

                if (loginResponse != null) {
                    return RedirectToAction("Index", "Home");
                }
            }

               return RedirectToAction("AccessDeniedView", "Home");
        }
    }
}
