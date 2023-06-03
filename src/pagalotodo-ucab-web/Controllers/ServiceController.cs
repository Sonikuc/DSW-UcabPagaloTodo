using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UCABPagaloTodoWeb.Models;
using System.Net.Http;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Responses;
using System.Text;
using System.Reflection;

namespace UCABPagaloTodoWeb.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private HttpClient _httpClient;

        public ServiceController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public IActionResult AddServiceView() 
        {
            AddServiceViewModel model = new AddServiceViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceAction(AddServiceViewModel service) {
            var apiUrl = "https://localhost:44339/api/addservice/addservice";
            var requestBody = new
            { 
                serviceName =  service.ServiceName,
                typeService = service.TypeService,
                contactNumber = service.ContactNumber,
                userName = service.UserName
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
                return View("~/Views/Administration/AdminServiceView.cshtml");
            }

                return View("~/Views/Administration/AdminHomeView.cshtml");
        }

        public async Task<IActionResult> UpdateServiceView(string id)
        {
            //Ojito pelao aqui, cambiar
            string ServiceGuid = id;
            var apiUrl = "https://localhost:44339/api/servicequery/byguid?id="; 
            var url = apiUrl + ServiceGuid;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<ServiceUpdateResponse>(responseContent);

                UpdateServiceViewModel model = new UpdateServiceViewModel
                {
                    ServiceName = loginResponse.ServiceName,
                    ContactNumber = loginResponse.ContactNumber,
                    TypeService = loginResponse.TypeService
                };
                return View(model);
            }

            return RedirectToAction("AccessDeniedView", "Home");
        }

        public async Task<IActionResult> DeleteServiceView(string ServiceName, string Provider)
        {
            var apiUrl = "https://localhost:44339/api/servicequery/byservicename";
            var requestBody = new 
            { 
                ServiceName = ServiceName, 
                ProviderUserName = Provider 
            };

            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var service = JsonConvert.DeserializeObject<OneServiceResponse>(responseContent);
                var model = new DeleteServiceViewModel
                {
                    ServiceName = service.ServiceName,
                    Provider = service.ProviderUsername
                };

                return View(model);
            }

            return RedirectToAction("AccessDeniedView", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(string ServiceName, string Provider)
        {
            var apiUrl = "https://localhost:44339/api/servicedelete/deleteservice";
            var requestBody = new
            {
                ServiceName = ServiceName,
                UserName = Provider
            };

            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var serviceList = JsonConvert.DeserializeObject<ServiceDeleteResponse>(responseContent);

                return View("~/Views/Administration/AdminHomeView.cshtml");
            }

            return View("~/Views/Home/AccessDeniedView.cshtml");
        }
    }
}

