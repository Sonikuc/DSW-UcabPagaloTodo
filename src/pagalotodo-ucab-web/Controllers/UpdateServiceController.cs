using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class UpdateServiceController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private HttpClient _httpClient;

        public UpdateServiceController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateServiceAction(UpdateServiceViewModel _model)
        {
            var apiUrl = "https://localhost:44339/api/serviceupdate/updateservice";
            var requestBody = new 
            { 
                serviceName = _model.ServiceName,
                typeService = _model.TypeService,
                contactNumber = _model.ContactNumber,
                userName = _model.Username
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<ServiceUpdateResponse>(responseContent);

                return RedirectToAction("AllServicesView", "AllServices");
            }

            return RedirectToAction("AccessDeniedView", "Home");
        }
    }
}

