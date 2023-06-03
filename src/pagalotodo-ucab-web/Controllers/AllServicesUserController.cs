using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AllServicesUserController : Controller
    {
        private HttpClient _httpClient;

        public AllServicesUserController()
        {
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


                return View("~/Views/Service/AllServicesToPayView.cshtml", services);
            }
            return View("~/Views/Home/AccessDeniedView");
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
                var service = JsonConvert.DeserializeObject<OneServiceResponse>(responseContent);

                var apiUrl2 = "https://localhost:44339/api/paymentoption/paymentoptionbyserviceid?request=";
                var url2 = apiUrl2 + serviceId;
                var response2 = await _httpClient.GetAsync(url2);

                if (response2.IsSuccessStatusCode)
                {
                    var responseContent2 = await response2.Content.ReadAsStringAsync();
                    var PaymentOption = JsonConvert.DeserializeObject<List<PaymentOptionsByServiceIdResponse>>(responseContent2);

                    if (PaymentOption.Count == 0) {
                        return View("~/Views/Service/NoPaymentOptionsOnService.cshtml");
                    }

                    if (service.TyperService == "Telefonia"){
                        var newList = new List<PaymentOptionsByServiceIdResponse>();
                        foreach (var option in PaymentOption)
                        {
                            newList.Add(option);
                        }
                        var modelPhone = new AddPaymentPhonesViewModel
                        {
                            ServiceId = service.ServiceId,
                            ServiceName = service.ServiceName,
                            UserId = Guid.Parse(HttpContext.Session.GetString("UserId")),
                            PaymentOption = newList,
                            PhoneNumber = "",
                            Amount = 0,
                            Date = DateTime.Now
                        };
                        return View("~/Views/AddPayment/AddPaymentPhoneView.cshtml", modelPhone);
                    }
                    var newList2 = new List<PaymentOptionsByServiceIdResponse>();
                    foreach (var option in PaymentOption)
                    {
                        newList2.Add(option);
                    }
                    var modelContract = new AddPaymentContractViewModel
                    {
                        ServiceId = service.ServiceId,
                        ServiceName = service.ServiceName,
                        UserId = Guid.Parse(HttpContext.Session.GetString("UserId")),
                        PaymentOption = newList2,
                        ContractNumber = "",
                        Amount = 0,
                        Date = DateTime.Now
                    };
                    return View("~/Views/AddPayment/AddPaymentContractView.cshtml", modelContract);
                }
            }
            return View("~/Views/Home/AccessDenied.cshtml");

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
