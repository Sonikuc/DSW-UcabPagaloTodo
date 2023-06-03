using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoWeb.Models;


namespace UCABPagaloTodoWeb.Controllers
{
    public class AdminController : Controller 
    {
        private readonly ILogger<AdminController> _logger;
        private HttpClient _httpClient;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();

        }

        public IActionResult AdminHomeView()
        {
            return View("~/Views/Administration/AdminHomeView.cshtml");
        }

        public IActionResult AdminUserView()
        {
            return View("~/Views/Administration/AdminUserView.cshtml");
        }

        public IActionResult AdminServiceView()
        {
            return View("~/Views/Administration/AdminServiceView.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> AllUsersView()
        {
            var apiUrl = "https://localhost:44339/api/userquery/allusers";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var people = JsonConvert.DeserializeObject<List<AllUserQueryResponse>>(responseContent);
                var users = MapAllUsersResponseToModel(people);
                return View("~/Views/Administration/AllUsersView.cshtml", users);

            }

            return View("~/Views/Home/AccessDeniedView");
        }


        [HttpPost]
        //Por alguna EXTRAÑA RAZON el metodo se debe llamar igual al de arriba arriba AllUsersView, A PESAR QUE EN LA VISTA A LA HORA DE DAR AL BOTON EDITAR deberia ejecutarse el metodo
        //UpdateUsersViewAdmin, pero no, NO LO HACE Y NO PREGUNTES PQ
        public async Task<IActionResult> AllUsersView(string username) //redirige a UpdateUser
        {
            string userName = username;
            var apiUrl = "https://localhost:44339/api/userquery/byusername?username=";
            var url = apiUrl + userName;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject<OneUserResponse>(responseContent);

                UpdateUserViewModel model = new UpdateUserViewModel
                {
                    Name = Response.Name,
                    Lastname = Response.Lastname,
                    UserName = Response.Username,
                    Email = Response.Email,
                    PhoneNumber = Response.PhoneNumber
                };
                return View("~/Views/User/UpdateUserView.cshtml", model);
            }

            return RedirectToAction("AccessDeniedView", "Home");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
