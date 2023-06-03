using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
{
    public class AllProvidersRegisteredController : Controller
    {
        private HttpClient _httpClient;

        public AllProvidersRegisteredController() 
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> AllProvidersRegisteredView()
        {
            var apiUrl = "https://localhost:44339/api/userquery/allproviders";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var people = JsonConvert.DeserializeObject<List<AllUserQueryResponse>>(responseContent);
                var users = MapAllUsersResponseToModel(people);
                return View("~/Views/Administration/AllProvidersRegisteredView.cshtml", users);

            }

            return View("~/Views/Home/AccessDeniedView");
        }

        public List<AllUsersViewModel> MapAllUsersResponseToModel(List<AllUserQueryResponse> response)
        {
            // Crear una lista para almacenar los objetos mapeados
            var usersViewModel = new List<AllUsersViewModel>();

            // Recorrer cada objeto en la lista original y crear un nuevo objeto mapeado
            foreach (var userQueryResponse in response)
            {
                var userViewModel = new AllUsersViewModel
                {
                    Dni = userQueryResponse.Dni,
                    Name = userQueryResponse.Name,
                    Lastname = userQueryResponse.Lastname,
                    Username = userQueryResponse.Username,
                    Email = userQueryResponse.Email,
                    PhoneNumber = userQueryResponse.PhoneNumber
                };
                usersViewModel.Add(userViewModel);
            }
            return usersViewModel;
        }
    }
}
