using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UCABPagaloTodoWeb.Models;
using System.Net.Http;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Responses;
using System.Text;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AllServicesController : Controller
    {
        private HttpClient _httpClient;

        public AllServicesController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> AllServicesView()
        {
            var apiUrl = "https://localhost:44339/api/servicequery/allservices";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllServicesQueryResponse>>(responseContent);

                var services = MapAllServicesResponseToModel(Response);


                return View("~/Views/Administration/AllServicesView.cshtml", services);
            }
            return View("~/Views/Home/AccessDeniedView.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AllServicesView(string id)
        {
            string serviceId = id;
            var apiUrl = "https://localhost:44339/api/servicequery/byguid?id=";
            var url = apiUrl + serviceId;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<OneServiceResponse>(responseContent);

                var model = new UpdateServiceViewModel
                {
                    ServiceName = Response.ServiceName,
                    TypeService = Response.TyperService,
                    ContactNumber = Response.ContactNumber,
                    Username = Response.ProviderUsername
                };
                return View("~/Views/Service/UpdateServiceView.cshtml", model);
            }
            return View("~/Views/Home/AccessDeniedView");
        }

        public List<AllServicesViewModel> MapAllServicesResponseToModel(List<AllServicesQueryResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var allUsersViewModel = new AllServicesViewModel();
            var usersViewModel = new List<AllServicesViewModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var userQueryResponse in response)
            {
                var userViewModel = new AllServicesViewModel
                {
                    ServiceId = userQueryResponse.ServiceId,
                    ServiceName = userQueryResponse.ServiceName,
                    TypeService = userQueryResponse.TyperService,
                    ContactNumber = userQueryResponse.ContactNumber,
                    ProviderUsername = userQueryResponse.ProviderUsername,
                    CompanyName = userQueryResponse.CompanyName
                };
                usersViewModel.Add(userViewModel);
            }
            return usersViewModel;
        }
    }
}
