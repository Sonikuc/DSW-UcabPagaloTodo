using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class ChangeUserStatusController : Controller
    {
        private readonly ILogger<ChangeUserStatusController> _logger;
        private HttpClient _httpClient;

        public ChangeUserStatusController(ILogger<ChangeUserStatusController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> ChangeUserStatusView(string username)
        {
            var apiUrl = "https://localhost:44339/api/userquery/byusername?username=";
            var url = apiUrl + username;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<OneUserResponse>(responseContent);

                ChangeUserStatusViewModel model = new ChangeUserStatusViewModel
                {
                    Username = loginResponse.Username,
                    Status = loginResponse.Status                    
                };
                return View("~/Views/Administration/ChangeUserStatusView.cshtml",model);
            }

            return RedirectToAction("AccessDeniedView", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserStatusViewAction(string _username, bool estado)
        {
            var apiUrl = "https://localhost:44339/api/userupdate/changeuserstatus";
            var requestBody = new { username = _username, status = estado };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<ChangeUserStatusResponse>(responseContent);
                return RedirectToAction("AllUsersView","Admin");

            }
            return View("~/Views/Administration/AdminHomeView.cshtml");
        }



    }
}
