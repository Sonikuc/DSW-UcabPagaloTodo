using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AllPaymentsByServiceController : Controller
    {
        private HttpClient _httpClient;

        public AllPaymentsByServiceController() { 
            _httpClient = new HttpClient(); 
        }

        public async Task<IActionResult> AllServicesView()
        {
            var apiUrl = "https://localhost:44339/api/servicequery/allservices";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllServicesQueryResponse>>(responseContent);

                var services = MapAllServicesResponseToModel(Response);


                return View("~/Views/Provider/AllProviderServicesView.cshtml", services);
            }
            return View("~/Views/Home/AccessDeniedView.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AllServicesView(string id)
        {
            string serviceId = id;
            var apiUrl = "https://localhost:44339/api/billquery/byserviceid=";
            var url = apiUrl + serviceId;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<List<AllBillsQueryResponse>>(responseContent);
                if (Response.Count == 0)
                {
                    return View("~/Views/Home/AccessDeniedView");
                }
                var model = MapAllServicesResponseToModel(Response);                
                return View("~/Views/Provider/AllProviderServicesView.cshtml", model);
            }
            return View("~/Views/Home/AccessDeniedView.cshtml");
        }

        public List<AllPaymentsByServiceViewModel> MapAllServicesResponseToModel(List<AllBillsQueryResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var allUsersViewModel = new AllPaymentsByServiceViewModel();
            var usersViewModel = new List<AllPaymentsByServiceViewModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var userQueryResponse in response)
            {
                var userViewModel = new AllPaymentsByServiceViewModel
                {
                   Amount = userQueryResponse.Amount,
                   Date = userQueryResponse.Date,
                   ContractNumber = userQueryResponse.ContractNumber,
                   PhoneNumber = userQueryResponse.PhoneNumber     
                };
                usersViewModel.Add(userViewModel);
            }
            return usersViewModel;
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
